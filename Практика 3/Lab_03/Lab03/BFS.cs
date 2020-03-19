using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class BFS
    {
        private Node currNode;
        private Queue<Node> queue;
        private bool[] visited;

        public void StartBFS(Node startNode, int nodes)
        {
            currNode = startNode;

            queue = new Queue<Node>();
            queue.Enqueue(startNode);

            visited = new bool[nodes + 1];
            visited[startNode.Name] = true;

            while(queue.Count != 0)
            {
                currNode = queue.Dequeue();

                Console.Write($"{currNode.Name} ");

                foreach(var node in currNode.Children)
                {
                    int to = node.Name;
                    if (!visited[to])
                    {
                        visited[to] = true;
                        queue.Enqueue(node);
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
