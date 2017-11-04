using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MasterPI2
{
	public class Group
	{
		public int id;
		public string characters;
		public int success, fails;

		public Group(string characters, int id)
		{
			this.characters = characters;
			this.id = id;
		}

		public char this[int iDigit] => characters[iDigit];
	}
}

