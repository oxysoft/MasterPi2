using System;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2
{
	public class LRField : Field
	{
		private readonly Action<bool> onSelected;
		private string label, lblLeft, lblRight;
		public bool selection;

		public LRField(string label, Action<bool> onSelected, string lblLeft = "Off", string lblRight = "ON")
		{
			this.label = label;
			this.onSelected = onSelected;
			this.lblLeft = lblLeft;
			this.lblRight = lblRight;
		}

		public override int Height => 1;

		public override void CheckInput(Keys k)
		{
			if (k == Keys.Left)
				onSelected(selection = false);
			else if (k == Keys.Right)
				onSelected(selection = true);
		}
		
		public override void Draw(MasterPiConsole c, int x, int y)
		{
			string lbl = $"{label}:";
			
			const int dist = 5;
			c.Print(x, y, lbl, Color.White, LabelBg);
			
			int headerSpan = lbl.Length + 2; 
			
			c.Print(x + headerSpan, y, !selection ? "OFF]" : "OFF", Color.OrangeRed,  selection ? Color.Black : Color.DarkRed);
			if (!selection)
				c.Print(x + headerSpan - 1, y,  "[" , Color.OrangeRed, Color.DarkRed);
				
			
			c.Print(x + headerSpan + dist, y,  selection ? "ON]"  : "ON" , Color.LawnGreen, !selection ? Color.Black : Color.DarkGreen);
			if (selection)
				c.Print(x + headerSpan + dist - 1, y,  "[" , Color.LawnGreen, Color.DarkGreen);
		}
	}
}

