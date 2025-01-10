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

           View view = commandData.View;      

            TaskDialog taskDialog = new TaskDialog(view.Name);
         return Result.Succeeded;
        }
      
    }

}
