using System;
using DungeonGeneration;

namespace DungeonConsoleDisplay
{
	class Displayer
	{
		public static void Main (string[] args)
		{
			var grid = new Grid (100, 100);
			var stamper = new RoomStamper (grid);

			var room = new RoomSpec {
				x = 1,
				y = 1,
				width = 10,
				height = 10
			};
			stamper.Stamp (room);

			DrawGrid (grid);
		}

		static void DrawGrid (Grid grid)
		{
			for (int y = 0; y < grid.Height; y++) {
				var line = "";
				for (int x = 0; x < grid.Width; x++) {
					var tile = grid.GetTile (x, y);
					line += (tile.IsOpen) ? " " : "#";
				}
				Console.WriteLine (line);
			}
		}
	}
}
