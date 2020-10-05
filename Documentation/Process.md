A long time ago in a galaxy called The Milkyway...
-----------------------------------------------------------------------------------------------------------------------------------------------------------------
Overview:
Our goal was to create a project that would run in the cloud. It would contain a frontend (Webapp) a backend (API) a Database (Code First),
a Continuous Integration (CI) and Continuous Deployment (CD) in Azure Devops Pipelines, using Docker to Containerize the backend and frontend with code repository in Github.

We started with checking out old projects to see if we could lend any code from them, but then had the idea of starting from scratch which we did.
We all felt a bit rusty and wanted to wait until the second lesson to really make a decision about what to do with the project. 

We created CRC cards for the models to make a choice about the structure of the database. 
We decided we wanted to work in Razor Pages for our Frontend since it could be a good thing to have worked with.

When creating the API we had an idea of having a "main" class/controller that would hold all logic concerning the application (Parkingguard controller).
But came to the realization that it would work as we thought so we split the logic into the Person, Starship and parkinglot controllers.

In the frontend development we decided to use Ajax requests to get, post, put and delete data in our database (through the backend).
Since we used Razor Pages we had the ambition to use the built-in funcationallity but it was more challenging than expected so we kind of didn't. 
In the beginning we struggled a lot with CORS. 
At first we only used a chrome exstention but then got some help from a senior developer and realized that we could allow all headers, origins and methods in the (backend) startup file.

We implemented the APICaller class, that contacts the Swapi API and checks if the person entered (by a string) exists in the Star Wars universe. 
The code for this was almost entirely taken from the old Spacepark-Group-7 project.
We added some tests to check if the implementation of the class worked and also to be sure our pipeline could run the tests as a proof of concept.

When we were trying to setup Docker we ran into some heavy problems which stemmed from not having a .gitingore file (It was not included in the repo when we got it, which we missed).
Onwards we created our database using Entity Framework Core through the "code first" principal. 

We ran into problems with our CI Piplines, we had to remodel and create a new pipleine. The problem seemed to derrive from having a faulty azure-pipelines.yml file.

We configured the database in Azure Portal to be a serverless SQL Database. 
We put our Connectionstring in a seperate appsettings file (appsettings.dev.json). 
This created problems when our application was running in the cloud because our backend could'nt find the the appsettings.dev.json file.
We consulted the teacher for help, after sometime he came back with a appriciated solution (to specify the appsettings file in the pipeline.yml file).
