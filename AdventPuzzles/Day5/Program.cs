using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var words = File.ReadLines("testfile.txt");

            var numberOfNiceWords = words.Count(NaughtyOrNiceCalc.IsNice);

            Console.WriteLine($"The number of nice words is: {numberOfNiceWords}");
            Console.ReadKey();
        }
    }

    public static class NaughtyOrNiceCalc
    {
        private static readonly HashSet<char> Vowels = new HashSet<char> {'a', 'e', 'i', 'o', 'u'};
        private static readonly string[] BadStrings = {"ab", "cd", "pq", "xy"};

        public static bool IsNice(string word)
        {
            return VowelCounter(word) >= 3 && DoubleLetterInRow(word) && !ContainsBadStrings(word);
        }

        private static int VowelCounter(string word)
        {
            return word.Count(character => Vowels.Contains(character));
        }

        private static bool DoubleLetterInRow(string word)
        {
            var prevCharacter = word[0];
            foreach (var character in word.Skip(1))
            {
                if (prevCharacter == character)
                    return true;
                prevCharacter = character;
            }
            return false;
        }

        private static bool ContainsBadStrings(string word)
        {
            return BadStrings.Any(word.Contains);
        }
    }

    [TestFixture]
    public class NaughtyOrNiceCalcTests
    {
        [TestCase("ugknbfddgicrmopn", true)]
        [TestCase("aaa", true)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void IsNice_GivenString_ReturnsTrueIfNice(string word, bool expectedIsNice)
        {
            var isNice = NaughtyOrNiceCalc.IsNice(word);

            Assert.That(isNice, Is.EqualTo(expectedIsNice));
        }
    }
}
