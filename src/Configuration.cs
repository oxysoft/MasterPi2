using Microsoft.Xna.Framework;

namespace MasterPI2
{
	public class Configuration
	{
		// MODE
		public const bool MODE_SETBACK    = false;
		public const bool MODE_RANDOM     = true;
		public const bool MODE_HAS_FINISH = true;
		
		// GENERAL
		public const int TARGET_MAX     = 100;
		public const int NEW_GROUP_SIZE = 5;
		
		// VISUALS
		public const int MAX_ROW_LENGTH   = 30;

		public static readonly Color[] ID_COLORS = {
			Color.Aquamarine,
			Color.Orange,
			Color.Red,
			Color.Aqua,
			Color.GreenYellow,
			Color.Coral
		};
		public static readonly Color PREVIEW_COLOR = Color.DarkOliveGreen;
		public static readonly Color GROUP_COLOR   = Color.DarkOliveGreen;
		
		// ---- PROGRESS
		public const int    PBAR_LENGTH     = 60;
		public const char   BAR_CHAR_LEFT   = '['; // [@@@------]
		public const char   BAR_CHAR_FILL   = '@';
		public const char   BAR_CHAR_RIGHT  = ']';
		public const char   BAR_CHAR_EMPTY  = '*';
	}
}

