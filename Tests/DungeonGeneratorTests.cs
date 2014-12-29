using System;
using NUnit.Framework;
using NSubstitute;
using DungeonGeneration;

namespace Tests
{
	[TestFixture]
	public class DungeonGeneratorTests
	{

		
		[Test ()]
		public void TestStampsRoomsSpecifiedTimes ()
		{
			var grid = new Grid (100, 100);
			var stamper = Substitute.For<IRoomStamper> ();

			var generator = new DungeonGenerator (stamper);
			generator.RoomPasses = 100;

			stamper.DidNotReceive ().Stamp (Arg.Any<RoomSpec> ());

			generator.Generate ();

			stamper.Received (100).Stamp (Arg.Any<RoomSpec> ());
		}
	}
}

