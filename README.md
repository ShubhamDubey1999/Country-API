#CountryAPI
-
SetUp :-
Step 1 : Install the dotnet sdk via this link :- https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.404-windows-x64-installer

Step 2 : Install C# extension in vs code
Step 3 : Open the Project that you have cloned from the git.

Step 4 : Open the terminal to build the project (command : dotnet build).

Step 5 : Run the project ( command : dotnet run). 

#API Instruction
To use any api click on the api, then dropdown will open , on the right hand side there is a button "Try it out", in every api. To use api click on the "Try it out" button.
After clicking the button input fields will enable to put parameters.

1. Authentication API
   { Username - Admin , 
   Password - Admin@123 }
   Pass the username & password in api parameters,   
   if username and password is correct then , api will generate an authentication token in response , else data will be null and reason will be displayed in the message of respnse like "Username not found or Password is incorrect",
   To validate the token copy the token then click on "Authorize" button a popup will show up , paste that token in the value field  (ex:-  ' bearer _token '). (Authorize button is on the top of APIs).

2. After the successful Authentication and Authorization , the other 2 APIs will be authorized to the authenticated user.

3. Second is Name API ,which take parameter country name ,  it'll fetch country data on the basis of country name in the response.

4. third is filter API , which can filter the country data on the basis of population , area , language and this api also support the pagination and sorting (sorting will be done on the basis of country name).

 I've set the defaut value of pagination = 100 and sort = 0 , but the values are changable, we can set any other values.

 sort 0 means ascending order
 sort 1 means descending order
