using System;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculatorWorker
    {
        public int Add(string numbers)
        {
            var separators = new string[] { ",", "\\n", "\n" };
            int result = 0;

            if (numbers.Length == 0)
            {
                return result;
            }


            if (numbers.Length == 1)
            {
                result += Convert.ToInt32(new string(numbers.FirstOrDefault(), 1));
                return result;
            }


            if (numbers.StartsWith("//"))
            {

                if (numbers[2] == '[' && numbers.Contains(']'))
                {
                    string tempSeparator = "";
                    bool isSeparator = false;
                    foreach (var el in numbers)
                    {
                        if (el == '[')
                        {
                            isSeparator = true;
                            continue;
                        }
                        else if (el == ']')
                        {
                            isSeparator = false;
                            Array.Resize(ref separators, separators.Length + 1);
                            separators[separators.Length - 1] = tempSeparator;
                            tempSeparator = "";
                            continue;
                        }
                        else if (isSeparator)
                        {
                            tempSeparator += el;
                        }
                    }
                    numbers = numbers.Substring(numbers.LastIndexOf(']') + 1);
                }
                else
                {
                    Array.Resize(ref separators, separators.Length + 1);
                    separators[separators.Length - 1] = Convert.ToString(numbers[2]);
                    numbers = numbers.Substring(3);
                }
            }

            var array = numbers.Split(separators, StringSplitOptions.None);
            bool isNegative = false;
            string warning = "negatives not allowed: ";

            foreach (var el in array)
            {
                if (el != "")
                {
                    int num = Convert.ToInt32(el);

                    if (num < 0)
                    {
                        warning += num;
                        warning += " ";
                        isNegative = true;
                        continue;
                    }
                    else
                    {
                        if (num > 1000) continue;
                    }

                    result += num;

                }
            }
            if (isNegative)
            {
                throw new Exception(warning);
            }
            return result;
        }
    }
}
