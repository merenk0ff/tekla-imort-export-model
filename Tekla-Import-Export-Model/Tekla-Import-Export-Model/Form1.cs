using System;
using System.Windows.Forms;

namespace Tekla_Import_Export_Model
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var settings = new MySettings()
            {
                ContourPlates = chB_ContourPlates.Checked,
                Beams = chB_Beams.Checked,
                BoltArrays = chB_BoltArrays.Checked,
                Fittings = chB_Fittings.Checked,
                PolyBeams = chB_PolyBeams.Checked,
                BoltXY = chB_BoltYX.Checked,
                Welds = chB_Welds.Checked,
                CheckPlates = chB_CheckPlates.Checked,
                BooleanParts = chB_BooleanParts.Checked
            };


            Export.Export.ExportModel(settings);
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            var settings = new MySettings()
            {
                ContourPlates = chB_ContourPlates.Checked,
                Beams = chB_Beams.Checked,
                BoltArrays = chB_BoltArrays.Checked,
                Fittings = chB_Fittings.Checked,
                PolyBeams = chB_PolyBeams.Checked,
                BoltXY = chB_BoltYX.Checked,
                Welds = chB_Welds.Checked,
                CheckPlates = chB_CheckPlates.Checked,
                BooleanParts = chB_BooleanParts.Checked
            };

            Import_Export.Import.importModel(settings);
        }
    }
}
