using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private matlab_plot.MP _matlabController = new matlab_plot.MP();
        private bool _isExist = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isExist)
            {
               _isExist = false;
                MainPlot.DestroyGraph();
            }
            else
            {
                _isExist = true;
                Mouse.OverrideCursor = Cursors.Wait;
              
                //1. Call Matlab to draw plot.
                _matlabController.matlab_plot();

                //2. Build plot in your WPF Window.
                MainPlot.BuildGraph("Figure 1");

                Mouse.OverrideCursor = null;
            }
        }
    }
}
