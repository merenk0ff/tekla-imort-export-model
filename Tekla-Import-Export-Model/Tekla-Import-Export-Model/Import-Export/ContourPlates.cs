using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class ContourPlates
    {
        public static void ExportContourPlates(List<ContourPlate> cplList, List<Point> contourPointCOG,
            List<double> contourPlateWeight, List<string> outStringList)
        {
            foreach (var cpl in cplList)
            {
                cpl.Select();
                var cplLocalList = new List<string>();

                cplLocalList.Add("###ContourPlateStart###");
                cplLocalList.Add('|' + cpl.Profile.ProfileString);
                cplLocalList.Add('|' + cpl.Material.MaterialString);
                cplLocalList.Add('|' + cpl.Name);
                cplLocalList.Add('|' + cpl.Position.Depth.ToString() + "$" + cpl.Position.DepthOffset.ToString() +
                                 "$" + cpl.Position.Plane.ToString() + "$" + cpl.Position.PlaneOffset.ToString() +
                                 "$" + cpl.Position.Rotation.ToString() + "$" +
                                 cpl.Position.RotationOffset.ToString());
                cplLocalList.Add('|' + cpl.Identifier.ID.ToString());
                cplLocalList.Add('|' + cpl.Class);

                for (int i = 0; i < cpl.Contour.ContourPoints.Count; i++)
                {
                    ContourPoint currentCP = cpl.Contour.ContourPoints[i] as ContourPoint;
                    cplLocalList.Add("|###ContourPoint###$" + currentCP.X.ToString() + '$' + currentCP.Y.ToString() +
                                     '$' + currentCP.Z.ToString() + '$' + currentCP.Chamfer.Type.ToString() + '$' +
                                     currentCP.Chamfer.X.ToString() + '$' + currentCP.Chamfer.Y.ToString());
                }

                double weight = 0;

                double cogX = 0;
                double cogY = 0;
                double cogZ = 0;
                cpl.GetReportProperty("COG_X", ref cogX);
                cpl.GetReportProperty("COG_Y", ref cogY);
                cpl.GetReportProperty("COG_Z", ref cogZ);

                cpl.GetReportProperty("WEIGHT_GROSS", ref weight);


                var cog = new Point(cogX, cogY, cogZ);
                contourPointCOG.Add(cog);
                contourPlateWeight.Add(weight);


                cplLocalList.Add('|' + weight.ToString());
                cplLocalList.Add('|' + cogX.ToString() + '$' + cogY.ToString() + '$' + cogZ.ToString());

                var currentAss = cpl.GetAssembly();
                var AssPrefix = currentAss.AssemblyNumber.Prefix;
                var AssNumber = "";
                double assWeight = 0;

                currentAss.GetReportProperty("ASSEMBLY_POS", ref AssNumber);
                currentAss.GetReportProperty("WEIGHT", ref assWeight);

                var assMainPart = currentAss.GetMainPart();
                var assMainPartID = assMainPart.Identifier.ID.ToString();
                cplLocalList.Add('|' + AssPrefix);
                cplLocalList.Add('|' + AssNumber);
                cplLocalList.Add('|' + assWeight.ToString());
                cplLocalList.Add('|' + assMainPartID);
                var outPhase = new Phase();
                cpl.GetPhase(out outPhase);
                cplLocalList.Add('|' + outPhase.PhaseNumber.ToString());


                cplLocalList.Add("|###ContourPlateEnd###");

                var outString = new StringBuilder();
                foreach (string localString in cplLocalList)
                {
                    outString = outString.Append(localString);
                }


                outStringList.Add(outString.ToString());
            }
        }

    }
}
