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
using Autodesk.Revit.DB;

namespace RevitToolKit
{
    /// <summary>
    /// Interaction logic for FilterForm.xaml
    /// </summary>
    public partial class FilterForm : Window
    {
        Document Doc;
        public FilterForm(Document doc)
        {
            InitializeComponent();
            Doc = doc;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            var categories = collector
                           .WhereElementIsNotElementType() 
                           .Select(element => element.Category) 
                           .Where(category => category != null) 
                           .Distinct();
            foreach (var item in categories)
            {
                ElementType.Items.Add(item.Name);
            }
        }

        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            var categories = collector
                           .WhereElementIsNotElementType()
                           .Select(element => element.Category)
                           .Where(category => category != null)
                           .Distinct();
            string selectedCategory = this.ElementType.SelectedItem.ToString();
            Category setCategory = null;

            foreach (Category item in categories)
            {
                if (item.Name == selectedCategory) 
                {
                    setCategory = item;
                }
            }

          using (Transaction tran = new Transaction(Doc, "Set Filter"))
            {
                tran.Start();

            }

        }
    }
}
