ATPTopTen

The ATP Top Ten web application's main page will display a list of top ten Tennis players sorted by rank. Players with a ranking of 1, 2 or 3 will have a medal next to their names. These medal images are displayed dynamically with HTML and CSS.

If you click on the name of any of the players, it will take you to the player's Bio page, where you can see a brief description of their career, as well as their nationality and a few pictures of them. You can also add them as a Favorite by clicking the "Add to Favorites" button on the top right corner of the page, which will make them display a star next to their name when going back to the top ten list. You can navigate back and forth between all the top ten players by clicking on the navigation button on the bottom of the page. Alternatively, you can go back to the top ten list by clicking the back button at the top of the page.

The database file is included in the App_Data folder. To run the application, you don't need to make any changes to the database file. Alternatively, you can recreate any version of the the database (including sample data) by running the Update-Database command in the Package Manager Console. Every version of the database has been carefully written with Up and Down scripts that will update or downgrade the database to the corresponding version. The initial data for the Top Ten Players is parsed from the JSON file received from the ATP api web service. In case that service is down, I have also saved the json data as an embedded resource on the application, so that it will always have the initial data readily available. The data for Career Summary, Head to Head information and Countries have also been scripted in the Up and Down methods for initial testing.

Oh and the background image will cycle every hour (fun!).

If I had more time, a few improvements that I would make are:

1) Finish displaying Head to Head information using Javascript
2) Make sure the app is resposive for different devices (started working on it, but ran out of time)
3) Adding to favorites with Javascript so that the page doesn't have to be reloaded when clicking Add to Favorites
4) Add dependency injection to be able to unit test the Controllers
5) Unit testing!! Write unit tests for all the controller actions
6) Call the api with Javascript so that the page doesn't have to refresh every time we need to call the api

All these improvements have been recorded in the code with "todo" comments. Go to Task List to see them.
List of technologies used:

Git for source control 
Entity Framework for database creation and versioning 
Newtonsoft.JSON for serializing/deserializing data between database and client app 
Bootstrap for responsive template (still working on it) 
MVC .Net framework for the front-end. 
WebAPI for the back-end. 
SVG for flags. 
Automapper to automatically map the DTOs with their Model counterparts

Technologies I wanted to implement but I ran out of time:

Dependency Injection (Ninject) 
Moq for unit testing 
Jquery Data Tables 
BootBox