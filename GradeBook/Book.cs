using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook
{
	public class Book
	{
		public static int BookCount { get; set; }
		private List<double> Grades { get; set; } = new List<double>();
		public string Name { get; set; }
		public Book(string name)
		{
			Name = name;
			BookCount++;
		}

		public void AddLetterGrade(char letter)
		{
			switch (letter)
			{
				case 'A':
					AddGrade(90);
					break;
				case 'B':
					AddGrade(80);
					break;
				case 'C':
					AddGrade(70);
					break;
				case 'D':
					AddGrade(60);
					break;
				case 'F':
					AddGrade(50);
					break;
				default:
					AddGrade(0);
					break;
			}
		}

		public void AddGrade(double grade)
		{
			if (grade >= 0 && grade <= 100)
			{
				Grades.Add(grade);
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(grade)}");
			}

		}

		public double GetAverage()
		{
			double average = 0;

			foreach (double grade in Grades)
			{
				average += grade / Grades.Count;
			}

			return average;
		}

		public double GetHighestGrade()
		{
			double result = double.MinValue;

			foreach (double grade in Grades)
			{
				result = Math.Max(result, grade);
			}

			return result;
		}

		public double GetLowestGrade()
		{
			double result = double.MaxValue;

			foreach (double grade in Grades)
			{
				result = Math.Min(result, grade);
			}

			return result;
		}

		public Statistics GetStatistics()
		{
			var result = new Statistics();
			result.Average = GetAverage();
			result.High = GetHighestGrade();
			result.Low = GetLowestGrade();


			switch (result.Average)
			{
				case var d when d >= 90.0:
					result.Letter = 'A';
					break;
				case var d when d >= 80.0:
					result.Letter = 'B';
					break;
				case var d when d >= 70.0:
					result.Letter = 'C';
					break;
				case var d when d >= 60.0:
					result.Letter = 'D';
					break;
				default:
					result.Letter = 'F';
					break;
			}
			return result;
		}
	}
}
