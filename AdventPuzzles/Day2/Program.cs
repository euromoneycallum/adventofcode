using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var totalSquareFeet = WrappingPaperCalc.TotalSquareFeet(File.ReadLines("testfile.txt"));

			Console.WriteLine($"The total amount of square feet of wrapping paper required is: {totalSquareFeet}");
			Console.ReadKey();
		}
	}

	public static class WrappingPaperCalc
	{
		public static int TotalSquareFeet(IEnumerable<string> measurements)
		{
			var totalSquareFeet = 0;
			foreach (var boxMeasurements in measurements)
			{
				var sideLengths = boxMeasurements.Split('x').Select(int.Parse).ToList();

				var smallestMeasurement = sideLengths.Min();
				var secondSmallestMeasurement =
					sideLengths.Count(m => m == smallestMeasurement) > 1
						? smallestMeasurement
						: sideLengths.Where(m => m != smallestMeasurement).Min();
				var largestMeasurement = sideLengths.Max();

				totalSquareFeet += smallestMeasurement * secondSmallestMeasurement;
				totalSquareFeet += 2 * smallestMeasurement * secondSmallestMeasurement;
				totalSquareFeet += 2 * smallestMeasurement * largestMeasurement;
				totalSquareFeet += 2 * secondSmallestMeasurement * largestMeasurement;
			}
			return totalSquareFeet;
		}
	}

	[TestFixture]
	public class WrappingPaperCalcTests
	{
		[TestCase("2x3x4", 58)]
		[TestCase("1x1x10", 43)]
		public void TotalSquareFeet_GivenSingleMeasurement_ReturnsCorrectTotal(string boxMeasurement, int expectedTotalSquareFeet)
		{
			var measurements = new List<string>() { boxMeasurement };

			var totalSquareFeet = WrappingPaperCalc.TotalSquareFeet(measurements);

			Assert.That(totalSquareFeet, Is.EqualTo(expectedTotalSquareFeet));
		}
	}
}
