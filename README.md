# Image File Handler Add-ins for Office 365
This repository contains a file handler add-in for Office 365 that allows .png files to be opened and edited in an in-browser web editor similar to Paint.

![](http://i.imgur.com/IhNfoB9.png)

## Register the Application ##
1.	Login to the Azure Management Portal at [https://manage.azurewebsites.net](https://manage.azurewebsites.net "https://manage.azurewebsites.net") using an account that has access to the O365 Organization’s Azure Active Directory 
2.	Click on the **ACTIVE DIRECTORY** option towards the bottom of the left side menu and select the appropriate directory in the directory listing (you may only have one directory).
![Azure Active Directory](http://i.imgur.com/GbW9j2R.jpg)
3.	Next, click on the **APPLICATIONS** link in the top tab menu to display a list of applications registered in the directory.
![AAD Apps](http://i.imgur.com/EMLupXo.jpg)
4.	Click the **ADD** button in the middle of the footer (don’t confuse this with the +NEW button on the bottom left).
![Add app](http://i.imgur.com/1dEHMcp.jpg)
5.	In the **What do you want to do?** dialog, select **Add an application my organization is developing**
![New app wizard 1](http://i.imgur.com/fm2mHXz.jpg)
6.	Give the application a **NAME** (ex: Image File Handler) and select **WEB APPLICATION AND/OR WEB API** for the Type and then click the next arrow button.
![Name app](http://i.imgur.com/wtKPRV4.jpg)
7.	For App properties, enter a **SIGN-ON URL** and **APP ID URL**. The **SIGN-ON URL** will likely be localhost address during testing/development (ex: http://localhost:8000) and the **APP ID URL** should be globally unique, so something with tenant name is common (ex: https://TENANT.onmicrosoft.com/ImageFileHandler).
![App URLs](http://i.imgur.com/gQt6ofs.jpg)
8.	When the application finishes provisioning, click on the **CONFIGURE** link in the top tab menu.
![Configure tab](http://i.imgur.com/szJEaAb.jpg)
9.	Locate the **CLIENT ID** and copy its value somewhere safe.
![Client ID](http://i.imgur.com/faOKIs5.jpg)
10.	Locate the **keys** section and use the duration dropdown to select key good for **2-years**.
![Generate Keys](http://i.imgur.com/mMzmXT8.jpg)
11.	Click the **SAVE** button in the footer to generate and display the key (also referred to as a secret or password) and then copy the key somewhere safe (**WARNING**: you cannot display an application key other than after a save, so it is urgent you copy it during this step).
![Generate Keys](http://i.imgur.com/6b1nDzG.jpg)
12.	Locate the **permissions to other applications** section and click on the **Add application** button to launch the Permissions to other applications dialog.
![Perms to other applications](http://i.imgur.com/r8kv0vh.jpg)
13.	Locate and add **Office 365 unified API (preview)** before clicking the check button to close the dialog.
![Permissions to other apps dialog](http://i.imgur.com/c9wFK5g.jpg)
14.	Add Delegated Permissions for **Sign in users** and **Read and write user selected files (preview)**.
![](http://i.imgur.com/zbMwhVf.png)
15.	Click the **SAVE** button in the footer to save the updated application permissions.

## Register the Application as Add-in ##
1. Open the Add-in Manager website at [https://addinsmanager.azurewebsites.net/](https://addinsmanager.azurewebsites.net/) and login with your developer Office 365 credentials.
2. Locate and select the File Handler Application from list of applications on the left.
3. Click on the **Register Handler** button on the top right and select new File Handler.
4. Provide the following details in your registration before clicking Save (exact URLs may vary):
![](http://i.imgur.com/R6RsmfN.png)