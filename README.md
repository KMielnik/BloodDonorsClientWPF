# BloodDonorsClientWPF
WPF client for BloodDonors Api. Uses BloodDonorsClientLibrary.

This is a learning project, simple desktop application for accessing/collecting blood donors data.

Written using Windows Presentation Forms from .NET Framework.

# Main functionalities:
 - Logging in as donor - looking at your data, donations, when can you give blood again.
 - Logging in as personel - adding new donors, blood donation entries, searching database of existing donations.
 - Statistics on main screen.
 
# Example user accounts
Those are the default users the database gets initialized with. 

[X] means one digit, for example donors pesel can be 51234567890 for fifth donor.

Donor:
 - pesel: [X]1234567890
 - password: [X]1234567890 (same as pesel)
 
Personnel:
 - pesel: [X]0987654321
 - password: [X]0987654321 (same as pesel)
 
 
Check releases, for compiled version. It should work out of the box, if the API is still on on Azure server.
If not, or if you want to use it with your own server instance: https://github.com/KMielnik/BloodDonorsAPI

Server address is configured in "serverAdress.config" file next to the exe, points to azure server by default.

API client is included in the app via: https://github.com/KMielnik/BloodDonorsClientLibrary

You will need it if compiling the WPF client by yourself.
