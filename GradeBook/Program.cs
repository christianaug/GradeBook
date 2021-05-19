using System;
using System.Collections.Generic;

namespace GradeBook
{
	class Program
	{
		static void Main(string[] args)
		{

			var book = new DiskBook("Bob's Grade Book");
			//applying a method to the book delegate
			book.GradeAdded += OnGradeAdded;
			EnterGrades(book);

			var stats = book.GetStatistics();

			Console.WriteLine($"the lowest grade is {stats.Low}");
			Console.WriteLine($"the highest grade is {stats.High}");
			Console.WriteLine($"the average grade is {stats.Average:N2}");
			Console.WriteLine($"the letter grade is {stats.Letter}");

			//Console.WriteLine($"The average grade is {average:N2}%");
		}

		private static void EnterGrades(IBook book)
		{
			bool done = false;
			while (!done)
			{
				Console.WriteLine("Enter a grade or 'q' to quit");
				var input = Console.ReadLine();
				if (input == "q")
				{
					done = true;
					continue;
				}

				try
				{
					var grade = double.Parse(input);
					book.AddGrade(grade);
				}
				catch (ArgumentException e)
				{
					Console.WriteLine(e.Message);
				}
				catch (FormatException e)
				{
					Console.WriteLine("Your input was not a valid number or letter grade, please try again");
				}
				finally
				{
					Console.WriteLine("***********");
				}

			}
		}

		static void OnGradeAdded(object sender, EventArgs args)
		{
			Console.WriteLine("a grade was added");
		}
	}
}
