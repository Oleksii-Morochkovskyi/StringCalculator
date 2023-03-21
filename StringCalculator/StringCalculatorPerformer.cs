using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private const string DefaultSeparator = ",";

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            IList<string> separators = new List<string> { DefaultSeparator, @"\n" };

            if (numbers.StartsWith("//"))
            {
                var separatedData = numbers.Split(@"\n");

                separators = ExtractCustomSeparators(separatedData[0], separators); //separatedData[0] - delimiters separated from numbers

                numbers = separatedData[1]; //numbers separated from delimiters
            }

            var extractedNumbers = ExtractNumbers(numbers, separators);

            return CalculateSum(extractedNumbers);
        }

        private IList<int> ExtractNumbers(string numbers, IList<string> customSeparators)
        {
            var separators = customSeparators.ToArray();

            var separatedNumbers = numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var convertedNumbers = separatedNumbers.Select(p => int.Parse(p)).ToList();

            return convertedNumbers;
        }

        private IList<string> ExtractCustomSeparators(string separators, IList<string> defaultSeparators)
        {
            if (separators[2] != '[')
            {
                defaultSeparators.Add(separators[2].ToString()); //if custom separator is one-character, it is located at separators[2] 

                return defaultSeparators;
            }
           
            var lengthOfSeparators = separators.Length - 4; //length of separators without 3 symbols from the start and 1 from the end

            var separatorsWithNoStartEndSymbols = separators.Substring(3, lengthOfSeparators);

            var customSeparators = separatorsWithNoStartEndSymbols.Split("][");

            var allSeparators = customSeparators.Union(defaultSeparators).ToList();

            return allSeparators;
        }

        private int CalculateSum(IEnumerable<int> numbers)
        {
            var maxNumber = 1000;

            var negatives = numbers.Where(p => p < 0);

            if (negatives.Any())
            {
                ThrowExceptionIfNumberIsNegative(negatives);
            }

            return numbers.Where(p => p <= maxNumber).Sum();
        }

        private void ThrowExceptionIfNumberIsNegative(IEnumerable<int> negatives)
        {
            var exceptionMessage = "negatives are not allowed: ";

            var message = string.Join(" ", negatives);

            throw new Exception(exceptionMessage + message);
        }
    }
}
