
using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Import_Export;

namespace Tekla_Import_Export_Model.Export
{
    public class BoltXYs
    {
        public static void ExportBoltXY(List<BoltXYList> boltXYList, List<string> outStringList)
        {
            foreach (var boltXY in boltXYList)
            {
                var boltXyListLocal = new List<string>();
                boltXyListLocal.Add("###BoltXYlistStart###");
                boltXyListLocal.Add('|' + boltXY.BoltSize.ToString());
                boltXyListLocal.Add('|' + boltXY.BoltStandard);
                boltXyListLocal.Add('|' + boltXY.BoltType.ToString());
                boltXyListLocal.Add('|' + boltXY.CutLength.ToString());
                boltXyListLocal.Add('|' + boltXY.StartPointOffset.Dx.ToString() + '$' +
                                    boltXY.StartPointOffset.Dy.ToString() + '$' +
                                    boltXY.StartPointOffset.Dz.ToString() + '$');
                boltXyListLocal.Add('|' + boltXY.EndPointOffset.Dx.ToString() + '$' +
                                    boltXY.EndPointOffset.Dy.ToString() + '$' + boltXY.EndPointOffset.Dz.ToString() +
                                    '$');
                boltXyListLocal.Add('|' + boltXY.ExtraLength.ToString());
                boltXyListLocal.Add('|' + boltXY.FirstPosition.X.ToString() + '$' +
                                    boltXY.FirstPosition.Y.ToString() + '$' + boltXY.FirstPosition.Z.ToString() +
                                    '$');
                boltXyListLocal.Add('|' + boltXY.SecondPosition.X.ToString() + '$' +
                                    boltXY.SecondPosition.Y.ToString() + '$' + boltXY.SecondPosition.Z.ToString() +
                                    '$');
                boltXyListLocal.Add('|' + boltXY.Hole1.ToString());
                boltXyListLocal.Add('|' + boltXY.Hole2.ToString());
                boltXyListLocal.Add('|' + boltXY.Hole3.ToString());
                boltXyListLocal.Add('|' + boltXY.Hole4.ToString());
                boltXyListLocal.Add('|' + boltXY.Hole5.ToString());
                boltXyListLocal.Add('|' + boltXY.HoleType.ToString());
                boltXyListLocal.Add('|' + boltXY.Nut1.ToString());
                boltXyListLocal.Add('|' + boltXY.Nut2.ToString());
                boltXyListLocal.Add('|' + boltXY.RotateSlots.ToString());
                boltXyListLocal.Add('|' + boltXY.SlottedHoleX.ToString());
                boltXyListLocal.Add('|' + boltXY.SlottedHoleY.ToString());
                boltXyListLocal.Add('|' + boltXY.ThreadInMaterial.ToString());
                boltXyListLocal.Add('|' + boltXY.Tolerance.ToString());
                boltXyListLocal.Add('|' + boltXY.Washer1.ToString());
                boltXyListLocal.Add('|' + boltXY.Washer2.ToString());
                boltXyListLocal.Add('|' + boltXY.Washer3.ToString());
                boltXyListLocal.Add('|' + boltXY.Position.Depth.ToString() + "$" +
                                    boltXY.Position.DepthOffset.ToString() + "$" + boltXY.Position.Plane.ToString() +
                                    "$" + boltXY.Position.PlaneOffset.ToString() + "$" +
                                    boltXY.Position.Rotation.ToString() + "$" +
                                    boltXY.Position.RotationOffset.ToString());


                var distX = new StringBuilder();
                for (var i = 0; i < boltXY.GetBoltDistXCount(); i++)
                {
                    distX = distX.Append(boltXY.GetBoltDistX(i).ToString() + '$');
                }

                var distY = new StringBuilder();
                for (var i = 0; i < boltXY.GetBoltDistYCount(); i++)
                {
                    distY = distY.Append(boltXY.GetBoltDistY(i).ToString() + '$');
                }

                boltXyListLocal.Add('|' + distX.ToString());
                boltXyListLocal.Add('|' + distY.ToString());
                boltXyListLocal.Add('|' + boltXY.PartToBeBolted.Identifier.ID.ToString());
                boltXyListLocal.Add('|' + boltXY.PartToBoltTo.Identifier.ID.ToString());
                boltXyListLocal.Add('|' + boltXY.Bolt.ToString());
                boltXyListLocal.Add('|' + boltXY.Identifier.ID.ToString());
                var otherPartToBolt = boltXY.GetOtherPartsToBolt();
                foreach (ModelObject otherParts in otherPartToBolt)
                {
                    if (otherParts is Part)
                    {
                        boltXyListLocal.Add("|OTHERPART$" + otherParts.Identifier.ID.ToString());
                    }
                }


                boltXyListLocal.Add("|###BoltXYlistEND###");
                var outString = new StringBuilder();
                foreach (var localString in boltXyListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }

        public static void ImportBoltXYs(List<Identifier> idNew, List<string> idOriginal, string[] properties, Model m, List<string> boltIdlist,
           List<string> boltIdNewlist, ref int boltArrayCount)
        {
            try
            {
                var boltXY = new BoltXYList();

                var ID = idNew[idOriginal.IndexOf(properties[29])];
                var ID2 = idNew[idOriginal.IndexOf(properties[30])];

                boltXY.PartToBeBolted = m.SelectModelObject(ID) as Part;
                boltXY.PartToBoltTo = m.SelectModelObject(ID2) as Part;
                boltXY.FirstPosition = Helper.ConvertStringToPoint(properties[8]);
                boltXY.SecondPosition = Helper.ConvertStringToPoint(properties[9]);
                var distXlist = Helper.getBoltDist(properties[27]);
                var distYlist = Helper.getBoltDist(properties[28]);
                foreach (var d in distXlist)
                    boltXY.AddBoltDistX(d);
                foreach (var d in distYlist)
                    boltXY.AddBoltDistY(d);
                boltXY.Bolt = Convert.ToBoolean(properties[31]);
                boltXY.Hole1 = Convert.ToBoolean(properties[10]);
                boltXY.Hole2 = Convert.ToBoolean(properties[11]);
                boltXY.Hole3 = Convert.ToBoolean(properties[12]);
                boltXY.Hole4 = Convert.ToBoolean(properties[13]);
                boltXY.Hole5 = Convert.ToBoolean(properties[14]);
                boltXY.Nut1 = Convert.ToBoolean(properties[16]);
                boltXY.Nut2 = Convert.ToBoolean(properties[17]);
                boltXY.Washer1 = Convert.ToBoolean(properties[23]);
                boltXY.Washer2 = Convert.ToBoolean(properties[24]);
                boltXY.Washer3 = Convert.ToBoolean(properties[25]);
                boltXY.Tolerance = Convert.ToDouble(properties[22]);
                boltXY.Position = Helper.GetBeamPosition(properties[26]);

                boltXY.BoltStandard = properties[2];

                boltXY.BoltSize = Convert.ToDouble(properties[1]);

                if (properties[3] == "BOLT_TYPE_SITE")
                    boltXY.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
                else
                    boltXY.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;

                if (properties[21] == "THREAD_IN_MATERIAL_YES")
                    boltXY.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;
                else
                    boltXY.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                boltXY.CutLength = Convert.ToDouble(properties[4]);
                boltXY.SlottedHoleX = Convert.ToDouble(properties[19]);
                boltXY.SlottedHoleY = Convert.ToDouble(properties[20]);
                boltXY.ExtraLength = Convert.ToDouble(properties[7]);

                if (properties[15] == "HOLE_TYPE_OVERSIZED")
                    boltXY.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_OVERSIZED;
                else
                    boltXY.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;
                if (properties[18] == "ROTATE_SLOTS_EVEN")
                    boltXY.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_EVEN;
                else if (properties[18] == "ROTATE_SLOTS_ODD")
                    boltXY.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_ODD;
                else
                    boltXY.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_PARALLEL;

                boltXY.StartPointOffset.Dx = Helper.ConvertStringToPoint(properties[5]).X;
                boltXY.StartPointOffset.Dy = Helper.ConvertStringToPoint(properties[5]).Y;
                boltXY.StartPointOffset.Dz = Helper.ConvertStringToPoint(properties[5]).Z;

                boltXY.EndPointOffset.Dx = Helper.ConvertStringToPoint(properties[6]).X;
                boltXY.EndPointOffset.Dy = Helper.ConvertStringToPoint(properties[6]).Y;
                boltXY.EndPointOffset.Dz = Helper.ConvertStringToPoint(properties[6]).Z;

                foreach (string hhh in properties)
                {
                    if (hhh.Contains("OTHERPART"))
                    {
                        try
                        {
                            var other = hhh.Split('$');
                            var ID3 = idNew[idOriginal.IndexOf(other[1])];
                            boltXY.AddOtherPartToBolt(m.SelectModelObject(ID3) as Part);
                        }
                        catch
                        {
                        }
                    }
                }

                boltIdlist.Add(properties[32]);

                boltXY.Insert();
                boltXY.Select();
                boltIdNewlist.Add(boltXY.Identifier.ID.ToString());
            }
            catch
            {
                boltArrayCount++;
            }
        }
    }
}
