# README - Ucommerce for Kentico Web-forms based accelerator #

### What is this repository for? ###

* This project contains the full Kentico solution with a functional Ucommerce store, which can be used as a starting point for projects, to accelerate development time.
* It is built with Ucommerce and Kentico best practices in mind.

### Requirements ###
Currently, the requirements of running this version of the accelerator are:

* Using SQL Server/ SQL Express. We do not support SQL CE.
* If you're using IIS, make sure you have the URL Rewrite module installed for your server. This is a requirements for the URLs generated by Ucommerce to work. You can find the module at [URL Rewrite](https://www.iis.net/downloads/microsoft/url-rewrite).

### Setup How-to ###
At Ucommerce we put a lot of emphasis on getting set up quick and without too many manual steps. The following will guide you through step-by-step on how to be up-and-running with Avenue Clothing for Kentico from scratch:

1. Make sure this folder (in which this readme is placed) is in a location on your machine from which you want to run the website. If you're going to use IIS, you might want to choose c:\inetpub or similar. If you want to run directly from Visual Studio (using IIS Express), you might put it into your usual folder for Visual Studio projects.

2. Set up a new site in your local Internet Information Servies. To do so, open the IIS application, right-click the **Sites** folder and select **Add Website**. Configure the site with the desired name and as **Physical path** select the **CMS** folder of the directory where you have the files of this repository. You should leave the **Host name** field empty, unless you have a specific Kentico License that allows you to use another domain.

3. In the repository, under the **database** folder, you will find a .bacpac database snapshot file which you need to restore into your databse server. The way you do this is by opening **SQL Server Management Studio**, right-clicking the **Databases** folder under your server, and selecting **Import Data-Tier Application**. Follow the wizard, and when prompted to select a file, select the previously mentioned .bacpac file. The .bacpac-file contains the data for the entire website to run.

4. Now that you have the database restored, you need to set the connection string in the **web.config** of the site located in the CMS folder in this repository to use this database.

5. Build the solution by opening the '.sln' file using Visual Studio and build the solution, e.g. by right-clicking the solution in the Solution Explorer and clicking 'Build Solution'.

6. Browse to the backend of Ucommerce to update the **faceted search**. You do this by browsing to **localhost/admin** (replace to 'localhost' to match your hostname). Login with username "administrator" and leave the password empty.
In the left hand menu select Ucommerce -> Settings. In the Ucommerce tree, select Settings -> Search and click "Index everything from scratch". This will reindex all products, so they can be filtered through faceted search.

6. When the build process is finished, you are ready to start using the Avenue Clothing Accelerator for Ucommerce on Kentico.

### Running the site as an IIS application ###

If you'd like to run the site as an IIS application, you have to follow the following steps:

1. Create an application in IIS. In **IIS Manager** you need to right-click your website and select **Add application**. You can choose the name of the application as this will be the URL of your local website in the form of "localhost/<application name>/". The **Physical path** of the application must point to the **CMS** folder in the repository as this is the folder where the website itself is located.

2. Comment or remove the URL-rewrite section in the **web.config**-file placed in the **CMS**-folder. It's surrounded by the `<rewrite>`-tags.

3. Verify that the **web.config** in the root-folder of the website contains the right rewrite-rules (containing the name of your application). So if you in step 1 created an application called "KenticoShop", you need to update your rules to look like:
_<action type="Rewrite" url="/KenticoShop/catalog/product.aspx?catalog={R:2}&amp;category={R:3}&amp;product={R:4}" />_
There're four rules in the web.config that needs to be updated.

### Further development ###

For further development when developing in a team, you can use the already-enabled Continuous Integration provided by Kentico. To restore data from CIRepository, you have to open a Command Prompt with administrator privileges (right-click and select 'run as administrator'), then you need to navigate to the **bin** folder of the website where the  **ContinuousIntegration.exe** is located (for example: ```cd "c:\Repositories\Avenue Clothing Kentico (WF)\CMS\bin"```). and running the following command: ```ContinuousIntegration.exe -r```. You will be notified when the restoration is complete. You can read more about how to use Continuous Integration at [Kentico's documentation](https://docs.kentico.com/k10/developing-websites/preparing-your-environment-for-team-development/setting-up-continuous-integration).

### Get to know us better! ###
* You can get free support for Ucommerce or Avenue Clothing for Kentico related issues or questions at our open support forum [Eureka!](http://eureka.ucommerce.net/#!/)
* You can find out more about Ucommerce at [Ucommerce.net](http://ucommerce.net).
* You can find technical information about Ucommerce in our [documentation](http://docs.ucommerce.net).
