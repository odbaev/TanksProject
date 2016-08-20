using System;
using System.Threading.Tasks;
using System.Windows;

namespace TanksProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model model;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                model = new Model(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

            new Task(() => model.Start()).Start();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int prevSpeed = Tank.Speed;

            Tank.Speed = (int)slider.Value;

            if (prevSpeed == 0 && model != null)
            {
                new Task(() => model.Start()).Start();
            }
        }
    }
}
