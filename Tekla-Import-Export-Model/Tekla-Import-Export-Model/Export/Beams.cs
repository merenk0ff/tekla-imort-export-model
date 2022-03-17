using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace Tekla_Import_Export_Model.Export
{
    public class Beams
    {
        /// <summary>
        /// Export beams to txt
        /// </summary>
        /// <param name="beamList"></param>
        /// <param name="beamCOG"></param>
        /// <param name="BeamWeight"></param>
        /// <param name="outStringList"></param>
        public static void ExportBeams(List<Beam> beamList, List<Point> beamCOG, List<double> BeamWeight, List<string> outStringList)
        {
            foreach (Beam beam in beamList)
            {
                var beamStringListLocal = new List<string>();
                beamStringListLocal.Add("###beamStart###"); //0
                beamStringListLocal.Add('|' + beam.StartPoint.X.ToString() + "$" + beam.StartPoint.Y + "$" +
                                        beam.StartPoint.Z); //1
                beamStringListLocal.Add('|' + beam.EndPoint.X.ToString() + "$" + beam.EndPoint.Y + "$" +
                                        beam.EndPoint.Z); //2
                beamStringListLocal.Add('|' + beam.StartPointOffset.Dx.ToString() + "$" +
                                        beam.StartPointOffset.Dy.ToString() + "$" +
                                        beam.StartPointOffset.Dz.ToString()); //3
                beamStringListLocal.Add('|' + beam.EndPointOffset.Dx.ToString() + "$" +
                                        beam.EndPointOffset.Dy.ToString() + "$" + beam.EndPointOffset.Dz.ToString());
                //4
                beamStringListLocal.Add('|' + beam.Profile.ProfileString); //5
                beamStringListLocal.Add('|' + beam.Material.MaterialString); //6
                beamStringListLocal.Add('|' + beam.Class); //7
                beamStringListLocal.Add('|' + beam.AssemblyNumber.Prefix); //8
                beamStringListLocal.Add('|' + beam.Name); //9
                beamStringListLocal.Add('|' + beam.Identifier.ID.ToString()); //10
                beamStringListLocal.Add('|' + beam.Type.ToString()); //11
                beamStringListLocal.Add('|' + beam.Position.Depth.ToString() + "$" +
                                        beam.Position.DepthOffset.ToString() + "$" + beam.Position.Plane.ToString() +
                                        "$" + beam.Position.PlaneOffset.ToString() + "$" +
                                        beam.Position.Rotation.ToString() + "$" +
                                        beam.Position.RotationOffset.ToString()); //12

                double weight = 0;

                double cogX = 0;
                double cogY = 0;
                double cogZ = 0;

                beam.GetReportProperty("COG_X", ref cogX);
                beam.GetReportProperty("COG_Y", ref cogY);
                beam.GetReportProperty("COG_Z", ref cogZ);

                beam.GetReportProperty("WEIGHT_GROSS", ref weight);


                var cog = new Point(cogX, cogY, cogZ);
                beamCOG.Add(cog);
                BeamWeight.Add(weight);

                beamStringListLocal.Add('|' + weight.ToString());
                beamStringListLocal.Add('|' + cogX.ToString() + '$' + cogY.ToString() + '$' + cogZ.ToString());

                var currentAss = beam.GetAssembly();
                var AssPrefix = currentAss.AssemblyNumber.Prefix;
                var AssNumber = "";
                double assWeight = 0;

                currentAss.GetReportProperty("ASSEMBLY_POS", ref AssNumber);
                currentAss.GetReportProperty("WEIGHT", ref assWeight);

                var assMainPart = currentAss.GetMainPart();
                var assMainPartID = assMainPart.Identifier.ID.ToString();
                beamStringListLocal.Add('|' + AssPrefix);
                beamStringListLocal.Add('|' + AssNumber);
                beamStringListLocal.Add('|' + assWeight.ToString());
                beamStringListLocal.Add('|' + assMainPartID);
                var outPhase = new Phase();
                beam.GetPhase(out outPhase);
                beamStringListLocal.Add('|' + outPhase.PhaseNumber.ToString());


                beamStringListLocal.Add("|###beamEnd###"); //13

                var outString = new StringBuilder();
                foreach (string localString in beamStringListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }

    }
}
