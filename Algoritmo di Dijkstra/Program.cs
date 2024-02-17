using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmo_di_Dijkstra
{
    internal class Program
    {
        class Dijkstra
        {
            public static void EseguiDijkstra(int[,] grafo, int numeroNodi, int nodoPartenza, int nodoArrivo)
            {
                int[] distanza = new int[numeroNodi];
                int[] precedente = new int[numeroNodi];
                bool[] visitato = new bool[numeroNodi];

                for (int i = 0; i < numeroNodi; i++)
                {
                    distanza[i] = int.MaxValue;
                    precedente[i] = -1;
                    visitato[i] = false;
                }

                distanza[nodoPartenza] = 0;

                for (int count = 0; count < numeroNodi - 1; count++)
                {
                    int u = TrovaMinimaDistanza(distanza, visitato);
                    visitato[u] = true;

                    for (int v = 0; v < numeroNodi; v++)
                    {
                        if (!visitato[v] && grafo[u, v] > 0)
                        {
                            int nuovaDistanza = distanza[u] + grafo[u, v];

                            if (nuovaDistanza < distanza[v])
                            {
                                distanza[v] = nuovaDistanza;
                                precedente[v] = u;
                            }
                        }
                    }
                }

                Console.WriteLine($"\nDistanza minima dal nodo {(char)(nodoPartenza + 65)} al nodo {(char)(nodoArrivo + 65)}: {distanza[nodoArrivo]}");
            }

            private static int TrovaMinimaDistanza(int[] distanza, bool[] visitato)
            {
                int minimaDistanza = int.MaxValue;
                int minimoNodo = -1;

                for (int nodo = 0; nodo < distanza.Length; nodo++)
                {
                    if (!visitato[nodo] && distanza[nodo] < minimaDistanza)
                    {
                        minimaDistanza = distanza[nodo];
                        minimoNodo = nodo;
                    }
                }

                return minimoNodo;
            }
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int numeroNodi = rnd.Next(3, 10);
            int[,] matrice;
            int ascii = 65;
            Console.WriteLine($"Il numero di nodi è {numeroNodi}");
            matrice = GeneratoreMatrice(numeroNodi);
            Console.WriteLine($"Il peso dei nodi è:");
            Console.Write("\n \t");
            for (int i = 0; i < numeroNodi; i++)
            {
                Console.Write($"{(char)(ascii + i)}\t");
            }
            for (int i = 0; i < numeroNodi; i++)
            {
                Console.WriteLine("\n");
                Console.Write($"{(char)(ascii + i)}\t");

                for (int j = 0; j < numeroNodi; j++)
                {
                    Console.Write($"{matrice[i, j]} \t");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Inserisci il nodo di partenza:");
            char nodoPartenzaLet = Convert.ToChar(Console.ReadLine());
            int nodoPartenza = (int)nodoPartenzaLet - ascii;
            Console.WriteLine("Inserisci il nodo di arrivo:");
            char nodoArrivoLet = Convert.ToChar(Console.ReadLine());
            int nodoArrivo = (int)nodoArrivoLet - ascii;       
            Dijkstra.EseguiDijkstra(matrice, numeroNodi, nodoPartenza, nodoArrivo);
            Console.ReadKey();
        }

        static int[,] GeneratoreMatrice(int dimensione)
        {
            Random rnd = new Random();
            int[,] matrice = new int[dimensione, dimensione];

            for (int i = 0; i < dimensione; i++)
            {
                for (int j = 0; j < dimensione; j++)
                {
                    int n = rnd.Next(-1, 9);
                    while (n == 0 && i != j)
                    {
                        n = rnd.Next(-1, 9);
                    }
                    if (i == j)
                    {
                        matrice[i, i] = 0;
                    }
                    else
                    {
                        matrice[i, j] = n;
                        matrice[j, i] = n;
                    }
                }

                matrice[i, i] = 0;
            }

            return matrice;
        }
    }
}
