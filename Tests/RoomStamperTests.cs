using System;
using NUnit.Framework;
using NSubstitute;
using DungeonGeneration;

namespace Grids
{
	[TestFixture]
	public class RoomStamperTests
	{
		Grid _grid;

		RoomStamper _stamper;

		[SetUp]
		public void Setup(){
			_grid = new Grid (20, 20);

			_stamper = new RoomStamper (_grid);
		}

		
		[Test ()]
		public void TestStampsRoom ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = 1,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasOpenRoom (_grid, room));
		}
		
		[Test ()]
		public void TestStampedRoomIsSurroundedByClosedTiles ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = 1,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			AssertRoomHasClosedBorders (room);
		}

		void AssertRoomHasClosedBorders (RoomStamper.RoomSpec room)
		{
			Assert.IsTrue (HasClosedRow (_grid, room.x - 1, room.y - 1, room.width + 2));
			Assert.IsTrue (HasClosedRow (_grid, room.x - 1, room.height + room.y + 1, room.width + 2));
			Assert.IsTrue (HasClosedColumn (_grid, room.x - 1, room.y - 1, room.height + 2));
			Assert.IsTrue (HasClosedColumn (_grid, room.width + room.x + 1, room.y - 1, room.height + 2));
		}
		
		[Test ()]
		public void TestWontStampIfStartTileIsAlongLeftBoundary ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = 0,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasClosedGrid(_grid));
		}

		
		[Test ()]
		public void TestWontStampIfStartTileIsAlongTopBoundary ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = 1,
				y = 0,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasClosedGrid(_grid));
		}

		
		[Test ()]
		public void TestWontStampIfRoomClipsToRight ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = _grid.Width-5,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasClosedGrid (_grid));
		}

		
		[Test ()]
		public void TestWontStampIfRoomClipsToBottom ()
		{
			RoomStamper.RoomSpec room = new RoomStamper.RoomSpec {
				x = 1,
				y = _grid.Height - 5,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasClosedGrid (_grid));
		}

		
		[Test ()]
		public void TestCanStampMultipleRooms ()
		{
			var room = new RoomStamper.RoomSpec {
				x = 1,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			Assert.IsTrue (HasOpenRoom (_grid, room));

			room = new RoomStamper.RoomSpec {
				x = 7,
				y = 1,
				width = 5,
				height = 5
			};

			_stamper.Stamp (room);

			Assert.IsTrue (HasOpenRoom (_grid, room));
		}

		
		[Test ()]
		public void TestWontStampOnOpenSpace ()
		{
			var room = new RoomStamper.RoomSpec {
				x = 1,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			var copyGrid = new Grid (_grid);

			room = new RoomStamper.RoomSpec {
				x = 3,
				y = 1,
				width = 5,
				height = 5
			};

			_stamper.Stamp (room);

			Assert.IsTrue (_grid.Equals(copyGrid));
		}

		
		[Test ()]
		public void TestWontStampAdjacentToOpenSpace ()
		{
			var room = new RoomStamper.RoomSpec {
				x = 1,
				y = 1,
				width = 5,
				height = 5
			};
			_stamper.Stamp (room);

			var copyGrid = new Grid (_grid);

			room = new RoomStamper.RoomSpec {
				x = 6,
				y = 1,
				width = 5,
				height = 5
			};

			_stamper.Stamp (room);

			Assert.IsTrue (_grid.Equals(copyGrid));
		}

		bool HasClosedGrid(Grid grid)
		{
			for(int i=0;i<grid.Height;i++){
				if(!HasClosedRow(grid, 0, i, grid.Width)){
					return false;
				}
			}
			return true;
		}

		bool HasOpenRoom(Grid grid, RoomStamper.RoomSpec room){
			for(int i=room.y;i<room.height;i++){
				if(!HasOpenRow(grid, room.x, i, room.width)){
					return false;
				}
			}
			return true;
		}

		bool HasClosedRoom(Grid grid, RoomStamper.RoomSpec room){
			for(int i=room.y;i<room.height;i++){
				if(!HasClosedRow(grid, room.x, i, room.width)){
					return false;
				}
			}
			return true;
		}

		bool HasOpenRow(Grid grid, int x, int y, int length){

			return HasRowOfState (grid, x, y, length, true);
		}

		bool HasRowOfState(Grid grid, int x, int y, int length, bool isOpen){
			if(!TileIsOfState (grid, x, y, isOpen)){
				return false;
			}

			for(int i=x;i<length;i++){
				if(!TileIsOfState(grid, i, y, isOpen)){
					return false;
				}
			}
			return true;
		}

		bool HasClosedRow(Grid grid, int x, int y, int length){
			return HasRowOfState (grid, x, y, length, false);
		}

		bool TileIsOfState (Grid grid, int x, int y, bool isOpen)
		{
			var tile = grid.GetTile (x, y);
			return (tile.IsOpen == isOpen);
		}

		bool HasClosedColumn(Grid grid, int x, int y, int length){
			return HasColumnOfState (grid, x, y, length, false);
		}


		bool HasColumnOfState(Grid grid, int x, int y, int length, bool isOpen){
			if(!TileIsOfState (grid, x, y, isOpen)){
				return false;
			}

			for(int i=y;i<length;i++){
				if(!TileIsOfState(grid, x, i, isOpen)){
					return false;
				}
			}
			return true;
		}
	}
}

