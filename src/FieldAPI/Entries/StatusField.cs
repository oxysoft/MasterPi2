using MasterPI2.Consoles;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2
{
	public class StatusField : Field
	{
		public string label;
		
		public StatusField(string label)
		{
			this.label = label;
		}

		public override int Height => 0;

		public override bool AcceptsHighlight => false;

		public override void CheckInput(Keys k)
		{
		}

		public override void Draw(MasterPiConsole c, int x, int y)
		{
			c.Print(0, c.Height - 1, label);
		}
	}
}

