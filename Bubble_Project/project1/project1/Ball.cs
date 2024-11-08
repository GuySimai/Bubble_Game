//Guy Simai
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
    public class Ball
    {
        private PointF Position { get; set; }
        private float Size { get; set; }
        private Color Color { get; set; }
        private float Dx { get; set; }
        private float Dy { get; set; }

        private static Random random = new Random();

        public Ball(PointF position, float size, Color color, float dx, float dy)
        {
            Position = position;
            Size = size;
            Color = color;
            Dx = dx;
            Dy = dy;
        }

        public void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, Position.X - Size / 2, Position.Y - Size / 2, Size, Size);
            }
        }

        public void Update(Size clientSize, int toolStripHeight)
        {
            Position = new PointF(Position.X + Dx, Position.Y + Dy);

            if (Position.X - Size / 2 <= 0)
            {
                Dy = GetRandomDirection(GetRandomSign());
                Dx = GetRandomDirection(1);
            }
            if (Position.X + Size / 2 >= clientSize.Width)
            {
                Dy = GetRandomDirection(GetRandomSign());
                Dx = GetRandomDirection(-1);
            }
            if (Position.Y - Size / 2 <= 0 + toolStripHeight)
            {
                Dx = GetRandomDirection(GetRandomSign());
                Dy = GetRandomDirection(1);
            }
            if (Position.Y + Size / 2 >= clientSize.Height)
            {
                Dx = GetRandomDirection(GetRandomSign());
                Dy = GetRandomDirection(-1);
            }
        }

        private float GetRandomDirection(int direction)
        {
            return (float)(random.NextDouble() * 4 + 1) * direction;
        }

        private int GetRandomSign()
        {
            Random random = new Random();
            return random.Next(0, 2) == 0 ? 1 : -1; 
        }

        public void stopLastBall()
        {
            Dx = 0;
            Dy = 0;
        }
    }

}
