const HtmlWebPackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require('copy-webpack-plugin');
const { ModuleFederationPlugin } = require("webpack").container;
const MiniCssExtractPlugin = require("mini-css-extract-plugin")
const path = require("path");
const styles = require('./templates/styles.json');
const dotenv = require('dotenv');
const { dependencies } = require('./package.json');

const envFile = (rpEnv = null) => typeof rpEnv === 'string' ? `${__dirname}/env/.env.${rpEnv}` : `${__dirname}/env/.env`

module.exports = (webpackConfigEnv, argv) => {
  const isProd = webpackConfigEnv.prod === 'true'

  const hasEnvObj = 'env' in argv && typeof argv.env !== 'undefined' && argv.env !== null
  const hasRpEnv = hasEnvObj && 'rp-env' in argv.env
  const rpEnv = hasRpEnv ? argv.env['rp-env'].trim().toLowerCase() : null

  dotenv.config({ path: envFile(rpEnv) })

  const microfrontendImports = Object.keys(process.env ?? {})
    .filter((key) => key.startsWith('MICROFRONTEND_'))
    .map((key) => process.env[key])
    .map((key) => [key, key.indexOf('@')])
    .map(([key, idx]) => [
      key.substring(0, idx),
      key.substring(idx + 1, key.length)
    ])
    .reduce((map, entry) => {
      const [name, url] = entry

      map[name] = {
        url: `${name}@${url}/entry.js`,
        css: `${url}/main.css`,
      };

      return map
    }, {})

  const configVariables = {
    systemCode: process.env.SYSTEM_CODE,
    sgaApiBaseUrl: process.env.SECURITY_MICROSERVICE_BASE_URL,
    identityBaseUrl: process.env.IDENTITY_MICROSERVICE_BASE_URL,
    identityInternalClientId: process.env.IDENTITY_INTERNAL_CLIENT_ID,
    identityInternalAuthority: process.env.IDENTITY_INTERNAL_AUTHORITY_URL,
    identityInternalRedirectUrl: process.env.IDENTITY_INTERNAL_CALLBACK_URL,
    identityInternalScope: process.env.IDENTITY_INTERNAL_SCOPE,
    identityRedirectAfterLogin: process.env.IDENTITY_REDIRECT_AFTER_LOGIN,
    identityRedirectAfterLogout: process.env.IDENTITY_REDIRECT_AFTER_LOGOUT,
    identityAuthLogoUrl: process.env.IDENTITY_AUTH_LOGO_URL,
    identityAuthBackgroundUrl: process.env.IDENTITY_AUTH_BACKGROUND_URL,
    identityAuthTitle: process.env.IDENTITY_AUTH_TITLE,
    identityGrandTypeRefresh: process.env.IDENTITY_GRAND_TYPE_REFRESH,
    identityClientSecret: process.env.IDENTITY_CLIENT_SECRET,
    identityExternalBaseUrl: process.env.IDENTITY_EXTERNAL_BASE_URL,
    identityExternalClientId: process.env.IDENTITY_EXTERNAL_CLIENT_ID,
    identityExternalClientSecret: process.env.IDENTITY_EXTERNAL_CLIENT_SECRET,
    identityExternalGrandType: process.env.IDENTITY_EXTERNAL_GRAND_TYPE,
    identityExternalGrandTypeRefresh: process.env.IDENTITY_EXTERNAL_GRAND_TYPE_REFRESH,
    identityExternalScope: process.env.IDENTITY_EXTERNAL_SCOPE,
    identityExternalSecurityMicroservice: process.env.SECURITY_MICROSERVICE_BASE_URL,
    headerTitle: process.env.HEADER_TITLE,
    headerTheme: process.env.HEADER_THEME,
    headerLogoUrl: process.env.HEADER_LOGO_URL,
    styleHeaders: JSON.stringify(styles)
  }

  return {
    mode: 'development',
    entry: './src/index.tsx',
    output: {
      publicPath: '/'
    },
    devServer: {
      static: path.join(__dirname, "build/index.js"),
      port: 8035,
      historyApiFallback: true
    },
    plugins: [
      new CopyWebpackPlugin({
        patterns: [
          {
            from: 'templates/config.ts',
            to: 'static/config.js',
            transform(content, absoluteFrom) {
              let result = content.toString()
              Object.keys(configVariables).map(x => {
                result = result.replace(`__${x}__`, configVariables[x]);
              })
              return result;
            }
          }
        ],
      }),
      new CopyWebpackPlugin({
        patterns: [
          { from: 'src/libs', to: 'libs' },
          { from: 'src/assets', to: 'static' }
        ]
      }),
      new HtmlWebPackPlugin({
        inject: "head",
        template: 'templates/index.ejs',
        templateParameters: {
          isLocal: webpackConfigEnv && webpackConfigEnv.isLocal,
          importsMap: Object.keys(microfrontendImports).map(x => microfrontendImports[x].css),
          headerTitle: process.env.HEADER_TITLE,
          devTools: isProd ? null : '<import-map-overrides-full show-when-local-storage="devtools" dev-libs></import-map-overrides-full>',
        },
        filename: "index.html"
      }),
      new MiniCssExtractPlugin({}),
      new ModuleFederationPlugin({
        name: "Host",
        remotes: Object.keys(microfrontendImports).reduce((obj, key) => {
          obj[key] = microfrontendImports[key].url; return obj;
        }, {}),
        shared: {
          ...dependencies,
          react: {
            singleton: true,
            requiredVersion: dependencies.react,
          },
          'react-dom': {
            singleton: true,
            requiredVersion: dependencies['react-dom'],
          },
          'react-router-dom': {
            singleton: true,
            requiredVersion: dependencies['react-router-dom'],
          }
        },
      })
    ],
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.jsx', '.json'],
    },
    module: {
      rules: [
        {
          test: /\.(woff|woff2|eot|ttf|otf|png|jpe?g|gif|eot|woff2|woff|ttf|svg|webp)$/i,
          type: "asset/resource",
        },
        {
          test: /\.(tsx|ts)$/,
          exclude: /node_modules/,
          use: {
            loader: "babel-loader"
          }
        },
        {
          test: /\.*css$/,
          use: [
            {
              loader: MiniCssExtractPlugin.loader,
              options: {
              },
            },
            'css-loader',
            "sass-loader"
          ]
        },
      ]
    }
  };
}