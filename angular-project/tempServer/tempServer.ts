const express = require('express');
const app = express();
const db = require('./db.json');
const fs = require('fs');

app.use(express.json());

app.post('/login', (req:any, res:any, next:any) => { 
  const users = readUsers();

  const user = users.filter(
    (u: { username: any; password: any; }) => u.username === req.body.username && u.password === req.body.password
  )[0];

  if (user) {
    res.send({ ...formatUser(user), token: checkIfAdmin(user) });
  } else {
    res.status(401).send('Incorrect username or password');
  }
});

function alphabethToMorse(alphabethPassword: string): string {
  const morseTable = new Map<string, string>([
    ['a', '1'], ['b', '1000'], ['c', '1010'], ['d', '100'], ['e', '0'], ['f', '0010'], ['g', '110'],
    ['h', '0000'], ['i', '00'], ['j', '0111'], ['k', '101'], ['l', '1000'], ['m', '11'], ['n', '10'],
    ['o', '111'], ['p', '0110'], ['q', '1101'], ['r', '010'], ['s', '000'], ['t', '1'], ['u', '001'],
    ['v', '0001'], ['w', '011'], ['x', '1001'], ['y', '1011'], ['z', '1100']
    // Add more characters if needed
  ]);

  const morsePassword: string = Array.from(alphabethPassword?.toLowerCase() || '')
    .map((char: string) => morseTable.get(char) || char) // Replace each character with its Morse code equivalent
    .join('');

  return morsePassword;
}

// Morse code to alphabetic conversion function
function morseToAlphabeth(morsePassword:string) {
  const alphabethTable: { [key: string]: string } = {
    '01': 'a', '1000': 'b', '1010': 'c', '100': 'd', '0': 'e', '0010': 'f', '110': 'g', '0000': 'h',
    '00': 'i', '0111': 'j', '101': 'k', '0100': 'l', '11': 'm', '10': 'n', '111': 'o', '0110': 'p',
    '1101': 'q', '010': 'r', '000': 's', '1': 't', '001': 'u', '0001': 'v', '011': 'w', '1001': 'x',
    '1011': 'y', '1100': 'z'
    // Add more Morse code mappings if needed
  };
  
  const alphabethPassword = morsePassword
  .split(' ')
  .map((morse) => {
    console.log('Morse:', morse);
    const alphabeth = alphabethTable[morse] || morse;
    console.log('Alphabeth:', alphabeth);
    return alphabeth;
  })
    
  console.log('Alphabeth Password:', alphabethPassword);
  return alphabethPassword;
}

// Server post route
app.post('/register', (req: any, res: any) => {
  console.log(res);
  const users = readUsers();
  const user = users.filter((u: { username: any; }) => u.username === req.body.username)[0];
  
  if (user === undefined || user === null) {
    const alphabethPassword = morseToAlphabeth(req.body.password); // Convert Morse code password to characters
    const userData = {
      ...formatUser(req.body),
      password: alphabethPassword, // Include the converted password in the response
      token: checkIfAdmin(req.body)
    };
    console.log(userData);
    res.send(userData);
    db.users.push(userData);
  } else {
    res.status(500).send('User already exists');
  }
});





function formatUser(user: { password: any; role: string; username: string; }) {
  delete user.password;
  user.role = user.username === 'admin'
    ? 'admin'
    : 'user';
  return user;
}

function checkIfAdmin(user: { username: string; }, bypassToken = false) {
  return user.username === 'admin' || bypassToken === true
    ? 'admin-token'
    : 'user-token';
}

function isAuthorized(req: { headers: { authorization: string; }; }) {
  return req.headers.authorization === 'admin-token' ? true : false;
}

function readUsers() {
  const dbRaw = fs.readFileSync('./server/db.json');  
  const users = JSON.parse(dbRaw).users
  return users;
}