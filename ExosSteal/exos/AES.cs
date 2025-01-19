using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace exos;

internal class AES
{
	private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

	public static string EncryptStringAES(string plainText, string sharedSecret)
	{
		if (string.IsNullOrEmpty(plainText))
		{
			throw new ArgumentNullException("plainText");
		}
		if (string.IsNullOrEmpty(sharedSecret))
		{
			throw new ArgumentNullException("sharedSecret");
		}
		string result = "";
		RijndaelManaged rijndaelManaged = null;
		try
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Salt);
			rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
			ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
			using MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(BitConverter.GetBytes(rijndaelManaged.IV.Length), 0, 4);
			memoryStream.Write(rijndaelManaged.IV, 0, rijndaelManaged.IV.Length);
			using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
			{
				using StreamWriter streamWriter = new StreamWriter(stream);
				streamWriter.Write(plainText);
			}
			result = Convert.ToBase64String(memoryStream.ToArray());
			return result;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			return result;
		}
		finally
		{
			rijndaelManaged?.Clear();
		}
	}
}
