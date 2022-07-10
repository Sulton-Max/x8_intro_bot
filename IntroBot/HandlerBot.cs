using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IntroBot
{
    public class HandlerBot
    {
        private readonly TelegramBotClient _botClient;

        public HandlerBot(string token)
        {
            _botClient = new TelegramBotClient(token);
        }

        public async Task Start()
        {
            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving
            (
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var bot = await _botClient.GetMeAsync();
            Console.WriteLine($"Receiving updates from {bot.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;

            Console.WriteLine($"Received message - {update.Message.Text} from chat id - {update.Message.Chat.Id}");
            await _botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: $"Bot received {update.Message.Text}",
                cancellationToken: cancellationToken
            );
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var message = exception switch
            {
                ApiRequestException requestException => $"Telegram bot api exception : \n{requestException.ErrorCode}\n{requestException.Message}"
            };

            Console.WriteLine(exception);
            return Task.CompletedTask;
        }
    }
}
