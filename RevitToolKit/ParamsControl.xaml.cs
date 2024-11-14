using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitToolKit
{
    /// <summary>
    /// Interaction logic for ParamsControl.xaml
    /// </summary>
    public partial class ParamsControl : Window
    {
        public string Parameters { get; private set; }
        public string Rules { get; private set; }

        public ParamsControl()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Parameters = txtParameters.Text;
            Rules = txtRules.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}
