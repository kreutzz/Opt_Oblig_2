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
			int fitness_1 = 0;

			int[] max_connections = new int[n];
			int[] fitness = new int[4];
			int[,] graph_of_connections = new int[n, n];

			char[] first_parent = new char[n];
			char[] second_parent = new char[n];
			char[] first_child = new char[n];
			char[] second_child = new char[n];

			FillNodesWithColor(n, first_parent, second_parent);

			PrintArray(n, first_parent);
			//PrintArray(n, second_parent);

			FillRandomGraph(n, max_connections, graph_of_connections);

			PrintArray(n, graph_of_connections);
			//PrintArray(n, max_connections);

			Crossover(n, first_parent, second_parent, first_child, second_child);
			fitness_1 = FindFitness(n, first_parent, graph_of_connections);
			Console.WriteLine("a fitness of " + fitness_1);

			//PrintArray(n, first_child);
			//PrintArray(n, second_child);

			Console.Read();
		}

		static void Crossover(int n, char[] parent_1, char[] parent_2, char[] child_1, char[] child_2) {
			Random rnd = new Random();

			int start = 0, end = 0;
			int first_point, second_point;

			first_point = rnd.Next(0, n);
			second_point = rnd.Next(0, n);
			while (second_point == first_point)
				second_point = rnd.Next(0, n);
			//Console.WriteLine("first: " + first_point + "second: " + second_point);
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

		static void WillItMutate() {
			Random rnd = new Random();


		}

		static void Mutation(int n, char[] solution) {
			Random rnd = new Random();
			int mutated_value = rnd.Next(0, n);
			char[] colors = { 'w', 'b', 'r' };

			solution[mutated_value] = colors[rnd.Next(0, 3)];
		}

		static int FindFitness(int n, char[] solution, int[,] graph) {
			int satisfied = 0;
			int column = 1;

			for (int row = 0; row < n; row++) {
				while(column < n) {
					if (graph[row, column] == 1 && solution[row] == solution[column])
						satisfied++;
					column++;
				}
				column = row + 2;
			}

			return satisfied;
		}
		
		static void FillRandomGraph(int n, int[] max_connections, int[,] graph) {
			Random rnd = new Random();
			int random;
			int amount_of_connections;
			int counter = 0;

			for (int i = 0; i < n; i++) {
				amount_of_connections = rnd.Next(1, 4);	//number of connectiosn that a node will have, from 1 to 3
				while (counter < amount_of_connections || counter == 0){
					//loop insures that a node will connect to other nodes, unless there a node can't connect to more

					random = rnd.Next(0, n);

					//if a node already has max connections
					if (max_connections[i] == 3 || max_connections[random] == 3)	
						counter = 99;
					//connect nodes as long as they haven't reached max connections and that they dont connect to itself
					else if (max_connections[i] != 3 && max_connections[random] != 3 && i != random) {
						if (graph[i, random] == 1)
							continue;
						graph[i, random] = 1;
						graph[random, i] = 1;
						max_connections[i] = max_connections[i] + 1;
						max_connections[random] = max_connections[random] + 1;
						counter++;
					}
				}
				counter = 0;	//reset counter so the while loop will work again
			}

		}

		static void FillNodesWithColor(int n, char[] parent_1, char[] parent_2 ) {
			Random rnd = new Random();
			char[] colors = {'w', 'b', 'r'};

			for (int i = 0; i < n; i++) { 
				parent_1[i] = colors[rnd.Next(0, 3)]; //0=white, 1=black and 2 = red
				parent_2[i] = colors[rnd.Next(0, 3)];
			}
		}

		static void PrintArray(int n, char[] c) {
			for (int i = 0; i < n; i++) {
				Console.Write(c[i]);
			}
			Console.WriteLine();
		}

		static void PrintArray(int n, int[] c) {
			for(int i = 0; i < n; i++) {
				Console.WriteLine(c[i]);
			}
		}

		static void PrintArray(int n, int[,] graph) {
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					Console.Write(graph[i, j]);
				}
				Console.WriteLine();
			}
		}
    }
}
