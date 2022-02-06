w# RestaurantGFT

Steps to run backend project

1) Install SQL Server Express. You can dowload it at: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

2) Instal Dotnet ASP.NET Core 5.0 runtime. You can download it at:
https://dotnet.microsoft.com/en-us/download/dotnet/5.0

3) After that, open the Terminal window, run the following command, where 'root' is the folder where you downloaded the solution
C:\>cd\<root>\Backend\API

4) Run the following command to initialize the database
c:\<root>\Backend\API\>dotnet ef database update

5) Start de web api service
C:\<root>\Backend\API\>dotnet run 

4) Open your browser and navigate to the folloing URL. If it is loaded, than the web api is running ok.
http://localhost:5000/swagger/index.html


