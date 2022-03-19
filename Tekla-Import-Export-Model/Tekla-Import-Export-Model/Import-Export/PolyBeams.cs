using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class PolyBeams
    {
        public static void ExportPolyBeam(List<PolyBeam> polyBeamList, List<string> outStringList, List<double> polyBeamWeight)
        {
            foreach (var beam in polyBeamList)
            {
                var beamStringListLocal = new List<string>
                {
                    "###polyBeamStart###", //0
                    '|' + beam.Profile.ProfileString, //5
                    '|' + beam.Material.MaterialString, //6
                    '|' + beam.Class, //7
                    '|' + beam.AssemblyNumber.Prefix, //8
                    '|' + beam.Name, //9
                    '|' + beam.Identifier.ID.ToString(), //10
                    '|' + beam.Type.ToString(), //11
                    '|' + beam.Position.Depth.ToString() + "$" +
                    beam.Position.DepthOffset.ToString() + "$" + beam.Position.Plane.ToString() +
                    "$" + beam.Position.PlaneOffset.ToString() + "$" +
                    beam.Position.Rotation.ToString() + "$" +
                    beam.Position.RotationOffset.ToString() //12
                };
                for (var i = 0; i < beam.Contour.ContourPoints.Count; i++)
                {
                    var currentCP = beam.Contour.ContourPoints[i] as ContourPoint;
                    beamStringListLocal.Add("|###ContourPoint###$" + currentCP.X.ToString() + '$' +
                                            currentCP.Y.ToString() + '$' + currentCP.Z.ToString() + '$' +
                                            currentCP.Chamfer.Type.ToString() + '$' + currentCP.Chamfer.X.ToString() +
                                            '$' + currentCP.Chamfer.Y.ToString());
                }

                beamStringListLocal.Add("|###polyBeamEnd###"); //13

                var outString = new StringBuilder();
                foreach (var localString in beamStringListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
                double localWeight = 0;
                beam.Select();
                beam.GetReportProperty("WEIGHT_GROSS", ref localWeight);
                polyBeamWeight.Add(localWeight);
            }
        }
    }
}
