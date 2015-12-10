using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Day3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var directions = File.ReadAllText("testfile.txt");
			var timesAtLocation = LocationCalc.TimesAtLocation(directions);

			Console.WriteLine($"{timesAtLocation.Count} houses received at least one present.");
			Console.ReadKey();
		}
	}

	public static class LocationCalc
	{
		public static Dictionary<Tuple<int, int>, int> TimesAtLocation(string directions)
		{
			int x = 0, y = 0;
			var timesAtLocation = new Dictionary<Tuple<int, int>, int>();
			timesAtLocation.Add(new Tuple<int, int>(x, y), 1);

			foreach (var direction in directions)
			{
				switch (direction)
				{
					case '^':
						y++;
						break;
					case '>':
						x++;
						break;
					case 'v':
						y--;
						break;
					case '<':
						x--;
						break;
				}
				if (timesAtLocation.ContainsKey(new Tuple<int, int>(x, y)))
					timesAtLocation[new Tuple<int, int>(x, y)]++;
				else
					timesAtLocation.Add(new Tuple<int, int>(x, y), 1);
			}
			return timesAtLocation;
		}
	}

	[TestFixture]
	public class LocationCalcTests
	{
		[TestCase(">", 2)]
		[TestCase("^>v<", 4)]
		[TestCase("^v^v^v^v^v", 2)]
		public void TimesAtLocation_GivenDirections_ReturnsCorrectHousesVisited(string directions, int expectedVisitedHouses)
		{
			var timesAtLocation = LocationCalc.TimesAtLocation(directions);

			Assert.That(timesAtLocation.Count, Is.EqualTo(expectedVisitedHouses));
		}
	}
}
