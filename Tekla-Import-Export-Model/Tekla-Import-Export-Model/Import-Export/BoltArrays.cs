
using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Import_Export;

namespace Tekla_Import_Export_Model.Export
{
    public class BoltArrays
    {
        public static void ExportBoltArray(List<BoltArray> boltArrayList, List<string> outStringList)
        {
            foreach (var boltArray in boltArrayList)
            {
                var boltArrayListLocal = new List<string>();
                boltArrayListLocal.Add("###BoltArrayStart###");
                boltArrayListLocal.Add('|' + boltArray.BoltSize.ToString());
                boltArrayListLocal.Add('|' + boltArray.BoltStandard);
                boltArrayListLocal.Add('|' + boltArray.BoltType.ToString());
                boltArrayListLocal.Add('|' + boltArray.CutLength.ToString());
                boltArrayListLocal.Add('|' + boltArray.StartPointOffset.Dx.ToString() + '$' +
                                       boltArray.StartPointOffset.Dy.ToString() + '$' +
                                       boltArray.StartPointOffset.Dz.ToString() + '$');
                boltArrayListLocal.Add('|' + boltArray.EndPointOffset.Dx.ToString() + '$' +
                                       boltArray.EndPointOffset.Dy.ToString() + '$' +
                                       boltArray.EndPointOffset.Dz.ToString() + '$');
                boltArrayListLocal.Add('|' + boltArray.ExtraLength.ToString());
                boltArrayListLocal.Add('|' + boltArray.FirstPosition.X.ToString() + '$' +
                                       boltArray.FirstPosition.Y.ToString() + '$' +
                                       boltArray.FirstPosition.Z.ToString() + '$');
                boltArrayListLocal.Add('|' + boltArray.SecondPosition.X.ToString() + '$' +
                                       boltArray.SecondPosition.Y.ToString() + '$' +
                                       boltArray.SecondPosition.Z.ToString() + '$');
                boltArrayListLocal.Add('|' + boltArray.Hole1.ToString());
                boltArrayListLocal.Add('|' + boltArray.Hole2.ToString());
                boltArrayListLocal.Add('|' + boltArray.Hole3.ToString());
                boltArrayListLocal.Add('|' + boltArray.Hole4.ToString());
                boltArrayListLocal.Add('|' + boltArray.Hole5.ToString());
                boltArrayListLocal.Add('|' + boltArray.HoleType.ToString());
                boltArrayListLocal.Add('|' + boltArray.Nut1.ToString());
                boltArrayListLocal.Add('|' + boltArray.Nut2.ToString());
                boltArrayListLocal.Add('|' + boltArray.RotateSlots.ToString());
                boltArrayListLocal.Add('|' + boltArray.SlottedHoleX.ToString());
                boltArrayListLocal.Add('|' + boltArray.SlottedHoleY.ToString());
                boltArrayListLocal.Add('|' + boltArray.ThreadInMaterial.ToString());
                boltArrayListLocal.Add('|' + boltArray.Tolerance.ToString());
                boltArrayListLocal.Add('|' + boltArray.Washer1.ToString());
                boltArrayListLocal.Add('|' + boltArray.Washer2.ToString());
                boltArrayListLocal.Add('|' + boltArray.Washer3.ToString());
                boltArrayListLocal.Add('|' + boltArray.Position.Depth.ToString() + "$" +
                                       boltArray.Position.DepthOffset.ToString() + "$" +
                                       boltArray.Position.Plane.ToString() + "$" +
                                       boltArray.Position.PlaneOffset.ToString() + "$" +
                                       boltArray.Position.Rotation.ToString() + "$" +
                                       boltArray.Position.RotationOffset.ToString());


                var distX = new StringBuilder();
                for (var i = 0; i < boltArray.GetBoltDistXCount(); i++)
                {
                    distX = distX.Append(boltArray.GetBoltDistX(i).ToString() + '$');
                }

                var distY = new StringBuilder();
                for (var i = 0; i < boltArray.GetBoltDistYCount(); i++)
                {
                    distY = distY.Append(boltArray.GetBoltDistY(i).ToString() + '$');
                }

                boltArrayListLocal.Add('|' + distX.ToString());
                boltArrayListLocal.Add('|' + distY.ToString());
                boltArrayListLocal.Add('|' + boltArray.PartToBeBolted.Identifier.ID.ToString());
                boltArrayListLocal.Add('|' + boltArray.PartToBoltTo.Identifier.ID.ToString());
                boltArrayListLocal.Add('|' + boltArray.Bolt.ToString());
                boltArrayListLocal.Add('|' + boltArray.Identifier.ID.ToString());
                var otherPartToBolt = boltArray.GetOtherPartsToBolt();
                foreach (ModelObject otherParts in otherPartToBolt)
                {
                    if (otherParts is Part)
                    {
                        boltArrayListLocal.Add("|OTHERPART$" + otherParts.Identifier.ID.ToString());
                    }
                }

                boltArrayListLocal.Add("|###BoltArrayEnd###");
                var outString = new StringBuilder();
                foreach (var localString in boltArrayListLocal)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }

        public static void ImportBoltArrays(List<Identifier> idNew, List<string> idOriginal, string[] properties, Model m, List<string> boltIdlist,
           List<string> boltIdNewlist, ref int boltArrayCount)
        {
            try
            {
                var boltArray = new BoltArray();

                var ID = idNew[idOriginal.IndexOf(properties[29])];
                var ID2 = idNew[idOriginal.IndexOf(properties[30])];

                boltArray.PartToBeBolted = m.SelectModelObject(ID) as Part;
                boltArray.PartToBoltTo = m.SelectModelObject(ID2) as Part;
                boltArray.FirstPosition = Helper.ConvertStringToPoint(properties[8]);
                boltArray.SecondPosition = Helper.ConvertStringToPoint(properties[9]);
                var distXlist = Helper.getBoltDist(properties[27]);
                var distYlist = Helper.getBoltDist(properties[28]);
                foreach (var d in distXlist)
                    boltArray.AddBoltDistX(d);
                foreach (var d in distYlist)
                    boltArray.AddBoltDistY(d);
                boltArray.Bolt = Convert.ToBoolean(properties[31]);
                boltArray.Hole1 = Convert.ToBoolean(properties[10]);
                boltArray.Hole2 = Convert.ToBoolean(properties[11]);
                boltArray.Hole3 = Convert.ToBoolean(properties[12]);
                boltArray.Hole4 = Convert.ToBoolean(properties[13]);
                boltArray.Hole5 = Convert.ToBoolean(properties[14]);
                boltArray.Nut1 = Convert.ToBoolean(properties[16]);
                boltArray.Nut2 = Convert.ToBoolean(properties[17]);
                boltArray.Washer1 = Convert.ToBoolean(properties[23]);
                boltArray.Washer2 = Convert.ToBoolean(properties[24]);
                boltArray.Washer3 = Convert.ToBoolean(properties[25]);
                boltArray.Tolerance = Convert.ToDouble(properties[22]);
                boltArray.Position = Helper.GetBeamPosition(properties[26]);

                boltArray.BoltStandard = properties[2];

                boltArray.BoltSize = Convert.ToDouble(properties[1]);

                if (properties[3] == "BOLT_TYPE_SITE")
                    boltArray.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
                else
                    boltArray.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;

                if (properties[21] == "THREAD_IN_MATERIAL_YES")
                    boltArray.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;
                else
                    boltArray.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                boltArray.CutLength = Convert.ToDouble(properties[4]);
                boltArray.SlottedHoleX = Convert.ToDouble(properties[19]);
                boltArray.SlottedHoleY = Convert.ToDouble(properties[20]);
                boltArray.ExtraLength = Convert.ToDouble(properties[7]);

                if (properties[15] == "HOLE_TYPE_OVERSIZED")
                    boltArray.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_OVERSIZED;
                else
                    boltArray.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;
                if (properties[18] == "ROTATE_SLOTS_EVEN")
                    boltArray.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_EVEN;
                else if (properties[18] == "ROTATE_SLOTS_ODD")
                    boltArray.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_ODD;
                else
                    boltArray.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_PARALLEL;

                boltArray.StartPointOffset.Dx = Helper.ConvertStringToPoint(properties[5]).X;
                boltArray.StartPointOffset.Dy = Helper.ConvertStringToPoint(properties[5]).Y;
                boltArray.StartPointOffset.Dz = Helper.ConvertStringToPoint(properties[5]).Z;

                boltArray.EndPointOffset.Dx = Helper.ConvertStringToPoint(properties[6]).X;
                boltArray.EndPointOffset.Dy = Helper.ConvertStringToPoint(properties[6]).Y;
                boltArray.EndPointOffset.Dz = Helper.ConvertStringToPoint(properties[6]).Z;

                foreach (var hhh in properties)
                {
                    if (hhh.Contains("OTHERPART"))
                    {
                        try
                        {
                            var other = hhh.Split('$');
                            var ID3 = idNew[idOriginal.IndexOf(other[1])];
                            boltArray.AddOtherPartToBolt(m.SelectModelObject(ID3) as Part);
                        }
                        catch
                        {
                        }
                    }
                }

                boltIdlist.Add(properties[32]);

                boltArray.Insert();
                boltArray.Select();
                boltIdNewlist.Add(boltArray.Identifier.ID.ToString());
            }
            catch
            {
                boltArrayCount++;
            }
        }
    }
}
