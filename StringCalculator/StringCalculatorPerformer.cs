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

            if (numbers.StartsWith("//")) numbers = ChangeSeparatorsOnDefault(numbers);

            return CalculateSum(numbers);
        }

        public string ChangeSeparatorsOnDefault(string numbers)
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
            string tempSeparator;

            while (true)
            {
                var leftBracketIndex = numbers.IndexOf('[');
                var rightBracketIndex = numbers.IndexOf(']');
                var separatorLength = rightBracketIndex - leftBracketIndex - 1;

                tempSeparator = numbers.Substring(leftBracketIndex + 1, separatorLength);
                tempSeparator = CheckOnBracketRepetitions(numbers, leftBracketIndex, rightBracketIndex, tempSeparator);

                var lengthOfSeparatorAndBrackets = tempSeparator.Length + 2;

                numbers = numbers.Remove(leftBracketIndex, lengthOfSeparatorAndBrackets);
                numbers = numbers.Replace(tempSeparator, _defaultSeparator);

                if (!numbers.Contains('[') && !numbers.Contains(']')) break;
            }

            return numbers;
        }

        public string CheckOnBracketRepetitions(string numbers, int leftBracketIndex, int rightBracketIndex, string separator)
        {
            if (numbers[leftBracketIndex] == numbers[leftBracketIndex + 1])
            {
                return separator.Insert(0, "[");
            }

            if (numbers[rightBracketIndex] == numbers[rightBracketIndex + 1])
            {
                return separator += ']';
            }

            if (numbers.IndexOf('[', leftBracketIndex + 1) == -1 && numbers.IndexOf(']', rightBracketIndex + 1) != -1)
            {
                var substringLength = numbers.IndexOf(',') - rightBracketIndex - 1;

                return separator += numbers.Substring(rightBracketIndex, substringLength);
            }

            if (numbers.IndexOf(']', rightBracketIndex + 1) < numbers.IndexOf('[', rightBracketIndex + 1))
            {
                var substringLength = numbers.IndexOf('[', leftBracketIndex + 1) - rightBracketIndex - 1;
                var separatorLength = numbers.LastIndexOf(']', rightBracketIndex + 1, substringLength);

                return separator += numbers.Substring(rightBracketIndex, separatorLength);
            }

            return separator;
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
