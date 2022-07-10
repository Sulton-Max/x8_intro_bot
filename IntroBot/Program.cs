using IntroBot;
using Telegram.Bot.Types.Enums;

var token = "5445716023:AAE6w8k5mVzlpgG8uM6sGdMm54E8c1ofxdA";

// To get Bot
//await GetBot.Start(token);

// To use Bot with polling
var handlerBot = new HandlerBot(token);
await handlerBot.Start();