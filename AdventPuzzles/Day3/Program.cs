using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var directions = File.ReadAllText("testfile.txt");
			var timesAtLocation = LocationCalc.LocationsVisited(directions);

			Console.WriteLine($"{timesAtLocation.Count} houses received at least one present.");
			Console.ReadKey();

		    var locationsVisited = LocationCalc.LocationsVisitedRobo(directions);

            Console.WriteLine($"{locationsVisited.Count} houses received at least one present.");
            Console.ReadKey();
        }
	}

    public class Pointer
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

	public static class LocationCalc
	{
		public static Dictionary<Tuple<int, int>, int> LocationsVisited(string directions)
        {
            var santa = new Pointer { X = 0, Y = 0 };
            var locationsVisited = new Dictionary<Tuple<int, int>, int>
            {
                {
                    new Tuple<int, int>(0, 0), 1
                }
            }; ;

		    return directions
                .Aggregate(locationsVisited, 
                    (current, direction) => UpdateLocationsVisited(direction, santa, current));
		}

        public static Dictionary<Tuple<int, int>, int> LocationsVisitedRobo(string directions)
        {
            var santa = new Pointer { X = 0, Y = 0 };
            var roboSanta = new Pointer { X = 0, Y = 0 };
            var isDirectionForSanta = true;
            var locationsVisited = new Dictionary<Tuple<int, int>, int>
            {
                {
                    new Tuple<int, int>(0, 0), 1
                }
            };

            foreach (var direction in directions)
            {
                locationsVisited = UpdateLocationsVisited(direction, isDirectionForSanta ? santa : roboSanta, locationsVisited);
                isDirectionForSanta = !isDirectionForSanta;
            }

            return locationsVisited;
        }

	    private static Dictionary<Tuple<int, int>, int> UpdateLocationsVisited(char direction, Pointer pointer, Dictionary<Tuple<int, int>, int> locationsVisited)
	    {
	        switch (direction)
	        {
	            case '^':
	                pointer.Y++;
	                break;
	            case '>':
                    pointer.X++;
	                break;
	            case 'v':
                    pointer.Y--;
	                break;
	            case '<':
                    pointer.X--;
	                break;
	        }
	        if (locationsVisited.ContainsKey(new Tuple<int, int>(pointer.X, pointer.Y)))
	            locationsVisited[new Tuple<int, int>(pointer.X, pointer.Y)]++;
	        else
	            locationsVisited.Add(new Tuple<int, int>(pointer.X, pointer.Y), 1);

            return locationsVisited;
	    }
	}

	[TestFixture]
	public class LocationCalcTests
	{
		[TestCase(">", 2)]
		[TestCase("^>v<", 4)]
		[TestCase("^v^v^v^v^v", 2)]
		public void LocationsVisited_GivenDirections_ReturnsCorrectHousesVisited(string directions, int expectedVisitedHouses)
		{
			var timesAtLocation = LocationCalc.LocationsVisited(directions);

			Assert.That(timesAtLocation.Count, Is.EqualTo(expectedVisitedHouses));
        }

        [TestCase("^v", 3)]
        [TestCase("^>v<", 3)]
        [TestCase("^v^v^v^v^v", 11)]
        public void LocationsVisitedRobo_GivenDirections_ReturnsCorrectHousesVisited(string directions, int expectedVisitedHouses)
        {
            var timesAtLocation = LocationCalc.LocationsVisitedRobo(directions);

            Assert.That(timesAtLocation.Count, Is.EqualTo(expectedVisitedHouses));
        }
    }
}
