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

            FillNodesWithColor(n, first_parent, second_parent);

			Console.WriteLine("first parent: ");  
			PrintArray(n, first_parent);
			Console.WriteLine("second parent: ");
			PrintArray(n, second_parent);

            FillRandomGraph(n, max_connections, graph_of_connections);

            PrintArray(n, graph_of_connections);
            //PrintArray(n, max_connections);

            Crossover(n, first_parent, second_parent, first_child, second_child);
			Console.WriteLine("first child: ");
			PrintArray(n, first_child);
			Console.WriteLine("second child: ");
			PrintArray(n, second_child);

			WillTheOffspringMutate(n, first_child);
			WillTheOffspringMutate(n, second_child);

			ChooseOffspring(n, first_parent, second_parent, first_child, second_child, graph_of_connections);

			Console.WriteLine("new parent: ");
			PrintArray(n, first_parent);
			Console.WriteLine("new parent: ");
			PrintArray(n, second_parent);

			Console.Read();
        }
        static void Crossover(int n, char[] parent_1, char[] parent_2, char[] child_1, char[] child_2){
            Random rnd = new Random();

            int start = 0, end = 0;
            int first_point, second_point;

            first_point = rnd.Next(0, n);
            second_point = rnd.Next(0, n);
            while (second_point == first_point)
                second_point = rnd.Next(0, n);
            //Console.WriteLine("first: " + first_point + "second: " + second_point);
            if (first_point < second_point){
                start = first_point;
                end = second_point;
            }
            else{
                start = second_point;
                end = first_point;
            }

            for (int i = 0; i < n; i++){
                if (i >= start && i <= end){
                    child_1[i] = parent_2[i];
                    child_2[i] = parent_1[i];
                }
                else{
                    child_1[i] = parent_1[i];
                    child_2[i] = parent_2[i];
                }
            }
        }

        static void WillTheOffspringMutate(int n, char[] solution){
            Random rnd = new Random();

            int chance = rnd.Next(1, 101);
            int mutation_factor = 10;

            if (chance <= mutation_factor) {
				Console.WriteLine("A child has mutated, old child: ");
				PrintArray(n, solution);
				Mutation(n, solution);
				Console.WriteLine("new child: ");
				PrintArray(n, solution);
            }

        }

        static void Mutation(int n, char[] solution){
            Random rnd = new Random();
            int mutated_value = rnd.Next(0, n);
            char[] colors = { 'w', 'b', 'r' };

            solution[mutated_value] = colors[rnd.Next(0, 3)];
        }

        static void ChooseOffspring(int n, char[] parent_1, char[] parent_2, char[] child_1, char[] child_2, int[,] graph){
            int[] fitness = new int[4];
            int most_satisfied = 0;
            int second_most_satisfied = 0;
            int position_of_most_satisfied = 0;
            int position_of_second_most_satisfied = 0;

            for (int i = 0; i < 4; i++){
                if (i == 0)
                    fitness[i] = FindFitness(n, parent_1, graph);
                else if (i == 1)
                    fitness[i] = FindFitness(n, parent_2, graph);
                else if (i == 2)
                    fitness[i] = FindFitness(n, child_1, graph);
                else
                    fitness[i] = FindFitness(n, child_2, graph);
            }

			Console.WriteLine("fitness: ");
			PrintArray(4, fitness);
			            
			for(int i = 0; i < 4; i++) {
				if(fitness[i] > most_satisfied) {
					second_most_satisfied = most_satisfied;
					most_satisfied = fitness[i];
					position_of_second_most_satisfied = position_of_most_satisfied;
					position_of_most_satisfied = i;
				}
				else if(fitness[i] > second_most_satisfied) {
					second_most_satisfied = fitness[i];
					position_of_second_most_satisfied = i;
				}
			}

			Console.WriteLine("satisfied: " + most_satisfied + " at position: " + position_of_most_satisfied + " second: " + second_most_satisfied + " at positin: " + position_of_second_most_satisfied);
            NewParents(n, position_of_most_satisfied, position_of_second_most_satisfied, parent_1, parent_2, child_1, child_2);

        }

        static void NewParents(int n, int most_satisfied, int second_most_satified, char[] parent_1, char[] parent_2, char[] child_1, char[] child_2){
            char[] temp_parent_1 = new char[n];
            char[] temp_parent_2 = new char[n];

            if(most_satisfied == 0){
				CopyArray(n, temp_parent_1, parent_1);
            }
			else if (most_satisfied == 1) {
				CopyArray(n, temp_parent_1, parent_2);
			}
			else if (most_satisfied == 2) {
				CopyArray(n, temp_parent_1, child_1);
			}
			else if (most_satisfied == 3) {
				CopyArray(n, temp_parent_1, child_2);
			}
			if (second_most_satified == 0) {
				CopyArray(n, temp_parent_2, parent_1);
			}
			else if (second_most_satified == 1) {
				CopyArray(n, temp_parent_2, parent_2);
			}
			else if (second_most_satified == 2) {
				CopyArray(n, temp_parent_2, child_1);
			}
			else if (second_most_satified == 3) {
				CopyArray(n, temp_parent_2, child_2);
			}

			CopyArrays(n, temp_parent_1, temp_parent_2, parent_1, parent_2);

		}

		static int FindFitness(int n, char[] solution, int[,] graph)
        {
            int satisfied = 0;
            int column = 1;

            for (int row = 0; row < n; row++)
            {
                while (column < n)
                {
                    if (graph[row, column] == 1 && solution[row] != solution[column])
                        satisfied++;
                    column++;
                }
                column = row + 2;
            }

            return satisfied;
        }

        static void FillRandomGraph(int n, int[] max_connections, int[,] graph){
            Random rnd = new Random();
            int random;
            int amount_of_connections;
            int counter = 0;

            for (int i = 0; i < n; i++){

                amount_of_connections = rnd.Next(1, 4); //number of connectiosn that a node will have, from 1 to 3
                while (counter < amount_of_connections){
                    //loop insures that a node will connect to other nodes, unless there a node can't connect to more

                    random = rnd.Next(0, n);

                    //if a node already has max connections
                    if (max_connections[i] == 3 || max_connections[random] == 3)
                        counter = 99;
                    //connect nodes as long as they haven't reached max connections and that they dont connect to itself
                    else if (max_connections[i] != 3 && max_connections[random] != 3 && i != random){
                        if (graph[i, random] == 1)
                            continue;
                        graph[i, random] = 1;
                        graph[random, i] = 1;
                        max_connections[i] = max_connections[i] + 1;
                        max_connections[random] = max_connections[random] + 1;
                        counter++;
                    }
                }
                counter = 0;    //reset counter so the while loop will work again
            }

        }

        static void FillNodesWithColor(int n, char[] parent_1, char[] parent_2)
        {
            Random rnd = new Random();
            char[] colors = { 'w', 'b', 'r' };

            for (int i = 0; i < n; i++)
            {
                parent_1[i] = colors[rnd.Next(0, 3)]; //0=white, 1=black and 2 = red
                parent_2[i] = colors[rnd.Next(0, 3)];
            }
        }

        static void PrintArray(int n, char[] c)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(c[i]);
            }
            Console.WriteLine();
        }

        static void PrintArray(int n, int[] c)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(c[i]);
            }
        }

        static void PrintArray(int n, int[,] graph) {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(graph[i, j]);
                }
                Console.WriteLine();
            }
        }

		static void CopyArray(int n, char[] table_1, char[] table_2) { 
			for(int i = 0; i < n; i++) {
				table_1[i] = table_2[i];
			}
		}

		static void CopyArrays(int n, char[] temp_table_1, char[] temp_table_2, char[] table_1, char[] table_2){
			for(int i = 0; i < n; i++) {
				table_1[i] = temp_table_1[i];
			}
			for(int i = 0; i < n; i++) {
				table_2[i] = temp_table_2[i];
			}
		}
    }
}