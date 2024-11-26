using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace RevitToolKit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            Reference faceReference = uidoc.Selection.PickObject(ObjectType.Element, "Select an element.");
            Element element = doc.GetElement(faceReference);
           
            if (element != null)
            {
                string s1 = element.LookupParameter("Comments").AsString();
                string s2 = element.GetParameter(ParameterTypeId.AllModelInstanceComments).AsString();
                string s3 = element.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString();
                

                TaskDialog.Show("show 1: ", s1);
                TaskDialog.Show("show 2: ", s2);
                TaskDialog.Show("show 3: ", s3);

            }
            var e = element.Parameters;
            foreach (var item in e)
            {
                TaskDialog.Show(": ", item.ToString());

            }

            return Result.Succeeded;
        }


    }


}
