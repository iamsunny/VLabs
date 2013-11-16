Before publishing this solution, make sure to replace the following placeholders according to exercise 1.

1. In server.js, find the following lines and complete with your configuration.

	- [YOUR-DATABASE-NAME]: is the database name that you have selected in exercise 1. You can write any database name, but then you will have to manually add the contacts.

	- [YOUR-ADMIN-ADMINISTRATOR-USER] and [PASSWORD]: is the user and the password you have configured when you have secured the MongoDB server. 

	- [YOUR-VM-DNS-ADDRESS]: check in Windows Azure portal dashboard the DNS host of your VM (that means, you don't have to append 'http://' ).

	//
	//Replace the placeholders with your own database server configuration.
	//Example connection
	//db = mongoose.connect('mongodb://AdminUser:Password!@mongoDBServer1.cloudapp-preview.net:27017/MyDB', function(err) { if (err) throw err; }); 

	db = mongoose.connect('mongodb://[YOUR-ADMIN-ADMINISTRATOR-USER]:[PASSWORD]!@[YOUR-VM-DNS-ADDRESS]:27017/[YOUR-DATABASE-NAME]', function(err) { if (err) throw err; }); 


