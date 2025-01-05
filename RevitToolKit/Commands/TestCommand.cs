using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Linq;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class TestCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            var collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls);

            var familyInstance = new ElementClassFilter(typeof(FamilyInstance));
            var door = new ElementCategoryFilter(BuiltInCategory.OST_Doors);

            var logical = new LogicalAndFilter(familyInstance, door);
         
            collector.WherePasses(logical);
            TaskDialog taskDialog = new TaskDialog("sELECT");
            TaskDialog.Show("ELEMENTS",collector.ToString());
         return Result.Succeeded;
        }
      
    }

}
