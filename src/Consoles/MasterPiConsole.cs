using System;
using MasterPI2.Logic;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace MasterPI2.Consoles
{
	public class MasterPiConsole : Console
	{
		Screen _screen;
		
		public MasterPiConsole(int width, int height) : base(width, height)
		{
			_screen = new MenuScreen(this, MasterPiGame.Instance.data);
			VirtualCursor.CursorEffect = null;
		}

		private float seconds;
		public void PrintLogo(TimeSpan dt, int x, int y) // 2 1
		{
			Print(x, y, "[ MasterPI2 ]", Utils.HSV((float) (Math.Sin(seconds += (float) dt.TotalSeconds * 12)*0.5f + 0.5f), .8f, 1f, 1.0f));
		}

		public override void Draw(TimeSpan dt)
		{
			base.Draw(dt);

			Clear();
			_screen.Draw(dt); // , new Point(2, 5)
		}

		public void PrintProgressBar(Point off, int width, float ratio, Color c, Color contentColor)
		{
			Print(off.X, off.Y, Configuration.BAR_CHAR_LEFT.ToString(), Color.White);
			for (int i = 0; i < width - 2; i++)
			{
				if (i / (float) width <= ratio)
				{
					Print(off.X + i + 1, off.Y, Configuration.BAR_CHAR_FILL.ToString(), contentColor);
				} else
				{
					Print(off.X + i + 1, off.Y, Configuration.BAR_CHAR_EMPTY.ToString(), Color.Lerp(c, Color.Black, 0.75f));
				}
			}
			Print(off.X + width - 1, off.Y, Configuration.BAR_CHAR_RIGHT.ToString(), Color.White);
		}

		public void Update(GameTime dt)
		{
			_screen.Update(dt);

			if (_screen.IsRequireExit)
			{
				Clear();

				_screen = _screen.NextScreen ?? new MenuScreen(this, MasterPiGame.Instance.data);
			}
		}
	}
}

