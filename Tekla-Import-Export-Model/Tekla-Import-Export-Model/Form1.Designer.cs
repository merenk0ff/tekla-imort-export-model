namespace Tekla_Import_Export_Model
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.chB_Welds = new System.Windows.Forms.CheckBox();
            this.chB_BoltYX = new System.Windows.Forms.CheckBox();
            this.chB_BoltArrays = new System.Windows.Forms.CheckBox();
            this.chB_Fittings = new System.Windows.Forms.CheckBox();
            this.chB_BooleanParts = new System.Windows.Forms.CheckBox();
            this.chB_PolyBeams = new System.Windows.Forms.CheckBox();
            this.chB_Beams = new System.Windows.Forms.CheckBox();
            this.chB_ContourPlates = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chB_CheckPlates = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(0, 0);
            this.btn_export.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(86, 96);
            this.btn_export.TabIndex = 0;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(0, 101);
            this.btn_import.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(86, 102);
            this.btn_import.TabIndex = 1;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // chB_Welds
            // 
            this.chB_Welds.AutoSize = true;
            this.chB_Welds.Checked = true;
            this.chB_Welds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Welds.Location = new System.Drawing.Point(16, 178);
            this.chB_Welds.Name = "chB_Welds";
            this.chB_Welds.Size = new System.Drawing.Size(56, 17);
            this.chB_Welds.TabIndex = 16;
            this.chB_Welds.Text = "Welds";
            this.chB_Welds.UseVisualStyleBackColor = true;
            // 
            // chB_BoltYX
            // 
            this.chB_BoltYX.AutoSize = true;
            this.chB_BoltYX.Checked = true;
            this.chB_BoltYX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_BoltYX.Location = new System.Drawing.Point(16, 155);
            this.chB_BoltYX.Name = "chB_BoltYX";
            this.chB_BoltYX.Size = new System.Drawing.Size(58, 17);
            this.chB_BoltYX.TabIndex = 15;
            this.chB_BoltYX.Text = "BoltXY";
            this.chB_BoltYX.UseVisualStyleBackColor = true;
            // 
            // chB_BoltArrays
            // 
            this.chB_BoltArrays.AutoSize = true;
            this.chB_BoltArrays.Checked = true;
            this.chB_BoltArrays.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_BoltArrays.Location = new System.Drawing.Point(16, 132);
            this.chB_BoltArrays.Name = "chB_BoltArrays";
            this.chB_BoltArrays.Size = new System.Drawing.Size(68, 17);
            this.chB_BoltArrays.TabIndex = 14;
            this.chB_BoltArrays.Text = "BoltArray";
            this.chB_BoltArrays.UseVisualStyleBackColor = true;
            // 
            // chB_Fittings
            // 
            this.chB_Fittings.AutoSize = true;
            this.chB_Fittings.Checked = true;
            this.chB_Fittings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Fittings.Location = new System.Drawing.Point(16, 109);
            this.chB_Fittings.Name = "chB_Fittings";
            this.chB_Fittings.Size = new System.Drawing.Size(56, 17);
            this.chB_Fittings.TabIndex = 13;
            this.chB_Fittings.Text = "fittings";
            this.chB_Fittings.UseVisualStyleBackColor = true;
            // 
            // chB_BooleanParts
            // 
            this.chB_BooleanParts.AutoSize = true;
            this.chB_BooleanParts.Checked = true;
            this.chB_BooleanParts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_BooleanParts.Location = new System.Drawing.Point(16, 86);
            this.chB_BooleanParts.Name = "chB_BooleanParts";
            this.chB_BooleanParts.Size = new System.Drawing.Size(84, 17);
            this.chB_BooleanParts.TabIndex = 9;
            this.chB_BooleanParts.Text = "BooleanPart";
            this.chB_BooleanParts.UseVisualStyleBackColor = true;
            // 
            // chB_PolyBeams
            // 
            this.chB_PolyBeams.AutoSize = true;
            this.chB_PolyBeams.Checked = true;
            this.chB_PolyBeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_PolyBeams.Location = new System.Drawing.Point(16, 63);
            this.chB_PolyBeams.Name = "chB_PolyBeams";
            this.chB_PolyBeams.Size = new System.Drawing.Size(73, 17);
            this.chB_PolyBeams.TabIndex = 10;
            this.chB_PolyBeams.Text = "PolyBeam";
            this.chB_PolyBeams.UseVisualStyleBackColor = true;
            // 
            // chB_Beams
            // 
            this.chB_Beams.AutoSize = true;
            this.chB_Beams.Checked = true;
            this.chB_Beams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_Beams.Location = new System.Drawing.Point(16, 40);
            this.chB_Beams.Name = "chB_Beams";
            this.chB_Beams.Size = new System.Drawing.Size(53, 17);
            this.chB_Beams.TabIndex = 11;
            this.chB_Beams.Text = "Beam";
            this.chB_Beams.UseVisualStyleBackColor = true;
            // 
            // chB_ContourPlates
            // 
            this.chB_ContourPlates.AutoSize = true;
            this.chB_ContourPlates.Checked = true;
            this.chB_ContourPlates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_ContourPlates.Location = new System.Drawing.Point(16, 17);
            this.chB_ContourPlates.Name = "chB_ContourPlates";
            this.chB_ContourPlates.Size = new System.Drawing.Size(86, 17);
            this.chB_ContourPlates.TabIndex = 12;
            this.chB_ContourPlates.Text = "contourPlate";
            this.chB_ContourPlates.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chB_CheckPlates);
            this.panel1.Controls.Add(this.chB_ContourPlates);
            this.panel1.Controls.Add(this.chB_Welds);
            this.panel1.Controls.Add(this.chB_Beams);
            this.panel1.Controls.Add(this.chB_BoltYX);
            this.panel1.Controls.Add(this.chB_PolyBeams);
            this.panel1.Controls.Add(this.chB_BoltArrays);
            this.panel1.Controls.Add(this.chB_BooleanParts);
            this.panel1.Controls.Add(this.chB_Fittings);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 223);
            this.panel1.TabIndex = 17;
            // 
            // chB_CheckPlates
            // 
            this.chB_CheckPlates.AutoSize = true;
            this.chB_CheckPlates.Checked = true;
            this.chB_CheckPlates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_CheckPlates.Location = new System.Drawing.Point(16, 202);
            this.chB_CheckPlates.Name = "chB_CheckPlates";
            this.chB_CheckPlates.Size = new System.Drawing.Size(89, 17);
            this.chB_CheckPlates.TabIndex = 17;
            this.chB_CheckPlates.Text = "Check Plates";
            this.chB_CheckPlates.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_export);
            this.panel2.Controls.Add(this.btn_import);
            this.panel2.Location = new System.Drawing.Point(154, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(86, 202);
            this.panel2.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 243);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Import-Export Tekla models";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.CheckBox chB_Welds;
        private System.Windows.Forms.CheckBox chB_BoltYX;
        private System.Windows.Forms.CheckBox chB_BoltArrays;
        private System.Windows.Forms.CheckBox chB_Fittings;
        private System.Windows.Forms.CheckBox chB_BooleanParts;
        private System.Windows.Forms.CheckBox chB_PolyBeams;
        private System.Windows.Forms.CheckBox chB_Beams;
        private System.Windows.Forms.CheckBox chB_ContourPlates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chB_CheckPlates;
    }
}

