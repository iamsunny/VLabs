module.exports = newContact;

function newContact (contactModel) {
    this.contactModel = contactModel;
};

newContact.prototype = {
    newContact: function (req, res) {
        var self = this;
        var item = req.body.item;
        contact = new self.contactModel({
          firstname: item.firstname,
          lastname: item.lastname,
          address: item.address,
          email: item.email,
          _keywords: [ item.firstname.toLowerCase(), item.lastname.toLowerCase(), item.email.toLowerCase() ]
        });

        contact.save(function (err) {
          if (err) {
            return console.log(err);
          }
        });
        return res.redirect('/contacts');
    },
};