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

            IList<string> customSeparators = new List<string>() { DefaultSeparator, @"\n" };

            if (numbers.StartsWith("//"))
            {
                customSeparators = ExtractCustomSeparators(numbers, customSeparators);

                var separatorsDeclarationLength = numbers.IndexOf(@"\n");

                numbers = numbers.Remove(0, separatorsDeclarationLength);
            }

            var separators = customSeparators.ToArray();

            var separatedNumbers = numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var convertedNumbers = separatedNumbers.Select(p => int.Parse(p));

            return CalculateSum(convertedNumbers);
        }

        private IList<string> ExtractCustomSeparators(string numbers, IList<string> separators)
        {
            if (numbers[2] == '[' && numbers.Contains(']')) //custom separator in *numbers* starts from index = 2
            {
                var leftBracketIndex = numbers.IndexOf('[');
                var rightBracketIndex = numbers.IndexOf(']');

                separators = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            separators.Add(numbers[2].ToString()); //custom separator in *numbers* starts from index = 2

            return separators;
        }

        private IList<string> GetCustomSeparators(string numbers, int leftBracketIndex, int rightBracketIndex, IList<string> separators)
        {
            var isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(DefaultSeparator);
            var isNewLineSeparator = numbers.Substring(rightBracketIndex + 1, 2) == @"\n";

            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator || isNewLineSeparator)
            {
                separators = AddSeparators(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1) //rightBracketIndex + 1 - start from next element
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                
                separators = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return separators;
        }

        private IList<string> AddSeparators(string numbers, int leftBracketIndex, int rightBracketIndex, IList<string> separators)
        {
            var separatorLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
            var separator = numbers.Substring(leftBracketIndex + 1, separatorLength);

            separators.Add(separator);

            if (numbers.IndexOf('[', rightBracketIndex + 1) != -1 || numbers.IndexOf('[', rightBracketIndex + 1) != -1)
            {
                leftBracketIndex = numbers.IndexOf('[', rightBracketIndex + 1);
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);

                separators = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return separators;
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
