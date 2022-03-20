using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Model;

using Tekla.Structures.Geometry3d;



namespace Tekla_Import_Export_Model.Import_Export
{
    public class Helper
    {
        public static Point ConvertStringToPoint(string pointString)
        {
            var startpoint = pointString.Split('$');
            return (new Point(Convert.ToDouble(startpoint[0]), Convert.ToDouble(startpoint[1]), Convert.ToDouble(startpoint[2])));
        }
        public static ContourPoint ConvertStringToContourPoint(string pointString)
        {
            var startpoint = pointString.Split('$');
            var cp = new ContourPoint();
            cp.X = Convert.ToDouble(startpoint[1]);
            cp.Y = Convert.ToDouble(startpoint[2]);
            cp.Z = Convert.ToDouble(startpoint[3]);
            var chamfer = new Chamfer();
            chamfer.X = Convert.ToDouble(startpoint[5]);
            chamfer.Y = Convert.ToDouble(startpoint[6]);

            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_ARC.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_ARC;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_LINE.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_LINE;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_LINE_AND_ARC.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_LINE_AND_ARC;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_NONE.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_NONE;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_ROUNDING.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_ROUNDING;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_SQUARE.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_SQUARE;
            if (startpoint[4] == Chamfer.ChamferTypeEnum.CHAMFER_SQUARE_PARALLEL.ToString())
                chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_SQUARE_PARALLEL;
            cp.Chamfer = chamfer;
             
            return cp;
        }
        public static Position GetBeamPosition(string pos)
        {
            var outPos = new Position();
            var position = pos.Split('$');
            if (position[0] == Position.DepthEnum.BEHIND.ToString())
                outPos.Depth = Position.DepthEnum.BEHIND;
            if (position[0] == Position.DepthEnum.FRONT.ToString())
                outPos.Depth = Position.DepthEnum.FRONT;
            if (position[0] == Position.DepthEnum.MIDDLE.ToString())
                outPos.Depth = Position.DepthEnum.MIDDLE;

            outPos.DepthOffset = Convert.ToDouble(position[1]);

            if (position[2] == Position.PlaneEnum.LEFT.ToString())
                outPos.Plane = Position.PlaneEnum.LEFT;
            if (position[2] == Position.PlaneEnum.MIDDLE.ToString())
                outPos.Plane = Position.PlaneEnum.MIDDLE;
            if (position[2] == Position.PlaneEnum.RIGHT.ToString())
                outPos.Plane = Position.PlaneEnum.RIGHT;

            outPos.PlaneOffset = Convert.ToDouble(position[3]);

            if (position[4] == Position.RotationEnum.BACK.ToString())
                outPos.Rotation = Position.RotationEnum.BACK;
            if (position[4] == Position.RotationEnum.BELOW.ToString())
                outPos.Rotation = Position.RotationEnum.BELOW;
            if (position[4] == Position.RotationEnum.FRONT.ToString())
                outPos.Rotation = Position.RotationEnum.FRONT;
            if (position[4] == Position.RotationEnum.TOP.ToString())
                outPos.Rotation = Position.RotationEnum.TOP;

            outPos.RotationOffset = Convert.ToDouble(position[5]);

            return outPos;
        }

        public static List<double> getBoltDist(string distListString)
        {
            var distList = new List<double>();
            var res = distListString.Split('$');
            for (var i = 0; i < res.Count() - 1; i++)
            {
                distList.Add(Convert.ToDouble(res[i]));
            }
            return distList;
        }
    }
}
