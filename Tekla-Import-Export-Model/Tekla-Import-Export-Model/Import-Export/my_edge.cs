using Tekla.Structures.Model;
using  Tekla.Structures.Geometry3d;

namespace Tekla_Import_Export_Model.Import_Export
{
    class MyEdge
    {
        public Point FirstEnd { get; set; }
        public Point SecondEnd { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Father { get; set; }

        public static MyEdge CreateEdge(EdgeChamfer chamfer)
        {
            var output = new MyEdge()
            {
                FirstEnd = chamfer.FirstEnd,
                SecondEnd = chamfer.SecondEnd,
                X = chamfer.Chamfer.X,
                Y = chamfer.Chamfer.Y,
                Father = chamfer.Father.Identifier.ID
            };


            return output;
        }
    }
}
