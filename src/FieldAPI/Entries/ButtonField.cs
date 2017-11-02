using System;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2
{
	public class ButtonField : Field
	{
		string label;
		Action onSelected;

		public ButtonField(string label, Action onSelected)
		{
			this.label = label;
			this.onSelected = onSelected;
		}

		public override int Height => 1;

		public override void CheckInput(Keys k)
		{
			if (k == Keys.Enter)
			{
				onSelected();
			}
		}

		public override void Draw(MasterPiConsole c, int x, int y)
		{
			c.Print(x, y, $"[{label}]", Color.White, LabelBg);
		}
	}
}

