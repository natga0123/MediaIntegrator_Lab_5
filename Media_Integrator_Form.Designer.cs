namespace MediaIntegrator_Lab5
{
    partial class Media_Integrator_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Csv_dir = new System.Windows.Forms.Button();
            this.Btn_xml_dir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Csv_dir
            // 
            this.Btn_Csv_dir.Location = new System.Drawing.Point(39, 54);
            this.Btn_Csv_dir.Name = "Btn_Csv_dir";
            this.Btn_Csv_dir.Size = new System.Drawing.Size(142, 43);
            this.Btn_Csv_dir.TabIndex = 0;
            this.Btn_Csv_dir.Text = "Sökväg till csv";
            this.Btn_Csv_dir.UseVisualStyleBackColor = true;
            this.Btn_Csv_dir.Click += new System.EventHandler(this.Btn_Csv_dir_Click);
            // 
            // Btn_xml_dir
            // 
            this.Btn_xml_dir.Location = new System.Drawing.Point(223, 54);
            this.Btn_xml_dir.Name = "Btn_xml_dir";
            this.Btn_xml_dir.Size = new System.Drawing.Size(142, 43);
            this.Btn_xml_dir.TabIndex = 1;
            this.Btn_xml_dir.Text = "Sökväg till xml";
            this.Btn_xml_dir.UseVisualStyleBackColor = true;
            this.Btn_xml_dir.Click += new System.EventHandler(this.Btn_xml_dir_Click);
            // 
            // Media_Integrator_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 233);
            this.Controls.Add(this.Btn_xml_dir);
            this.Controls.Add(this.Btn_Csv_dir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Media_Integrator_Main";
            this.Text = "MediaIntegrator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Csv_dir;
        private System.Windows.Forms.Button Btn_xml_dir;
    }
}

