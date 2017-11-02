using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Shapes;
using SadConsole.Surfaces;
using Console = SadConsole.Console;

namespace MasterPI2.Consoles
{
	public class BorderedConsole : Console
	{
		BasicSurface sfc;
		
		public BorderedConsole(int width, int height) : base(width, height)
		{
			Print(0, 0, "This is a bunch of text data where we can see that there is a border around it");
			Position = new Point(4);

			sfc = new BasicSurface(width + 2, height+ 2);
			SurfaceEditor editor = new SurfaceEditor(sfc);
			
			Box box = Box.GetDefaultBox();
			box.Width  = sfc.Width;
			box.Height = sfc.Height;
			box.Draw(editor);

			Renderer.Render(sfc);	
		}

		public override void Draw(TimeSpan delta)
		{
			Global.DrawCalls.Add(new DrawCallSurface(sfc, new Point(-1), UsePixelPositioning));
			
			base.Draw(delta);
		}
	}
}

