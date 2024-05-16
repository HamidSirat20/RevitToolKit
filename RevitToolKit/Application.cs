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
using Autodesk.Revit.Attributes;

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
            //can enable or disable the panel
           //ribbonPanel.Enabled = false;
           //will hide or make visible the panel
           // ribbonPanel.Visible = true;
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            if (ribbonPanel.AddItem(new PushButtonData("Delete Element", "Delete", assemblyPath, "RevitToolKit.DeleteElement")) is PushButton deleteButton)
            {
                deleteButton.ToolTip = "Delete Element";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "delete.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                deleteButton.LargeImage = bitmap;
            }

            ribbonPanel.AddSeparator();
           
            if (ribbonPanel.AddItem(new PushButtonData("Duplicate Element", "Duplicate", assemblyPath, "RevitToolKit.DuplicateElement")) is PushButton infoButton)
            {
                infoButton.ToolTip = "Duplicate element";
                ContextualHelp contextualHelp = new ContextualHelp(ContextualHelpType.Url, "https://eshaqzada.netlify.app/");
                infoButton.SetContextualHelp(contextualHelp);
                Uri uriInfo = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "duplicate.gif"));
                BitmapImage bitmapInfo = new BitmapImage(uriInfo);
                infoButton.LargeImage = bitmapInfo;
            }

            ribbonPanel.AddSeparator();

            if (ribbonPanel.AddItem(new PushButtonData("Dimention Element", "Dimention", assemblyPath, "RevitToolKit.DimentionELements")) is PushButton dimButton)
            {
                dimButton.ToolTip = "Duplicate element";
                Uri uriInfo = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "dimention.png"));
                BitmapImage bitmapInfo = new BitmapImage(uriInfo);
                dimButton.LargeImage = bitmapInfo;
            }

            ribbonPanel.AddSeparator();
            if (ribbonPanel.AddItem(new PushButtonData("Test1", "Test1", assemblyPath, "RevitToolKit.Test")) is PushButton btnCommand)
            {
                btnCommand.ToolTip = "Reverse Elements";
                Uri uriInfo = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "toolkit.png"));
                BitmapImage bitmapInfo = new BitmapImage(uriInfo);
                btnCommand.LargeImage = bitmapInfo;
            }

            ribbonPanel.AddSeparator();

            if (ribbonPanel.AddItem(new PushButtonData("Export BIM Data", "Export", assemblyPath, "RevitToolKit.ExportBIMData")) is PushButton exportButton)
            {
                exportButton.ToolTip = "Export BIM data to CSV";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "export.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                exportButton.LargeImage = bitmap;
            }

            ribbonPanel.AddSeparator();


            if (ribbonPanel.AddItem(new PushButtonData("Import BIM Data", "Import", assemblyPath, "RevitToolKit.ImportBIMData")) is PushButton importButton)
            {
                importButton.ToolTip = "Import BIM data from CSV";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "import.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                importButton.LargeImage = bitmap;
            }

            ribbonPanel.AddSeparator();

            if (ribbonPanel.AddItem(new PushButtonData("Validate BIM Data", "Validate", assemblyPath, "RevitToolKit.ValidateBIMData")) is PushButton validateButton)
            {
                validateButton.ToolTip = "Validate BIM data";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "validate.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                validateButton.LargeImage = bitmap;
            }

            ribbonPanel.AddSeparator();

            if (ribbonPanel.AddItem(new PushButtonData("Generate BIM Report", "Report", assemblyPath, "RevitToolKit.GenerateBIMReport")) is PushButton reportButton)
            {
                reportButton.ToolTip = "Generate BIM data report";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "report.png"));
                BitmapImage bitmap = new BitmapImage(uri);
                reportButton.LargeImage = bitmap;
            }


            // Add a pulldown button to the ribbon panel
            PulldownButtonData pulldownButtonData = new PulldownButtonData("CommandDropdown", "Command");
            PulldownButton pulldownButton = ribbonPanel.AddItem(pulldownButtonData) as PulldownButton;

            if (pulldownButton != null)
            {
                // Set tooltip and icon for the pulldown button
                pulldownButton.ToolTip = "Select a Command";
                Uri uriInfo = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "toolkit.png"));
                BitmapImage bitmapInfo = new BitmapImage(uriInfo);
                pulldownButton.LargeImage = bitmapInfo;

                // Add individual commands to the pulldown button
                PushButton btnCommand1 = pulldownButton.AddPushButton(new PushButtonData("Command1", "Inverse selection", assemblyPath, "RevitToolKit.InverseSelection")) as PushButton;
                btnCommand1.ToolTip = "Inverse Selection walls";

                PushButton btnCommand2 = pulldownButton.AddPushButton(new PushButtonData("Command2", "Command 2", assemblyPath, "RevitToolKit.Command2")) as PushButton;
                btnCommand2.ToolTip = "Command 2 test";
            }

            // Create the split button data
            SplitButtonData splitButtonData = new SplitButtonData("CommandSplit", "Command");

            // Add the split button to the ribbon panel
            SplitButton splitButton = ribbonPanel.AddItem(splitButtonData) as SplitButton;

            if (splitButton != null)
            {
                // Set the tooltip for the split button
                splitButton.ToolTip = "Choose a Command";

                // Add individual commands to the split button
                PushButton btnCommand1 = splitButton.AddPushButton(new PushButtonData("Command3", "Command 3", assemblyPath, "RevitToolKit.Command3")) as PushButton;
                btnCommand1.ToolTip = "Command 1 test";
                Uri uriInfo1 = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "toolkit.png"));
                btnCommand1.LargeImage = new BitmapImage(uriInfo1);

                PushButton btnCommand2 = splitButton.AddPushButton(new PushButtonData("Command4", "Command 4", assemblyPath, "RevitToolKit.Command4")) as PushButton;
                btnCommand2.ToolTip = "Command 2 test";
                Uri uriInfo2 = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", "toolkit.png"));
                btnCommand2.LargeImage = new BitmapImage(uriInfo2);

                // Optionally set the default button (visibly clicked when the main button part is used)
                splitButton.CurrentButton = btnCommand1;
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
