/* eslint-env node */

/* eslint-disable @typescript-eslint/no-var-requires */
const pathGlobal = './ui-mf-users';
const path = require("path");
const dotenv = require('dotenv');
const { WebpackConfig } = require('web-mf-configuration');
const { dependencies } = require( './package.json');

const envFile = (rpEnv = null) => typeof rpEnv === 'string' ? `${__dirname}/env/.env.${rpEnv}` : `${__dirname}/env/.env`
const fileEntryTs = pathGlobal + '/src/index.tsx';
const fileEntryJs = path.join(__dirname, pathGlobal + "/" + "build/index.js");
const port = 3001;
const moduleName = "usersModule";
const moduleFileName = "entry.js";

module.exports = (webpackConfigEnv, argv) => {

  const hasEnvObj = 'env' in argv && typeof argv.env !== 'undefined' && argv.env !== null
  const hasRpEnv = hasEnvObj && 'rp-env' in argv.env
  const rpEnv = hasRpEnv ? argv.env['rp-env'].trim().toLowerCase() : null

  if(typeof rpEnv === 'string'){
    dotenv.config({ path: path.join(__dirname, pathGlobal + "/" + `env/.env.${rpEnv}`) });
  }else{
    dotenv.config({ path: path.join(__dirname, pathGlobal + "/" + `env/.env`) });
  }

  return WebpackConfig(
    fileEntryTs,
    fileEntryJs,
    port,
    moduleName,
    moduleFileName,
    {
      "./Root": pathGlobal + "/src/index.tsx"
    },
    {
      ...dependencies, // some other dependencies
      react: {
        singleton: true,
        requiredVersion: dependencies['react'],
      },
      'react-dom': {
        singleton: true,
        requiredVersion: dependencies['react-dom'],
      },
      'react-router-dom': {
        singleton: true,
        requiredVersion: dependencies['react-router-dom'],
      },
    },
    {
      ...Object.keys(process.env ?? {})
        .map((key) => [`process.env.${key}`, process.env[key]])
        .reduce((map, entry) => {
          map[entry[0]] = JSON.stringify(entry[1]);

          return map;
        }, {})
    }
  )
}