using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cercuri
{
    public static class Engine
    {
        public static Random rnd;

        public static PictureBox pictureBox;
        public static ListBox listBox;
        public static Label maxPoints;

        public static Bitmap bitmap;
        public static Graphics graphics;

        public static int pointSize = 5;
        public static int numPoints = 100;
        public static int numCircles = 6;
        public static double mutationRate = 0.01; // 0.01 -> 1%, 1 -> 100%

        public static int N = 1000; // numarul populatiilor
        public static int K = 100; // cele mai bune populatii

        public static List<Point> points = new List<Point>();

        public static List<Sol> population = new List<Sol>();

        public static void Init()
        {
            rnd = new Random();

            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);

            maxPoints.Text = "0";
        }

        public static void Do()
        {
            Task.Run(async () =>
            {
                GeneratePopulation();

                for (int generation = 0; generation < N; generation++)
                {
                    graphics.Clear(Color.White);
                    SortPopulation();
                    DrawPoints();
                    population[0].Draw();
                    pictureBox.Image = bitmap;

                    maxPoints.Invoke((MethodInvoker)delegate
                    {
                        maxPoints.Text = population[0].CalculateFitness().ToString();
                    });

                    listBox.Invoke((MethodInvoker)delegate {
                        listBox.Items.Add($"Generația {generation}: Cel mai bun punctaj = {population[0].CalculateFitness()}");
                        listBox.SelectedIndex = listBox.Items.Count - 1;
                    });

                    List<Sol> temp = new List<Sol>();

                    for (int i = 0; i < N; i++)
                    {
                        Sol parent = population[rnd.Next(K)];
                        Sol child = population[i].Crossover(parent);

                        if (rnd.NextDouble() < mutationRate)
                            child.Mutate();

                        temp.Add(child);
                    }

                    for (int i = 0; i < N; i++)
                        population[i] = temp[i];

                    await Task.Delay(1);
                }
            });
        }

        public static void SortPopulation()
        {
            population.Sort((Sol A, Sol B) => B.CalculateFitness().CompareTo(A.CalculateFitness()));
        }

        public static void GeneratePopulation()
        {
            for (int i = 0; i < N; i++)
                population.Add(new Sol());
        }

        public static void GeneratePoints()
        {
            for (int i = 0; i < numPoints; i++)
            {
                int x = rnd.Next(pictureBox.Width);
                int y = rnd.Next(pictureBox.Height);

                points.Add(new Point(x, y, Color.Black));
            }

            DrawPoints();
        }

        public static void DrawPoints()
        {
            for (int i = 0; i < numPoints; i++)
                points[i].Draw();
        }
    }
}
