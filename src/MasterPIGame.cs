using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SadConsole;
using Game = SadConsole.Game;

namespace MasterPI2
{
	public class MasterPiGame : Game
	{
		public new static MasterPiGame Instance;
		
		public Data data;
		
		public const int WIDTH = 44;
		public const int HEIGHT = 25;
		
		public MasterPiConsole c;
		public FontMaster font;

		public MasterPiGame() : base("Fonts/C64.font", WIDTH, HEIGHT, null)
		{
			Instance = this;
		}

		protected override void Initialize()
		{
			base.Initialize();
			IsMouseVisible = true;

			DeserializeData();
			
			Global.CurrentScreen = c = new MasterPiConsole(WIDTH, HEIGHT);
			font = Global.LoadFont("Fonts/C64.font");
			c.TextSurface.Font = font.GetFont(Font.FontSizes.One);
		}
		
		public void SerializeData()
		{
			FileInfo fiExe = new FileInfo(Assembly.GetEntryAssembly().Location);
			DirectoryInfo diExe = fiExe.Directory;
			File.WriteAllText(Path.Combine(diExe.FullName, "data.json"), JsonConvert.SerializeObject(data, Formatting.Indented));
		}

		public void DeserializeData()
		{
			FileInfo fiExe = new FileInfo(Assembly.GetEntryAssembly().Location);
			DirectoryInfo diExe = fiExe.Directory;
			
			string datPath = Path.Combine(diExe.FullName, "data.json");
			
			if (!new FileInfo(datPath).Exists)
			{
				data = new Data();
				return;
			}
			
			data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(datPath));
		}

		protected override void Update(GameTime dt)
		{
			if (Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.F11))
			{
				Settings.ToggleFullScreen();
			} else if (Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
			{
				Exit();
			}
			
			c.Update(dt);
			base.Update(dt);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			base.OnExiting(sender, args);
			SerializeData();
		}
	}
}
