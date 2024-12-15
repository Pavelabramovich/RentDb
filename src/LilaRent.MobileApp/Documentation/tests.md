**Announcements**

| Test name           | Setup                 | How to execute                                                  | Expected result              |
| :------------------ | :-------------------- | :-------------------------------------------------------------- | :--------------------------- |
| Create announcement | Enter as Legal person | Press "Create announcement", enter fields data, finish creating | announcement will be created |
| Update announcement | Enter as legal person | Choose announcement, press "edit", change data and finish       | announcement will be updated |
| Delete announcement | Enter as Legal person | Choose announcement, press "Delete", and confirm                | announcement will be deleted |
| View announcements  | Enter as individual   | Select catalog                                                  | announcements will be loaded |

**Authentication**

| Test name            | Setup                   | How to execute                                                | Expected result                   |
| :------------------- | :---------------------- | :------------------------------------------------------------ | :-------------------------------- |
| Register as renter   | Not logged in           | Press "Registration", select "Individual", and press next     | Renter account will be created    |
| Register as landlord | Not logged in           | Press "Registration", select "Legal person", and press next   | Landlord account will be created  |
| Reregister warning   | Register with login     | Try register with login, that already used for account        | Warning will be occur             |
| Login                | Account already created | Enter email and password and press "Login"                    | User will be logged in            |
| Logout               | Logged in as any user   | Press "Exit" in the menu                                      | User will be logged out           |
| Reset password       | Not logged in           | Press "Forgot Password", enter email, and follow instructions | Password reset will be successful |

**Reservations**

| Test name            | Setup               | How to execute                                                | Expected result                   |
| :------------------- | :------------------ | :------------------------------------------------------------ | :-------------------------------- |
| Create a reservation | Enter as a renter   | Select a rental, choose available dates, and press "Book now" | Reservation will be created       |
| View booking details | Enter as a renter   | Go to "My Bookings", select a booking, and open details       | booking details will be displayed |
| Cancel a booking     | Enter as a renter   | Go to "My Bookings", choose a booking, and press "Cancel"     | Booking will be canceled          |
| Edit booking details | Enter as a landlord | Go to "My Rentals", select one, and press "Edit Booking"      | Changes will be saved             |
| View all bookings    | Enter as a renter   | Navigate to "My Bookings" in the menu                         | All bookings will be displayed    |
| View booking status  | Enter as any user   | Navigate to booking details                                   | Status will be visible            |
