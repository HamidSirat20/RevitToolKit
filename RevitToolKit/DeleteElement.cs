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
      public class DeleteElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
                        
            try
            {
                var reference = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                var eleId = reference.ElementId;

                var ele = doc.GetElement(eleId);

                using (Transaction trans = new Transaction(doc, "Delete Element"))
                {

                    trans.Start();
                    Delete0ne(doc, ele);
                    trans.Commit();
                  
                    return Result.Succeeded;

                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
                return Result.Failed;
            }
           
            
        }
        /// <summary>
        /// This method require a Doc and a element to delete it.
        /// </summary>
        /// <param name="document">Revit doc</param>
        /// <param name="element">Revit element</param>
        /// <exception cref="Exception"></exception>
        private void Delete0ne(Document document, Element element)
        {
            
            ICollection<ElementId> deletedIdSet = document.Delete(element.Id);

            if (0 == deletedIdSet.Count)
            {
                throw new Exception("Deleting the selected element in Revit failed.");
            }

            String prompt = "The selected element has been removed and ";
            prompt += deletedIdSet.Count - 1;
            prompt += " more dependent elements have also been removed.";

            TaskDialog.Show("Revit", prompt);
        }
    }
}
