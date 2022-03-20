using System.Collections.Generic;
using System.Text;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Import_Export;

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

        public static void ImportFittings(List<Identifier> idNew, List<string> idOriginal, string[] properties, Model m, ref int fittingCount)
        {
            try
            {
                var fitting = new Fitting();
                var ID = idNew[idOriginal.IndexOf(properties[1])];
                fitting.Father = m.SelectModelObject(ID);
                fitting.Plane.Origin = Helper.ConvertStringToPoint(properties[2]);
                fitting.Plane.AxisX = new Vector(Helper.ConvertStringToPoint(properties[3]));
                fitting.Plane.AxisY = new Vector(Helper.ConvertStringToPoint(properties[4]));
                fitting.Insert();
            }
            catch
            {
                fittingCount++;
            }
        }

        public static void ImportCutPlanes(List<Identifier> idNew, List<string> idOriginal, string[] properties, Model m, ref int cutPlaneCount)
        {
            try
            {
                var cutPlane = new CutPlane();
                var id = idNew[idOriginal.IndexOf(properties[1])];
                cutPlane.Father = m.SelectModelObject(id);
                cutPlane.Plane.Origin = Helper.ConvertStringToPoint(properties[2]);
                cutPlane.Plane.AxisX = new Vector(Helper.ConvertStringToPoint(properties[3]));
                cutPlane.Plane.AxisY = new Vector(Helper.ConvertStringToPoint(properties[4]));
                cutPlane.Insert();
            }
            catch
            {
                cutPlaneCount++;
            }
        }
    }
}
