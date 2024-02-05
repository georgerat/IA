using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dispersia_unui_graf
{
    public static class Engine
    {
        public static Random rnd = new Random();

        public static Bitmap bitmap;
        public static Graphics graphics;

        public static PictureBox pictureBox;
        public static ListBox listBox;
        public static Label generation;
        public static Label penalty;
        public static Label bestPenalty;
        public static Label bestGeneration;

        public static int width;
        public static int height;
        public static int zoom = 20;

        public static int n;
        public static double[,] M;

        public static int N = 1000; // numarul populatiilor
        public static int K = 100; // cele mai bune populatii 
        public static double mutationRate = 0.1; // 0.01 -> 1%, 1 -> 100%
        public static List<Sol> population;

        public static void Init()
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
        }

        public static void Do()
        {
            Task.Run(async () =>
            {
                Init();

                LoadGraph("../../Graph.in");
                ViewGraph();

                GeneratePopulation();
                SortPopulation();

                generation.Text = "0";
                penalty.Text = population[0].CalculateFitness().ToString();
                bestPenalty.Text = population[0].CalculateFitness().ToString();

                int g = 1;
                while (g <= 1000)
                {
                    graphics.Clear(Color.White);

                    population[0].View();
                    generation.Text = g.ToString();
                    penalty.Text = population[0].CalculateFitness().ToString();

                    if (population[0].CalculateFitness() < double.Parse(bestPenalty.Text))
                    {
                        bestPenalty.Text = population[0].CalculateFitness().ToString();
                        bestGeneration.Text = g.ToString();
                    }

                    pictureBox.Image = bitmap;

                    List<Sol> temp = new List<Sol>();

                    for (int i = 0; i < N; i++)
                    {
                        Sol parent = population[rnd.Next(K)];
                        Sol child = population[i].Crossover(parent);

                        if (rnd.NextDouble() <= mutationRate)
                            child.Mutate();

                        temp.Add(child);
                    }

                    for (int i = 0; i < N; i++)
                        population[i] = temp[i];

                    SortPopulation();

                    g++;

                    await Task.Delay(1);
                }
            });
        }

        public static void LoadGraph(string file)
        {
            StreamReader sr = new StreamReader(@file);

            n = int.Parse(sr.ReadLine());
            M = new double[n, n];

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                int x = int.Parse(line.Split(' ')[0]);
                int y = int.Parse(line.Split(' ')[1]);
                double distance = double.Parse(line.Split(' ')[2]);

                M[x, y] = distance;
                M[y, x] = distance;
            }
        }

        public static void ViewGraph()
        {
            for (int i = 0; i < n; i++)
            {
                string line = "";
                for (int j = 0; j < n; j++)
                    line += $"{M[i, j]} ";
                listBox.Items.Add(line);
            }
            listBox.Items.Add("");
        }

        public static void GeneratePopulation()
        {
            population = new List<Sol>();

            for (int i = 0; i < N; i++)
                population.Add(new Sol());
        }

        public static void SortPopulation()
        {
            population.Sort((Sol A, Sol B) => A.CalculateFitness().CompareTo(B.CalculateFitness()));
        }

        public static double GetDistance(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }
    }
}
