using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System.Reflection;
using System.IO;
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


            CreateCustomBtn(application, assemblyPath, "Smart Select", "Smart Select", "RevitToolKit.SmartSelect", "Select elements Intelligently", "choose.png");
            CreateCustomBtn(application, assemblyPath, "Batch Editor", "Edit Params", "RevitToolKit.TestCommand", "Edit parameters in bulk", "toolkit.png");
            CreateCustomBtn(application, assemblyPath, "Align Elements", "Align Elements", "RevitToolKit.TestCommand", "Align elements precisely", "dimension.png");
            CreateCustomBtn(application, assemblyPath, "Health Checker", "Check Health", "RevitToolKit.TestCommand", "Analyze project health", "validate.png");


            PushButtonData pushData = new PushButtonData("Change Display Graphic", "Change Graphic", assemblyPath, "RevitToolKit.TestCommand");
            PushButtonData pushData1 = new PushButtonData("Inverse Selection", "Inverse Selected Elements", assemblyPath, "RevitToolKit.TestCommand");


            RevitCustomPullDownButton(application, assemblyPath, "Utilities", "utilities.png", pushData, pushData1);
                    
            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication application)
        {
            string tabName = "S-BIM Toolkit";
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
        public void CreateCustomBtn(UIControlledApplication application, string assemblyPath,string uniqueBtnName,string uIBtnName,string className,string toolTip,
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
