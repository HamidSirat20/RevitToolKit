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
    public class ChangeDiplayGraphic : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = doc.ActiveView;

            using (Transaction trans = new Transaction(doc, "Change View Graphics"))
            {
                trans.Start();

                // Change the display style to Shaded with Edges
                view.DisplayStyle = DisplayStyle.ShadingWithEdges;
                view.DetailLevel = ViewDetailLevel.Medium;
                // Set the detail level to Fine
                view.DetailLevel = ViewDetailLevel.Fine;
              

                // Commit the transaction
                trans.Commit();
            }



            return Result.Succeeded;
        }
    }
}
