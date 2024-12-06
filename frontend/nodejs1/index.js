
// wrapper function
// function(_dirname, _filename, module, require, exports){
    console.log(_dirname);
    console.log(_filename);
    
    const add = require('./calculator.js');
    console.log(add(2, 3));

    require('http');
    const server = http.createServer(function(req, res){
        res.writeHead(200);
        res.end('Hello World!');
    });

    server.listen(8080);
// }