var log = require('./libs/logger')(module);
var mongoose = require('./libs/mongoose');
var async = require('async');

async.series([
    open,
    dropDatabase,
    requireModels,
    createMusic
    ], function(err) {
    log.info(arguments);
    mongoose.disconnect();
    process.exit(err ? 255 : 0);
});

function open(callback){
    mongoose.connection.on('open', callback);
}

function dropDatabase(callback){
    var db = mongoose.connection.db;
    db.dropDatabase(callback);
}

function requireModels(callback){
    require('./models/music');

    async.each(Object.keys(mongoose.models), function(modelName, callback){
        mongoose.models[modelName].ensureIndexes(callback);
    }, callback);
}

function createMusic(callback) {
    var music = [
        {title: '1', path: '/home/mf-side/7parts/hypnosis/server/files/temp/1.mp3', cost: 1}
    ];

    async.each(music, function(musicData, callback){
        var cur_music = new mongoose.models.Music(musicData);
        cur_music.save(callback);
    }, callback);
}