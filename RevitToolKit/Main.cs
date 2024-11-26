using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI.Selection;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            TaskDialog td = new TaskDialog("Filter Selection");
            td.MainInstruction = "Choose the type of elements to filter";
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Walls");
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Windows");
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink3, "Doors");

            TaskDialogResult result = td.Show();

            string selectedType = "";
            if (result == TaskDialogResult.CommandLink1) selectedType = "Walls";
            else if (result == TaskDialogResult.CommandLink2) selectedType = "Windows";
            else if (result == TaskDialogResult.CommandLink3) selectedType = "Doors";
            

            ICollection<Element> selectionIds = uidoc.Selection.PickElementsByRectangle("Pick Elements by rectangle");

            ICollection<ElementId> selectedWallIds = new List<ElementId>();

            foreach (Element element in selectionIds)
            {
                if (selectedType == "Walls" && element is Wall)
                {
                    selectedWallIds.Add(element.Id);
                }
                else if (selectedType == "Windows" && element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows)
                {
                    selectedWallIds.Add(element.Id);
                }
                else if (selectedType == "Doors" && element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Doors)
                {
                    selectedWallIds.Add(element.Id);
                }
            }

            if (selectedWallIds.Any())
            {
                uidoc.Selection.SetElementIds(selectedWallIds);
            }
            else
            {
                TaskDialog.Show("Selection Update", "No walls were found in the selection.");
            }


            return Result.Succeeded;
        }
    }
}
