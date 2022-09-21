using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversePostfix
{
	public class Test
	{
		public static void Main()
		{
			RunTestCase(10, new[] { "5", "2", "*" }); // 5 * 2 = 10
			RunTestCase(13, new[] { "10", "2", "*", "6", "+", "2", "/" }); // ((10 * 2) + 6) / 2 = 13
			RunTestCase(95, new[] { "100", "10", "-", "30", "6", "/", "+" }); // (100 - 10) + (30 / 6) = 95
			RunTestCase(6, new[] { "10", "7", "-", "!" }); // (10 - 7)! = 6 
		}

		public static double Calculate(IEnumerable<string> input)
		{
			Stack<double> stack = new Stack<double>();

			foreach (string element in input)
			{
				if (!"+-*/!".Contains(element))
				{
					stack.Push(Convert.ToDouble(element));
					continue;
				}

				double second = stack.Count() > 0 ? stack.Pop() : 0;
				double first = stack.Count() > 0 ? stack.Pop() : 0;
				double ans = 0;
				switch (element)
				{
					case "+":
						ans = first + second;
						break;
					case "-":
						ans = first - second;
						break;
					case "/":
						ans = first / second;
						break;
					case "*":
						ans = first * second;
						break;
					case "!":
						ans = second * 2;
						break;

				}
				stack.Push(ans);
			}
			return stack.Pop();
		}
		public static void RunTestCase(double expectedResult, IEnumerable<string> input)
		{
			var result = Calculate(input);
			if (Math.Abs(result - expectedResult) < 0.001)
			{
				Console.WriteLine("Pass");
			}
			else
			{
				Console.WriteLine("Fail: Expected = {0}, Actual = {1}. Input = '{2}'", expectedResult, result, string.Join(",", input));
			}
		}
	}
}