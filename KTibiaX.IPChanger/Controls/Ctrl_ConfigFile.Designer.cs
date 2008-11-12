namespace KTibiaX.IPChanger.Controls {
    partial class Ctrl_ConfigFile {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ddlFiles = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFiles.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlFiles
            // 
            this.ddlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlFiles.Location = new System.Drawing.Point(0, 0);
            this.ddlFiles.Name = "ddlFiles";
            this.ddlFiles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.ddlFiles.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Version", "Version", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description", 100),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Vocation", "Vocation", 50)});
            this.ddlFiles.Properties.DisplayMember = "Description";
            this.ddlFiles.Properties.NullText = "[Default]";
            this.ddlFiles.Properties.ValueMember = "Path";
            this.ddlFiles.Size = new System.Drawing.Size(162, 20);
            this.ddlFiles.TabIndex = 0;
            this.ddlFiles.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ddlFiles_ButtonClick);
            this.ddlFiles.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.ddlFiles_EditValueChanging);
            this.ddlFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ddlFiles_KeyUp);
            this.ddlFiles.TextChanged += new System.EventHandler(this.ddlFiles_TextChanged);
            // 
            // Ctrl_ConfigFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ddlFiles);
            this.Name = "Ctrl_ConfigFile";
            this.Size = new System.Drawing.Size(162, 20);
            this.Load += new System.EventHandler(this.Ctrl_ConfigFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ddlFiles.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit ddlFiles;
    }
}
