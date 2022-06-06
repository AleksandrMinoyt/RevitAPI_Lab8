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

namespace RevitAPI_ExportToIFC
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            using (var trans = new Transaction(doc, "Экспорт в NWC"))
            {
                trans.Start();

                //  public void Export(string folder, string name, NavisworksExportOptions options);
                NavisworksExportOptions opt = new NavisworksExportOptions();                
                doc.Export(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ModelExport", opt);

                trans.Commit();
            }
     
            return Result.Succeeded;
        }
}
}
