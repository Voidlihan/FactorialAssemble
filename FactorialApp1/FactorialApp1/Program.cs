using System;

namespace FactorialApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens = textBoxFactorial.Text.Split(',');
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (int.TryParse(tokens[i], out var num))
                {
                    list.Add(num);
                }
            }
            string factorials = "";
            for (int i = 0; i < list.Count; i++)
            {
                factorials += FactorialFunc(list[i]).ToString() + "\n";
            }
        }
        static int FactorialFunc(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * FactorialFunc(x - 1);
            }
        }
    }
}
