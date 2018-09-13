using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LitBot
{
	public class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadAllLines(@"..\..\..\..\pizdec.txt").ToList();
			List<FileLine> fileLines = new List<FileLine>();
			for (int i = 0; i < lines.Count; i++)
				if (!string.IsNullOrEmpty(lines[i]))
				{
					var line = lines[i];
					int tabCount = 0;
					FileLineType type;
					while (line.Length > 0 && line[0] == '\t')
					{
						tabCount++;
						line = line.Substring(1);
					}
					line = line.Trim();
					if (line.StartsWith("[[") && line.EndsWith("]]"))
					{
						type = FileLineType.Context;
						line = line.Substring(2, line.Length - 4).Trim();
					}
					else if (line.StartsWith("[") && line.EndsWith("]"))
					{
						type = FileLineType.Mark;
						line = line.Substring(1, line.Length - 2).Trim();
					}
					else if (line.StartsWith("->"))
					{
						type = FileLineType.Edge;
						line = line.Substring(2).Trim();
					}
					else
					{
						type = FileLineType.Node;
					}

					FileLine fileLine = new FileLine() {Indent = tabCount, Type = type, Text = line};
					fileLines.Add(fileLine);
				}

			fileLines = GetTree(fileLines);
		}

		private static List<FileLine> GetTree(List<FileLine> fileLines)
		{
			return GetTree(fileLines, 0, fileLines.Count);
		}

		private static List<FileLine> GetTree(List<FileLine> original, int a, int b)
		{
			List<FileLine> lines = new List<FileLine>();
			int elem = a;
			for (int i = elem + 1; i <= b; i++)
				if (i == b || original[a].Indent == original[i].Indent)
				{
					original[elem].Children = GetTree(original, elem + 1, i);
					lines.Add(original[elem]);
					elem = i;
				}

			return lines;
		}
	}
}
