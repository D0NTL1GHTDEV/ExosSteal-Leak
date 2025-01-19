using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramAPI;

public class Bot
{
	private readonly TelegramBotClient _botClient;

	private readonly string _chatId;

	public Bot(string token, string chatId)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		_botClient = new TelegramBotClient(token, (HttpClient)null);
		_chatId = chatId;
	}

	public async Task SendMessage(string message)
	{
		await TelegramBotClientExtensions.SendTextMessageAsync((ITelegramBotClient)(object)_botClient, ChatId.op_Implicit(_chatId), message, (int?)null, (ParseMode?)null, (IEnumerable<MessageEntity>)null, (bool?)null, (bool?)null, (bool?)null, (int?)null, (bool?)null, (IReplyMarkup)null, default(CancellationToken));
	}

	public async Task SendFile(string filePath, string caption = null)
	{
		using FileStream fileStream = File.OpenRead(filePath);
		InputFileStream val = new InputFileStream((Stream)fileStream, Path.GetFileName(filePath));
		await TelegramBotClientExtensions.SendDocumentAsync((ITelegramBotClient)(object)_botClient, ChatId.op_Implicit(_chatId), (InputFile)(object)val, (int?)null, (InputFile)null, caption, (ParseMode?)null, (IEnumerable<MessageEntity>)null, (bool?)null, (bool?)null, (bool?)null, (int?)null, (bool?)null, (IReplyMarkup)null, default(CancellationToken));
	}
}
