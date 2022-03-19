
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class Welds
    {
        public static void ExportWelds(List<Weld> weldList, List<string> outStringList, List<PolygonWeld> polygonWeldList)
        {
            foreach (var weld in weldList)
            {
                if (weld.ShopWeld)
                {
                    var weldListLocal = new List<string>();
                    weldListLocal.Add("###WeldStart###");
                    weldListLocal.Add('|' + weld.MainObject.Identifier.ID.ToString());
                    weldListLocal.Add('|' + weld.SecondaryObject.Identifier.ID.ToString());

                    weldListLocal.Add("|###WeldEnd###");
                    var outString = new StringBuilder();
                    foreach (var localString in weldListLocal)
                    {
                        outString = outString.Append(localString);
                    }

                    outStringList.Add(outString.ToString());
                }
            }

            foreach (var weld in polygonWeldList)
            {
                if (weld.ShopWeld)
                {
                    var weldListLocal = new List<string>();
                    weldListLocal.Add("###WeldStart###");
                    weldListLocal.Add('|' + weld.MainObject.Identifier.ID.ToString());
                    weldListLocal.Add('|' + weld.SecondaryObject.Identifier.ID.ToString());

                    weldListLocal.Add("|###WeldEnd###");
                    var outString = new StringBuilder();
                    foreach (var localString in weldListLocal)
                    {
                        outString = outString.Append(localString);
                    }

                    outStringList.Add(outString.ToString());
                }
            }
        }
    }
}
