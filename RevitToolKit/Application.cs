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
using System.Configuration.Assemblies;

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

            RevitCusttomBtn(application, assemblyPath, "Delete Element", "Delete","RevitToolKit.DeleteElement", "Delete an Element", "delete.png");

            RevitCusttomBtn(application, assemblyPath, "Duplicate an Element", "Duplicate", "RevitToolKit.DuplicateElement", "Duplicate an Element", "duplicate.gif");

            RevitCusttomBtn(application, assemblyPath, "Dimension two Element", "Dimension", "RevitToolKit.DimensionELements", "Dimension an Element", "dimention.png");

            RevitCusttomBtn(application, assemblyPath, "Rotate an Element", "Rotate", "RevitToolKit.RotateElement", "Rotate an Element", "rotate.png");

            RevitCusttomBtn(application, assemblyPath, "Intersects", "Intersects", "RevitToolKit.ElementIntersectElement", "Intersect an Element", "report.png");

            RevitCusttomBtn(application, assemblyPath, "test button", "Test btn", "RevitToolKit.TestClass", "Intersect an Element", "toolkit.png");

           

            PushButtonData pushData = new PushButtonData("Hamdo", "Hamdo1", assemblyPath, "RevitToolKit.Class1");
            PushButtonData pushData1 = new PushButtonData("Command2", "test", assemblyPath, "RevitToolKit.TestClass");


            RevitCustomPullDownButton(application, assemblyPath, "DropDownEx", "toolkit.png", pushData, pushData1);


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
            string tabName = "OptiBIM";
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

        /// <summary>
        /// This method creates a pushbutton in Revit.
        /// </summary>
        /// <param name="application">UIControlledApplication</param>
        /// <param name="assemblyPath">Assembly path to project</param>
        /// <param name="uniqueBtnName">A unique name of btn for the internal use of project</param>
        /// <param name="uIBtnName">Button name showed on Revit UI.</param>
        /// <param name="className">Class name doing the intended action when button pressed</param>
        /// <param name="toolTip">By hovering on btn, a tooltip is shown</param>
        /// <param name="btnImagePath">btn image name with png format</param>
        public void RevitCusttomBtn(UIControlledApplication application, string assemblyPath,string uniqueBtnName,string uIBtnName,string className,string toolTip,
            string btnImagePath)
        {

         RibbonPanel ribbonPanel = RibbonPanel(application);

            if (ribbonPanel.AddItem(new PushButtonData(uniqueBtnName, uIBtnName, assemblyPath, className)) is PushButton generalBtn)
            {
                generalBtn.ToolTip = toolTip;
                ContextualHelp contextualHelp = new ContextualHelp(ContextualHelpType.Url, "https://eshaqzada.netlify.app/");
                generalBtn.SetContextualHelp(contextualHelp);

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", btnImagePath));
                BitmapImage bitmap = new BitmapImage(uri);
                generalBtn.LargeImage = bitmap;
            }

            ribbonPanel.AddSeparator();
        }

        /// <summary>
        /// This method adds a pulldown button to the Revit UI.
        /// </summary>
        /// <param name="application">UIControlledApplication</param>
        /// <param name="assemblyPath">assemblyPath</param>
        /// <param name="dropDownBtnName">UI button name</param>
        /// <param name="btnImage">button icon</param>
        /// <param name="pushButtonData1">push button one data</param>
        /// <param name="pushButtonData2">Push button two data</param>
        public void RevitCustomPullDownButton(UIControlledApplication application, string assemblyPath,string dropDownBtnName,string btnImage,  PushButtonData pushButtonData1, PushButtonData pushButtonData2)
        {
            RibbonPanel ribbonPanel = RibbonPanel(application);
        
            PulldownButtonData pulldownButtonData = new PulldownButtonData("CommandDropdown", dropDownBtnName);

            PulldownButton pulldownButton = ribbonPanel.AddItem(pulldownButtonData) as PulldownButton;

            if (pulldownButton != null)
            {
                pulldownButton.ToolTip = "Select a Command";
                Uri uriInfo = new Uri(Path.Combine(Path.GetDirectoryName(assemblyPath), "Resources", btnImage));
                BitmapImage bitmapInfo = new BitmapImage(uriInfo);
                pulldownButton.LargeImage = bitmapInfo;

                 pulldownButton.AddPushButton(pushButtonData1);

                pulldownButton.AddPushButton(pushButtonData2);
                
            }
        }
    }
}
