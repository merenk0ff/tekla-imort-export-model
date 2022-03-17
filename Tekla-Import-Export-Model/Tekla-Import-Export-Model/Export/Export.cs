using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace Tekla_Import_Export_Model.Export
{
    public class Export
    {
        public static void ExportModel(Preference preference)
        {
            var model = new Model();
            ModelObjectEnumerator moe;
            var outStringList = new List<string>();

            Phases.ExportPhases(model, outStringList);


            var beamList = new List<Beam>();
            var cplList = new List<ContourPlate>();
            var fittingList = new List<Fitting>();
            var boltArrayList = new List<BoltArray>();
            var boltXYList = new List<BoltXYList>();
            var weldList = new List<Weld>();
            var polygonWeldList = new List<PolygonWeld>();
            var booleanPartList = new List<BooleanPart>();
            var cutPlaneList = new List<CutPlane>();
            var polyBeamList = new List<PolyBeam>();
            var contourPointCOG = new List<Point>();
            var type = new List<string>();
            var polyBeamWeight = new List<double>();
            var BeamWeight = new List<double>();
            var beamCOG = new List<Point>();
            var contourPlateWeight = new List<double>();
            var assList = new List<Assembly>();

            var myType = new Type[]
            {
                typeof(ContourPlate), typeof(Beam), typeof(PolyBeam), typeof(Fitting), typeof(CutPlane),
                typeof(BoltArray), typeof(BoltXYList), typeof(Assembly), typeof(BooleanPart)
            };

            //get objects from 3d model and sort by types
            moe = model.GetModelObjectSelector().GetAllObjectsWithType(myType);
            foreach (ModelObject mo in moe)
            {
                switch (mo)
                {
                    case Beam beam:
                        beamList.Add(beam);
                        break;
                    case ContourPlate plate:
                        cplList.Add(plate);
                        break;
                    case Fitting fitting:
                        fittingList.Add(fitting);
                        break;
                    case BoltArray array:
                        boltArrayList.Add(array);
                        break;
                    case BoltXYList list:
                        boltXYList.Add(list);
                        break;
                    case Weld weld:
                        weldList.Add(weld);
                        break;
                    case PolygonWeld weld:
                        polygonWeldList.Add(weld);
                        break;
                    case BooleanPart part:
                        booleanPartList.Add(part);
                        break;
                    case CutPlane plane:
                        cutPlaneList.Add(plane);
                        break;
                    case PolyBeam beam:
                        polyBeamList.Add(beam);
                        break;
                    case Assembly assembly:
                        assList.Add(assembly);
                        break;
                }

                if (!type.Contains(mo.ToString()))
                    type.Add(mo.ToString());

            }

            if (preference.Beams)
            {
                Beams.ExportBeams(beamList, beamCOG, BeamWeight, outStringList);
            }
        }

            }
}
