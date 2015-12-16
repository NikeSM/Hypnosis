var mongoose = require('./../libs/mongoose'),
    Schema = mongoose.Schema;
var async = require('async');
var crypto = require('crypto');
var config = require('./../config');
var utils = require('./../libs/utils');

var schema = new Schema({
    googleID: {
        type: String,
        unique: true,
        required: true
    },
    hashedPassword:{
        type: String,
        unique: true,
        required: true
    },

    salt: {
        type: String,
        unique: true
    },

    musicsPurchased:{
        type: Array,
        unique: false,
        required: false
    },

    imgsPurchased:{
        type: Array,
        unique: false,
        required: false
    },
    textsPurchased:{
        type: Array,
        unique: false,
        required: false
    }
});

schema.methods.encryptPassword = function(password){
    return crypto.createHmac('sha1', this.salt).update(password).digest('hex');
};

schema.virtual('password')
    .set(function(password) {
        this._plainPassword = password;
        this.salt = Math.random() + '';
        this.hashedPassword = this.encryptPassword(password);
    })
    .get(function() { return this._plainPassword;});

schema.methods.checkPassword = function(password) {
    return this.encryptPassword(password) === this.hashedPassword;
};

schema.methods.updateDefaults = function(){
    this.imgsPurchased = utils.mergeStringArrays(this.imgsPurchased, config.get('mongoose: defaultPurchased:imgs'));
    this.textsPurchased = utils.mergeStringArrays(this.textsPurchased, config.get('mongoose: defaultPurchased:texts'));
    this.musicsPurchased = utils.mergeStringArrays(this.musicsPurchased, config.get('mongoose: defaultPurchased:musics'));
};

schema.statics.authorize = function(username, password, callback) {
    var User = this;

    async.waterfall([
        function(callback){
            User.findOne({username: username}, callback);
        },
        function(user, callback) {
            if (user) {
                if (user.checkPassword(password)) {
                    callback(null, user);
                } else {
                    callback(new Error('Неверный пароль'))
                }
            } else {
                var user = new User({username: username, password: password});
                user.updateDefaults();
                user.save(function(err){
                    if (err) return callback(err);
                    callback(null, user);
                });
            }
        }
    ], callback);
};

module.exports.User = mongoose.model('User', schema);
