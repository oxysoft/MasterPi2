using System;
using System.Collections.Generic;
using System.Linq;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Input;

namespace MasterPI2.Logic
{
	public class GameScreen : Screen
	{
		public Data dat;
		public List<Group> groups;
		public int iGroup, iDigit;
		
		public bool IsDone => iGroup >= groups.Count;
		public Group CurrentGroup => !IsDone ? groups[iGroup] : null;
		public char NextDigit => CurrentGroup[iDigit];
		
		public bool isRevealUnentered;
		int minGroup = -1;

		public GameScreen(MasterPiConsole c, Data dat) : base(c)
		{
			this.dat = dat;
			this.groups = dat.structure.groups;
		}

		public override void Update(GameTime dt)
		{
			List<AsciiKey> keys = Global.KeyboardState.KeysPressed;

			if (keys.Count <= 0)
				return;

			AsciiKey k = keys.FirstOrDefault();

			if (k.Key == Keys.Back)
			{
				NextScreen = new MenuScreen(c, dat);
				Finish();
				return;
			}
			
			if (isGameEnded)
			{
				if (k.Key == Keys.Enter)
				{
					NextScreen = new GameScreen(c, dat) {
						minGroup = minGroup,
						iGroup = minGroup
					};
					
					Finish();
				}
			} else
			{
				if (k.Key == Keys.R)
					isRevealUnentered = !isRevealUnentered;
				
				if (k.Key == Keys.Left)
					DecrementGroup();
				else if (k.Key == Keys.Right)
					IncrementGroup();
				else if (k.Key == Keys.Space)
				{
					if (minGroup == -1)
						minGroup = iGroup;
					else
						minGroup = -1;
				}
				
				// game running
				if (k.Character == NextDigit)
				{
					Advance();
				} else if (char.IsDigit(k.Character))
				{
					if (dat.options.enableSetbacks)
					{
						DecrementGroup();
					} else
					{
						Fail();
					}
				}
			}
		}

		private void Fail()
		{
			isGameEnded = true;
			isRevealUnentered = true;
		}
		
		private void DecrementGroup()
		{
			iDigit = 0;
			iGroup = Math.Max(iGroup - 1, minGroup != -1 ? minGroup : 0);
		}
		
		private void IncrementGroup()
		{
			iDigit = 0;
			iGroup = Math.Min(groups.Count - 1, iGroup + 1);
		}
		
		private bool isGameEnded;
		private Group GetGroup(int i)
		{
			if (i < 0 ||
			    i >= groups.Count
			    ) return null;
			return groups[i];
		}
		
		private void Advance()
		{
			if (++iDigit >= CurrentGroup.characters.Length)
			{
				// Out of digits in the group.
				iDigit = 0;
			
				if (++iGroup >= groups.Count)
				{
					// Ran out of groups.
					// Win or populate new group...
				}
			}
		}

		float seconds;
		public override void Draw(TimeSpan dt)
		{
			Point margin = new Point(2, 1);
			
			c.PrintLogo(dt, margin.X, margin.Y);
			
			margin.Y += 2;
			
			const string HEADER = "3.";
			c.Print(margin.X, margin.Y, HEADER, Color.AntiqueWhite);

			int xx = 0;
			int yy = 0;
			int nDigits = 0;

			for (int i = 0; i < groups.Count; i++)
			{
				Group g = GetGroup(i);

				Color fgColor = Configuration.ID_COLORS[g.id % Configuration.ID_COLORS.Length];
				Color bgColor = Color.Black;
				if (i < iGroup)
					bgColor = Color.Lerp(Color.Blue, Color.Black, 0.3f);
				
				bool isPastCursor = i > iGroup; 
					
				for (int j = 0; j < groups[i].characters.Length; j++)
                {
	                isPastCursor |= i == iGroup && j >= iDigit;

	                if (!isPastCursor)
		                nDigits++;

	                // Is the cursor digit
	                if (i == iGroup && j == iDigit)
	                {
		                c.VirtualCursor.IsVisible = !isGameEnded;
		                c.VirtualCursor.Position = new Point(margin.X + xx + HEADER.Length, margin.Y + yy);
	                }
	                
	                char ch = g.characters[j];
	                if (isPastCursor)
	                {
		                if (!isRevealUnentered)
		                	ch = '.';
		                else
			                fgColor = Color.Gray;
	                }
	                
	                if (minGroup >= 0 && i < minGroup)
	                {
		                fgColor = Color.DarkGray;
	                }
	                
                    // Draw character
	                c.Print(margin.X + xx + HEADER.Length, margin.Y + yy, ch.ToString(), fgColor, bgColor);

	                xx++;
	                
	                if (margin.X + xx + HEADER.Length >= c.Width - 1)
	                {
		                yy++;
		                xx = 0;
	                }
                }
				
				xx++;
				
				Group gr = GetGroup(i + 1);
				if (gr != null && gr.id != g.id)
				{
					yy++;
					xx = 0;
				}
			}

			if (isGameEnded)
			{
				yy += 3;
				
				c.Print(0, margin.Y + yy, "".PadLeft(c.Width, '='), Color.Lerp(Color.DarkGray, Color.Black, 0.75f));
				
				yy += 2;
				float p = nDigits / (float) dat.options.goal;
				string percentString = $"{p * 100:0.00}%";
				c.Print(margin.X, margin.Y + yy, percentString, Color.Red);
				c.PrintProgressBar(new Point(margin.X + percentString.Length + 2, margin.Y + yy), 25, p, Color.Red, Utils.HSV((float) (Math.Sin(seconds += (float) dt.TotalSeconds * 4)*0.5f + 0.5f), .8f, 1f, 1.0f));
				
				yy += 1;
				c.Print(margin.X                                        , margin.Y + yy,  "Fail at ",   Color.White);
				c.Print(margin.X + 8                                    , margin.Y + yy, $"{nDigits} ", Color.Red);
				c.Print(margin.X + 8 + nDigits.ToString().Length + 1    , margin.Y + yy,  "/ ",         Color.White);
				c.Print(margin.X + 8 + nDigits.ToString().Length + 1 + 2, margin.Y + yy, $"{dat.options.goal}", Color.Red);
			}
		}
	}
}

