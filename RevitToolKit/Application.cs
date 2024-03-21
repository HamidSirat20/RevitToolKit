using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System.Reflection;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.DB.Visual;
using System.Windows.Media.Imaging;

namespace RevitToolKit
{
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel ribbonPanel = RibbonPanel(application);
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            if (ribbonPanel.AddItem(new PushButtonData("Delete Element", "Delete", assemblyPath, "RevitToolKit.DeleteElement")) is PushButton button)
            {
                button.ToolTip = "Delete Element";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "delete.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;
            }
            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication application)
        {
            string tabName = "RevitWizard";
            RibbonPanel ribbonPanel = null;
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            try
            {
                application.CreateRibbonPanel(tabName, "Edit Tab");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            List<RibbonPanel> ribbonPanels = application.GetRibbonPanels(tabName);
            foreach (RibbonPanel panel in ribbonPanels.Where(p => p.Name == "Edit Tab"))
            {
                ribbonPanel = panel;
            }
            return ribbonPanel;
        }
    }
}
