using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opt_Oblig_2{

    class Opt_Oblig_2{

        static void Main(string[] args){

			Console.WriteLine("enter a number: ");
			int n = Convert.ToInt32(Console.ReadLine());

			int[] max_connections = new int[n];
			int[] fitness = new int[4];
			int[,] graph_of_connections = new int[n, n];

			char[] first_parent = new char[n];
			char[] second_parent = new char[n];
			char[] first_child = new char[n];
			char[] second_child = new char[n];

			fillNodesWithColor(n, first_parent, second_parent);

			printArray(n, first_parent);
			printArray(n, second_parent);

			fillRandomGraph(n, max_connections, graph_of_connections);

			printArray(n, graph_of_connections);
			printArray(n, max_connections);

			crossover(n, first_parent, second_parent, first_child, second_child);

			printArray(n, first_child);
			printArray(n, second_child);

			Console.Read();

		}

		static void crossover(int n, char[] parent_1, char[] parent_2, char[] child_1, char[] child_2) {
			Random rnd = new Random();

			int start = 0, end = 0;
			int first_point, second_point;

			first_point = rnd.Next(0, n);
			second_point = rnd.Next(0, n);
			while (second_point == first_point)
				second_point = rnd.Next(0, n);
			Console.WriteLine("first: " + first_point + "second: " + second_point);
			if (first_point < second_point) {
				start = first_point;
				end = second_point;
			}
			else {
				start = second_point;
				end = first_point;
			}

			for(int i = 0; i < n; i++) {
				if (i >= start && i <= end) {
					child_1[i] = parent_2[i];
					child_2[i] = parent_1[i];
				}
				else {
					child_1[i] = parent_1[i];
					child_2[i] = parent_2[i];
				}
			}

		}

		static void mutation() {

		}

		static void findFitness() {

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
					random = rnd.Next(0, n);
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

		static void fillNodesWithColor(int n, char[] parent_1, char[] parent_2 ) {
			Random rnd = new Random();
			char[] colors = {'w', 'b', 'r'};

			for (int i = 0; i < n; i++) { 
				parent_1[i] = colors[rnd.Next(0, 3)]; //0=white, 1=black and 2 = red
				parent_2[i] = colors[rnd.Next(0, 3)];
			}
		}

		static void printArray(int n, char[] c) {
			for (int i = 0; i < n; i++) {
				Console.Write(c[i]);
			}
			Console.WriteLine();
		}

		static void printArray(int n, int[] c) {
			for(int i = 0; i < n; i++) {
				Console.WriteLine(c[i]);
			}
		}

		static void printArray(int n, int[,] graph) {
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					Console.Write(graph[i, j]);
				}
				Console.WriteLine();
			}
		}
    }
}
