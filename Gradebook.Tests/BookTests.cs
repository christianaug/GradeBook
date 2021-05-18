using System;
using GradeBook;
using Xunit;

namespace Gradebook.Tests
{
	public class BookTests
	{
		[Fact]
		public void BookCalculatesAnAverageGrade()
		{
			var book = new Book("Scott's book");
			book.AddGrade(89.1);
			book.AddGrade(90.5);
			book.AddGrade(77.3);

			var stats = book.GetStatistics();

			Assert.Equal(85.63, stats.Average, 2);
			
		}
	}
}
