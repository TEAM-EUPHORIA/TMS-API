# Training Management System - TMS
> ## __Created By__ Team Euphoria
---
## __Description__
### This is a donet core web api project for the Training Management System - TMS.

---
# Tools and Technologies
![C#](https://cdn-icons-png.flaticon.com/128/6132/6132221.png "C# programming language")
![.NET Core](https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/128px-.NET_Core_Logo.svg.png "CSS 3")
![Swagger](https://cdn.icon-icons.com/icons2/2107/PNG/128/file_type_swagger_icon_130134.png "swagger")

---
# Pre-Requisites

- .Net core SDK
- Visual Studio code
- Basic Knowledge in C#
- SQL Server
- SQL Server Management Studio

# Setup

## Initail Setup

```bash

> git clone https://github.com/TEAM-EUPHORIA/TMS-API.git

> cd TMS-API

> code .
````

---

## Setting up database

## Open __SQL Server Management Studio__
---
> ## Right click on Databases 

![SQL Server Management Studio](./images/Screenshot%20(2).png)

> ## Click Restore Database...

![SQL Server Management Studio](./images/Screenshot%20(3).png)

> ## Click Device then Click on the button next to the text box

![SQL Server Management Studio](./images/Screenshot%20(4).png)

> ## Click on add locate the tms.bak file click ok again click ok
> Location of backup file - TMS.API/Sample Data/Database backup/tms.bak

![SQL Server Management Studio](./images/Screenshot%20(4).png)

> ## if You want you can change the the Database name and after adding TMS.bak

![SQL Server Management Studio](./images/Screenshot%20(5).png)

> ## Finally Click Ok to restore database If you did everything you will get a window like this

![SQL Server Management Studio](./images/Screenshot%20(6).png)

---

# Running Web api Project
- ### change the connection string in __appsettings.json__

> ## Hop back to Visual studio code open up a new terminal

```bash

dotnet build
dotnet run

```

---
# That's it the web api will be up and running on

![Terminal](./images/Screenshot%20(7).png)

## You can click on the link to view it in browser

![Swagger Output](./images/Screenshot%20(8).png)

---
# The End