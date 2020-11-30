using Microsoft.Win32;
using System;
using URIScheme.Enums;

namespace URIScheme
{
	public class URISchemeService
	{
		public RegisterType Type { get; }
		public string Key { get; }
		public string Description { get; }
		public string RunPath { get; }

		private const string RegistryPath = @"Software\Classes";
		private string Value => $@"""{RunPath}"" ""%1""";

		public URISchemeService(string key, string description, string runPath, RegisterType type = RegisterType.CurrentUser)
		{
			Key = key;
			Description = description;
			RunPath = runPath;
			Type = type;
		}

		private RegistryKey? OpenRegKey(string name, bool writable)
		{
			var hive = Type == RegisterType.LocalMachine ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
			var view = Environment.Is64BitProcess ? RegistryView.Registry64 : RegistryView.Registry32;
			return RegistryKey.OpenBaseKey(hive, view).OpenSubKey(name, writable);
		}

		public bool Check()
		{
			using var regKey = OpenRegKey(RegistryPath, false);
			if (regKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}
			using var root = regKey.OpenSubKey(Key);

			var protocolMark = root?.GetValue(@"URL Protocol");
			if (protocolMark is null)
			{
				return false;
			}

			var uriKey = root?
					.OpenSubKey(@"shell")?
					.OpenSubKey(@"open")?
					.OpenSubKey(@"command")?
					.GetValue(null);

			return uriKey is string str && str == Value;
		}

		public void Set()
		{
			using var regKey = OpenRegKey(RegistryPath, true);
			if (regKey is null)
			{
				throw new SystemException(@"Cannot open Registry!");
			}

			using var root = regKey.CreateSubKey(Key);
			root.SetValue(null, Description);
			root.SetValue(@"URL Protocol", string.Empty);
			using var command = root.CreateSubKey(@"shell\open\command");
			command.SetValue(null, Value);
		}

		public void Delete()
		{
			using var regKey = OpenRegKey(RegistryPath, true);
			regKey?.DeleteSubKeyTree(Key);
		}
	}
}
