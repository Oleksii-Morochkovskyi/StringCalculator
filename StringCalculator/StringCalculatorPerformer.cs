using System;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private readonly string _defaultSeparator = ",";
        private readonly int _maxNumber = 1000;

        public int Add(string numbers)
        {
            if (numbers.Length == 0) return 0;

            if (numbers.Length == 1) return int.Parse(numbers);

            numbers = numbers.Replace(@"\n", _defaultSeparator);

            if (numbers.StartsWith("//")) numbers = ReplaceCustomSeparatorsByDefaultOne(numbers);

            return CalculateSum(numbers);
        }

        public string ReplaceCustomSeparatorsByDefaultOne(string numbers)
        {
            numbers = numbers.Remove(0, 2);

            if (numbers[0] == '[' && numbers.Contains(']'))
            {
                return ProcessMultipleSeparators(numbers);
            }
            else
            {
                return numbers.Replace(numbers[0], ',');
            }
        }

        public string ProcessMultipleSeparators(string numbers)
        {
            var leftBracketIndex = numbers.IndexOf('[');
            var rightBracketIndex = numbers.IndexOf(']');

            numbers = ReplaceBracketRepetitions(numbers, leftBracketIndex, rightBracketIndex);

            return numbers;
        }

        public string ReplaceBracketRepetitions(string numbers, int leftBracketIndex, int rightBracketIndex)
        {
            bool isDefaultSeparator = numbers[rightBracketIndex + 1] == char.Parse(_defaultSeparator);
            
            if (numbers[rightBracketIndex + 1] == '[' || isDefaultSeparator)
            {
                var substringLength = numbers.IndexOf(']', rightBracketIndex) - leftBracketIndex - 1;
                var separator = numbers.Substring(leftBracketIndex + 1, substringLength);
                var lengthOfSeparatorAndBrackets = separator.Length + 2;

                numbers = numbers.Remove(leftBracketIndex, lengthOfSeparatorAndBrackets);
                numbers = numbers.Replace(separator, _defaultSeparator);

                if (numbers.IndexOf('[') != -1 || numbers.IndexOf(']') != -1)
                {
                    leftBracketIndex = numbers.IndexOf('[');
                    rightBracketIndex = numbers.IndexOf(']');

                    numbers = ReplaceBracketRepetitions(numbers, leftBracketIndex, rightBracketIndex);
                }
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) != -1)
            {
                rightBracketIndex = numbers.IndexOf(']', rightBracketIndex + 1);
                leftBracketIndex = numbers.IndexOf('[');

                numbers = ReplaceBracketRepetitions(numbers, leftBracketIndex, rightBracketIndex);
            }

            return numbers;
        }

        public int CalculateSum(string numbers)
        {
            int result = 0;
            var separatedNumbers = numbers.Split(_defaultSeparator, StringSplitOptions.None);

            for (int i = 0; i < separatedNumbers.Length; i++)
            {
                if (separatedNumbers[i] == "" || int.Parse(separatedNumbers[i]) > _maxNumber) continue;

                int currentNumber = int.Parse(separatedNumbers[i]);

                if (currentNumber < 0) ThrowExceptionIfNumberIsNegative(i, separatedNumbers);

                result += currentNumber;
            }

            return result;
        }

        public string ThrowExceptionIfNumberIsNegative(int currentIndex, string[] separatedNumbers)
        {
            string exceptionMessage = "negatives are not allowed: ";

            for (int i = currentIndex; i < separatedNumbers.Length; i++)
            {
                if (separatedNumbers[i] == "") continue;

                int currentNumber = int.Parse(separatedNumbers[i]);

                if (currentNumber < 0)
                {
                    exceptionMessage += currentNumber;
                    exceptionMessage += " ";
                }
            }

            throw new Exception(exceptionMessage);
        }
    }
}
