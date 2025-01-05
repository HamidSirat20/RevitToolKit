using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitToolKit.UI;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    public class SmartSelect : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            SmartSelection select = new SmartSelection(doc,uidoc);
            select.ShowDialog();
            return Result.Succeeded;
        }
    }
}
