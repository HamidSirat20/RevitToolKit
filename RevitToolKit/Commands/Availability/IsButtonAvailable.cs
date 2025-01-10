using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitToolKit.Commands.Availability
{
    public class IsButtonAvailable : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            UIDocument uidoc = applicationData.ActiveUIDocument;
            var selectedIds = uidoc.Selection.GetElementIds();
            if (selectedIds.Count == 0)
                return false;

            Document doc = uidoc.Document;
            foreach (ElementId id in selectedIds)
            {
                Element element = doc.GetElement(id);
                if (element?.Category != null && element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Walls)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
