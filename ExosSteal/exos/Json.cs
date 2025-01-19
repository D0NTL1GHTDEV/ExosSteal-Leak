using System.Text.RegularExpressions;

namespace exos;

internal sealed class Json
{
	public string Data;

	public Json(string data)
	{
		Data = data;
	}

	public string GetValue(string value)
	{
		string empty = string.Empty;
		Match match = new Regex("\"" + value + "\":\"([^\"]+)\"").Match(Data);
		if (!match.Success)
		{
			return empty;
		}
		return Regex.Split(match.Value, "\"")[3];
	}

	public void Remove(string[] values)
	{
		foreach (string oldValue in values)
		{
			Data = Data.Replace(oldValue, "");
		}
	}

	public string[] SplitData(string delimiter = "},")
	{
		return Regex.Split(Data, delimiter);
	}
}
