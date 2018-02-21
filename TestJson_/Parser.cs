using System;

namespace ParseTests
{
	public class Parser
	{
		public Action<IRepository> Parse(string[] args)
		{
			if (args.Length < 2) {
				throw new ArgumentOutOfRangeException("Invalid argument length");
			}

			string action = args[0];
			switch (action)
			{
				case "add":
					return repository => { repository.Add(args[1], args[2], args[3]); };
				case "remove":
					return repository => { repository.Remove(args[1]); };
				case "modify":
					return repository => { repository.Modify(args[1]); };
				default:
					throw new InvalidOperationException("Unknown action " + action);
			}
		}
	}
}
