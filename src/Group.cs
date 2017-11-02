using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MasterPI2
{
	public class Group
	{
		public int id;
		public string characters;
		public int success, fails;
		[JsonIgnore] public Point[] lastDrawPositions; 

		public Group(string characters, int id)
		{
			this.characters = characters;
			this.id = id;
			lastDrawPositions = new Point[characters.Length];
		}

		public char this[int iDigit] => characters[iDigit];
	}
}

