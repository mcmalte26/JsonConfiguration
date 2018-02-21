using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJson_
{
	class Program
	{
		//add <vokabel> <translation> <hint>
		//remove <voc>

		static void Main(string[] args)
		{
			if(args.Length == 1)
			{
				Console.WriteLine("syntax!!");
			}else if(args.Length == 2)
			{
				Console.WriteLine("syntax!!");
			}else if(args.Length == 3)
			{
				if (args[0].Equals("add")){
					String voc = args[1];
					String translation = args[2];
					//Füge voc hinzu voc -übersetzung
					String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Vokabel-trainer");
					/*if (Directory.Exists("Vokabel-trainer"){

					}*/
				}
			}
		}
	}
}
