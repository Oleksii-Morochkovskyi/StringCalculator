using System;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private readonly string _separator = ",";
        private readonly int _maxNumber = 1000;

        public int Add(string numbers)
        {

            if (numbers.Length == 0) return 0;

            if (numbers.Length == 1) return int.Parse(numbers);

            numbers = numbers.Replace("\n", _separator);

            if (numbers.StartsWith("//")) ChangeSeparatorOnDefault(ref numbers);

            return CalculateSum(numbers);
        }

        public void ChangeSeparatorOnDefault(ref string numbers)
        {
            numbers = numbers.Remove(0, 2);
            if (numbers[0] == '[' && numbers.Contains(']'))
            {
                string tempSeparator;
                bool isMoreSeparators = true;

                while (isMoreSeparators)
                {
                    var leftBracketIndex = numbers.IndexOf('[');
                    var rightBracketIndex = numbers.IndexOf(']');
                    var separatorLength = rightBracketIndex - leftBracketIndex - 1;


                    tempSeparator = numbers.Substring(leftBracketIndex + 1, separatorLength);
                    tempSeparator = CheckOnBracketExceptions(numbers, leftBracketIndex, rightBracketIndex, tempSeparator);

                    numbers = numbers.Remove(leftBracketIndex, tempSeparator.Length + 2);
                    numbers = numbers.Replace(tempSeparator, _separator);

                    if (numbers.IndexOf('[') == -1) break;

                }
            }
            else numbers = numbers.Replace(numbers[0], ',');
        }

        public string CheckOnBracketExceptions(string numbers, int leftBracketIndex, int rightBracketIndex, string separator)
        {

            if (numbers[leftBracketIndex] == numbers[leftBracketIndex + 1])
            {
                separator.Insert(0, "[");
            }
            if (numbers[rightBracketIndex] == numbers[rightBracketIndex + 1])
            {
                separator += ']';
            }
            return separator;
        }

        public int CalculateSum(string numbers)
        {
            int result = 0;
            var separatedNumbers = numbers.Split(_separator, StringSplitOptions.None);

            for (int i = 0; i < separatedNumbers.Length; i++)
            {
                if (separatedNumbers[i] == "") continue;
                
                int currentNumber = int.Parse(separatedNumbers[i]);

                if (currentNumber < 0) ThrowExceptionIfNumberIsNegative(i, separatedNumbers);

                if (currentNumber > _maxNumber) continue;

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
