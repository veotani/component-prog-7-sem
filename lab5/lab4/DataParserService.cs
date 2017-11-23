using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class DataParserService
    {
        public static Tuple<int, int> ProceedButtonClick(string rows, string cols)
        {
            var res = new Tuple<int, int>(int.Parse(rows), int.Parse(cols));
            if (res.Item1 < 1 || res.Item2 < 1)
            {
                throw new Exception("Некорректные данные!");
            }
            return res;
        }
    }
}
