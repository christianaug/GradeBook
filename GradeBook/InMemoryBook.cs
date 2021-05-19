using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GradeBook
{
	//this can be used to call (delegate) other functions that are assigned to one variable. in this case we are using it for an event
	public delegate void GradeAddedDelegate(object sender, EventArgs args);

	public class NamedObject
	{
		public string Name { get; set; }
		public NamedObject(string name)
		{
			Name = name;
		}
	}

	public interface IBook
	{
		void AddGrade(double grade);
		Statistics GetStatistics();
		string Name { get; }
		event GradeAddedDelegate GradeAdded;
	}

	public abstract class Book : NamedObject, IBook
	{
		public Book(string name) : base(name)
		{
				
		}

		public abstract event GradeAddedDelegate GradeAdded;

		public abstract void AddGrade(double grade);

		public abstract Statistics GetStatistics();
	}

	public class InMemoryBook : Book
	{
		public static int BookCount { get; set; }
		private List<double> Grades { get; set; } = new List<double>();

		public override event GradeAddedDelegate GradeAdded;

		public InMemoryBook(string name) : base(name)
		{
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

		//use override keyword to help over ride the abstract method in the BaseBook class
		public override void AddGrade(double grade)
		{
			if (grade >= 0 && grade <= 100)
			{
				Grades.Add(grade);
				//check if anyone is interested in this event (anyone is assigned to it)
				if (GradeAdded != null)
				{
					GradeAdded(this, new EventArgs());
				}
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(grade)}");
			}

		}

		public override Statistics GetStatistics()
		{
			var result = new Statistics();

			for(var i = 0; i < Grades.Count; i++)
			{
				result.Add(Grades[i]);
			}

			return result;
		}
	}

	public class DiskBook : Book
	{
		public DiskBook(string name) : base(name)
		{

		}

		public override event GradeAddedDelegate GradeAdded;

		public override void AddGrade(double grade)
		{
			//do something
			
			if (grade >= 0 && grade <= 100)
			{
				//using makes sure to clean up when you use something that uses the interface IDisposable
				using (var writer = File.AppendText($"{Name}.txt"))
				{
					writer.WriteLine(grade);
					if(GradeAdded != null)
					{
						GradeAdded(this, new EventArgs());
					}
				}
			}
			else
			{
				throw new ArgumentException($"Invalid {nameof(grade)}");
			}
		}

		public override Statistics GetStatistics()
		{
			var result = new Statistics();

			using(var reader = File.OpenText($"{Name}.txt"))
			{
				var line = reader.ReadLine();

				while (line != null)
				{
					var number = double.Parse(line);
					result.Add(number);
					line = reader.ReadLine();
				}
			}

			return result;
		}
	}
}
