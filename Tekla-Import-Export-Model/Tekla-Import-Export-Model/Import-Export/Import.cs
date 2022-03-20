using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NFX.Serialization.Slim;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Export;

namespace Tekla_Import_Export_Model.Import_Export
{
    public class Import
    {
        public static void importModel(MySettings settings)
        {
            var m = new Model();

            var beamList = new List<Beam>();
            var beamWeight = new List<double>();
            var cplList = new List<ContourPlate>();
            var cplWeight = new List<double>();
            var idOriginal = new List<string>();
            var idNew = new List<Identifier>();
            var idList = new List<string>();
            var phaseList = new List<Phase>();
            var path = $"C:\\EXP\\export-import.txt";

            var inputStringList = new List<string>();
            var contourPointStringList = new List<string>();
            if (File.Exists(path))
            {
                var sr1 = new StreamReader(path);
                while (!sr1.EndOfStream)
                {
                    inputStringList.Add(sr1.ReadLine());
                }

                sr1.Close();
            }

            var boltIdlist = new List<string>();
            var boltIdNewlist = new List<string>();

            var fittingCount = 0;
            var cutPlaneCount = 0;
            var boltArrayCount = 0;
            var weldCount = 0;
            var booleanCp = 0;
            var booleanBeamCount = 0;
            var booleanPolybeam = 0;


            foreach (var s in inputStringList)
            {
                var properties = s.Split('|');

                if (properties[0] == "###phaseStart###") //import phases from file
                {
                    Phases.ImportPhases(properties, phaseList);
                }

                if (properties[0] == "###beamStart###")
                {
                    if (settings.Beams)
                        Beams.ImportBeams(properties, beamList, phaseList, idList, idOriginal, idNew);
                }

                if (properties[0] == "###polyBeamStart###")
                {
                    if (settings.PolyBeams)
                    {
                        PolyBeams.ImportPolyBeams(properties, idList, idOriginal, idNew);
                    }
                }

                if (properties[0] == "###ContourPlateStart###")
                {
                    if (settings.ContourPlates)
                        ContourPlates.ImportPlates(contourPointStringList, s, properties, phaseList, cplList, idList,
                            idOriginal, idNew);
                }

                if (settings.Fittings)
                {
                    if (properties[0] == "###FittingStart###")
                    {

                        Fittings.ImportFittings(idNew, idOriginal, properties, m, ref fittingCount);
                    }

                    if (properties[0] == "###CutPlaneStart###")
                    {
                        Fittings.ImportCutPlanes(idNew, idOriginal, properties, m, ref cutPlaneCount);
                    }



                }

                if (settings.BooleanParts)
                {
                    if (properties[0] == "###BooleanContourPlateStart###")
                    {
                        BooleanParts.ImportBooleanContourPlates(properties, idNew, idOriginal, m, ref booleanCp);
                    }

                    if (properties[0] == "###BooleanBeamStart###")
                    {
                        BooleanParts.ImportBooleanBeams(properties, idNew, idOriginal, m, ref booleanBeamCount);
                    }

                    if (properties[0] == "###BooleanPolyBeamStart###")
                    {
                        BooleanParts.ImportBooleanPolybeam(properties, idNew, idOriginal, m, ref booleanPolybeam);
                    }
                }

                if (settings.BoltArrays)
                {
                    if (properties[0] == "###BoltArrayStart###")
                    {
                        BoltArrays.ImportBoltArrays(idNew, idOriginal, properties, m, boltIdlist, boltIdNewlist, ref boltArrayCount);
                    }
                }

                if (settings.BoltXY)
                {
                    if (properties[0] == "###BoltXYlistStart###")
                    {
                        BoltXYs.ImportBoltXYs(idNew, idOriginal, properties, m, boltIdlist, boltIdNewlist, ref boltArrayCount);
                    }
                }

                if (settings.Welds)
                {
                    if (properties[0] == "###AssStart###")
                    {
                        Welds.ImportWelds(idNew, idOriginal, properties, m);
                    }
                }

            }

            //save matchning old to new ids for model objects
            WriteFileMapPartIds(idOriginal, idNew);
            CreateFileMatchingBoltsIds(boltIdlist, boltIdNewlist);


            var repairPlate = 0;
            if (settings.CheckPlates) //check plate new and old COG and try repair plate
            {
                ContourPlates.RepairPlatePositions(cplList, idOriginal, idNew, contourPointStringList, cplWeight, repairPlate);
            }
            m.CommitChanges();

            //get allBeams weight for report
            foreach (var b in beamList)
            {
                double wb = 0;
                b.Select();
                b.GetReportProperty("WEIGHT_GROSS", ref wb);
                beamWeight.Add(wb);
            }

            MessageBox.Show("Импорт завершен" + '\n' + "fittingCount = " + fittingCount + "cutPlaneCount = " +
                            cutPlaneCount + '\n' + "boltArrayCount = " + boltArrayCount + '\n' + "weldCount = " +
                            weldCount + '\n' + "booleanCP = " + booleanCp + '\n' + "booleanBEAM = " + booleanBeamCount + '\n' +
                            "booleanPOLYBEAM = " + booleanPolybeam
                            + '\n' + "не отремонтировано листов=" + repairPlate);
            MessageBox.Show("Количество пластин =" + cplWeight.Count() + "| Вес пластин=" + cplWeight.Sum().ToString()
                            + '\n' + "Количество балок =" + beamList.Count() + "|Вес=" + beamWeight.Sum().ToString()
            );


            var reader = new StreamReader("C://EXP//compareParts.txt");
            var compareList = reader.ReadToEnd().Split('\n');

            ImportEdges(compareList, m);
        }

        public static void ImportEdges(string[] compareList, Model m)
        {
            using (Stream fStream = File.OpenRead("C://EXP//#chamfers.list"))
            {
                var viewPartSer = new SlimSerializer();
                var list = viewPartSer.Deserialize(fStream) as List<MyEdge>;

                foreach (var myEdge in list)
                {
                    var edge = new EdgeChamfer(myEdge.FirstEnd, myEdge.SecondEnd);
                    edge.Chamfer = new Chamfer(myEdge.X, myEdge.Y, Chamfer.ChamferTypeEnum.CHAMFER_LINE);

                    foreach (var s in compareList)
                    {
                        if (s.Contains('='))
                        {
                            var masterID = s.Split('=')[0];
                            var slaveID = s.Split('=')[1].Replace("\r", "");
                            if (masterID == myEdge.Father.ToString())
                            {
                                edge.Father = m.SelectModelObject(new Identifier(int.Parse(slaveID)));
                                break;
                            }
                        }
                    }

                    if (edge.Father != null)
                        edge.Insert();
                }
            }

            m.CommitChanges();
        }


        /// <summary>
        /// Save file with matching old Ids to new Ids for parts
        /// </summary>
        /// <param name="boltIdlist"></param>
        /// <param name="boltIdNewlist"></param>
        private static void CreateFileMatchingBoltsIds(List<string> boltIdlist, List<string> boltIdNewlist)
        {
            var compareBoltWriter = new StreamWriter("C://EXP//compareBolts.txt");
            for (var i = 0; i < boltIdlist.Count; i++)
            {
                compareBoltWriter.WriteLine(boltIdlist[i] + "=" + boltIdNewlist[i]);
            }

            compareBoltWriter.Close();
            compareBoltWriter.Dispose();
        }

        /// <summary>
        /// Save file with matching old Ids to new Ids for parts
        /// </summary>
        /// <param name="idOriginal"></param>
        /// <param name="idNew"></param>
        private static void WriteFileMapPartIds(List<string> idOriginal, List<Identifier> idNew)
        {
            var compareWriter = new StreamWriter("C://EXP//compareParts.txt");
            for (int i = 0; i < idOriginal.Count; i++)
            {
                compareWriter.WriteLine(idOriginal[i] + "=" + idNew[i]);
            }

            compareWriter.Close();
            compareWriter.Dispose();
        }
    }
}




