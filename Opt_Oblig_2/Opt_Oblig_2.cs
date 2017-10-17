using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt_Oblig_2{

    class Opt_Oblig_2{

        static void Main(string[] args){
			int n;
			Console.WriteLine("enter a number: ");
			n = Convert.ToInt32(Console.ReadLine());

			int[] max_connections = new int[n];
			int[,] graph_of_connections = new int[n, n];

			//printConnections(n, max_connections);
			fillRandomGraph(n, max_connections, graph_of_connections);
			printArray(n, graph_of_connections);
			printConnections(n, max_connections);

        }

		static void fillRandomGraph(int n, int[] max_connections, int[,] graph) {
			Random rnd = new Random();
			int random;
			int amount_of_connections;
			int counter = 0;

			for (int i = 0; i < n; i++) {
				amount_of_connections = rnd.Next(1, 4);
				while (counter < amount_of_connections || counter == 0){
					//Console.WriteLine("test");
					random = rnd.Next(1, n);
					if (max_connections[i] == 3 || max_connections[random] == 3)
						counter = 99;
					else if (max_connections[i] != 3 && max_connections[random] != 3 && i != random) {
						if (graph[i, random] == 1)
							continue;
						graph[i, random] = 1;
						graph[random, i] = 1;
						max_connections[i] = max_connections[i] + 1;
						max_connections[random] = max_connections[random] + 1;
						counter++;
						//Console.WriteLine("test2");
					}
				}
				counter = 0;
			}

		}

		static void printConnections(int n, int[] c) {
			for(int i = 0; i < n; i++) {
				Console.WriteLine(c[i]);
			}
			Console.Read();
		}

		static void printArray(int n, int[,] graph) {
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					Console.Write(graph[i, j]);
				}
				Console.WriteLine();
			}
			//Console.Read();
		}
    }
}
