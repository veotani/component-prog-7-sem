using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab3
{
    public class NumberParser
    {
        public static Tuple<int, int> GetNumberInterval(string str)
        {
            int startIndexOfError;
            int errorLength;
            var pattern = new Regex(@"-?[1-9]\d*\.?\d*");
            if (pattern.IsMatch(str))
            {
                var foundNumber = pattern.Match(str).Value;
                var firstIndexOfNumber = str.IndexOf(foundNumber);
                if (firstIndexOfNumber == 0)
                {
                    startIndexOfError = foundNumber.Length;
                    errorLength = str.Length - startIndexOfError + 1;
                    return new Tuple<int, int>(startIndexOfError, errorLength);
                }
                else
                {
                    startIndexOfError = 0;
                    errorLength = firstIndexOfNumber;
                    return new Tuple<int, int>(startIndexOfError, errorLength);
                }
            }
            else
            {
                startIndexOfError = 0;
                errorLength = str.Length;
                return new Tuple<int, int>(startIndexOfError, errorLength);
            }
        }
    }
}
