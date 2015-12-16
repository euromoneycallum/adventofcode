using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace Day4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lowestPositiveNumberWithFiveZeros = AdventMiner.AdventMine("yzbqklnj", 5);

            Console.WriteLine($"The lowest positive number with five zeros was: {lowestPositiveNumberWithFiveZeros}");
            Console.ReadKey();

            var lowestPositiveNumberWithSixZeros = AdventMiner.AdventMine("yzbqklnj", 6);

            Console.WriteLine($"The lowest positive number with six zeros was: {lowestPositiveNumberWithSixZeros}");
            Console.ReadKey();
        }
    }

    public static class AdventMiner
    {
        public static int AdventMine(string secretKey, int numberOfZeros)
        {
            var lowestPositiveNumber = 0;
            while (true)
            {
                var hash = 
                    BitConverter.ToString(
                        ((HashAlgorithm) CryptoConfig.CreateFromName("MD5"))
                            .ComputeHash(new UTF8Encoding().GetBytes(secretKey + lowestPositiveNumber)))
                        .Replace("-", string.Empty);

                if (hash.Take(numberOfZeros).All(c => c == '0'))
                {
                    return lowestPositiveNumber;
                }
                lowestPositiveNumber++;
            }
        }
    }

    [TestFixture]
    public class AdventMinerTests
    {
        [TestCase("abcdef", 609043)]
        [TestCase("pqrstuv", 1048970)]
        public void AdventMine_GivenSecretKey_ReturnsLowestCorrectPositiveNumber(string secretKey,
            int expectedLowestPositiveNumber)
        {
            var lowestPositiveNumber = AdventMiner.AdventMine(secretKey, 5);

            Assert.That(lowestPositiveNumber, Is.EqualTo(expectedLowestPositiveNumber));
        }
    }
}
