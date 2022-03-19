using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;

namespace Tekla_Import_Export_Model.Export
{
    public class Phases
    {
        /// <summary>
        /// Export Phases to TXT
        /// </summary>
        /// <param name="model"></param>
        /// <param name="outStringList"></param>
        public static void ExportPhases(Model model, List<string> outStringList)
        {
            var phaseColl = model.GetPhases();
            var phaseENUM = phaseColl.GetEnumerator();
            while (phaseENUM.MoveNext())
            {
                var currentPhase = phaseENUM.Current as Phase;
                var phasesLocalList = new List<string>();
                phasesLocalList.Add("###phaseStart###"); //0
                phasesLocalList.Add('|' + currentPhase.PhaseComment);
                phasesLocalList.Add('|' + currentPhase.PhaseName);
                phasesLocalList.Add('|' + currentPhase.PhaseNumber.ToString());
                phasesLocalList.Add("|###phaseEND###"); //0
                var outString = new StringBuilder();
                foreach (string localString in phasesLocalList)
                {
                    outString = outString.Append(localString);
                }

                outStringList.Add(outString.ToString());
            }
        }

        public static void ImportPhases(string[] properties, List<Phase> phaseList)
        {
            var phase = new Phase
            {
                PhaseComment = properties[1],
                PhaseName = properties[2],
                PhaseNumber = Convert.ToInt32(properties[3])
            };
            phase.Insert();
            phaseList.Add(phase);
        }
    }
}
