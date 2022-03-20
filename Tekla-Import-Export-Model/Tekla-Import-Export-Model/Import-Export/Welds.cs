
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures;
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

        public static void ImportWelds(List<Identifier> idNew, List<string> idOriginal, string[] properties, Model m)
        {
            try
            {
                var id = idNew[idOriginal.IndexOf(properties[4])];
                if (properties.Count() > 6)
                {
                    for (var i = 5; i < properties.Count() - 1; i++)
                    {
                        var weld = new Weld();
                        weld.MainObject = m.SelectModelObject(id);
                        var id2 = idNew[idOriginal.IndexOf(properties[i])];
                        weld.SecondaryObject = m.SelectModelObject(id2);
                        weld.ShopWeld = true;
                        weld.Insert();
                    }
                }

                var mainObject = m.SelectModelObject(id);
                mainObject.Select();
                var currentAssembly = (mainObject as Part).GetAssembly();
                currentAssembly.Select();
                currentAssembly.SetMainPart(mainObject as Part);

                currentAssembly.Name = properties[1];
                currentAssembly.AssemblyNumber.Prefix = properties[2];

                try
                {
                    var temp = properties[3].Replace(properties[2], "");
                    temp = temp.Replace("-", "");
                    var assNumber = Convert.ToInt32(temp);
                }
                catch
                {
                }

                currentAssembly.Modify();
            }
            catch
            {
            }
        }
    }
}
