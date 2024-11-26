using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitToolKit
{
    public class WallFilterClass : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {

           
            if (elem.Category.Name == "Walls")
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            
            return false;
        }
    }
}
