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

            var customSeparators = new List<string>() { DefaultSeparator, @"\n" };

            if (numbers.StartsWith("//"))
            {
                customSeparators = ExtractCustomSeparators(numbers, customSeparators);

                var substringLength = numbers.IndexOf(@"\n");

                numbers = numbers.Remove(0, substringLength);
            }

            var separators = customSeparators.ToArray();

            var separatedNumbers = numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var convertedNumbers = Array.ConvertAll(separatedNumbers, s => int.Parse(s));

            return CalculateSum(convertedNumbers);
        }

        public List<string> ExtractCustomSeparators(string numbers, List<string> separators)
        {
            if (numbers[2] == '[' && numbers.Contains(']'))
            {
                var leftBracketIndex = numbers.IndexOf('[');
                var rightBracketIndex = numbers.IndexOf(']');

                separators = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            separators.Add(numbers[2].ToString()); //custom separator in numbers starts from index = 2

            return separators;
        }

        public List<string> GetCustomSeparator(string numbers, int leftBracketIndex, int rightBracketIndex, List<string> separators)
        {
            var isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(DefaultSeparator);
            var isNewLineSeparator = numbers.Substring(rightBracketIndex + 1, 2) == @"\n";

            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator || isNewLineSeparator)
            {
                separators = AddSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1) //rightBracketIndex + 1 - start from next element
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                separators = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return separators;
        }

        public List<string> AddSeparator(string numbers, int leftBracketIndex, int rightBracketIndex, List<string> separators)
        {
            var substringLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
            var separator = numbers.Substring(leftBracketIndex + 1, substringLength);

            separators.Add(separator);

            if (numbers.IndexOf('[', rightBracketIndex + 1) != -1 || numbers.IndexOf('[', rightBracketIndex + 1) != -1)
            {
                leftBracketIndex = numbers.IndexOf('[', rightBracketIndex + 1);
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);

                separators = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return separators;
        }

        public int CalculateSum(int[] numbers)
        {
            var maxNumber = 1000;

            var negatives = numbers.Where(p => p < 0);

            if (negatives.Any())
            {
                ThrowExceptionIfNumberIsNegative(numbers);
            }

            return numbers.Where(p => p <= maxNumber).Sum();
        }

        public void ThrowExceptionIfNumberIsNegative(int[] negatives)
        {
            var exceptionMessage = "negatives are not allowed: ";

            var message = string.Join(" ", negatives);

            throw new Exception(exceptionMessage + message);
        }
    }
}
