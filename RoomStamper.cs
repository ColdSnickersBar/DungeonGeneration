using System;

namespace DungeonGeneration
{
	public class RoomStamper
	{
		Grid _grid;

		public RoomStamper (Grid grid)
		{
			_grid = grid;
		}

		public void Stamp (RoomSpec room)
		{
			if (ShouldStampRoom (room)) {
				StampRoom (room);
			}
		}

		void StampRoom (RoomSpec room)
		{
			for (int iy = room.y; iy < room.height+room.y; iy++) {
				StampRow (room.x, iy, room.width);
			}
		}

		bool ShouldStampRoom (RoomSpec room)
		{
			return !IsOnGridBorder (room) && !HasOpenSpaceInRoom(room) && !IsAdjacentToOpenSpace(room);
		}

		bool HasOpenSpaceInRoom(RoomSpec room)
		{
			for(int y=room.y;y<room.y + room.height;y++){
				for(int x=room.x;x<room.x+room.width;x++){
					if(_grid.GetTile(x, y).IsOpen){
						return true;
					}
				}
			}
			return false;
		}

		bool IsOnGridBorder (RoomSpec room)
		{
			return IsRoomOnLeftOrTop (room) || IsRoomOnRight (room) || IsRoomOnBottom (room);
		}

		bool IsRoomOnLeftOrTop (RoomSpec room)
		{
			return room.x < 1 || room.y < 1;
		}

		bool IsRoomOnRight(RoomSpec room)
		{
			return room.x + room.width >= _grid.Width;
		}

		bool IsRoomOnBottom(RoomSpec room)
		{
			return room.y + room.height >= _grid.Height;
		}

		bool IsAdjacentToOpenSpace(RoomSpec room)
		{
			return !HasClosedTop (room) || !HasClosedBottom (room) || !HasClosedLeft (room) || !HasClosedRight (room);
		}

		bool HasClosedTop (RoomSpec room)
		{
			return _grid.HasClosedRow (room.x - 1, room.y - 1, room.width + 2);
		}

		bool HasClosedBottom (RoomSpec room)
		{
			return _grid.HasClosedRow (room.x - 1, room.height + room.y + 1, room.width + 2);
		}

		bool HasClosedLeft (RoomSpec room)
		{
			return _grid.HasClosedColumn (room.x - 1, room.y - 1, room.height + 2);
		}

		bool HasClosedRight (RoomSpec room)
		{
			return _grid.HasClosedColumn (room.width + room.x + 1, room.y - 1, room.height + 2);
		}

		void StampRow (int x, int y, int width)
		{
			for (int ix = x; ix < width+x; ix++) {
				var tile = _grid.GetTile (ix, y);
				tile.IsOpen = true;
			}
		}

		public struct RoomSpec
		{
			public int x;
			public int y;
			public int width;
			public int height;
		}
	}
}

