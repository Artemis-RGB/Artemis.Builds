# Artemis Build fetcher
[![GitHub license](https://img.shields.io/badge/license-GPL3-blue.svg)](https://github.com/Artemis-RGB/Artemis.Builds/blob/master/LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/Artemis-RGB/Artemis.Plugins.svg)](https://github.com/Artemis-RGB/Artemis.Builds/stargazers)
[![Discord](https://img.shields.io/discord/392093058352676874?logo=discord&logoColor=white)](https://discord.gg/S3MVaC9) 
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VQBAEJYUFLU4J) 

A simple web application that fetches the latest build artifacts for the given build definition

#### Want to build? Follow these instructions
1. Clone the repository
2. Restore NuGet packages
3. Open `src\appsettings.json` and replace `<insert the token here>` with your DevOps access token
4. Optionally also replace the value of `ApiBaseUrl`  depending on which organisation and project you want to access.
