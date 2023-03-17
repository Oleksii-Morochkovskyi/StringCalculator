using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private const string DefaultSeparator = ",";
        private const int MaxNumber = 1000;
        private List<string> customSeparators = new List<string> { DefaultSeparator };

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            numbers = numbers.Replace(@"\n", DefaultSeparator);

            if (numbers.StartsWith("//"))
            {
                numbers = ExtractCustomSeparator(numbers);
            }
            
            var separators = customSeparators.ToArray();

            var separatedNumbers = numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var convertedNumbers = Array.ConvertAll(separatedNumbers, s => int.Parse(s));

            return CalculateSum(convertedNumbers);
        }

        public string ExtractCustomSeparator(string numbers)
        {
            numbers = numbers.Remove(0, 2);

            if (numbers[0] == '[' && numbers.Contains(']'))
            {
                var leftBracketIndex = numbers.IndexOf('[');
                var rightBracketIndex = numbers.IndexOf(']');

                return GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex);
            }
            else
            {
                return numbers.Replace(numbers[0], ',');
            }
        }

        public string GetCustomSeparators(string numbers, int leftBracketIndex, int rightBracketIndex)
        {
            bool isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(DefaultSeparator);

            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator)
            {
                var substringLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
                var separator = numbers.Substring(leftBracketIndex + 1, substringLength);
                var lengthOfSeparatorAndBrackets = separator.Length + 2;

                customSeparators.Add(separator);
                numbers = numbers.Remove(leftBracketIndex, lengthOfSeparatorAndBrackets);

                if (numbers.IndexOf('[') != -1 || numbers.IndexOf(']') != -1)
                {
                    leftBracketIndex = numbers.IndexOf('[');
                    rightBracketIndex = numbers.IndexOf(']');

                    numbers = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex);
                }
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1)
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                leftBracketIndex = numbers.IndexOf('[');

                numbers = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex);
            }

            return numbers;
        }

        public int CalculateSum(int[] numbers)
        {
            if (numbers.Any(p => p < 0))
            {
                ThrowExceptionIfNumberIsNegative(numbers);
            }

            return numbers.Except(numbers.Where(p => p > MaxNumber)).Sum();
        }

        public void ThrowExceptionIfNumberIsNegative(int[] separatedNumbers)
        {
            string exceptionMessage = "negatives are not allowed: ";
            var negativeNumbers = separatedNumbers.Where(p => p < 0);

            foreach (var number in negativeNumbers)
            {
                exceptionMessage += number;
                exceptionMessage += " ";
            }

            throw new Exception(exceptionMessage);
        }
    }
}
