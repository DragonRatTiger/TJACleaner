using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TJACleaner
{
    // Test Cases to ensure that every function of TJACleaner works as expected
	// More cases will be added as more conditions are raised and in need of testing.
    class TestCase
    {
        //Input: A bar, which includes a string of numbers that has notes and empty space, and ends with a comma (,).
        //Output: A shortened bar, which should remove any excess 0's that have no influence on note timing, printed in console.

		// Note that this will only read a single line inputted by the user via. console. Files can not be used for this case.
        public static void TestCase1()
        {
			Console.WriteLine("Please write a sample bar.");
			string input = Console.ReadLine();
			// Converts the bar (string) inputted by the user into a char array.
			// The ternary operator will be used to check if the string given contains a comma, which is used to separate bars.
			// To ensure it's only reading a single bar and not multiple bars, any comma added to the user input will be used to remove any extra input after the comma.
			char[] bar = input.Contains(",") ? input.Remove(input.IndexOf(','), (input.Length - input.IndexOf(','))).ToCharArray() : input.ToCharArray();

			int a = 0;
			List<int> spaces = new List<int> { 0 };

			++spaces[a];
			for (int i = 1; i < bar.Length; ++i)
			{
				// If the char is a comma, finish scanning this bar by breaking out of the for loop.
				if (bar[i] == ',') { break; }
				// Otherwise, check for space.
				switch (bar[i])
				{
					case '0':
						++spaces[a];
						break;
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					case 'A':
					case 'B':
					case 'F':
						++a;
						// Create a new space counter, start with 1.
						spaces.Insert(a, 1);
						break;
					default:
						break;
				}
			}

			/*
			Checks to see if the bar can be reduced without compromsing note placement.
			"canBeReduced" starts false, and is only given a true value if the minimum space of a note is not 1.
			If the minimum space is 2 or greater, the algorithm will take the smallest value and find the remainder of every single space value.
			If there is any remainder, the bar can not be reduced, as it will shift note(s) to the wrong place(s).
			*/

			bool canBeReduced = false;
			if (spaces.Min() > 1)
			{
				canBeReduced = true;
				foreach (int i in spaces)
				{
					if (i % spaces.Min() != 0)
					{
						canBeReduced = false;
					}
				}
			}

			// If the bar can, in fact, be reduced of its excess spacing, this if statement will be used.
			string newbar = string.Join("", bar);
			int removecount = 0;
			if (canBeReduced)
			{
				int nextNote = 0;
				for (int j = 0; j < (bar.Length / spaces.Min()); ++j)
                {
					newbar = newbar.Remove((nextNote + 1), (spaces.Min() - 1));
					removecount += (spaces.Min() - 1);
					++nextNote;
                }
            }

			Console.WriteLine("\nSpace count: {0} (Total: {1})", string.Join(",", spaces), bar.Length);
			Console.WriteLine("Can be reduced: {0}\n", canBeReduced);
			if (canBeReduced) { Console.WriteLine("{0}\n",newbar); Console.WriteLine("Number of spaces removed: {0}\n", removecount); }
			Console.WriteLine("Done!");
		}
    }
}
