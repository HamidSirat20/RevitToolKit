using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    public class TestClass : IExternalCommand
    {        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                // Delete selected elements

                ICollection<Autodesk.Revit.DB.ElementId> ids =
                    doc.Delete(uidoc.Selection.GetElementIds());
               
                TaskDialog taskDialog = new TaskDialog("Revit");
                taskDialog.MainContent =
                    ("Click Yes to return Succeeded. Selected members will be deleted.\n" +
                    "Click No to return Failed.  Selected members will not be deleted.\n" +
                    "Click Cancel to return Cancelled.  Selected members will not be deleted.");
                TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Yes |
                    TaskDialogCommonButtons.No | TaskDialogCommonButtons.Cancel;
                taskDialog.CommonButtons = buttons;
                TaskDialogResult taskDialogResult = taskDialog.Show();

                if (taskDialogResult == TaskDialogResult.Yes)
                {
                    return Autodesk.Revit.UI.Result.Succeeded;
                }
                else if (taskDialogResult == TaskDialogResult.No)
                {
                    ICollection<ElementId> selectedElementIds = uidoc.Selection.GetElementIds();
                    foreach (ElementId id in selectedElementIds)
                    {
                        elements.Insert(doc.GetElement(id));
                    }
                    message = "Failed to delete selection.";
                    return Autodesk.Revit.UI.Result.Failed;
                }
                else
                {
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
            }
            catch
            {
                message = "Unexpected Exception thrown.";
                return Autodesk.Revit.UI.Result.Failed;
            }


        }


    }
}
