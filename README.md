# Medikeeper
Sample project for Medikeeper to demonstate REST API coordination between a client side SPA and server side MVC application.

## Details

API:  
ASP.NET Core 3.1  
Visual Studio 2019  

Client:  
Angular 9  
Visual Studio Code

## Demo
Start server  
1. Open the API solution in Visual Studio and start the application by selecting IIS Express.  
2. Navigate to `http://localhost:5000/items` and check JSON output is displaying. This indicates server is ready.  

Start client  
1. Open a command line utility to the Client app folder  
2. Run command 'ng serve'  
3. Navigate to `http://localhost:4200` and verify display of front end. Medikeeper title, items form, items list should display.

## Preview
![alt text](https://github.com/psethu/Medikeeper/blob/master/MedikeeperClient/Supporting/MedikeeperClient.png?raw=true)  