using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class BooleanParts
    {
        public static void ExportBooleanParts(List<BooleanPart> booleanPartList, List<string> outStringList)
        {
            foreach (var booleanPart in booleanPartList)
            {
                var bPart = booleanPart as BooleanPart;
                var booleanListLocal = new List<string>();
                var operativePart = bPart.OperativePart;
                if (operativePart is ContourPlate)
                {
                    var operativePartCP = operativePart as ContourPlate;
                    double cogX = 0;
                    double cogY = 0;
                    double cogZ = 0;
                    operativePart.GetReportProperty("COG_X", ref cogX);
                    operativePart.GetReportProperty("COG_Y", ref cogX);
                    operativePart.GetReportProperty("COG_Z", ref cogX);


                    booleanListLocal.Add("###BooleanContourPlateStart###");
                    booleanListLocal.Add('|' + bPart.Father.Identifier.ID.ToString());
                    booleanListLocal.Add('|' + operativePartCP.Profile.ProfileString);
                    booleanListLocal.Add('|' + operativePartCP.Position.Depth.ToString() + "$" +
                                         operativePartCP.Position.DepthOffset.ToString() + "$" +
                                         operativePartCP.Position.Plane.ToString() + "$" +
                                         operativePartCP.Position.PlaneOffset.ToString() + "$" +
                                         operativePartCP.Position.Rotation.ToString() + "$" +
                                         operativePartCP.Position.RotationOffset.ToString());
                    for (var i = 0; i < (operativePart as ContourPlate).Contour.ContourPoints.Count; i++)
                    {
                        var currentCP =
                            (operativePart as ContourPlate).Contour.ContourPoints[i] as ContourPoint;
                        booleanListLocal.Add("|###ContourPoint###$" + currentCP.X.ToString() + '$' +
                                             currentCP.Y.ToString() + '$' + currentCP.Z.ToString() + '$' +
                                             currentCP.Chamfer.Type.ToString() + '$' +
                                             currentCP.Chamfer.X.ToString() + '$' + currentCP.Chamfer.Y.ToString());
                    }

                    booleanListLocal.Add('|' + cogX.ToString() + '$' + cogY.ToString() + '$' + cogZ.ToString());
                    booleanListLocal.Add("|###BooleanContourPlateEnd###");
                }

                if (operativePart is Beam)
                {
                    var operativePartBeam = operativePart as Beam;
                    booleanListLocal.Add("###BooleanBeamStart###");
                    booleanListLocal.Add('|' + bPart.Father.Identifier.ID.ToString());
                    booleanListLocal.Add('|' + operativePartBeam.Profile.ProfileString);
                    booleanListLocal.Add('|' + operativePartBeam.Position.Depth.ToString() + "$" +
                                         operativePartBeam.Position.DepthOffset.ToString() + "$" +
                                         operativePartBeam.Position.Plane.ToString() + "$" +
                                         operativePartBeam.Position.PlaneOffset.ToString() + "$" +
                                         operativePartBeam.Position.Rotation.ToString() + "$" +
                                         operativePartBeam.Position.RotationOffset.ToString()); //12
                    booleanListLocal.Add('|' + operativePartBeam.StartPoint.X.ToString() + "$" +
                                         operativePartBeam.StartPoint.Y + "$" + operativePartBeam.StartPoint.Z); //1
                    booleanListLocal.Add('|' + operativePartBeam.EndPoint.X.ToString() + "$" +
                                         operativePartBeam.EndPoint.Y + "$" + operativePartBeam.EndPoint.Z); //2
                    booleanListLocal.Add('|' + operativePartBeam.StartPointOffset.Dx.ToString() + "$" +
                                         operativePartBeam.StartPointOffset.Dy.ToString() + "$" +
                                         operativePartBeam.StartPointOffset.Dz.ToString()); //3
                    booleanListLocal.Add('|' + operativePartBeam.EndPointOffset.Dx.ToString() + "$" +
                                         operativePartBeam.EndPointOffset.Dy.ToString() + "$" +
                                         operativePartBeam.EndPointOffset.Dz.ToString()); //4
                    booleanListLocal.Add("|###BooleanBeamEnd###");
                }

                if (operativePart is PolyBeam)
                {
                    var operativePartBeam = operativePart as PolyBeam;
                    booleanListLocal.Add("###BooleanPolyBeamStart###");
                    booleanListLocal.Add('|' + bPart.Father.Identifier.ID.ToString());
                    booleanListLocal.Add('|' + operativePartBeam.Profile.ProfileString); //5
                    booleanListLocal.Add('|' + operativePartBeam.Position.Depth.ToString() + "$" +
                                         operativePartBeam.Position.DepthOffset.ToString() + "$" +
                                         operativePartBeam.Position.Plane.ToString() + "$" +
                                         operativePartBeam.Position.PlaneOffset.ToString() + "$" +
                                         operativePartBeam.Position.Rotation.ToString() + "$" +
                                         operativePartBeam.Position.RotationOffset.ToString()); //12
                    for (var i = 0; i < operativePartBeam.Contour.ContourPoints.Count; i++)
                    {
                        var currentCP = operativePartBeam.Contour.ContourPoints[i] as ContourPoint;
                        booleanListLocal.Add("|###ContourPoint###$" + currentCP.X.ToString() + '$' +
                                             currentCP.Y.ToString() + '$' + currentCP.Z.ToString() + '$' +
                                             currentCP.Chamfer.Type.ToString() + '$' +
                                             currentCP.Chamfer.X.ToString() + '$' + currentCP.Chamfer.Y.ToString());
                    }

                    booleanListLocal.Add("|###BooleanPolyBeamEnd###");
                }


                var outString = new StringBuilder();
                foreach (var localString in booleanListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }

    }
}
