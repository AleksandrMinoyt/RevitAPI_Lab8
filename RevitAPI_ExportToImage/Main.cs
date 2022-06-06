using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_ExportToImage
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            using (var trans = new Transaction(doc, "Экспорт в изображение"))
            {
                trans.Start();

                ViewPlan viewPlan = new FilteredElementCollector(doc)
                                    .OfClass(typeof(ViewPlan))
                                    .Cast<ViewPlan>()
                                    .FirstOrDefault(level => level.ViewType == ViewType.FloorPlan &&
                                                             level.Name.Equals("Level 1"));

                List<ElementId> viewPlanID = new List<ElementId>();
                viewPlanID.Add(viewPlan.Id);

                //public void ExportImage(ImageExportOptions options);
                ImageExportOptions opt = new ImageExportOptions();
                opt.FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testimage.jpg";                               
                opt.ExportRange = ExportRange.SetOfViews;
                opt.SetViewsAndSheets(viewPlanID);
               // opt.ViewName = "Level 1";

                doc.ExportImage(opt);

                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
