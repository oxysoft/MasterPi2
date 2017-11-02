using System;
using System.Collections.Generic;
using System.Linq;
using MasterPI2.Consoles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;

namespace MasterPI2.Logic
{
	public class GroupEditorScreen : Screen
	{
		private Data dat;
		private string digits;
		private int iCursor;
		private List<Group> Groups => dat.structure.groups;
		
		private Lazy<Group> _CurrentGroup;
		private Lazy<int> _CurrentDigitIndex;
		
		public GroupEditorScreen(MasterPiConsole c, Data dat) : base(c)
		{
			this.dat = dat;
			digits = Pi.Get(dat.options.goal + 1).Substring(1); // Skip the first number as it isn't part of the floating numbers.
			
			_CurrentGroup = new Lazy<Group>(() =>
			{
				Group ret = null;
				int nDigits = 0;

				foreach (Group g in Groups)
				{
					for (int i = 0; i < g.characters.Length; i++)
					{
						if (nDigits == iCursor)
						{
							ret = g;
						}
						nDigits++;
					}
				}
				
				return ret;
			});
			
			_CurrentDigitIndex = new Lazy<int>(() =>
			{
				int nDigits = 0;

				foreach (Group g in Groups)
				{
					for (int i = 0; i < g.characters.Length; i++)
					{
						if (nDigits == iCursor)
							return i;

						nDigits++;
					}
				}
				
				return -1;
			});
		}
		
		public int GetGroupFirstIndex(Group g) => Groups.TakeWhile(gg => gg != g).Sum(gg => gg.characters.Length);
		public int GetGroupLastIndex(Group g) => Groups.TakeWhile(gg => gg != g).Sum(gg => gg.characters.Length) + g.characters.Length - 1;

		private Group CurrentGroup => _CurrentGroup.Value;
		private int CurrentDigitIndex => _CurrentDigitIndex.Value;
		
		public override void Update(GameTime dt)
		{
			if (Global.KeyboardState.IsKeyPressed(Keys.Back))
			{
				Finish();
			} else if (Global.KeyboardState.IsKeyPressed(Keys.F1))
			{
				Groups.Clear();
				Groups.Add(new Group(string.Join("", digits), 0));
			}
			
			if (Global.KeyboardState.IsKeyPressed(Keys.Left))
			{
				iCursor = Math.Max(iCursor - 1, 0);
				_CurrentGroup.Invalidate();
				_CurrentDigitIndex.Invalidate();
			} else if (Global.KeyboardState.IsKeyPressed(Keys.Right))
			{
				iCursor = Math.Min(iCursor + 1, digits.Length);
				_CurrentGroup.Invalidate();
				_CurrentDigitIndex.Invalidate();
			}

			if (Global.KeyboardState.IsKeyPressed(Keys.Space))
			{
				SplitGroup();
			} else if (Global.KeyboardState.IsKeyPressed(Keys.Enter))
			{
				SplitGroup(true);
			}
			
			if (Global.KeyboardState.IsKeyPressed(Keys.Delete))
			{
				JoinGroup();
			}
		}
		
		/// <summary>
		/// Splits the current group into two based on the current cursor position.
		/// </summary>
		private void SplitGroup(bool isIncrementId = false)
		{
			Group cgroup = CurrentGroup;
			Group ngroup = new Group(cgroup.characters.Substring(CurrentDigitIndex, cgroup.characters.Length - CurrentDigitIndex), cgroup.id + (isIncrementId ? 1 : 0));
			cgroup.characters = cgroup.characters.Substring(0, CurrentDigitIndex);
			int cindex = Groups.IndexOf(cgroup);
			Groups.Insert(cindex + 1, ngroup);
		}
		
		/// <summary>
		/// Joins the current and next group.
		/// </summary>
		private void JoinGroup()
		{
			Group cgroup = CurrentGroup;
			if (cgroup == Groups.Last())
				return;
			
			Group ngroup = Groups[Groups.IndexOf(CurrentGroup) + 1];
			cgroup.characters += ngroup.characters;
			Groups.Remove(ngroup);
		}

		private Group GetGroup(int i)
		{
			if (i < 0 || i >= Groups.Count)
				return null;
			
			return Groups[i];
		}
		
		public override void Draw(TimeSpan dt)
		{
			
			Point margin = new Point(2, 1);
			
			c.Print(margin.X, margin.Y, "[Group Editor]");
			
			margin.Y += 2;
			
			const string HEADER = "3.";
			c.Print(margin.X, margin.Y, HEADER, Color.AntiqueWhite);

			int xx = 0;
			int yy = 0;
			int nDigits = 0;

			for (int i = 0; i < Groups.Count; i++)
			{
				Group g = GetGroup(i);

				Color fgColor = Configuration.ID_COLORS[g.id % Configuration.ID_COLORS.Length];
				Color bgColor = Color.Black;
				
				for (int j = 0; j < Groups[i].characters.Length; j++)
				{
					// Is the cursor digit
					if (nDigits == iCursor)
					{
						c.VirtualCursor.IsVisible = true;
						c.VirtualCursor.Position = new Point(margin.X + xx + HEADER.Length, margin.Y + yy);
					}
					
					// Draw character
					c.Print(margin.X + xx + HEADER.Length, margin.Y + yy, g.characters[j].ToString(), fgColor, bgColor);

					xx++;
					
					if (margin.X + xx + HEADER.Length >= c.Width - 1)
					{
						yy++;
						xx = 0;
					}
					
					nDigits++;
				}
				
				xx++;
				
				Group gr = GetGroup(i + 1);
				if (gr != null && gr.id != g.id)
				{
					yy++;
					xx = 0;
				}
			}
		}
	}
}

