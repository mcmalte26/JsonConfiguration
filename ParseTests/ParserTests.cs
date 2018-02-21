using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParseTests
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod]
		public void AddNewVoc()
		{
			string[] args = new[] { "add", "de:Stadt", "en:city", "hint" };
			Parser p = new Parser();
			Action<InMemoryRepository> a = p.Parse(args);
			InMemoryRepository repository = new InMemoryRepository();
			a(repository);

			Assert.AreEqual("city", repository.Items["de:Stadt"].Words["en"]);
			Assert.AreEqual("Stadt", repository.Items["en:city"].Words["de"]);
		}

		[TestMethod]
		public void RemoveVocIfExists()
		{
			string[] args = new[] { "remove", "de:voc" };
			Parser p = new Parser();
			Action<InMemoryRepository> a = p.Parse(args);
			InMemoryRepository repository = new InMemoryRepository();
			repository.Add("de:voc", "en:translation", "hint");
			a(repository);

			Assert.IsFalse(repository.Items.Contains("de:voc"));
		}

		[TestMethod]
		public void RemoveVocWithOtherLanguage()
		{
			string[] args = new[] { "remove", "en:translation" };
			Parser p = new Parser();
			Action<InMemoryRepository> a = p.Parse(args);
			InMemoryRepository repository = new InMemoryRepository();
			repository.Add("de:voc", "en:translation", "hint");
			a(repository);

			Assert.IsFalse(repository.Items.Contains("en:translation"));
			Assert.IsFalse(repository.Items.Contains("de:voc"));
		}

		[TestMethod]
		public void RemoveVocIfNotExists()
		{
			string[] args = new[] { "remove", "de:voc" };
			Parser p = new Parser();
			Action<InMemoryRepository> a = p.Parse(args);
			InMemoryRepository repository = new InMemoryRepository();
			a(repository);

			Assert.IsFalse(repository.Items.Contains("de:voc"));
		}

	}
}
