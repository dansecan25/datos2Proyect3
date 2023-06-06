var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var express = require('express');
var app = express();
var db = require('./db.json');
var fs = require('fs');
app.use(express.json());
app.post('/login', function (req, res, next) {
    var users = readUsers();
    var user = users.filter(function (u) { return u.username === req.body.username && u.password === req.body.password; })[0];
    if (user) {
        res.send(__assign(__assign({}, formatUser(user)), { token: checkIfAdmin(user) }));
    }
    else {
        res.status(401).send('Incorrect username or password');
    }
});
function alphabethToMorse(alphabethPassword) {
    var morseTable = new Map([
        ['a', '1'], ['b', '1000'], ['c', '1010'], ['d', '100'], ['e', '0'], ['f', '0010'], ['g', '110'],
        ['h', '0000'], ['i', '00'], ['j', '0111'], ['k', '101'], ['l', '1000'], ['m', '11'], ['n', '10'],
        ['o', '111'], ['p', '0110'], ['q', '1101'], ['r', '010'], ['s', '000'], ['t', '1'], ['u', '001'],
        ['v', '0001'], ['w', '011'], ['x', '1001'], ['y', '1011'], ['z', '1100']
        // Add more characters if needed
    ]);
    var morsePassword = Array.from((alphabethPassword === null || alphabethPassword === void 0 ? void 0 : alphabethPassword.toLowerCase()) || '')
        .map(function (char) { return morseTable.get(char) || char; }) // Replace each character with its Morse code equivalent
        .join('');
    return morsePassword;
}
// Morse code to alphabetic conversion function
function morseToAlphabeth(morsePassword) {
    var alphabethTable = {
        '01': 'a', '1000': 'b', '1010': 'c', '100': 'd', '0': 'e', '0010': 'f', '110': 'g', '0000': 'h',
        '00': 'i', '0111': 'j', '101': 'k', '0100': 'l', '11': 'm', '10': 'n', '111': 'o', '0110': 'p',
        '1101': 'q', '010': 'r', '000': 's', '1': 't', '001': 'u', '0001': 'v', '011': 'w', '1001': 'x',
        '1011': 'y', '1100': 'z'
        // Add more Morse code mappings if needed
    };
    var alphabethPassword = morsePassword
        .split(' ')
        .map(function (morse) {
        console.log('Morse:', morse);
        var alphabeth = alphabethTable[morse] || morse;
        console.log('Alphabeth:', alphabeth);
        return alphabeth;
    });
    console.log('Alphabeth Password:', alphabethPassword);
    return alphabethPassword;
}
// Server post route
app.post('/register', function (req, res) {
    console.log(res);
    var users = readUsers();
    var user = users.filter(function (u) { return u.username === req.body.username; })[0];
    if (user === undefined || user === null) {
        var alphabethPassword = morseToAlphabeth(req.body.password); // Convert Morse code password to characters
        var userData = __assign(__assign({}, formatUser(req.body)), { password: alphabethPassword, token: checkIfAdmin(req.body) });
        console.log(userData);
        res.send(userData);
        db.users.push(userData);
    }
    else {
        res.status(500).send('User already exists');
    }
});
function formatUser(user) {
    delete user.password;
    user.role = user.username === 'admin'
        ? 'admin'
        : 'user';
    return user;
}
function checkIfAdmin(user, bypassToken) {
    if (bypassToken === void 0) { bypassToken = false; }
    return user.username === 'admin' || bypassToken === true
        ? 'admin-token'
        : 'user-token';
}
function isAuthorized(req) {
    return req.headers.authorization === 'admin-token' ? true : false;
}
function readUsers() {
    var dbRaw = fs.readFileSync('./server/db.json');
    var users = JSON.parse(dbRaw).users;
    return users;
}
