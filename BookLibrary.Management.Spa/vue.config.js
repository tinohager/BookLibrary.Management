var fs = require('fs');
var path = require('path');
 
function resolve (dir) {
  return path.join(__dirname, '.', dir)
}

var devServer = {};

if (process.env.NODE_ENV === 'development') {
  devServer = {
    host: 'localhost',
    historyApiFallback: true,
    headers: {
      "Access-Control-Allow-Origin": "*"
    },
    //https: {
    //  key: fs.readFileSync('./cert/private.key'),
    //  cert: fs.readFileSync('./cert/private.crt'),
    //},
    proxy: {
      '/api': {
        target: 'https://localhost:5001',
        changeOrigin: true,
      },
    }
  };
}

module.exports = {
  devServer: devServer,
  publicPath: process.env.NODE_ENV === 'production'
    ? '/spa/'
    : '/'
}