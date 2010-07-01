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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ctrl_ConfigFile));
            this.ddlFiles = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFiles.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlFiles
            // 
            resources.ApplyResources(this.ddlFiles, "ddlFiles");
            this.ddlFiles.BackgroundImage = null;
            this.ddlFiles.EditValue = null;
            this.ddlFiles.Name = "ddlFiles";
            this.ddlFiles.Properties.AccessibleDescription = null;
            this.ddlFiles.Properties.AccessibleName = null;
            this.ddlFiles.Properties.AutoHeight = ((bool)(resources.GetObject("ddlFiles.Properties.AutoHeight")));
            this.ddlFiles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ddlFiles.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ddlFiles.Properties.Buttons1")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ddlFiles.Properties.Buttons2"))))});
            this.ddlFiles.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("ddlFiles.Properties.Columns"), ((int)(resources.GetObject("ddlFiles.Properties.Columns1"))), resources.GetString("ddlFiles.Properties.Columns2")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("ddlFiles.Properties.Columns3"), ((int)(resources.GetObject("ddlFiles.Properties.Columns4"))), resources.GetString("ddlFiles.Properties.Columns5")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("ddlFiles.Properties.Columns6"), ((int)(resources.GetObject("ddlFiles.Properties.Columns7"))), resources.GetString("ddlFiles.Properties.Columns8"))});
            this.ddlFiles.Properties.DisplayMember = "Description";
            this.ddlFiles.Properties.NullText = resources.GetString("ddlFiles.Properties.NullText");
            this.ddlFiles.Properties.NullValuePrompt = resources.GetString("ddlFiles.Properties.NullValuePrompt");
            this.ddlFiles.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("ddlFiles.Properties.NullValuePromptShowForEmptyValue")));
            this.ddlFiles.Properties.ValueMember = "Path";
            this.ddlFiles.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ddlFiles_ButtonClick);
            this.ddlFiles.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.ddlFiles_EditValueChanging);
            this.ddlFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ddlFiles_KeyUp);
            this.ddlFiles.TextChanged += new System.EventHandler(this.ddlFiles_TextChanged);
            // 
            // Ctrl_ConfigFile
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.ddlFiles);
            this.Name = "Ctrl_ConfigFile";
            this.Load += new System.EventHandler(this.Ctrl_ConfigFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ddlFiles.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit ddlFiles;
    }
}
