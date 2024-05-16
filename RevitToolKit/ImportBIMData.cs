using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    public class ImportBIMData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BIMData.csv");
            if (!File.Exists(filePath))
            {
                TaskDialog.Show("Import BIM Data", "CSV file not found.");
                return Result.Failed;
            }

            List<string> csvLines = File.ReadAllLines(filePath).ToList();
            using (Transaction trans = new Transaction(doc, "Import BIM Data"))
            {
                trans.Start();
                foreach (string line in csvLines.Skip(1)) // Skip header
                {
                    string[] values = line.Split(',');
                    ElementId elementId = new ElementId(int.Parse(values[0]));
                    Wall wall = doc.GetElement(elementId) as Wall;
                    if (wall != null)
                    {
                        double height = double.Parse(values[3]);
                        wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).Set(height);
                    }
                }
                trans.Commit();
            }
            TaskDialog.Show("Import BIM Data", "Data imported successfully.");
            return Result.Succeeded;
        }
    }
}
