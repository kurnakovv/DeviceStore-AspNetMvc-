***

## About the project:


_DeviceStore is an online store for selling various devices._

---

## Why the application was created:

_This project was created to study ASP .Net technology. Also, to study MS Sql Server. I did not use the migrations, but created the database and all the logic myself. I learned to use DI and IoC - container (Ninject)._

---

## Technologies:

- ASP .Net (MVC).
- MS Sql Server.
- Ninject 3.
- Bootstrap 3.

---

## How to start:

##### 1    
Go to the DeviceStore.WebUI / App_Data / folder, find the DeviceStore.mdf file and copy the link to it.
##### 2
Go to Web.config and paste the link into <connectionString>:

```
<connectionStrings>
    <add name="AppDbContext"
         connectionString="Your way for db"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

---

## What projects includes:

### The application is divided into 3 parts:

- Domain - the basis. Most of the logic is here.
- Tests - checks how a part of the functional works.
- WebUI - the interface through which it interacts with the user.

---

## Functional:
-  Admin: 
 GRUD system.

-  Home: 
Gives a list of all products divided by Pagination, you can also split by categories and find manually via search.

- Basket: 
A basket with goods is created and deleted automatically (valid for 1 day).
#### Basket functionality:
- View basket contents.
- Add to basket.
- Delete from basket.
- Calculate the sum of the price of goods.
- To order.

---

## Special thanks to:

- Ulyanov-programmer.
https://github.com/Ulyanov-programmer

---

## Plans:

#### UI:
- Index.
- Basket.
- Admin.
- OrderManager.

#### Admin:
- order management.
- System of discounts.

#### Entities:
- Categories.
- Author.

#### General:

- User bookmarks.
- System for withdrawing the quantity of goods.

___