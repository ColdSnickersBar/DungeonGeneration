using System;

namespace DungeonGeneration
{
	public class Tile
	{
		public bool IsOpen {
			get;
			set;
		}

		/// <summary>
		/// Initializes a new copy instance of the <see cref="DungeonGeneration.Tile"/> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public Tile(Tile tile){
			IsOpen = tile.IsOpen;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DungeonGeneration.Tile"/> class.
		/// </summary>
		public Tile ()
		{
		}

		public override bool Equals (object obj)
		{
			if(obj.GetType() != typeof(Tile)){
				return false;
			}

			var tile = (Tile)obj;
			return tile.IsOpen == IsOpen;
		}
	}
}

