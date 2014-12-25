using System;

namespace DungeonGeneration
{
	public interface IRoomStamper
	{
		void Stamp (RoomSpec room);
	}

	public struct RoomSpec
	{
		public int x;
		public int y;
		public int width;
		public int height;
	}
}

