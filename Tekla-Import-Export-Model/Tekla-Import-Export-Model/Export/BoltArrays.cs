
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

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
    }
}
