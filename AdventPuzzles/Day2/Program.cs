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
			var measurements = File.ReadLines("testfile.txt").ToList();

			var totalSquareFeet = PresentCalc.TotalSquareFeetWrappingPaper(measurements);

			Console.WriteLine($"The total amount of square feet of wrapping paper required is: {totalSquareFeet}");
			Console.ReadKey();

			var totalRibbonLength = PresentCalc.TotalLengthRibbon(measurements);

			Console.WriteLine($"The total length of ribbon required is: {totalRibbonLength}");
			Console.ReadKey();
		}
	}

	public static class PresentCalc
	{
		public static int TotalSquareFeetWrappingPaper(IEnumerable<string> measurements)
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

		public static int TotalLengthRibbon(IEnumerable<string> measurements)
		{
			var totalRibbonLength = 0;
			foreach (var boxMeasurements in measurements)
			{
				var sideLengths = boxMeasurements.Split('x').Select(int.Parse).ToList();

				var smallestMeasurement = sideLengths.Min();
				var secondSmallestMeasurement =
					sideLengths.Count(m => m == smallestMeasurement) > 1
						? smallestMeasurement
						: sideLengths.Where(m => m != smallestMeasurement).Min();
				var largestMeasurement = sideLengths.Max();

				totalRibbonLength += smallestMeasurement*2 + secondSmallestMeasurement*2;
				totalRibbonLength += smallestMeasurement*secondSmallestMeasurement*largestMeasurement;
			}
			return totalRibbonLength;
		}
	}

	[TestFixture]
	public class PresentCalcTests
	{
		[TestCase("2x3x4", 58)]
		[TestCase("1x1x10", 43)]
		public void TotalSquareFeetWrappingPaper_GivenSingleMeasurement_ReturnsCorrectTotal(string boxMeasurement, int expectedTotalSquareFeet)
		{
			var measurements = new List<string>() { boxMeasurement };

			var totalSquareFeet = PresentCalc.TotalSquareFeetWrappingPaper(measurements);

			Assert.That(totalSquareFeet, Is.EqualTo(expectedTotalSquareFeet));
		}

		[TestCase("2x3x4", 34)]
		[TestCase("1x1x10", 14)]
		public void TotalLengthRibbon_GivenSingleMeasurement_ReturnsCorrectTotal(string boxMeasurement, int expectedTotalLength)
		{
			var measurements = new List<string>() { boxMeasurement };

			var totalRibbonLength = PresentCalc.TotalLengthRibbon(measurements);

			Assert.That(totalRibbonLength, Is.EqualTo(expectedTotalLength));
		}
	}
}
