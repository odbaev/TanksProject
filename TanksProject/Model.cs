using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace TanksProject
{
    public class Model
    {
        private List<Tank> tanks;
        private List<Button> obstacles;
        private Grid grid;

        public Model(Grid grid)
        {
            tanks = grid.Children.OfType<Label>().Select(label => new Tank(label)).ToList<Tank>();
            obstacles = grid.Children.OfType<Button>().ToList<Button>();
            this.grid = grid;
        }
    }
}
