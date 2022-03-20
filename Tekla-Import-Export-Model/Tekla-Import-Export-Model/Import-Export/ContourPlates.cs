using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Import_Export;

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

        public static void ImportPlates(List<string> contourPointStringList, string s, string[] properties, List<Phase> phaseList,
            List<ContourPlate> cplList, List<string> idList, List<string> idOriginal, List<Identifier> idNew)
        {
            contourPointStringList.Add(s);
            var cpl = new ContourPlate();
            cpl.Profile.ProfileString = properties[1];
            if (cpl.Profile.ProfileString.Contains("PL"))
                cpl.Profile.ProfileString = cpl.Profile.ProfileString.Replace("PL", "—");
            cpl.Material.MaterialString = properties[2];
            if (cpl.Material.MaterialString.Contains("345-3"))
                cpl.Material.MaterialString = cpl.Material.MaterialString.Replace("345-3", "345-6");
            cpl.Name = properties[3];
            cpl.Position = Helper.GetBeamPosition(properties[4]);
            cpl.Class = properties[6];
            foreach (var ss in properties)
            {
                if (ss.Contains("###ContourPoint###"))
                {
                    cpl.Contour.AddContourPoint(Helper.ConvertStringToContourPoint(ss));
                }
            }

            cpl.Insert();
            cpl.Select();

            var phaseNumber = Convert.ToInt32(properties[properties.Count() - 2]);
            foreach (var p in phaseList)
            {
                if (p.PhaseNumber == phaseNumber)
                    cpl.SetPhase(p);
            }

            cpl.Modify();


            cplList.Add(cpl);


            idList.Add(properties[5] + "$" + cpl.Identifier.ID.ToString());

            idOriginal.Add(properties[5]);
            idNew.Add(cpl.Identifier);
        }

        /// <summary>
        /// if plate have not middle position TS may insert plate with offset to another side. This method try to correct this
        /// if plate have chamfer with different x and y dist TS can swap x and y dist in new plate
        /// </summary>
        /// <param name="cplList"></param>
        /// <param name="idOriginal"></param>
        /// <param name="idNew"></param>
        /// <param name="contourPointStringList"></param>
        /// <param name="cplWeight"></param>
        /// <param name="repairPlate"></param>
        public static void RepairPlatePositions(List<ContourPlate> cplList, List<string> idOriginal, List<Identifier> idNew, List<string> contourPointStringList,
            List<double> cplWeight, int repairPlate)
        {
            for (var ik = 0; ik < cplList.Count(); ik++)
            {
                try
                {
                    var cpl = cplList[ik];
                    var idIndexOriginal = idOriginal[idNew.IndexOf(cpl.Identifier)];
                    var weight = 0;
                    cpl.GetReportProperty("WEIGHT_GROSS", ref weight);
                    foreach (var s in contourPointStringList)
                    {
                        var properties = s.Split('|');
                        var idString = properties[5];
                        var currentWeight = properties[properties.Count() - 8];

                        if (idIndexOriginal == idString)
                        {
                            if (Math.Abs(weight - Convert.ToDouble(currentWeight)) > 0.1)
                            {
                                for (int i = 0; i < cpl.Contour.ContourPoints.Count; i++)
                                {
                                    var currentCP = cpl.Contour.ContourPoints[i] as ContourPoint;
                                    var curreChamfer = currentCP.Chamfer;
                                    if (curreChamfer.X != curreChamfer.Y &&
                                        curreChamfer.Type != Chamfer.ChamferTypeEnum.CHAMFER_NONE &&
                                        curreChamfer.X > 0 && curreChamfer.Y > 0)
                                    {
                                        cpl.Select();
                                        var newCp = new ContourPoint
                                        {
                                            X = currentCP.X,
                                            Y = currentCP.Y,
                                            Z = currentCP.Z,
                                            Chamfer =
                                            {
                                                Type = curreChamfer.Type, X = curreChamfer.Y, Y = curreChamfer.X
                                            }
                                        };
                                        cpl.Contour.ContourPoints[i] = newCp;
                                        cpl.Modify();
                                    }
                                }
                            }

                            cpl.Select();
                            double secondWeight = 0;
                            cpl.GetReportProperty("WEIGHT_GROSS", ref secondWeight);
                            double cogX = 0;
                            double cogY = 0;
                            double cogZ = 0;
                            cpl.GetReportProperty("COG_X", ref cogX);
                            cpl.GetReportProperty("COG_Y", ref cogY);
                            cpl.GetReportProperty("COG_Z", ref cogZ);
                            var cogPointNew = new Point(cogX, cogY, cogZ);
                            var originalCOG = Helper.ConvertStringToPoint(properties[properties.Count() - 7]);
                            if (Distance.PointToPoint(cogPointNew, originalCOG) > 1 &&
                                Math.Abs(secondWeight - Convert.ToDouble(currentWeight)) < 0.1)
                            {
                                if (cpl.Position.Depth == Position.DepthEnum.MIDDLE)
                                    cpl.Position.DepthOffset = -1 * cpl.Position.DepthOffset;
                                if (cpl.Position.Depth == Position.DepthEnum.BEHIND)
                                    cpl.Position.Depth = Position.DepthEnum.FRONT;
                                else if
                                    (cpl.Position.Depth == Position.DepthEnum.FRONT)
                                    cpl.Position.Depth = Position.DepthEnum.BEHIND;
                                cpl.Modify();
                            }
                        }
                    }

                    double weightCurPlate = 0;
                    cpl.GetReportProperty("WEIGHT_GROSS", ref weightCurPlate);
                    cplWeight.Add(weightCurPlate);
                }
                catch
                {
                    repairPlate++;
                }
            }
        }

    }
}
