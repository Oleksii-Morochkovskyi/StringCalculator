using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private const string DefaultSeparator = ",";

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            IList<string> separators = new List<string> { DefaultSeparator, @"\n" };

            var numbers = input;

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

            var separatorsWithNoSlashes = separators.Substring(2); //removes \\ from the string
            var lengthOfSeparatorsWithoutFirstAndLastBracket = separatorsWithNoSlashes.Length-2; //length of separators without 3 symbols from the start and 1 from the end
            var separatorsWithNoStartEndSymbols = separatorsWithNoSlashes.Substring(1, lengthOfSeparatorsWithoutFirstAndLastBracket);
            var customSeparators = separatorsWithNoStartEndSymbols.Split("][");

            return customSeparators;
        }

        private int CalculateSum(IEnumerable<int> numbers)
        {
            var maxNumber = 1000;

            var negativeNumbers = numbers.Where(p => p < 0);

            if (negativeNumbers.Any())
            {
                ThrowExceptionIfNumberIsNegative(negativeNumbers);
            }

            return numbers.Where(p => p <= maxNumber).Sum();
        }

        private void ThrowExceptionIfNumberIsNegative(IEnumerable<int> negativeNumbers)
        {
            var exceptionMessage = "negatives are not allowed: ";

            var message = string.Join(" ", negativeNumbers);

            throw new Exception(exceptionMessage + message);
        }
    }
}
