using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitBot
{
	public enum NodeType
	{
		Simple,
		Marked,
		Context
	}

	public class Node
	{
		public string Text { get; set; }

		public string Mark { get; set; }

		public List<Edge> Edges {get; set;}
	}

	public class Edge
	{
		public string Text { get; set; }

		public Node Node { get; set; }
	}
}
