var fs = require('fs');
var babelConfig = JSON.parse(fs.readFileSync('./.babelrc'));

require('babel-core/register')(babelConfig);
require('./server');
