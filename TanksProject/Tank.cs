using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Tank(Label form)
        {
            this.form = form;
            
            Size = new Size(form.ActualWidth, form.ActualHeight);
            Location = new Point(form.Margin.Left, form.Margin.Top);
        }
    }
}
