using System;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        private string _numbers;
        private string separator = ",";
        public int Add(string numbers)
        {
            _numbers = numbers;

            if (_numbers.Length == 0) return 0;

            if (_numbers.Length == 1) return Convert.ToInt32(_numbers);

            _numbers = _numbers.Replace("\n", separator);

            if (_numbers.StartsWith("//")) AddSeparator();

            return CalculateSum();
        }

        public void AddSeparator()
        {
            if (_numbers[2] == '[' && _numbers.Contains(']'))
            {
                string tempSeparator = "";
                bool isMoreSeparators = true;
                _numbers = _numbers.Remove(0, 2);
                              
                while (isMoreSeparators)
                {
                    var LeftBracketIndex = _numbers.IndexOf('[');
                    var RightBracketIndex = _numbers.IndexOf(']');

                    tempSeparator = _numbers.Substring(LeftBracketIndex + 1, RightBracketIndex - LeftBracketIndex - 1);
                    
                    if (_numbers[LeftBracketIndex] == _numbers[LeftBracketIndex + 1])
                    {
                        tempSeparator.Insert(0,"[");
                    }
                    if (_numbers[RightBracketIndex] == _numbers[RightBracketIndex + 1])
                    {
                        tempSeparator += ']';
                    }

                    _numbers = _numbers.Remove(LeftBracketIndex, tempSeparator.Length + 2);
                    _numbers = _numbers.Replace(tempSeparator, separator);
                                     
                    if (_numbers.IndexOf('[') == -1)
                    {
                        isMoreSeparators = false;
                        break;
                    }
                }              
            }
            else
            {
                _numbers = _numbers.Replace(_numbers[2], ','); 
                _numbers = _numbers.Substring(3);
            }
        }
        public int CalculateSum()
        {
            int result = 0;
            var separatedNumbers = _numbers.Split(separator, StringSplitOptions.None);

            for (int i = 0; i < separatedNumbers.Length; i++)
            {
                if (separatedNumbers[i] != "")
                {
                    int currentNumber = Convert.ToInt32(separatedNumbers[i]);

                    if (currentNumber < 0)
                    {
                        throw new Exception(ThrowExceptionIfNumberIsNegative(i, separatedNumbers));
                    }

                    if (currentNumber > 1000) continue;

                    result += currentNumber;
                }
            }
            return result;
        }
        public string ThrowExceptionIfNumberIsNegative(int currentIndex, string[] separatedNumbers)
        {
            string exceptionMessage = "negatives are not allowed: ";
            for (int i = currentIndex; i < separatedNumbers.Length; i++)
            {
                if (separatedNumbers[i] == "") continue;

                int currentNumber = Int32.Parse(separatedNumbers[i]);

                if (currentNumber < 0)
                {
                    exceptionMessage += currentNumber;
                    exceptionMessage += " ";
                }
            }
            return exceptionMessage;
        }
    }
}
