using System;
using System.Runtime.InteropServices;

namespace exos;

internal class Buffer
{
	private const uint CF_UNICODETEXT = 13u;

	public static string GetBuffer()
	{
		if (WinAPI.IsClipboardFormatAvailable(13u) && WinAPI.OpenClipboard(IntPtr.Zero))
		{
			string result = string.Empty;
			IntPtr clipboardData = WinAPI.GetClipboardData(13u);
			if (!clipboardData.Equals((object?)(nint)IntPtr.Zero))
			{
				IntPtr intPtr = WinAPI.GlobalLock(clipboardData);
				if (!intPtr.Equals((object?)(nint)IntPtr.Zero))
				{
					try
					{
						result = Marshal.PtrToStringUni(intPtr);
						WinAPI.GlobalUnlock(intPtr);
					}
					catch (Exception value)
					{
						Console.WriteLine(value);
					}
				}
			}
			WinAPI.CloseClipboard();
			return result;
		}
		return null;
	}
}
