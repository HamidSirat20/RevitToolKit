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

    public class InverseSelection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;


            FilteredElementCollector collector = new FilteredElementCollector(doc);

            var windows = new ElementCategoryFilter(BuiltInCategory.OST_Windows,true);
        
            var elementId = collector.OfClass(typeof(Wall)).ToElementIds();
            
            ExclusionFilter exclusionFilter = new ExclusionFilter(elementId);

            LogicalAndFilter logicalAndFilter = new LogicalAndFilter(windows, exclusionFilter);

            var ele = collector.WherePasses(logicalAndFilter).WhereElementIsNotElementType().ToElements();
            AreaFilter areaFilter = new AreaFilter();

            BuiltInParameter builtInParameter = BuiltInParameter.ROOM_AREA;
            
            TaskDialog.Show("walls ", ele.Count().ToString());

            return Result.Succeeded;
        }
       
    }
}
