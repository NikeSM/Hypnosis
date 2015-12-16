var express = require('express');
var config = require('./config');
var bodyParser = require('body-parser');
var log = require('./libs/logger')(module);

var app = express();

//app.use(express.logger('dev'));

app.listen(config.get('port'), function(){
    log.info('Express server listening on port ', config.get('port'));
});

app.use(bodyParser.urlencoded({extended: false}));
app.use(bodyParser.json());

//var router = express.Router();
//app.use(router);
require('./routes')(app);

app.use(function(err, req, res) {
    if (err){
        log.error('Error ', err.message);
        res(err.message);
    }
});