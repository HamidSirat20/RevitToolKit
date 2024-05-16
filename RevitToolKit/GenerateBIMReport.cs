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
    public class GenerateBIMReport : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> walls = collector.OfClass(typeof(Wall)).ToElements();

            List<string> reportLines = new List<string> { "ElementId,Type,Width,Height" };

            foreach (Element wall in walls)
            {
                WallType wallType = doc.GetElement(wall.GetTypeId()) as WallType;
                double width = wallType.Width;
                double height = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();
                reportLines.Add($"{wall.Id},{wallType.Name},{width},{height}");
            }

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BIMReport.txt");
            File.WriteAllLines(filePath, reportLines);

            TaskDialog.Show("Generate BIM Report", "Report generated at " + filePath);
            return Result.Succeeded;
        }
    }
}
