using System;
using System.Collections.Generic;
using System.IO;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla_Import_Export_Model.Export;

namespace Tekla_Import_Export_Model.Import_Export
{
    public class Import
    {
        public static void importModel(MySettings settings)
        {
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
            var booleanBeam = 0;
            var booleanPolybeam = 0;

            //import phases from file
            foreach (var s in inputStringList)
            {
                var properties = s.Split('|');

                if (properties[0] == "###phaseStart###")
                {
                    Phases.ImportPhases(properties, phaseList);
                }

            }
        }

        
    }
}
