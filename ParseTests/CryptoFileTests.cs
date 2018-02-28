using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ParseTests
{
	[TestClass]
	public class CryptoFileTests
	{

		[TestMethod]
		public void AddNewVoc()
		{
			string[] args = { "add", "de:Küste", "en:coast", "hint" };
			Parser p = new Parser();
			Action<IRepository> a = p.Parse(args);
			CryptoFileRepository repository = new CryptoFileRepository();
			a(repository);

			Assert.AreEqual("coast", repository.Items["de:Küste"].Words["en"]);
			Assert.AreEqual("Küste", repository.Items["en:coast"].Words["de"]);
		}
	}
}
