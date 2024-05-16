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
    public class ExportBIMData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
          
            // Collect all walls in the document
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> walls = collector.OfClass(typeof(Wall)).ToElements();

            // Prepare CSV content
            List<string> csvContent = new List<string> { "ElementId,Type,Width,Height" };
            foreach (Element wall in walls)
            {
                WallType wallType = doc.GetElement(wall.GetTypeId()) as WallType;
                double width = wallType.Width;
                double height = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();
                csvContent.Add($"{wall.Id},{wallType.Name},{width},{height}");
            }

            // Save to CSV
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BIMData.csv");
            File.WriteAllLines(filePath, csvContent);

            TaskDialog.Show("Export BIM Data", "Data exported to " + filePath);
            return Result.Succeeded;
        }
    }
}
