using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace TanksProject
{
    public class Tank
    {
        public static int Speed { get; set; }

        private Label form;

        public Size Size { get; }
        public Point Location { get; private set; }

        public enum TankDirection { Left, Right, Up, Down };
        public TankDirection Direction { get; private set; }

        private static Random rand = new Random();

        public Tank(Label form)
        {
            this.form = form;

            Size = new Size(form.ActualWidth, form.ActualHeight);
            Location = new Point(form.Margin.Left, form.Margin.Top);

            TankDirection[] directions = (TankDirection[])Enum.GetValues(typeof(TankDirection));
            Direction = directions[rand.Next(directions.Length)];
        }

        private void ChangeDirection()
        {
            switch (Direction)
            {
                case TankDirection.Left:
                    Direction = TankDirection.Right;
                    break;
                case TankDirection.Right:
                    Direction = TankDirection.Left;
                    break;
                case TankDirection.Up:
                    Direction = TankDirection.Down;
                    break;
                case TankDirection.Down:
                    Direction = TankDirection.Up;
                    break;
            }
        }

        private bool HasObstacleToMove(List<Tank> tanks, List<Rect> obstacles, Grid grid)
        {
            Rect tankRect = new Rect(Location, Size);

            if (tankRect.Left == 0 || tankRect.Right == (int)grid.ActualWidth || 
                tankRect.Top == 0 || tankRect.Bottom == (int)grid.ActualHeight) return true;

            foreach (Rect rect in obstacles)
            {
                if (tankRect.IntersectsWith(rect)) return true;
            }

            foreach (Tank tank in tanks)
            {
                if (tank == this) continue;

                Rect rect = new Rect(tank.Location, tank.Size);
                
                if (tankRect.IntersectsWith(rect))
                {
                    switch (Direction)
                    {
                        case TankDirection.Left:
                            if (tankRect.Left == rect.Right)
                            {
                                if (tank.Direction == TankDirection.Right) tank.ChangeDirection();
                                return true;
                            }
                            break;
                        case TankDirection.Right:
                            if (tankRect.Right == rect.Left)
                            {
                                if (tank.Direction == TankDirection.Left) tank.ChangeDirection();
                                return true;
                            }
                            break;
                        case TankDirection.Up:
                            if (tankRect.Top == rect.Bottom)
                            {
                                if (tank.Direction == TankDirection.Down) tank.ChangeDirection();
                                return true;
                            }
                            break;
                        case TankDirection.Down:
                            if (tankRect.Bottom == rect.Top)
                            {
                                if (tank.Direction == TankDirection.Up) tank.ChangeDirection();
                                return true;
                            }
                            break;
                    }
                }
            }

            return false;
        }

        public void Move(List<Tank> tanks, List<Rect> obstacles, Grid grid)
        {
            if (HasObstacleToMove(tanks, obstacles, grid)) ChangeDirection();

            switch (Direction)
            {
                case TankDirection.Left:
                    Location = new Point(Location.X - 1, Location.Y);
                    break;
                case TankDirection.Right:
                    Location = new Point(Location.X + 1, Location.Y);
                    break;
                case TankDirection.Up:
                    Location = new Point(Location.X, Location.Y - 1);
                    break;
                case TankDirection.Down:
                    Location = new Point(Location.X, Location.Y + 1);
                    break;
            }

            form.Dispatcher.Invoke(delegate
            { form.Margin = new Thickness(Location.X, Location.Y, 0, 0); });
        }
    }
}
