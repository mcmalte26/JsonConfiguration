using System;

namespace ParseTests
{
	public class InMemoryRepository : IRepository
	{
		public readonly ItemsCollection Items = new ItemsCollection();

		public void Add(string voc, string translation, string hint)
		{
			Items.Add(voc, translation);
		}

		public void Modify(string voc)
		{
			throw new NotImplementedException();
		}

		public void Remove(string voc)
		{
			if (Items[voc] != null)
			{
				Items.Remove(voc);
			}
		}
	}
}
