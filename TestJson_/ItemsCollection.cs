using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ParseTests
{
	public class ItemsCollection:ICollection<Item>
	{
	  private readonly List<Item> _items;

	  public ItemsCollection(){
	    _items = new List<Item>();
	  }

	  public bool Contains(string key)
		{
			KeyValuePair<string, string> parsedKey = ParseVocKey(key);
			return this.Any(item => item.Words[parsedKey.Key] == parsedKey.Value);
		}
		public void Add(string voc, string translation)
		{
			KeyValuePair<string, string> parsedVocKey = ParseVocKey(voc);
			KeyValuePair<string, string> parsedTranslationKey = ParseVocKey(translation);
			Item existing = this[voc];
			if (null == existing)
			{
				existing = new Item();
				_items.Add(existing);
				existing.AddWord(parsedVocKey.Key, parsedVocKey.Value);
			}
			existing.AddWord(parsedTranslationKey.Key, parsedTranslationKey.Value);
		}

		public Item this[string key]
		{

			get
			{
				KeyValuePair<string, string> parsedKey =  ParseVocKey(key);
				return this.FirstOrDefault(item => item.Words[parsedKey.Key] == parsedKey.Value);

			}
		}

		private static KeyValuePair<string, string> ParseVocKey(string key)
		{
			Regex regex = new Regex(@"^(?<lang>\w{2}):(?<value>.*)$");
			Match match = regex.Match(key);
			string lang = match.Groups["lang"].Value;
			string value = match.Groups["value"].Value;
			return new KeyValuePair<string, string>(lang, value);
		}

		public void Remove(string voc)
		{
			Item item = this[voc];
			if (item != null)
			{
			  _items.Remove(item);
			}
		}

	  /// <inheritdoc />
	  IEnumerator<Item> IEnumerable<Item>.GetEnumerator(){
	    return _items.GetEnumerator();
	  }

	  /// <inheritdoc />
	  IEnumerator IEnumerable.GetEnumerator(){
	    return ((IEnumerable) _items).GetEnumerator();
	  }

	  /// <inheritdoc />
	  public void Add(Item item){
	    _items.Add(item);
	  }

	  /// <inheritdoc />
	  void ICollection<Item>.Clear(){
	    _items.Clear();
	  }

	  /// <inheritdoc />
	  bool ICollection<Item>.Contains(Item item){
	    return _items.Contains(item);
	  }

	  /// <inheritdoc />
	  void ICollection<Item>.CopyTo(Item[] array, int arrayIndex){
	    _items.CopyTo(array, arrayIndex);
	  }

	  /// <inheritdoc />
	  bool ICollection<Item>.Remove(Item item){
	    return _items.Remove(item);
	  }

	  /// <inheritdoc />
	  public int Count => _items.Count;

	  /// <inheritdoc />
	  bool ICollection<Item>.IsReadOnly => false;
	}
}
