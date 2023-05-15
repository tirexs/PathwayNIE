using System.Text;
using PathwayNIE.GPT.JSONClasses;

namespace PathwayNIE.GPT
{
	public static class GptResponse
	{
		private static readonly StringBuilder GptMessage = new();
		private const string Path = "GPT/.key.txt";

		public static string GetResponse(string userMessage)
		{
			GptMessage.Clear();
			string apiKey = File.ReadAllText(Path, Encoding.UTF8);
			// адрес api для взаимодействия с чат-ботом
			string endpoint = "https://api.openai.com/v1/chat/completions";
			// набор соообщений диалога с чат-ботом
			List<Message> messages = new List<Message>();
			// HttpClient для отправки сообщений
			var httpClient = new HttpClient();

			var message = new Message() { Role = "user", Content = userMessage };
			// добавляем сообщение в список сообщений
			messages.Add(message);

			// формируем отправляемые данные
			var requestData = new Request()
			{
				ModelId = "gpt-3.5-turbo",
				Messages = messages
			};

			// устанавливаем отправляемый в запросе токен
			httpClient.DefaultRequestHeaders.Clear();
			httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
			// отправляем запрос

			using var response = httpClient.PostAsJsonAsync(endpoint, requestData).Result;

			if (!response.IsSuccessStatusCode)
			{
				GptMessage.Append("Сервер не отвечает, повторите позже");
				return GptMessage.ToString();
			}
			ResponseData? responseData = response.Content.ReadFromJsonAsync<ResponseData>().Result;

			var choices = responseData?.Choices ?? new List<Choice>();
			if (choices.Count == 0)
			{
				GptMessage.Append("Сервер не отвечает, повторите позже");
				return GptMessage.ToString();
			}
			var choice = choices[0];
			var responseMessage = choice.Message;
			// добавляем полученное сообщение в список сообщений
			messages.Add(responseMessage);
			GptMessage.Append(responseMessage.Content.Trim());

			return GptMessage.ToString();
		}
	}
}
