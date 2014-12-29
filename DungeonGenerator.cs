using System;

namespace DungeonGeneration
{
	public class DungeonGenerator
	{
		public int RoomPasses {
			get;
			set;
		}

		IRoomStamper _roomStamper;

		public DungeonGenerator (IRoomStamper stamper)
		{
			_roomStamper = stamper;
		}

		public void Generate ()
		{
			if(RoomPasses > 0){
				for(int i=0;i<RoomPasses;i++){

					var room = new RoomSpec {
						x = 1,
						y = 1,
						width = 10, 
						height = 10
					};
					_roomStamper.Stamp (room);
				}
			}
		}
	}
}

