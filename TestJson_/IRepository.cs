namespace ParseTests
{
	public interface IRepository
	{
		void Add(string voc, string translation, string hint);
		void Modify(string voc);
		void Remove(string voc);
	}
}