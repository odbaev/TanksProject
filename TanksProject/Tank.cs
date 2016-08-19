using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TanksProject
{
    public class Tank
    {
        public static int Speed { get; set; }

        private Label form;

        public Tank(Label form)
        {
            this.form = form;
        }
    }
}
