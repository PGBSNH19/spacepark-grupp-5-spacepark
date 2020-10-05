A long time ago in a galaxy called The Milky way...
-----------------------------------------------------------------------------------------------------------------------------------------------------------------
##### Overview:
Our goal was to create a project that would run in the cloud. It would contain a frontend (Webapp) a backend (API) a Database (Code First), a Continuous Integration (CI) and Continuous Deployment (CD) in Azure Devops Pipelines, using Docker to Containerize the backend and frontend with code repository in Github.

<div align="center"> - - - - - - </div>

We started with checking out old projects to see if we could lend any code from them, but then had the idea of starting from scratch which we did. We created CRC cards for the models to make a choice about the structure of the database. 

##### Frontend:

We decided we wanted to work in Razor Pages for our Frontend since it could be a good thing to have worked with. In the frontend development we decided to use Ajax requests to get, post, put and delete data in our database (through the backend). Since we used Razor Pages we had the ambition to use the built-in functionality but it was more challenging than expected so we kind of didn't. In the beginning we struggled a lot with CORS. At first we only used a chrome extension but then got some help from a senior developer and realized that we could allow all headers, origins and methods in the (backend) startup file.

##### Backend/API:

When creating the API we had an idea of having a "main" class/controller that would hold all logic concerning the application (Parkingguard controller). But came to the realization that it would not work as we thought so we split the logic into the Person, Starship and parkinglot controllers.

We implemented the APICaller class, that contacts the Swapi API and checks if the person entered (by a string) exists in the Star Wars universe. The code for this was almost entirely taken from the old Spacepark-Group-7 project.
We added some tests to check if the implementation of the class worked and also to be sure our pipeline could run the tests as a proof of concept.

When we were trying to setup Docker we ran into some heavy problems which stemmed from not having a .gitingore file (It was not included in the repo when we got it, which we missed).

We created our database using Entity Framework Core through the "code first" principal. 

##### Database:

In an early stage we created CRC cards for the models to make a choice about the structure of the database. 
The relations is displayed in the image below.

<p align="center">
  <img width="700" src="https://media.discordapp.net/attachments/699253771074535456/762662537409331200/DATABAS-RELATION-SPACE.jpg?width=705&height=485">
</p>


The picture is somewhat misleading though. The only relationship between Starship and Parkinglot is the Foreign Key in Starships pointing on  ParkinglotID in Parkinglots. But in the Parkinglot model we have a property holding a starship-object. We realized that we could have used the attribute [NotMapped], but thats for the future. 

We configured the database in Azure Portal to be a serverless SQL Database. 
We put our Connectionstring in a separate appsettings file (appsettings.dev.json). 
This created problems when our application was running in the cloud because our backend couldn't find the the appsettings.dev.json file.
We consulted the teacher for help, after sometime he came back with a appreciated solution (to specify the appsettings file in the pipeline.yml file).

##### Pipelines:

We ran into problems with our CI Pipelines, we had to remodel and create a new pipeline. The problem seemed to derive from having a faulty azure-pipelines.yml file. The last thing we did was set up our release pipelines, remarkably we did not stumble upon any huge problems. We created one CD for the backend project and one CD for the frontend project, each containing a inline-script which creates  Azure Container Instances of the two projects. 
