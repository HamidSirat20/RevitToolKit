using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
        public class DimentionELements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uiDoc = uiapp.ActiveUIDocument;
            var doc = uiDoc.Document;
           

            Reference ref1 = uiDoc.Selection.PickObject(ObjectType.Element, "Please select the first element.");
            Reference ref2 = uiDoc.Selection.PickObject(ObjectType.Element, "Please select the second element.");

            Element elem1 = doc.GetElement(ref1);
            Element elem2 = doc.GetElement(ref2);

            LocationCurve curve1 = elem1.Location as LocationCurve;
            LocationCurve curve2 = elem2.Location as LocationCurve;

            XYZ point1 = (curve1.Curve.GetEndPoint(0) + curve1.Curve.GetEndPoint(1)) / 2;
            XYZ point2 = (curve2.Curve.GetEndPoint(0) + curve2.Curve.GetEndPoint(1)) / 2;

            ReferenceArray refArray = new ReferenceArray();
                       
            refArray.Append(ref1);
            refArray.Append(ref2);

            using (Transaction trans = new Transaction(doc, "Create Quick Dimension"))
            {
                trans.Start();

                XYZ direction = point2 - point1;
                XYZ normDirection = direction.Normalize();
                XYZ offset = new XYZ(0, -10, 0); // Example offset
                Line dimLine = Line.CreateBound(point1 + offset, point2 + offset);

                View activeView = doc.ActiveView;
                Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);

                trans.Commit();
            }


            return Result.Succeeded;
        }
    }
}
