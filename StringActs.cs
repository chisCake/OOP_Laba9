using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Laba9 {
	static class StringActs {
		public static Func<string, string> DelCommas = str => {
			while (str.Contains(','))
				str = str.Remove(str.IndexOf(','), 1);
			return str;
		};

		public static Action<string> Print = str => Console.WriteLine(str);

		public static Func<string, List<string>> GetWords = str => new List<string>(str.Split(' '));

		public static Func<string, string> ToUpperCase = str => str.ToUpper();

		public static Func<string, string> DelSpaces = str => {
			while (str.Contains("  "))
				str = str.Remove(str.IndexOf("  "), 1);
			return str;
		};
	}
}
