using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace exos.utils;

internal class GeoGet
{
	public static Task<string> getGEO()
	{
		return Task.Run(async delegate
		{
			string country = "Error country";
			HttpClient client = new HttpClient();
			try
			{
				JObject val = JObject.Parse(await client.GetStringAsync("https://ipinfo.io/json"));
				country = ((object)val["country"]).ToString();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			finally
			{
				((IDisposable)client)?.Dispose();
			}
			return country;
		});
	}

	public static string Geo()
	{
		Task<string> gEO = getGEO();
		gEO.Wait();
		return gEO.Result;
	}
}
