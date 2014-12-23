using System;

namespace DungeonGeneration
{
	public class Grid
	{
		public int Width { get; private set;}

		public int Size {get; private set;}

		public int Height {get; private set;}

		Tile[,] _tiles;

		/// <summary>
		/// Initializes a new copy instance of the <see cref="DungeonGeneration.Grid"/> class, using the 
		/// provided grid to copy.
		/// </summary>
		/// <param name="grid">Grid.</param>
		public Grid(Grid grid){
		
			this.Width = grid.Width;
			this.Height = grid.Height;

			_tiles = new Tile[Width, Height];
			for(int x=0;x<Width;x++){
				for(int y=0;y<Height; y++){
					_tiles [x, y] = new Tile (grid.GetTile(x, y));
				}
			}

			Size = Width * Height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DungeonGeneration.Grid"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
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

		bool HasRowOfState(int x, int y, int length, bool isOpen){
			if(!IsTileOfState (x, y, isOpen)){
				return false;
			}

			for(int i=x;i<length;i++){
				if(!IsTileOfState(i, y, isOpen)){
					return false;
				}
			}
			return true;
		}

		bool IsTileOfState (int x, int y, bool isOpen)
		{
			var tile = GetTile (x, y);
			return (tile.IsOpen == isOpen);
		}

		public bool HasClosedRow(int x, int y, int length){
			return HasRowOfState (x, y, length, false);
		}

		public bool HasClosedColumn(int x, int y, int length){
			return HasColumnOfState (x, y, length, false);
		}


		bool HasColumnOfState(int x, int y, int length, bool isOpen){
			if(!IsTileOfState (x, y, isOpen)){
				return false;
			}

			for(int i=y;i<length;i++){
				if(!IsTileOfState(x, i, isOpen)){
					return false;
				}
			}
			return true;
		}

		public override bool Equals (object obj)
		{
			if(obj.GetType() != typeof(Grid)){
				return false;
			}
			var grid = (Grid)obj;
			if(grid.Size != Size || grid.Width != Width || grid.Height != Height){
				return false;
			}

			for(int y=0;y<grid.Height;y++){
				for(int x=0;x<grid.Width;x++){
					if (!grid.GetTile (x, y).Equals(GetTile (x, y))) {
						return false;
					}
				}
			}

			return true;
		}
	}
}

