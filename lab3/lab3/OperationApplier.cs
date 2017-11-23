using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class OperationApplier
    {
        public static double ApplyOperation(double a, double b, string operation)
        {
            switch (operation)
            {
                case "Сумма":
                    return a + b;
                case "Разность":
                    return a - b;
                case "Произведение":
                    return a * b;
                case "Частное":
                    if (b == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return a / b;
                case "Остаток от деления":
                    if (b == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return a % b;
                case "Абсолютная величина":
                    return Math.Abs(a);
                case "Arccos":
                    if (a > 1 || a < -1)
                    {
                        throw new Exception("Аргументом арк функций может быть число от -1 до 1");
                    }
                    return Math.Acos(a);
                case "Arctg":
                    if (a > 1 || a < -1)
                    {
                        throw new Exception("Аргументом арк функций может быть число от -1 до 1");
                    }
                    return Math.Atan(a);
                case "Cos":
                    return Math.Cos(a);
                case "Tg":
                    return Math.Tan(a);
                case "Exp":
                    return Math.Exp(a);
                case "pi":
                    return Math.PI;
                case "log10":
                    if (a <= 0)
                    {
                        throw new Exception("Аргумент логарифма может быть только положительным числом");
                    }
                    return Math.Log10(a);
                case "logb":
                    if (a <= 0)
                    {
                        throw new Exception("Аргумент логарифма может быть только положительным числом");
                    }
                    if (b == 1 || b <= 0)
                    {
                        throw new Exception("Основание логарифма не может быть равной 1 или быть неположительным");
                    }
                    return Math.Log(a, b);
                case "Округление":
                    return Math.Round(a);
                case "sqrt":
                    if (a < 0)
                    {
                        throw new Exception("С комплексными числами мы ещё не работаем!");
                    }
                    return Math.Sqrt(a);
            }
            return 0;
        }
}
}
