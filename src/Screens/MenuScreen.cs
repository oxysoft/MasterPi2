using System;
using MasterPI2.Consoles;
using MasterPI2.MenuAPI;
using Microsoft.Xna.Framework;

namespace MasterPI2.Logic
{
	public class MenuScreen : Screen
	{
		private Data dat;
		private FieldSystem fs;

		public MenuScreen(MasterPiConsole c, Data dat) : base(c)
		{
			this.dat = dat;
			
			fs = new FieldSystem(new FieldStack(
				new ButtonField("Start", () =>
				{
					NextScreen = new GameScreen(c, dat);
					Finish();
				}), 
				
				new ButtonField("Options", () =>
				{
					NextScreen = new OptionScreen(c, dat);
					Finish();
				}), 
				
				new ButtonField("Group Editor", () =>
				{
					NextScreen = new GroupEditorScreen(c, dat);
					Finish();
				}),
				
				new ButtonField("Exit", () =>
				{
					MasterPiGame.Instance.Exit();
				})
			));
		}

		public override void Update(GameTime dt)
		{
			fs.Update();
		}

		public override void Draw(TimeSpan dt)
		{	
			c.VirtualCursor.IsVisible = false;

			Point margin = new Point(2, 1);
			c.PrintLogo(dt, margin.X, margin.Y);
			margin.Y += 2;
			
			fs.Draw(c, margin.X, margin.Y);
			
//			for (int i = 0; i < items.Length; i++)
//			{
//				string item = items[i];
//				
//				if (i == iSelection)
//				{
//					c.Print(margin.X, margin.Y + i, $"[ {item}{"]".PadLeft(items.Max(it => it.Length) - item.Length + 2)}");
//				} else
//				{
//					c.Print(margin.X, margin.Y + i, $"  {item}", Color.DarkOrange);
//				}
//			}
		}
	}
}

