 [[_TOC_]]

# 1. Introducción 
Esta es una guía para la eejcución del proyecto HOST del proyecto Test App.

# 2. Desarrollo

## 2.1. Pre Requisitos

Tenemos que instalar primero las herramientas así que solo levantamos una ventana CMD y ejecutamos el siguiente comando:

`npm install pm2 -g`


## 2.2. Ejecución

a) Descargamos la solución y nos ubicamos en el siguiente directorio: `src/host`

b) Ejecutamos los siguientes comandos:

Línea de comando necesario para la autenticación con el artifact de Real Plaza. (Si tienes error 401 consulta con Edgar Flores o Aarón Mejía).
```
vsts-npm-auth -config .npmrc
```

Línea de comando para la instalación de paquetes.
```
npm install
```

c) Para Iniciar la Aplicación ejecutamos el siguiente comando: `npm run start:locally`. Esta línea de comandos nos levantará la aplicación en el puerto 80.

d) Accedenos a la siguiente ruta y debemos poder visualizar la página de login: `http://localhost/`

e) Para Iniciar la Aplicación ejecutamos el siguiente comando: `npm run stop:locally`.

<strong>NOTA: Esta ejecución durará en todo el tiempo de vida de que tienes una sessión activa en tu laptop, si apagas o reinicias deberás realizar los pasos nuevamente. </strong>