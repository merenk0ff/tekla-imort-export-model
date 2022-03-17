
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class Assemblies
    {
        public static void ExportAssemblies(List<Assembly> assList, List<string> outStringList)
        {
            foreach (var ass in assList)
            {
                try
                {
                    ass.Select();
                    var assListLocal = new List<string>();
                    assListLocal.Add("###AssStart###"); //0
                    var mainPart = ass.GetMainPart();
                    var assNumber = "";
                    ass.GetReportProperty("ASSEMBLY_POS", ref assNumber);
                    assListLocal.Add('|' + ass.Name); //1
                    assListLocal.Add('|' + ass.AssemblyNumber.Prefix); //2
                    assListLocal.Add('|' + assNumber); //3
                    assListLocal.Add('|' + mainPart.Identifier.ID.ToString()); //4

                    ass.Select();

                    var secondaries = ass.GetSecondaries();

                    foreach (ModelObject currentObject in secondaries)
                    {
                        try
                        {
                            var currentPart = currentObject as Part;
                            assListLocal.Add('|' + currentPart.Identifier.ID.ToString());
                        }
                        catch
                        {
                        }
                    }


                    assListLocal.Add("|###AssEND###");
                    var outString = new StringBuilder();
                    foreach (var localString in assListLocal)
                    {
                        outString = outString.Append(localString);
                    }

                    outStringList.Add(outString.ToString());
                }
                catch
                {
                    int l = 0;
                    l++;
                }
            }
        }
    }
}
