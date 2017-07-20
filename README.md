# README - Ucommerce for Kentico Web-forms based accelerator #

### What is this repository for? ###

* This project contains the full solution for a functional e-commerce store, which can be used as a starting point for projects, to accelerate development time.
* It is built with Ucommerce and Kentico best practices in mind.
* Version: This product is currently under development

### Setup How-to ###
Here at Ucommerce we put a lot of emphasis on getting set up quick and without too many manual steps. The following will guide you through step-by-step on how to be up-and-running with Avenue Clothing for Kentico from scratch:

* First off, you need to set up a new site in your local Internet Information Servies. To do so, open the IIS application, right-click the **Sites** folder and select **Add Website**. Configure the site with the desired name and as **Physical path** select a folder in which you will clone this repository. You should leave the **Host name** field empty, unless you have a specific Kentico License that allows you for another host.
* Clone this repository into the folder mentioned before.

![Website](http://i.imgur.com/hefGx9p.png)

* In the repository, under the **database** folder, you will find a .bacpac database snapshot file which you need to restore into your databse server. The way you do this is by opening **SQL Server Management Studio**, right-clicking the **Databases** folder under your server, and selecting **Import Data-Tier Application**. Follow the wizard, and when prompted to select a file, select the previously mentioned .bacpac file.
* Now that you have the database restored, you need to set the connection string in the **web.config** of the site located in the CMS folder in this repository to use this database.
* Next up, since Kentico websites run as an IIS sub-application, you have to create one. In **IIS Manager** you need to right-click your website and select **Add application**. You can choose the name of the application as this will be the URL of your local website in the form of    "localhost/<application name>/". The **Physical path** of the application must point to the **CMS** folder in the repository as this is the folder where the website itself is located.

![Application](http://i.imgur.com/WuqknUi.png)

* The final step before you are ready to start using the Accelerator, is to restore all database data from Kentico's Continuous Integration repository. To restore data from CIRepository, you have to open a Command Prompt with administrator privileges (right-click and select 'run as administrator'), then you need to navigate to the **bin** folder of the website where the  **ContinuousIntegration.exe** is located (for example: ```cd "c:\Repositories\Avenue Clothing Kentico (WF)\CMS\bin"```). and running the following command: ```ContinuousIntegration.exe -r```. You will be notified when the restoration is complete.
* When the restoring process is finished, you are ready to start using the Avenue Clothing for Kentico Accelerator.

### Get to know us better! ###
* You can get free support for Ucommerce or Avenue Clothing for Kentico related isses or questions at our open support forum [Eureka!](http://eureka.ucommerce.net/#!/), or by e-mail at [support@ucommerce.net](support@ucommerce.net).
* You can find out more about Ucommerce at [Ucommerce.net](http://ucommerce.net).
* You can find technical information about Ucommerce in our [documentation](http://docs.ucommerce.net).