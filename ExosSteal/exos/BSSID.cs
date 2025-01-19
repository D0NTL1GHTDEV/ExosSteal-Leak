using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace exos;

internal class BSSID
{
	[DllImport("iphlpapi.dll", ExactSpelling = true)]
	private static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

	public static string GetBSSID()
	{
		byte[] array = new byte[6];
		uint physicalAddrLen = (uint)array.Length;
		try
		{
			if (SendARP(BitConverter.ToInt32(IPAddress.Parse(GetDefaultGateway()).GetAddressBytes(), 0), 0, array, ref physicalAddrLen) != 0)
			{
				return "unknown";
			}
			string[] array2 = new string[physicalAddrLen];
			for (int i = 0; i < physicalAddrLen; i++)
			{
				array2[i] = array[i].ToString("x2");
			}
			return string.Join(":", array2);
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return "Failed";
	}

	public static string GetDefaultGateway()
	{
		try
		{
			return (from g in (from n in NetworkInterface.GetAllNetworkInterfaces()
					where n.OperationalStatus == OperationalStatus.Up
					where n.NetworkInterfaceType != NetworkInterfaceType.Loopback
					select n).SelectMany((NetworkInterface n) => n.GetIPProperties()?.GatewayAddresses)
				select g?.Address into a
				where a != null
				select a).FirstOrDefault().ToString();
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return "Unknown";
	}
}
