using System.Collections.Generic;

namespace MasterPI2
{
	public class Data
	{
		public Data_Structure structure = new Data_Structure();
		public Data_Options options = new Data_Options();
	}
	
	public class Data_Structure
	{
		public List<Group> groups = new List<Group>();
	}
	
	public class Data_Options
	{
		public int goal = 100;
		public bool enableSetbacks = false;
	}
}

