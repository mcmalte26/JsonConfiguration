using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace ParseTests
{
	public class FileRepository : IRepository
	{
		public ItemsCollection Items
		{
			get
			{
				return Load();
			}
		}

		public void Add(string voc, string translation, string hint)
		{
			ItemsCollection items = Load();

			items.Add(voc, translation);

			Save(items);
		}

		private void Save(ItemsCollection items)
		{
			string jsonString = JsonConvert.SerializeObject(items);

			byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
			//buffer = Encoding.ASCII.GetBytes(jsonString);
			int count = 1;

			Stream fileStream = File.Open("words.json", System.IO.FileMode.OpenOrCreate);
			for (int offset = 0; offset < buffer.Length; offset++)
			{
				fileStream.Write(buffer, offset, count);
			}
			fileStream.Close();

		}

		private ItemsCollection Load()
		{
		  using (Stream fileStream = File.Open("words.json", FileMode.OpenOrCreate))
			{
				using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
				{
					string content = streamReader.ReadToEnd();
					string jsonString = string.IsNullOrWhiteSpace(content) ? "[]" : content;
					ItemsCollection items = JsonConvert.DeserializeObject<ItemsCollection>(jsonString);
					return items;
				}
			}
		}

		public void Modify(string voc)
		{
			throw new NotImplementedException();
		}

		public void Remove(string voc)
		{
			ItemsCollection items = Load();
			if (items[voc] != null)
			{
				items.Remove(voc);
			}
			Save(items);
		}
	}
}
