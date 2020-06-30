using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Data
{
    public enum NodeType
    {
        Root
    }

    /// <summary>
    /// AST Node
    /// </summary>
    public class Node
    {
        private List<Node> _children;
        public Node(NodeType type, params Node[] children)
        {
            Type = type;
            _children = new List<Node>(children);
        }

        public NodeType Type { get; }
        public Node[] Children { get { return _children.ToArray(); } }
        public override string ToString() => Enum.GetName(typeof(NodeType), Type);

        public void Add(Node node) => _children.Add(node);
    }
}
