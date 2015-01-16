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
		IRoomSpecGenerator _roomSpecGenerator;

		public DungeonGenerator (IRoomStamper stamper, IRoomSpecGenerator roomSpecGenerator)
		{
			_roomStamper = stamper;
			_roomSpecGenerator = roomSpecGenerator;
		}

		public void Generate ()
		{
			if(RoomPasses > 0){
				for(int i=0;i<RoomPasses;i++){

					_roomStamper.Stamp (_roomSpecGenerator.getNext());
				}
			}
		}
	}
}

