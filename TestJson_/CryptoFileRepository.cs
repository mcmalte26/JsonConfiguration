using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ParseTests
{
	public class CryptoFileRepository : IRepository
	{

		private String key = "$6397he*03§$6397he*03§$6397he*03";
		private String iv = "$6397he*03§$6397he*03§$6397he*03";
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

			using (RijndaelManaged r = new RijndaelManaged())
			{
				SetCryptoKeys(r);
				ICryptoTransform encryptor = r.CreateEncryptor(r.Key, r.IV);

				Stream fileStream = File.Open("words.json", System.IO.FileMode.OpenOrCreate);
				using(CryptoStream csEncrypt = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
				{
					using(StreamWriter writer = new StreamWriter(csEncrypt))
					{
						writer.Write(jsonString);
					}
				}

	 
				fileStream.Close();
			}
		}

		private ItemsCollection Load()
		{
			using (RijndaelManaged r = new RijndaelManaged())
			{
				SetCryptoKeys(r);
				ICryptoTransform decryptor = r.CreateDecryptor(r.Key, r.IV);
				using (Stream fileStream = File.Open("words.json", FileMode.OpenOrCreate,FileAccess.Read, FileShare.Read))
				{
					if (fileStream.Length == 0)
					{
						return new ItemsCollection();
					}
					using (CryptoStream csDecrypt = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
					{
						
						using (StreamReader streamReader = new StreamReader(csDecrypt))
						{
							
							string content = streamReader.ReadToEnd();
							string jsonString = string.IsNullOrWhiteSpace(content) ? "[]" : content;
							ItemsCollection items = JsonConvert.DeserializeObject<ItemsCollection>(jsonString);
							return items;
						}
					}

				}
			}
		}

		private void SetCryptoKeys(Rijndael r)
		{
			r.Key = Encoding.UTF32.GetBytes(key).Take(r.KeySize/8).ToArray();
			r.IV = Encoding.UTF32.GetBytes(iv).Take(r.BlockSize/8).ToArray();
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
