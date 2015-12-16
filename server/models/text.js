var mongoose = require('mongoose'),
    Schema = mongoose.Schema;

var schema = new Schema({
    title: {
        type: String,
        unique: true,
        required: true
    },
    path: {
        type: String,
        unique: true,
        required: true
    },
    cost: {
        type: Number,
        unique: false,
        required: true
    }
});

exports.Text = mongoose.model('Text', schema);