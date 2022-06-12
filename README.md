# TwitterWalletReplies
Project to retrieve the wallet addresses for the replies of a twitter thread by using the Twitter API.

This is a Console App on .NET 6. To build and compile this yourself you need something like Visual Studio 2022. I suggest downloading one of the compiled releases.

# Setup 
Download one of the compiled releases in the [Releases](https://github.com/AngleArt/TwitterWalletReplies/releases) section. You will need to edit the included `appsettings.json` file with your own Twitter API details; ie API bearer token. You can export it from your account via https://developer.twitter.com/. Remember to keep these values private and do not share with anyone!

## Usage
You can call TwitterWalletReplies via command line as follows, where the first argument is the tweet status id of your tweet (for https://twitter.com/AngleArtNFT/status/1527657442133393408 the status id will be "1527657442133393408"):

- Change `appsettings.json` file, `TwitterBearerToken` property with your own Twitter API Bearer Token.
- Optional: you can change the `appsettings.json` to save or not the twitter `username`
 - `"SaveUsernames": false` will save only the wallet addresses in the .csv file (default: true).
- Open PowerShell or cmd.
- Navigate to the folder with the "TwitterWalletReplies.exe"
- Execute `.\TwitterWalletReplies {status_id}`
 - ex.: `.\TwitterWalletReplies 1527657442133393408`
- The app will create a new folder (if not existing) called "/output" and save the .csv there

# Limitations
- Twitter Search API only allows retrieving replies less than 7 days old.
- For the `out of the box` version of this app only these 3 patterns will be retrieved:
 - Classic eth addresses: **0x13a7434ba28eee742f3cdbc06f392e826e296a01**
 - Loopring ENS: **angleart.loopring.eth**
 - Eth ENS: **angleart.eth**