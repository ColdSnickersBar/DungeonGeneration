using System;

namespace DungeonGeneration
{
	public class Grid
	{
		public int Width { get; private set;}

		public int Size {get; private set;}

		public int Height {get; private set;}

		Tile[,] _tiles;

		public Grid (int width, int height)
		{
			this.Width = width;
			this.Height = height;

			_tiles = new Tile[Width, Height];
			for(int x=0;x<Width;x++){
				for(int y=0;y<Height; y++){
					_tiles [x, y] = new Tile ();
				}
			}

			Size = Width * Height;
		}

		public Tile GetTile (int x, int y)
		{
			return _tiles [x, y];
		}
	}
}

