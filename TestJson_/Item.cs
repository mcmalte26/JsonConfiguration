using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParseTests
{
	public class Item
	{
		public Item()
		{
			Words = new Dictionary<string, string>();
		}

		public Dictionary<string, string> Words { get; }

		public void AddWord(string key, string value)
		{
			if (Words.ContainsKey(key))
			{
				Words.Remove(key);
			}
			Words.Add(key, value);
		}
	}
}
