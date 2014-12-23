using System;
using NUnit;
using NUnit.Framework;
using NSubstitute;
using DungeonGeneration;

namespace Grids
{
	[TestFixture]
	public class GridTests
	{
		const int kWidth = 100;
		const int kHeight = 100;

		Grid _grid;

		[SetUp]
		public void SetUp ()
		{

			_grid = new Grid (kWidth, kHeight);
		}
		
		[Test ()]
		public void TestGridHasSize ()
		{
			Assert.AreEqual (kWidth * kHeight, _grid.Size);
			Assert.AreEqual (kWidth, _grid.Width);
			Assert.AreEqual (kHeight, _grid.Height);
		}

		
		[Test ()]
		public void TestHasTiles ()
		{

			var tile = _grid.GetTile (1, 1);

			Assert.IsNotNull (tile);
		}

		
		[Test ()]
		public void TestHasDifferentTiles ()
		{
			var tile = _grid.GetTile (1, 1);

			Assert.IsNotNull (tile);

			var sameTime = _grid.GetTile (1, 1);

			Assert.AreSame (tile, sameTime);

			var otherTile = _grid.GetTile (5, 5);

			Assert.AreNotSame (tile, otherTile);
		}
	}
}

