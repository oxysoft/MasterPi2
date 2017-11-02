using System;
using System.Collections.Generic;
using System.Linq;
using MasterPI2.Consoles;
using MasterPI2.MenuAPI;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2
{
	public class FieldStack : FieldContainer
	{
		public List<Field> fields;

		public FieldStack(params Field[] fields)
		{
			this.fields = new List<Field>(fields);
			foreach (Field entry in fields)
			{
				entry.container = this;
			}
		}

		public override IEnumerable<Field> Entries => fields;

		public override Field GetFirstEntry()
		{
			return fields[0];
		}

		public override Field GetNextEntry(Field current, Keys k)
		{
			int idx = fields.IndexOf(current);
			if (k == Keys.Up)
			{
				
				for (int i = idx - 1; i >= 0; i--)
				{
					Field field = fields[i];
					if (field != null && field.AcceptsHighlight)
						return field;
				}
			} else if (k == Keys.Down)
			{
				for (int i = idx + 1; i < fields.Count; i++)
				{
					Field field = fields[i];
					if (field != null && field.AcceptsHighlight)
						return field;
				}
			}
			
			return current;
		}

		public override int Height => fields.Sum(e => e.Height);

		public override void CheckInput(Keys k)
		{
		}

		public override void Draw(MasterPiConsole c, int x, int y)
		{
			int xx = 0;
			int yy = 0;
			
			for (int i = 0; i < fields.Count; i++)
			{
				fields[i].Draw(c, x + xx, y + yy);
				yy++;
			}
		}
	}
}

