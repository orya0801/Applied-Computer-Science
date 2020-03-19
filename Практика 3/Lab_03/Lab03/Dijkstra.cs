using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class Dijkstra
    {
        private Graph Graph { get; set; }
        private int[,] weightOfEdges;
        private int weight;
        private int[] path;
        private bool[] visited;
        private bool[,] visited_edges;
        private Node startNode, currNode;
        private Queue<Node> queue;


        public Dijkstra(Graph graph)
        {
            Graph = graph;
        }

        private void SetWeightForEdges()
        {
            weightOfEdges = new int[Graph.Nodes.Count + 1, Graph.Nodes.Count + 1];

            foreach (var currNode in Graph.Nodes)
            { 
                foreach(var childNode in currNode.Children)
                {
                    if(childNode.Name < currNode.Name
                        && weightOfEdges[childNode.Name, currNode.Name] != 0)
                    {
                        weightOfEdges[currNode.Name, childNode.Name] = weightOfEdges[childNode.Name, currNode.Name];
                    }
                    else
                    {
                        Console.Write($"Вес связи от узла {currNode.Name} до узла {childNode.Name}: ");
                        weight = int.Parse(Console.ReadLine());
                        weightOfEdges[currNode.Name, childNode.Name] = weight;
                    }   
                }
            }   
        }

        private void PrintWeightOfEdges()
        {
            foreach (var currNode in Graph.Nodes)
            {
                foreach (var childNode in currNode.Children)
                {
                    Console.WriteLine($"Вес связи от узла {currNode.Name} до узла {childNode.Name}: {weightOfEdges[currNode.Name, childNode.Name]}");
                }
            }
        }

        public void Start(Node node)
        {
            SetWeightForEdges();

            startNode = node;

            StartDijkstra();

            PrintShortestPaths();

            Console.WriteLine();
        }

        private void PrintShortestPaths()
        {
            for (int i = 1; i < path.Length; i++)
            {
                if (startNode.Name != i)
                    Console.WriteLine($"Наикратчаший путь из {startNode.Name} в {i}: {path[i]}");
            }
        }

        private void StartDijkstra()
        {
            int to;

            path = new int[Graph.Nodes.Count + 1];

            visited = new bool[Graph.Nodes.Count + 1];

            queue = new Queue<Node>();

            currNode = startNode;

            queue.Enqueue(currNode);

            while (queue.Count != 0)
            {
                currNode = queue.Dequeue();

                foreach (var childNode in currNode.Children)
                {
                    to = childNode.Name;
                    if (!visited[to])
                    {
                        if (path[to] == 0)
                            path[to] = path[currNode.Name] + weightOfEdges[currNode.Name, childNode.Name];
                        else if (path[to] >= path[currNode.Name] + weightOfEdges[currNode.Name, childNode.Name])
                            path[to] = path[currNode.Name] + weightOfEdges[currNode.Name, childNode.Name];
                        else path[to] += weightOfEdges[currNode.Name, childNode.Name];
                        visited[to] = true;
                        queue.Enqueue(childNode);
                    }
                    else 
                    {
                        if (weightOfEdges[currNode.Name, childNode.Name] + path[currNode.Name] < path[to])
                            path[to] = path[currNode.Name] + weightOfEdges[currNode.Name, childNode.Name];
                    }
                }
            }

        }
    }
}
