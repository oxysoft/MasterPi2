using MasterPI2.Consoles;
using MasterPI2.MenuAPI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2
{
	public abstract class Field
	{
		public FieldContainer container;

		public virtual bool AcceptsHighlight => true;
		
		public Color LabelBg => HasHighlight ? Color.DarkOrchid : Color.Black;
		
		public bool HasHighlight { get; set; }
		
		public abstract int Height { get; }
		
		public abstract void CheckInput(Keys k);
		
		public abstract void Draw(MasterPiConsole c, int x, int y);
	}
}

