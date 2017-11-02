using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MasterPI2.MenuAPI
{
	public abstract class FieldContainer : Field
	{
		public abstract IEnumerable<Field> Entries { get; }
		public int HeaderSpan => 15;

		public FieldSystem System;
		
		public abstract Field GetFirstEntry();
		
		public abstract Field GetNextEntry(Field current, Keys k);
		
		public virtual void Update()
		{
		}
	}
}

