using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class Fittings
    {
        public static void ExportFittings(List<Fitting> fittingList, List<string> outStringList)
        {
            foreach (var fitting in fittingList)
            {
                var fittingListLocal = new List<string>();
                fittingListLocal.Add("###FittingStart###");
                fittingListLocal.Add('|' + fitting.Father.Identifier.ID.ToString());
                fittingListLocal.Add('|' + fitting.Plane.Origin.X.ToString() + '$' +
                                     fitting.Plane.Origin.Y.ToString() + '$' + fitting.Plane.Origin.Z.ToString());
                fittingListLocal.Add('|' + fitting.Plane.AxisX.X.ToString() + '$' + fitting.Plane.AxisX.Y.ToString() +
                                     '$' + fitting.Plane.AxisX.Z.ToString());
                fittingListLocal.Add('|' + fitting.Plane.AxisY.X.ToString() + '$' + fitting.Plane.AxisY.Y.ToString() +
                                     '$' + fitting.Plane.AxisY.Z.ToString());
                fittingListLocal.Add("|###FittingEnd###");

                var outString = new StringBuilder();
                foreach (string localString in fittingListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }
    }
}
