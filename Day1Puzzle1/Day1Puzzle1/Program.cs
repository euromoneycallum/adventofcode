using System;
using System.IO;
using System.Linq;

namespace Day1Puzzle1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var testFileText = File.ReadAllText("testfile.txt");
			var numberFloorsUp = testFileText.Count(ch => ch == '(');
			var numberFloorsDown = testFileText.Count(ch => ch == ')');
			var resultingFloor = numberFloorsUp - numberFloorsDown;

			Console.WriteLine($"You've ended up on floor {resultingFloor}.");
			Console.ReadKey();

			var currentPosition = 1;
			var currentFloor = 0;
			foreach (var ch in testFileText)
			{
				if (ch == '(')
					currentFloor++;
				if (ch == ')')
					currentFloor--;

				if (currentFloor == -1)
					break;

				currentPosition++;
			}

			Console.WriteLine($"The position that resulted in first reaching the -1 floor was {currentPosition}");
			Console.ReadKey();
		}
	}
}
