using System;

namespace OOP_Laba9 {
	class Program {
		delegate void VoidFunc();
		delegate void PrintFunc(string str);

		static void Main() {
			var Print = new PrintFunc(StringActs.Print);
			string str = "abc,  abc,cda,  aabcd";
			var str1 = StringActs.ToUpperCase(str);
			Print(str1);
			var str2 = StringActs.DelCommas(str1);
			Print(str2);
			var str3 = StringActs.DelSpaces(str2);
			Print(str3);
			var list = StringActs.GetWords(str3);
			foreach (var item in list) {
				Console.WriteLine(item);
			}

			Console.ReadKey();
			new VoidFunc(Game.Play)();
		}
	}
}
