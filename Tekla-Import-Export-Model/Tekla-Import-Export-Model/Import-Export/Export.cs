﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using NFX.Serialization.Slim;
using Tekla_Import_Export_Model.Import_Export;

namespace Tekla_Import_Export_Model.Export
{
    public class Export
    {
        public static void ExportModel(MySettings settings)
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
                typeof(ContourPlate), typeof(Beam), typeof(PolyBeams), typeof(Fitting), typeof(CutPlane),
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

            //Export
            if (settings.Beams)
            {
                Beams.ExportBeams(beamList, beamCOG, BeamWeight, outStringList);
            }

            if (settings.PolyBeams)
            {
                PolyBeams.ExportPolyBeam(polyBeamList, outStringList, polyBeamWeight);
            }

            if (settings.ContourPlates)
            {
                ContourPlates.ExportContourPlates(cplList, contourPointCOG, contourPlateWeight, outStringList);
            }

            if (settings.Fittings)
            {
                Fittings.ExportFittings(fittingList, outStringList);
            }

            if (settings.BoltArrays)
            {
                BoltArrays.ExportBoltArray(boltArrayList, outStringList);
            }

            if (settings.BoltXY)
            {
                BoltXYs.ExportBoltXY(boltXYList, outStringList);
            }

            if (settings.Welds)
            {
                Welds.ExportWelds(weldList, outStringList, polygonWeldList);
            }

            if (settings.BooleanParts)
            {
                BooleanParts.ExportBooleanParts(booleanPartList, outStringList);
            }

            Assemblies.ExportAssemblies(assList, outStringList);


            WriteFileAndShowReport(outStringList, contourPointCOG, beamCOG, contourPlateWeight, polyBeamList, polyBeamWeight, beamList, BeamWeight, boltArrayList, boltXYList);

            
            ExportEdges(model);
        }

        private static void ExportEdges(Model model)
        {
            var edgeEnum = model.GetModelObjectSelector()
                .GetAllObjectsWithType(ModelObject.ModelObjectEnum.EDGE_CHAMFER);
            var edgeChamferList = new List<MyEdge>(edgeEnum.GetSize());
            while (edgeEnum.MoveNext())
            {
                var currentChamfer = edgeEnum.Current as EdgeChamfer;
                if (currentChamfer.Chamfer.Type == Chamfer.ChamferTypeEnum.CHAMFER_LINE)
                {
                    edgeChamferList.Add(MyEdge.CreateEdge(currentChamfer));
                }
            }

            using (Stream fStream = new FileStream("C://EXP//#chamfers.list", FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                var a = new SlimSerializer();
                a.Serialize(fStream, edgeChamferList);
            }
        }

        private static void WriteFileAndShowReport(
            List<string> outStringList, 
            List<Point> contourPointCOG, 
            List<Point> beamCOG,
            List<double> contourPlateWeight, 
            List<PolyBeam> polyBeamList, 
            List<double> polyBeamWeight, 
            List<Beam> beamList, 
            List<double> BeamWeight, 
            List<BoltArray> boltArrayList,
            List<BoltXYList> boltXYList)
        {
            var path = $"C:\\EXP\\export-import.txt";
            if (File.Exists(path))
                File.Delete(path);
            StreamWriter SW1 = new StreamWriter(path);
            foreach (var s in outStringList)
                SW1.WriteLine(s);
            SW1.Close();

            double cogx = 0;
            double cogy = 0;
            double cogz = 0;
            foreach (var p in contourPointCOG)
            {
                cogx += p.X;
                cogy += p.Y;
                cogz += p.Z;
            }

            cogx /= contourPointCOG.Count();
            cogy /= contourPointCOG.Count();
            cogz /= contourPointCOG.Count();

            double beamCOGx = 0;
            double beamCOGy = 0;
            double beamCOGz = 0;
            foreach (Point p in beamCOG)
            {
                beamCOGx = beamCOGx + p.X;
                beamCOGy = beamCOGy + p.Y;
                beamCOGz = beamCOGz + p.Z;
            }

            beamCOGx /= beamCOG.Count();
            beamCOGy /= beamCOG.Count();
            beamCOGz /= beamCOG.Count();

            MessageBox.Show(
                "Экспорт завершен.\n"+
                $"Центр тяжести контурных пластин: X={cogx}, Y={cogy}, Z={cogz}.\n"+
                $"Всего контурных пластин {contourPointCOG.Count()}. Вес= {contourPlateWeight.Sum().ToString()}\n" + 
                $"Количество полибалок {polyBeamList.Count()} Вес={polyBeamWeight.Sum()}\n" + 
                $"Количество балок={beamList.Count()}, Вес={BeamWeight.Sum().ToString()}.\n"  +
                $"Центр тяжести балок: X={beamCOGx}, Y={beamCOGy}, Z={beamCOGz}\n" + 
                $"Количество Массивов болтов={boltArrayList.Count()}\n" + 
                $"Количество Списков болтов={boltXYList.Count()}");
        }
    }
}
