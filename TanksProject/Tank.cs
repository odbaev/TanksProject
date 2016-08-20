using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace TanksProject
{
    public class Tank
    {
        public static int Speed { get; set; }

        private Label form;

        public Size Size { get; }
        public Point Location { get; private set; }

        public enum TankDirection { Left, Right, Up, Down};
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

        public void Move()
        {
            switch(Direction)
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
