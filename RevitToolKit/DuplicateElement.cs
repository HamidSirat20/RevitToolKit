using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    public class DuplicateElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            
            try
            {
                var element = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
                var elementId = element.ElementId;
                if (element == null)
                {
                    return Result.Failed;
                }
                XYZ point = uidoc.Selection.PickPoint("Pick a point");

                using (Transaction trans = new Transaction(doc, "Duplicate Element"))
                {
                    trans.Start();
                    ElementTransformUtils.CopyElement(doc, elementId, point);
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
