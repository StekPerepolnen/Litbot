using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitBot
{
	public enum FileLineType
	{
		Mark,
		Node,
		Edge,
		Context
	}

	public class FileLine
	{
		public FileLineType Type { get; set; }

		public int Indent { get; set; }

		public string Text { get; set; }

		public List<FileLine> Children { get; set; }
	}
}
