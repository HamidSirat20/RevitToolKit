using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    public class ValidateBIMData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> walls = collector.OfClass(typeof(Wall)).ToElements();

            List<string> missingData = new List<string>();

            foreach (Element wall in walls)
            {
                Parameter heightParam = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
                if (heightParam == null || !heightParam.HasValue)
                {
                    missingData.Add($"Wall ID {wall.Id} is missing height parameter.");
                }
            }

            if (missingData.Count > 0)
            {
                TaskDialog.Show("Validate BIM Data", string.Join("\n", missingData));
                return Result.Failed;
            }
            else
            {
                TaskDialog.Show("Validate BIM Data", "All data is valid.");
                return Result.Succeeded;
            }
        }
    }
}
