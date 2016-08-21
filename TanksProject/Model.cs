using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Threading;

namespace TanksProject
{
    public class Model
    {
        private List<Tank> tanks;
        private List<Rect> obstacles;
        private Grid grid;

        public Model(Grid grid)
        {
            tanks = grid.Children.OfType<Label>().Select(label => new Tank(label)).ToList<Tank>();

            obstacles = new List<Rect>();
            foreach (Button obstacle in grid.Children.OfType<Button>())
            {
                Point location = obstacle.TranslatePoint(new Point(0, 0), grid);
                obstacles.Add(new Rect(location, obstacle.RenderSize));
            }

            this.grid = grid;

            if (!IsValidInitialState())
            {
                throw new Exception("Invalid initial state!");
            }
        }

        private bool IsValidInitialState()
        {
            List<Rect> rects = new List<Rect>(obstacles);

            foreach (Tank tank in tanks)
            {
                rects.Add(new Rect(tank.Location, tank.Size));
            }

            for (int i = 0; i < rects.Count - 1; i++)
            {
                for (int j = i + 1; j < rects.Count; j++)
                {
                    if (rects[i].IntersectsWith(rects[j])) return false;
                }
            }

            foreach (Rect rect in rects)
            {
                if (rect.Left <= 0 || rect.Right >= (int)grid.ActualWidth ||
                    rect.Top <= 0 || rect.Bottom >= (int)grid.ActualHeight) return false;
            }

            return true;
        }

        public void Start()
        {
            for (;;)
            {
                foreach (Tank tank in tanks)
                {
                    tank.Move(tanks, obstacles, grid);
                }

                if (Tank.Speed == 0) return;

                Thread.Sleep(1000 / Tank.Speed);
            }
        }
    }
}
