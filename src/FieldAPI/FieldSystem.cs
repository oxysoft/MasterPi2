using System;
using System.Collections.Generic;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Input;

namespace MasterPI2.MenuAPI
{
	public class FieldSystem
	{
		public Field SelectedField;
		public FieldContainer Root;

		public FieldSystem(FieldContainer root)
		{
			Root = root;
			root.System = this;
			
			SelectedField = root.GetFirstEntry();
			SelectedField.HasHighlight = true;
		}
		
		public void Update()
		{
			List<AsciiKey> keys = Global.KeyboardState.KeysPressed;
			
			foreach (AsciiKey ak in keys)
			{
				Keys k = ak.Key;
				
				if (k == Keys.Up)
				{
					SelectPrevious();
				} else if (k == Keys.Down)
				{
					SelectNext();
				}
			}
			
			foreach (AsciiKey ak in keys)
			{
				Keys k = ak.Key;
				
				SelectedField.CheckInput(k);
			}
		}

		private void Select(Field field)
		{
			if (SelectedField != null)
				SelectedField.HasHighlight = false;

			SelectedField = field;
			
			if (SelectedField != null)
				SelectedField.HasHighlight = true;
		}

		private void SelectNext() => Select(SelectedField.container.GetNextEntry(SelectedField, Keys.Down));

		private void SelectPrevious() => Select(SelectedField.container.GetNextEntry(SelectedField, Keys.Up));

		public void Draw(MasterPiConsole c, int x, int y)
		{
			Root.Draw(c, x, y);
		}
	}
}

