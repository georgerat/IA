using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dispersia_unui_graf
{
    public class Sol
    {
        public List<Point> points;

        public Sol()
        {
            points = new List<Point>();

            for (int i = 0; i < Engine.n; i++)
                points.Add(new Point(Engine.rnd.Next(12, Engine.width - 12), Engine.rnd.Next(12, Engine.height - 12)));
        }

        public Sol(List<Point> points)
        {
            this.points = points;
        }

        public double CalculateFitness()
        {
            double result = 0;

            for (int i = 0; i < Engine.n - 1; i++)
                for (int j = i + 1; j < Engine.n; j++)
                    if (Engine.M[i, j] != 0)
                        result += Math.Abs(Engine.GetDistance(points[i], points[j]) - Engine.M[i, j] * Engine.zoom);

            return result;
        }

        public Sol Crossover(Sol parent)
        {
            int cut = Engine.rnd.Next(1, Engine.n - 1);
            List<Point> temp = new List<Point>();

            for (int i = 0; i < cut; i++)
                temp.Add(points[i]);

            for (int i = cut; i < Engine.n; i++)
                temp.Add(parent.points[i]);

            return new Sol(temp);
        }

        public void Mutate()
        {
            int i = Engine.rnd.Next(Engine.n);
            points[i] = new Point(Engine.rnd.Next(12, Engine.width - 12), Engine.rnd.Next(12, Engine.height - 12));
        }

        public void View()
        {
            for (int i = 0; i < Engine.n - 1; i++)
            {
                for (int j = i + 1; j < Engine.n; j++)
                {
                    if (Engine.M[i, j] != 0)
                    {
                        double distance = Math.Round(Engine.GetDistance(points[i], points[j]), 2) / Engine.zoom;
                        Engine.graphics.DrawLine(new Pen(Color.Black, 2), points[i], points[j]);
                        Engine.graphics.DrawString(distance.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), (points[i].X + points[j].X) / 2, (points[i].Y + points[j].Y) / 2);
                    }
                }
            }

            for (int i = 0; i < Engine.n; i++)
            {
                Engine.graphics.DrawEllipse(new Pen(Color.Black, 4), points[i].X - 12, points[i].Y - 12, 24, 24);
                Engine.graphics.FillEllipse(new SolidBrush(Color.White), points[i].X - 12, points[i].Y - 12, 24, 24);
                Engine.graphics.DrawString(i.ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), points[i].X - 7, points[i].Y - 8);
            }
        }
    }
}
