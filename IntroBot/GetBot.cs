using Telegram.Bot;

namespace IntroBot
{
    public static class GetBot
    {
        public static async Task Start(string token)
        {
            var botClient = new TelegramBotClient(token);
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Hello World! I'm user {me.Id} and my name is {me.FirstName}");
        }
    }
}
