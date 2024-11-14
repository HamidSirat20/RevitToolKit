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
    public class RotateElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;


            try
            {
                var element = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                XYZ point1 = uidoc.Selection.PickPoint();
                XYZ point2 = uidoc.Selection.PickPoint();
                Line axis = Line.CreateBound(point1, point2);

                var elementId = element.ElementId;

                if (element == null)
                {
                    return Result.Failed;
                }

           
                using (Transaction trans = new Transaction(doc, "Rotate Element"))
                {
                    trans.Start();
                    ElementTransformUtils.RotateElement(doc, elementId, axis,Math.PI/3.0);
                    trans.Commit();
                    return Result.Succeeded;
                }
            }
            catch
            {
                return Result.Failed;
            }
            
        }
    }
}
