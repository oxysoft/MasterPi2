using System;
using System.Linq;
using MasterPI2.Consoles;
using MasterPI2.MenuAPI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;

namespace MasterPI2.Logic
{
	public class OptionScreen : Screen
	{
		private Data dat;
		public FieldSystem fs;

		public OptionScreen(MasterPiConsole c, Data dat) : base(c)
		{
			this.dat = dat;
			fs = new FieldSystem(
				new FieldStack(
					new LRField("Failure Setback", b => dat.options.enableSetbacks = b) { selection = dat.options.enableSetbacks }
				)
			);
		}

		public override void Update(GameTime dt)
		{
			if (Global.KeyboardState.IsKeyPressed(Keys.Back))
			{
				Finish();
			}
			
			fs.Update();
		}

		public override void Draw(TimeSpan dt)
		{	
			Point margin = new Point(2, 1);
			c.PrintLogo(dt, margin.X, margin.Y);
			margin.Y += 2;
			
			fs.Draw(c, margin.X, margin.Y);
		}
	}
}

