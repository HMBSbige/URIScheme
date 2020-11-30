using Microsoft.VisualStudio.TestTools.UnitTesting;
using URIScheme;
using URIScheme.Enums;

namespace UnitTest
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void CurrentUserTest()
		{
			const string key = @"ssa";
			var service = new URISchemeService(key, @"URL:ssa Protocol", @"D:\MyAppPath\MyApp.exe --openurl");
			Assert.IsFalse(service.Check());

			service.Set();
			Assert.IsTrue(service.Check());

			service.Delete();
			Assert.IsFalse(service.Check());
		}

		[TestMethod]
		public void LocalMachineTest()
		{
			const string key = @"ssb";
			var service = new URISchemeService(key, @"URL:ssa Protocol", @"D:\MyAppPath\MyApp.exe --openurl", RegisterType.LocalMachine);
			Assert.IsFalse(service.Check());

			service.Set();
			Assert.IsTrue(service.Check());

			service.Delete();
			Assert.IsFalse(service.Check());
		}
	}
}
