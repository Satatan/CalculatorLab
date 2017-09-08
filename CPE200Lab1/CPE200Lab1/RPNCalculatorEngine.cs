using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            string result = "";
            Stack allText = new Stack();
            string[] parts = str.Split(' ');
            int real = 0;
            for( int i = 0; i < parts.Length; i++)
            {
                if (isNumber(parts[i]))
                {
                    real++;
                }
                else if (isOperator(parts[i]))
                {
                    real--;
                }
                if( real < 1)
                {
                    return "Error";
                }
            }
            for( int i = 0; i < parts.Length; i++)
            {
                if (isNumber(parts[i]))
                {
                    allText.Push(parts[i]);
                }
                else if (isOperator(parts[i]))
                {
                    string second = allText.Pop().ToString();
                    string first = allText.Pop().ToString();
                    result = calculate(parts[i], first, second);
                    allText.Push(result);
                }
            }
            return allText.Pop().ToString();
        }
    }
}

