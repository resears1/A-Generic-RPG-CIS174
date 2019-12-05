# A-Generic-RPG-CIS174
CIS174 Final Project

Controls:
Movement: WASD/ Arrow Keys
Attack: Spacebar/ Mouse 0
Quit: Escape

Web / API Access
When a user registers an account through the application, their data is being sent via JSON to "https://cis174gamewebsite.azurewebsites.net/api/user/reg". If there is an error with the request they will not be able to play the game and an error will appear on the screen asking them to try again.

When a user logs in to the game with an existing registered account, their data is being sent via JSON to "https://cis174gamewebsite.azurewebsites.net/api/user/login". If their password is incorrect or no account is found they will get an error message asking them to try again.

After playing the game, upon a players death their score for that session is logged to the database, and sent to the API at "https://cis174gamewebsite.azurewebsites.net/api/HighScore/addscore". A score number and User ID is logged to the database. After that a scoreboard will appear showing them their top 10 high scores. This information is retrieved from the API address "http://cis174gamewebsite.azurewebsites.net/api/highscore/" with a UserID, getting all of the scores for that user.
