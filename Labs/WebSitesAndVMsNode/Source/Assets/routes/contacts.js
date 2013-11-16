module.exports = contacts;

function contacts (contactModel) {
    this.contactModel = contactModel;
};

contacts.prototype = {
    showItems: function (req, res) {
        var self = this;
        self.contactModel.find(function (err, contacts) {
            if (!err) {
                res.render('contacts', { 
                title: 'Contact Manager', 
                layout: true, 
                contactlist: contacts });
            } else {
              return console.log(err);
            }
          });
    },
    filterItems: function (req, res) {
        var self = this;
		var search = req.query.search.toLowerCase();
        self.contactModel.find({_keywords: search}, function (err, contacts) {
            if (!err) {
                res.render('contacts', { 
                title: 'Contact Manager', 
                layout: true, 
                contactlist: contacts });
            } else {
              return console.log(err);
            }
     	});
    },
};