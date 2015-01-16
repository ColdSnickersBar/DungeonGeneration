using System;
using NUnit.Framework;
using NSubstitute;
using DungeonGeneration;

namespace Tests
{
	[TestFixture]
	public class DungeonGeneratorTests
	{
		IRoomStamper _stamper;

		IRoomSpecGenerator _roomGenerator;

		DungeonGenerator _generator;

		[SetUp]
		public void Setup()
		{
			_stamper = Substitute.For<IRoomStamper> ();
			_roomGenerator = Substitute.For<IRoomSpecGenerator> ();
			_generator = new DungeonGenerator (_stamper, _roomGenerator);
		}
		
		[Test ()]
		public void TestStampsRoomsSpecifiedTimes ()
		{
			var expectedRoom = new RoomSpec {
				x = 1,
				y = 1,
				width = 10, 
				height = 10
			};



			_roomGenerator.getNext ().Returns (expectedRoom);


			_generator.RoomPasses = 100;


			_stamper.DidNotReceive ().Stamp (Arg.Any<RoomSpec> ());

			_generator.Generate ();

			_stamper.Received (100).Stamp (expectedRoom);
		}

		
		[Test ()]
		public void TestStampsRoomsFromRoomSpecGenerator ()
		{
			var expectedRoom = new RoomSpec {
				x = 1,
				y = 1,
				width = 10, 
				height = 10
			};

			_roomGenerator.getNext ().Returns (expectedRoom);
			_generator.RoomPasses = 1;
			_generator.Generate ();

			_stamper.Received (1).Stamp (expectedRoom);

			var otherExpectedRoom = new RoomSpec {
				x = 1,
				y = 1,
				width = 9, 
				height = 9
			};
			_roomGenerator.getNext ().Returns (otherExpectedRoom);
			_generator.Generate ();

			_stamper.Received (1).Stamp (expectedRoom);
			_stamper.Received (1).Stamp (otherExpectedRoom);
		}
	}
}

