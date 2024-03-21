using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System.Windows;
using Autodesk.Revit.UI.Events;
using System.Windows.Forms;

namespace RevitToolkit
{
    [Transaction(TransactionMode.Manual)]
    public class Commands : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var app = uiapp.Application;
            var doc = uidoc.Document;

            Level level = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType()
                .Cast<Level>()
                .First(x => x.Name == "Ground Floor");

            XYZ p1 = uidoc.Selection.PickPoint("Pick a point");
            XYZ p2 = uidoc.Selection.PickPoint("Pick a point");
            XYZ p3 = uidoc.Selection.PickPoint("Pick a point");
            XYZ p4 = uidoc.Selection.PickPoint("Pick a point");
            XYZ p5 = uidoc.Selection.PickPoint("Pick a point");

            List<Curve> curves = new List<Curve>();
            Line line1 = Line.CreateBound(p1, p2);
            Arc arc = Arc.Create(p2, p4, p3);
            Line line2 = Line.CreateBound(p4, p5);
            Line line3 = Line.CreateBound(p5, p1);
            curves.Add(line1);
            curves.Add(arc);
            curves.Add(line2);
            curves.Add(line3);



            try
            {
                using (Transaction trans = new Transaction(doc, "Create a Wall"))
                {
                    trans.Start();
                    foreach (var item in curves)
                    {
                        Wall.Create(doc, item, level.Id, false);
                    }
                    trans.Commit();
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;

            }
        }
    }
}
