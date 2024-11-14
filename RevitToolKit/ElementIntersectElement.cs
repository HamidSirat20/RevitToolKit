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
    public class ElementIntersectElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            // Collect cable trays
            //FilteredElementCollector cableTrayCollector = new FilteredElementCollector(doc)
            //    .OfCategory(BuiltInCategory.OST_CableTray)
            //    .WhereElementIsNotElementType();
       
            //FilteredElementCollector ele = new FilteredElementCollector(doc).OfClass(typeof(View));
            //List<BuiltInCategory> builtInCategories = new List<BuiltInCategory>() { 
            //    BuiltInCategory
            //.OST_CableTray,
            //    BuiltInCategory.OST_Walls,
            //    BuiltInCategory.OST_Windows
            //};
            //ElementMulticategoryFilter elementMulticategoryFilter = new ElementMulticategoryFilter(builtInCategories);

            //List<ElementId> elementIds = new List<ElementId>()
            //{
            //    new ElementId(123232323),
            //new ElementId(2324343)
            //};
            //ExclusionFilter exclusionFilter = new ExclusionFilter(elementIds);
            //VisibleInViewFilter visibleInViewFilter = new VisibleInViewFilter(doc, new ElementId(1212323));
            // // Select the first cable tray for intersection test
            // Element cableTray = cableTrayCollector.FirstOrDefault();
            //if (cableTray == null)
            //{
            //    message = "No cable tray found in the document.";
            //    return Result.Failed;
            //}

            //// Collect walls
            //FilteredElementCollector wallsCollector = new FilteredElementCollector(doc)
            //    .OfCategory(BuiltInCategory.OST_Walls)
            //    .WhereElementIsNotElementType();

            //// Create an intersection filter
            //ElementIntersectsElementFilter intersectionFilter = new ElementIntersectsElementFilter(cableTray);

            //// Find walls intersecting with the cable tray
            //IEnumerable<Element> intersectingWalls = wallsCollector.WherePasses(intersectionFilter).ToElements();

            //if (!intersectingWalls.Any())
            //{
            //    TaskDialog.Show("Intersection Check", "No walls intersect with the selected cable tray.");
            //}
            //else
            //{
            //    foreach (var wall in intersectingWalls)
            //    {
            //        TaskDialog.Show("Intersection", $"Cable tray intersects with Wall ID: {wall.Id}");
            //    }
            //}

            ChangeSelection(uidoc);

            return Result.Succeeded;
        }

        private void ChangeSelection(UIDocument uidoc)
        {
            // Get selected elements from current document.
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

            // Display current number of selected elements
            TaskDialog.Show("Revit", "Number of selected elements: " + selectedIds.Count.ToString());

            // Go through the selected items and filter out walls only.
            ICollection<ElementId> selectedWallIds = new List<ElementId>();

            foreach (ElementId id in selectedIds)
            {
                Element elements = uidoc.Document.GetElement(id);
                if (elements is Wall)
                {
                    selectedWallIds.Add(id);
                }
            }

            // Set the created element set as current select element set.
            uidoc.Selection.SetElementIds(selectedWallIds);

            // Give the user some information.
            if (0 != selectedWallIds.Count)
            {
                TaskDialog.Show("Revit", selectedWallIds.Count.ToString() + " Walls are selected!");
            }
            else
            {
                TaskDialog.Show("Revit", "No Walls have been selected!");
            }
        }

        private void GetParameters( Element element)
        {
            element.get_Parameter(BuiltInParameter.WALKTHROUGH_FRAMES_COUNT);

            element.GetParameter(ParameterTypeId.AllModelInstanceComments);

            element.LookupParameter("Comments");
            
            foreach(Parameter ele in element.ParametersMap)
            {
                string Name = ele.Definition.Name;
      
            }
        }



    }
}
