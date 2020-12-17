***

![MainHome](ImgForReadme/MainHomeImg.png)

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

``` xml
<connectionStrings>
    <add name="AppDbContext"
         connectionString="Your way for db"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

## How to use admin account:

#### 1
Go to the address bar and enter: Admin/Index or AdminCompany/Index (1 for devices 2 for companies)
#### 2
Enter email: admin@gmail.com
Enter password: 123456

You can change in Web.config

``` xml
<add key="adminEmail" value="admin@gmail.com"/>
<add key="adminPassword" value="123456"/>
```

#### How to send check order by email:
Go to the config and enter your email and password (Unreliable applications that have access to the account must be enabled):

``` xml
	<!--Your email and password-->
    <add key="EmailForSendingTheCheck" value="email"/> <!-- enter to "key"-->
    <add key="PasswordForSendingTheCheck" value="password"/>
```
After that, all check orders will be sent to the user by email.

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
 ![MainAdmin](ImgForReadme/MainAdminImg.png)
![ModalWindowAdmin](ImgForReadme/ModalWindowAdminImg.png)

-  Home: 
Gives a list of all products with rotate animation, divided by Pagination, you can also split by categories and find with the use of search.
![MainHome](ImgForReadme/MainHomeImg.png)

- Basket: 
A basket with goods is created and deleted automatically (valid for 1 day).
![MainBasket](ImgForReadme/MainBasketImg.png)

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