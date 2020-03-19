using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class Node
    {
        public int Name { get; }

        public List<Node> Children { get; }

        public Node(int nodeName)
        {
            Name = nodeName;
            Children = new List<Node>();
        }

        public Node AddChildren(Node node, bool isDirected = false) 
        {
            Children.Add(node);

            if (!isDirected)
            {
                node.Children.Add(this);
            }

            return this;
        }
    }
}
