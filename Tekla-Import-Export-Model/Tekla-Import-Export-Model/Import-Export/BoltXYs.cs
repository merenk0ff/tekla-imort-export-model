
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

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
    }
}
