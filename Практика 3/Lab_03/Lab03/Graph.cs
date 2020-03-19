using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class Graph
    {
        private bool[] visited;
        private int edges;
        private int currEdge;

        public List<Node> Nodes { get; set; }
        public int[,] AdjMatrix { get; set; }
        public int[,] IncMatrix { get; set; }

        public Graph(List<Node> nodes)
        {
            Nodes = nodes;
        }

        public void CreateAdjMatrix()
        {
            AdjMatrix = new int[Nodes.Count + 1, Nodes.Count  + 1];

            foreach (var node in Nodes)
            {
                foreach (var childNode in node.Children)
                {
                    AdjMatrix[node.Name, childNode.Name ] = 1;
                }
            }

            PrintMatrix(AdjMatrix);
        }

        private int GetNumberOfEdges()
        {
            edges = 0;
            visited = new bool[Nodes.Count + 1];

            foreach (var node in Nodes)
            {
                visited[node.Name] = true;
                foreach (var child in node.Children)
                {
                    if (!visited[child.Name] || !child.Children.Contains(node))
                        edges++;
                }
            }

            return edges;
        }

        public void CreateIncMatrix()
        {
            GetNumberOfEdges();

            IncMatrix = new int[Nodes.Count + 1, edges + 1];

            visited = new bool[Nodes.Count + 1];

            currEdge = 1;

            foreach (var node in Nodes)
            {
                visited[node.Name] = true;
                foreach (var child in node.Children)
                {
                    if (!visited[child.Name])
                    {
                        IncMatrix[node.Name, currEdge] = 1;
                        if (child.Children.Contains(node))
                            IncMatrix[child.Name, currEdge] = 1;
                        else IncMatrix[child.Name, currEdge] = -1;

                    }
                    else if (!child.Children.Contains(node)) IncMatrix[child.Name, currEdge] = -1;
                    else continue;
                    currEdge++;
                }
            }

            PrintMatrix(IncMatrix);
        }

        private void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                if (i != 0) Console.Write(i);
                for (int j = 1; j < columns; j++)
                {
                    if (i == 0) Console.Write($"\t{j}");
                    else Console.Write($"\t{matrix[i, j]}");
                }
                Console.WriteLine();
            }
        }
    }
}
