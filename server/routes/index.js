var User = require('./../models/user').User;
var Img = require('./../models/img').Img;
var Music = require('./../models/music').Music;
var Text = require('./../models/text').Text;
var path = require('path');
var logger = require('./../libs/logger')(module);

module.exports = function(app){

    //Authrization
    app.use(function(req, res, next){
        var username = req.body.username;
        var password = req.body.password;
        if (username && password){
            User.authorize(username, password, function(err, user){
                if (err)
                    next(err);
                res.locals.user = user;
            });
        }
        else {
            next();
        }

    });
    app.post('/getImg', function(req, res, next){
        if (res.locals.user){
            if (user.imgsPurchased.includes(req.body.imgTitle)){
                Img.findOne({title: req.body.imgTitle}, function (err, img){
                    if (err) next(new Error('Img not found'));
                    res.sendFile(path.resolve(img.path));
                });
            } else {
                next(new Error('You are not authorized'));
            }
        }
    });
    app.post('/getText', function(req, res, next){
        if (res.locals.user){
            if (user.textsPurchased.includes(req.body.textTitle)){
                Text.findOne({title: req.body.textTitle}, function (err, text){
                    if (err) next(new Error('Text not found'));
                    res.sendFile(path.resolve(text.path));
                });
            } else {
                next(new Error('You are not authorized'));
            }
        }
    });
    app.post('/getMusic', function(req, res, next){
        if (res.locals.user){
            if (user.musicsPurchased.includes(req.body.musicTitle)){
                Music.findOne({title: req.body.musicTitle}, function (err, music){
                    if (err) next(new Error('Music not found'));
                    res.sendFile(path.resolve(music.path));
                });
            } else {
                next(new Error('You are not authorized'));
            }
        }
    });
    app.post('/test', function(req, res, next){
        logger.info(req.body);
        res.send('Ok!');

    });
    app.get('/test', function(req, res, next){
        logger.info(req.body);
        res.send('Ok!');

    });
};