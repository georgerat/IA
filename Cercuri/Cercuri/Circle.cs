using System.Drawing;

namespace Cercuri
{
    public class Circle
    {
        public Point center;
        public static int radius = 10;

        public Circle()
        {
            center = new Point(Engine.rnd.Next(Engine.pictureBox.Width), Engine.rnd.Next(Engine.pictureBox.Height), Color.Blue);
        }

        public Circle(Point center)
        {
            this.center = center;
        }

        public void Draw()
        {
            Engine.graphics.FillEllipse(new SolidBrush(center.color), center.x - Engine.pointSize / 2, center.y - Engine.pointSize / 2, Engine.pointSize, Engine.pointSize);
            Engine.graphics.DrawEllipse(new Pen(center.color, 2), center.x - radius, center.y - radius, radius * 2, radius * 2);
        }
    }
}
