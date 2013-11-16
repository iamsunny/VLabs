var express = require('express'),
    mongoose = require('mongoose'),
    ContactPage = require('./routes/contacts'),
    ContactNewPage = require('./routes/newContact');
    

var app = module.exports = express.createServer();

//
//Replace the placeholders with your own database server configuration.
//Example connection
//db = mongoose.connect('mongodb://AdminUser:Password!@mongoDBServer1.cloudapp-preview.net:27017/MyDB', function(err) { if (err) throw err; }); 
//
db = mongoose.connect('mongodb://[YOUR-ADMIN-USERNAME]:[YOUR-ADMIN-PASSWORD]!@[YOUR-VM-DNS-ADDRESS]:27017/[YOUR-DATABASE-NAME]', function(err) { if (err) throw err; }); 


app.configure(function(){
  app.set('views', __dirname + '/views');
  app.set('view engine', 'jade');
  app.use(express.bodyParser());
  app.use(express.methodOverride());
  app.use(app.router);
  app.use(express.static(__dirname + '/public'));
});

app.configure('development', function(){
  app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
});

app.configure('production', function(){
  app.use(express.errorHandler());
});

var Schema = mongoose.Schema, ObjectId = Schema.ObjectId;

var Contact = new Schema({
  id: ObjectId,
  firstname: String,
  lastname: String,
  address: String,
  email: String,
  _keywords: Array, index: { unique: false }
});

var ContactModel = mongoose.model('Contact', Contact); 

// Routes
var contactPage = new ContactPage(ContactModel);
app.get('/', contactPage.showItems.bind(contactPage));
app.get('/contacts', contactPage.showItems.bind(contactPage));
app.get('/contacts/filter', contactPage.filterItems.bind(contactPage));
var contactNewPage = new ContactNewPage(ContactModel);
app.get('/contacts/newContact', function(req, res){
  res.render('newContact', {
    title: 'New Contact'
  });
});
app.post('/contacts/newContact', contactNewPage.newContact.bind(contactNewPage));

// Only listen on $ node app.js
var port = process.env.PORT || 1337;

app.listen(port, function(){
  console.log("Express server listening on port %d in %s mode", app.address().port, app.settings.env);
});
