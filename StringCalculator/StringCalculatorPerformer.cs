using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private const string DefaultSeparator = ",";
        private const int MaxNumber = 1000;
        private List<string> _customSeparators = new List<string> { DefaultSeparator };

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

            var separators = _customSeparators.ToArray();

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

            _customSeparators.Add(numbers[0].ToString());

            return numbers;

        }

        public string GetCustomSeparators(string numbers, int leftBracketIndex, int rightBracketIndex)
        {
            bool isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(DefaultSeparator);

            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator)
            {
                numbers = AddSeparator(numbers, leftBracketIndex, rightBracketIndex);
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1)
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                leftBracketIndex = numbers.IndexOf('[');

                numbers = GetCustomSeparators(numbers, leftBracketIndex, rightBracketIndex);
            }

            return numbers;
        }

        public string AddSeparator(string numbers, int leftBracketIndex, int rightBracketIndex)
        {
            var substringLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
            var separator = numbers.Substring(leftBracketIndex + 1, substringLength);
            var lengthOfSeparatorAndBrackets = separator.Length + 2;

            _customSeparators.Add(separator);
            numbers = numbers.Remove(leftBracketIndex, lengthOfSeparatorAndBrackets);

            if (numbers.Contains('[') || numbers.Contains(']'))
            {
                leftBracketIndex = numbers.IndexOf('[');
                rightBracketIndex = numbers.IndexOf(']');

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
