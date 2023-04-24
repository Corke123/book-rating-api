# Book Rating API

### BACKEND

- [x] Design the DB that will contain all data necessary for your frontend app.
    - Migration schemes can be found in [Migrations](Migrations). Those are generated using Entity Framework. I am not
      thrilled with this solution, but this was the easiest that I found. To execute migrations against your database
      execute ```dotnet ef database update```. I used Postgres database for local development. To be able connect to
      database some environment variables must be set:
      ```shell
        export DB_HOST=localhost
        export DB_PORT=5432
        export DB_NAME=bookrating
        export DB_USER=bookrating-rw
        export DB_PASSWORD=bookrating-rw
      ```

- [ ] Make corresponding API for all frontend app features.
    - This is almost completed. There is missing search option by phrases. I didn't figure out how that can be achieved
- [x] Create additional endpoint which will enable user to upload big collection of books in terms of gigabytes of
  data (one book has all the fields you created in database model).
    - Endpoint location: api/v1/books/bulk
- [x] Protect your API, so every unauthorized request should be rejected.
    - All APIs are protected. I implemented basic signup & login endpoints to play around with it. Better solution
      definitely would be to use some Authorization server (Keycloak for example) to delegate authorization & provide
      OAuth2 specification.
- [x] Where you store images is a matter of personal choice (AWS S3, BLOB in the DB...).
    - I've chosen [Cloudinary](https://cloudinary.com/) to store images. It provides free image hosting and it is easy
      to use. Example of uploaded
      image: http://res.cloudinary.com/dlclfag6g/image/upload/v1682285188/books/fb098215-6716-4e80-b8d8-c4c76f335759.gif
-[x] Make sure your API follows REST principles.

#### Additional notes

- I imagined that there will be two types of users: Admin & Member to play with Role based authentication. Admin would
  be able to upload new Books and delete existing books. User is able to get books and rate the book.
- This is my first time working with .NET so maybe I didn't follow all standards in .NET world and there would be a lot
  of warnings