using System;
using GradeBook;
using Xunit;

namespace Gradebook.Tests
{
	public class TypeTest
	{
		[Fact]
		public void GetBookReturnsDifferentObjects()
		{
			var book1 = GetBook("Book 1");
			var book2 = GetBook("Book 2");

			Assert.Equal("Book 1", book1.Name);
			Assert.Equal("Book 2", book2.Name);

		}

		[Fact]
		public void TwoVariablesCanRefferenceTheSameObject()
		{
			var book1 = GetBook("Book 1");
			var book2 = book1;

			Assert.Equal("Book 1", book1.Name);
			Assert.Equal("Book 1", book2.Name);

			//both variables point to the same object
			Assert.Same(book1, book2);
			Assert.True(Object.ReferenceEquals(book1, book2));

		}

		[Fact]
		public void CanSetNameFromRefference()
		{
			var book1 = GetBook("Book 1");
			SetName(book1, "New Name");
			Assert.Equal("New Name", book1.Name);
		}

		[Fact]
		public void CSharpIsPassByValue()
		{
			var book = GetBook("Book 1");
			GetBookSetName(book, "New Name");
			//this shows that C# is not pass by reffernce and is pass by value
			Assert.Equal("Book 1", book.Name);
		}

		[Fact]
		public void CSharpCanPassByRef()
		{
			var book = GetBook("Book 1");
			GetBookSetNameByRef(ref book, "New Name");
			//You can alter a methods arguments to allow for pass by refference, must use keyword "ref"
			Assert.Equal("New Name", book.Name);
		}

		[Fact]
		public void ValueTypesAlsoPassByValue()
		{
			var x = GetInt();
			setInt(ref x);
			Assert.Equal(42, x);
		}

		private void setInt(ref int x)
		{
			x = 42;
		}

		public int GetInt()
		{
			return 3;
		}

		private void SetName(InMemoryBook book1, string name)
		{
			book1.Name = name;
		}

		private void GetBookSetNameByRef(ref InMemoryBook book, string name)
		{
			book = new InMemoryBook(name);
		}

		private void GetBookSetName(InMemoryBook book, string name)
		{
			book = new InMemoryBook(name);
		}

		private InMemoryBook GetBook(string name)
		{
			return new InMemoryBook(name);
		}
	}
}
