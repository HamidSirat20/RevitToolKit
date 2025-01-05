using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitToolKit.UI
{
    public partial class SmartSelection : Window
    {
        private Document document;
        private UIDocument uIDocument;
        private ObservableCollection<Element> elements;

        public SmartSelection(Document doc,UIDocument uidoc)
        {
            InitializeComponent();
            uIDocument = uidoc;
            document = doc;

            elements = new ObservableCollection<Element>();
            this.DataContext = elements; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IList<Element> elementsList = new FilteredElementCollector(document)
                               .WhereElementIsNotElementType()
                               .ToElements();


            foreach (Element element in elementsList)
            {
                elements.Add(element); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Element selectedElement = MyComboBox.SelectedItem as Element;

            if (selectedElement != null)
            {
                MessageBox.Show($"Selected Element: {selectedElement.Name}");


                Category selectedCategory = selectedElement.Category;

                if (selectedCategory != null)
                {
                    var elementsOfSameCategory = new FilteredElementCollector(document)
                                                 .OfCategory((BuiltInCategory)selectedCategory.Id.IntegerValue)
                                                 .WhereElementIsNotElementType()
                                                 .ToElements();
                    UIDocument uidoc = new UIDocument(document);
                    ICollection<ElementId> elementIds = elementsOfSameCategory.Select(el => el.Id).ToList();
                    uidoc.Selection.SetElementIds(elementIds);

                    MessageBox.Show($"Selected {elementIds.Count} elements of type {selectedCategory.Name}.");
                }
                else
                {
                    MessageBox.Show("The selected element does not have a valid category.");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("No element selected.");
            }
        }

    }
}
