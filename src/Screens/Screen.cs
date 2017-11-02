using System;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace MasterPI2.Logic
{
	public abstract class Screen
	{
		protected MasterPiConsole c;
		public bool IsRequireExit { get; private set; }
		public Screen NextScreen { get; protected set; }

		protected Screen(MasterPiConsole c)
		{
			this.c = c;
		}

		protected void Finish() => IsRequireExit = true;

		public abstract void Update(GameTime dt);
		
		public abstract void Draw  (TimeSpan dt);
	}
}

