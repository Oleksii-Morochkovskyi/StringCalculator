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

            numbers = numbers.Replace(@"\n", DefaultSeparator);

            var customSeparators = new List<string>() {DefaultSeparator};

            if (numbers.StartsWith("//"))
            {
                (numbers, customSeparators) = ExtractCustomSeparators(numbers, customSeparators);
            }

            var separators = customSeparators.ToArray();

            var separatedNumbers = numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var convertedNumbers = Array.ConvertAll(separatedNumbers, s => int.Parse(s));

            return CalculateSum(convertedNumbers);
        }

        public (string, List<string>) ExtractCustomSeparators(string numbers, List<string> separators)
        {

            numbers = numbers.Remove(0, 2); //removes // from string

            if (numbers[0] == '[' && numbers.Contains(']'))
            {
                var leftBracketIndex = numbers.IndexOf('[');
                var rightBracketIndex = numbers.IndexOf(']');

                (numbers, separators) = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            separators.Add(numbers[0].ToString());

            return (numbers, separators);
        }

        public (string, List<string>) GetCustomSeparator(string numbers, int leftBracketIndex, int rightBracketIndex, List<string> separators)
        {
            var isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(DefaultSeparator);

            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator)
            {
                (numbers, separators) = AddSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1) //rightBracketIndex + 1 - start from next element
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                leftBracketIndex = numbers.IndexOf('[');

                (numbers, separators) = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return (numbers, separators);
        }

        public (string, List<string>) AddSeparator(string numbers, int leftBracketIndex, int rightBracketIndex, List<string> separators)
        {
            var substringLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
            var separator = numbers.Substring(leftBracketIndex + 1, substringLength);
            var lengthOfSeparatorAndBrackets = separator.Length + 2;

            numbers = numbers.Remove(leftBracketIndex, lengthOfSeparatorAndBrackets);
            separators.Add(separator);

            if (numbers.Contains('[') || numbers.Contains(']'))
            {
                leftBracketIndex = numbers.IndexOf('[');
                rightBracketIndex = numbers.IndexOf(']');

                (numbers, separators) = GetCustomSeparator(numbers, leftBracketIndex, rightBracketIndex, separators);
            }

            return (numbers, separators);
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
