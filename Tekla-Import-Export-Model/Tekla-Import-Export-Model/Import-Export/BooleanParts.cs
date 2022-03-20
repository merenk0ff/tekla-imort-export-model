using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Import_Export;

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

        public static void ImportBooleanContourPlates(string[] properties, List<Identifier> idNew, List<string> idOriginal, Model m, ref int booleanCp)
        {
            try
            {
                var booleanPart = new ContourPlate();
                booleanPart.Profile.ProfileString = properties[2];
                if (booleanPart.Profile.ProfileString.Contains("—"))
                    booleanPart.Profile.ProfileString = booleanPart.Profile.ProfileString.Replace("—", "PL");
                booleanPart.Class = BooleanPart.BooleanOperativeClassName;
                booleanPart.Position = Helper.GetBeamPosition(properties[3]);
                foreach (var kkk in properties)
                {
                    if (kkk.Contains("###ContourPoint###"))
                    {
                        booleanPart.Contour.AddContourPoint(Helper.ConvertStringToContourPoint(kkk));
                    }
                }

                booleanPart.Insert();
                booleanPart.Select();

                double cogX = 0;
                double cogY = 0;
                double cogZ = 0;
                booleanPart.GetReportProperty("COG_X", ref cogX);
                booleanPart.GetReportProperty("COG_Y", ref cogX);
                booleanPart.GetReportProperty("COG_Z", ref cogX);

                var newCOgPoin = new Point(cogX, cogY, cogZ);

                var originalCogPoint =
                    new Point(Helper.ConvertStringToPoint(properties[properties.Count() - 2]));
                var dist = Distance.PointToPoint(newCOgPoin, originalCogPoint);
                if (dist > 1)
                {
                    booleanPart.Select();
                    if (booleanPart.Position.Depth == Position.DepthEnum.MIDDLE)
                        booleanPart.Position.DepthOffset = -1 * booleanPart.Position.DepthOffset;
                    if (booleanPart.Position.Depth == Position.DepthEnum.BEHIND)
                        booleanPart.Position.Depth = Position.DepthEnum.FRONT;
                    else if
                        (booleanPart.Position.Depth == Position.DepthEnum.FRONT)
                        booleanPart.Position.Depth = Position.DepthEnum.BEHIND;
                    booleanPart.Modify();
                }

                var firstBoolean = new BooleanPart();
                var ID = idNew[idOriginal.IndexOf(properties[1])];
                firstBoolean.Father = m.SelectModelObject(ID);
                firstBoolean.SetOperativePart(booleanPart);
                firstBoolean.Insert();

                booleanPart.Delete();
            }
            catch
            {
                booleanCp++;
            }
        }

        public static void ImportBooleanBeams(string[] properties, List<Identifier> idNew, List<string> idOriginal, Model m,
            ref int booleanBeamCount)
        {
            try
            {
                var booleanBeam = new Beam();
                booleanBeam.Profile.ProfileString = properties[2];
                if (booleanBeam.Profile.ProfileString.Contains("—"))
                    booleanBeam.Profile.ProfileString = booleanBeam.Profile.ProfileString.Replace("—", "PL");
                booleanBeam.Class = BooleanPart.BooleanOperativeClassName;
                booleanBeam.Position = Helper.GetBeamPosition(properties[3]);
                booleanBeam.StartPoint = Helper.ConvertStringToPoint(properties[4]);
                booleanBeam.EndPoint = Helper.ConvertStringToPoint(properties[5]);
                booleanBeam.StartPointOffset.Dx = Helper.ConvertStringToPoint(properties[6]).X;
                booleanBeam.StartPointOffset.Dy = Helper.ConvertStringToPoint(properties[6]).Y;
                booleanBeam.StartPointOffset.Dz = Helper.ConvertStringToPoint(properties[6]).Z;

                booleanBeam.EndPointOffset.Dx = Helper.ConvertStringToPoint(properties[7]).X;
                booleanBeam.EndPointOffset.Dy = Helper.ConvertStringToPoint(properties[7]).Y;
                booleanBeam.EndPointOffset.Dz = Helper.ConvertStringToPoint(properties[7]).Z;
                booleanBeam.Insert();

                var firstBoolean = new BooleanPart();
                var id = idNew[idOriginal.IndexOf(properties[1])];
                firstBoolean.Father = m.SelectModelObject(id);
                firstBoolean.SetOperativePart(booleanBeam);
                firstBoolean.Insert();

                booleanBeam.Delete();
            }
            catch
            {
                booleanBeamCount++;
            }
        }

        public static void ImportBooleanPolybeam(string[] properties, List<Identifier> idNew, List<string> idOriginal, Model m,
            ref int booleanPolybeam)
        {
            try
            {
                var booleanBeam = new PolyBeam();
                booleanBeam.Profile.ProfileString = properties[2];
                if (booleanBeam.Profile.ProfileString.Contains("—"))
                    booleanBeam.Profile.ProfileString = booleanBeam.Profile.ProfileString.Replace("—", "PL");
                booleanBeam.Class = BooleanPart.BooleanOperativeClassName;
                booleanBeam.Position = Helper.GetBeamPosition(properties[3]);
                for (var i = 4; i < properties.Count() - 1; i++)
                {
                    booleanBeam.Contour.AddContourPoint(Helper.ConvertStringToContourPoint(properties[i]));
                }

                booleanBeam.Insert();


                var firstBoolean = new BooleanPart();
                var id = idNew[idOriginal.IndexOf(properties[1])];
                firstBoolean.Father = m.SelectModelObject(id);
                firstBoolean.SetOperativePart(booleanBeam);
                firstBoolean.Insert();

                booleanBeam.Delete();
            }
            catch
            {
                booleanPolybeam++;
            }
        }

    }
}
