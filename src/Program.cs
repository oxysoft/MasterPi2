using SadConsole;

namespace MasterPI2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			MasterPiGame game = new MasterPiGame();
			
			Game.Instance.Run();
			Game.Instance.Dispose();
		}
	}
}

