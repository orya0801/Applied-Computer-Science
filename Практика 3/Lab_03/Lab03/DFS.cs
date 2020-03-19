using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class DFS
    {
        bool[] _visited;
        Queue<int> queue;

        public void StartDFS(Node startNode, int nodes)
        {
            _visited = new bool[nodes + 1];

            _visited[startNode.Name] = true;

            queue = new Queue<int>();
            queue.Enqueue(startNode.Name);

            foreach(var node in startNode.Children.Where(node => !_visited[node.Name]))
            {
                StartDFS(node);
            }

            PrintQueue();
        }

        private void StartDFS(Node startNode)
        {
            _visited[startNode.Name] = true;

            queue.Enqueue(startNode.Name);

            foreach (var node in startNode.Children.Where(node => !_visited[node.Name]))
            {
                StartDFS(node);
            }

        }

        private void PrintQueue()
        {
            foreach (var nodeName in queue)
                Console.Write($"{nodeName} ");
            Console.WriteLine();
        }
    }
}
