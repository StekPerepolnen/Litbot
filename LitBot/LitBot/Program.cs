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
			var intend = 0;
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

			GetTree(fileLines, 0, fileLines.Count);
		}

		private static List<Node> GetTree(List<FileLine> fileLines, int a, int b)
		{
			List<Node> nodes = new List<Node>();
			int k = a;
			for (int i = a + 1; i < b || fileLines[a].Indent > fileLines[i].Indent; i++)
			{
				if (fileLines[i].Type == FileLineType.Mark || fileLines[i].Type == FileLineType.Context || i == b - 1)
				{
					GetTree(fileLines, k + 1, i);
					k = i;
				}
			}

			return nodes;
		}
	}
}
