using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n1 = new Node(1);
            Node n2 = new Node(2);
            Node n3 = new Node(3);
            Node n4 = new Node(4);
            Node n5 = new Node(5);
            Node n6 = new Node(6);
            Node n7 = new Node(7);
            Node n8 = new Node(8);
            Node n9 = new Node(9);
            Node n10 = new Node(10);

            List<Node> nodes1 = new List<Node>();

            // Задание 1
            n1.AddChildren(n2).AddChildren(n3);
            n2.AddChildren(n4).AddChildren(n5);
            n3.AddChildren(n4).AddChildren(n5);

            nodes1.Add(n1);
            nodes1.Add(n2);
            nodes1.Add(n3);
            nodes1.Add(n4);
            nodes1.Add(n5);

            Graph graph1 = new Graph(nodes1);

            Console.WriteLine("Задание 1\n\nМатрица смежности");
            graph1.CreateAdjMatrix();
            Console.WriteLine("Матрица инцидентности");
            graph1.CreateIncMatrix();
            Console.WriteLine();

            //Задание 2 - 3 
            foreach (var node in nodes1)
            {
                node.Children.Clear();
            }
            nodes1.Add(n6);
            nodes1.Add(n7);

            //Неориентированный граф
            n3.AddChildren(n1).AddChildren(n2).AddChildren(n4).AddChildren(n5).AddChildren(n6);
            n1.AddChildren(n2);
            n7.AddChildren(n5).AddChildren(n6);

            Console.WriteLine("Задание 2-3. Неориентированный граф\nМатрица смежности");
            graph1.CreateAdjMatrix();
            Console.WriteLine("Матрица инцидентности");
            graph1.CreateIncMatrix();
            Console.WriteLine();

            //Ориентированный граф
            foreach (var node in nodes1)
            {
                node.Children.Clear();
            }

            n1.AddChildren(n2, true).AddChildren(n3, true);
            n2.AddChildren(n3, true);
            n3.AddChildren(n4, true).AddChildren(n5, true).AddChildren(n6, true);
            n5.AddChildren(n7, true);
            n6.AddChildren(n7, true);

            Console.WriteLine("Задание 2-3. Ориентированный граф\nМатрица смежности");
            graph1.CreateAdjMatrix();
            Console.WriteLine("Матрица инцидентности");
            graph1.CreateIncMatrix();
            Console.WriteLine();

            //Тестирование поиска в глубину и ширину

            foreach (var node in nodes1)
            {
                node.Children.Clear();
            }

            n1.AddChildren(n2, true).AddChildren(n3);
            n2.AddChildren(n3, true).AddChildren(n4);
            n3.AddChildren(n4, true);

            graph1.Nodes = nodes1;

            var dfs = new DFS();
            var bfs = new BFS();

            Console.WriteLine("Задание 4\n");
            Console.WriteLine("Поиск в глубину: ");
            dfs.StartDFS(n4, 4);
            dfs.StartDFS(n3, 4);
            dfs.StartDFS(n2, 4);
            dfs.StartDFS(n1, 4);

            Console.WriteLine();
            Console.WriteLine("Поиск в ширину: ");

            bfs.StartBFS(n4, 4);
            bfs.StartBFS(n3, 4);
            bfs.StartBFS(n2, 4);
            bfs.StartBFS(n1, 4);

            Console.WriteLine();

            n1.Children.Clear();
            n2.Children.Clear();
            n3.Children.Clear();
            n4.Children.Clear();

            n1.AddChildren(n5).AddChildren(n2);
            n3.AddChildren(n5).AddChildren(n2);
            n4.AddChildren(n5).AddChildren(n2);

            Console.WriteLine("Задание 5\n");
            Console.Write("Поиск в глубину: ");
            dfs.StartDFS(n5, 5);
            Console.Write("Поиск в ширину: ");
            bfs.StartBFS(n5, 5);
            Console.WriteLine();

            n1.Children.Clear();
            n2.Children.Clear();
            n3.Children.Clear();
            n4.Children.Clear();
            n5.Children.Clear();



            n1.AddChildren(n2).AddChildren(n7);
            n2.AddChildren(n3).AddChildren(n4);
            n4.AddChildren(n5).AddChildren(n6);
            n7.AddChildren(n8);
            n8.AddChildren(n9).AddChildren(n10);

            Console.WriteLine("Собственный пример графа: ");
            Console.Write("Поиск в глубину: ");
            dfs.StartDFS(n8, 10);
            Console.Write("Поиск в ширину: ");
            bfs.StartBFS(n8, 10);
            Console.WriteLine();

            n1.Children.Clear();
            n2.Children.Clear();
            n3.Children.Clear();
            n4.Children.Clear();
            n5.Children.Clear();
            n6.Children.Clear();

            n1.AddChildren(n2, true).AddChildren(n3).AddChildren(n5, true);
            n2.AddChildren(n4);
            n3.AddChildren(n4);
            n4.AddChildren(n6, true);
            n5.AddChildren(n6, true);

            List<Node> graph = new List<Node>();
            graph.Add(n1);
            graph.Add(n2);
            graph.Add(n3);
            graph.Add(n4);
            graph.Add(n5);
            graph.Add(n6);

            graph1 = new Graph(graph);

            Dijkstra dijkstra = new Dijkstra(graph1);

            Console.WriteLine("Задание 6. Алгоритм Дейкстры:\n");
            dijkstra.Start(n1);


            n1.Children.Clear();
            n2.Children.Clear();
            n3.Children.Clear();
            n4.Children.Clear();
            n5.Children.Clear();
            n6.Children.Clear();
            n7.Children.Clear();

            n1.AddChildren(n2).AddChildren(n3);
            n2.AddChildren(n4).AddChildren(n5);
            n3.AddChildren(n4).AddChildren(n6);
            n4.AddChildren(n5).AddChildren(n6);
            n5.AddChildren(n7);
            n6.AddChildren(n7);


            List<Node> nodes2 = new List<Node>();
            nodes2.Add(n1);
            nodes2.Add(n2);
            nodes2.Add(n3);
            nodes2.Add(n4);
            nodes2.Add(n5);
            nodes2.Add(n6);
            nodes2.Add(n7);
            Graph graph2 = new Graph(nodes2);

            Dijkstra dijkstra2 = new Dijkstra(graph2);

            Console.WriteLine("Задание 6. Алгоритм Дейкстры:\n");
            dijkstra2.Start(n1);



            Console.ReadKey();
        }
    }
}
