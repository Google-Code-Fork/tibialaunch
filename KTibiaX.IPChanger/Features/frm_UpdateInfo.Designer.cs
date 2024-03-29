namespace KTibiaX.IPChanger.Features {
    partial class frm_UpdateInfo {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UpdateInfo));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.imgLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblReleaseDate = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblVersion = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReleaseDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Controls.Add(this.btnDownload);
            this.layoutControl1.Controls.Add(this.groupControl1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::KTibiaX.IPChanger.Properties.Resources.search;
            resources.ApplyResources(this.pictureEdit1, "pictureEdit1");
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.StyleController = this.layoutControl1;
            // 
            // btnDownload
            // 
            this.btnDownload.ImageIndex = 0;
            this.btnDownload.ImageList = this.imgLarge;
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.StyleController = this.layoutControl1;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // imgLarge
            // 
            resources.ApplyResources(this.imgLarge, "imgLarge");
            this.imgLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgLarge.ImageStream")));
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl2);
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.txtNotes);
            resources.ApplyResources(this.layoutControl2, "layoutControl2");
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.AllowFocused = false;
            this.txtNotes.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtNotes.Properties.Appearance.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Properties.Appearance.Options.UseBackColor = true;
            this.txtNotes.Properties.Appearance.Options.UseFont = true;
            this.txtNotes.Properties.ReadOnly = true;
            this.txtNotes.StyleController = this.layoutControl2;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblReleaseDate,
            this.lblVersion,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Size = new System.Drawing.Size(264, 177);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblReleaseDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.lblReleaseDate, "lblReleaseDate");
            this.lblReleaseDate.Location = new System.Drawing.Point(108, 24);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(136, 23);
            this.lblReleaseDate.TextSize = new System.Drawing.Size(0, 0);
            this.lblReleaseDate.TextVisible = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblVersion.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Location = new System.Drawing.Point(108, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(136, 24);
            this.lblVersion.TextSize = new System.Drawing.Size(0, 0);
            this.lblVersion.TextVisible = true;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtNotes;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(244, 110);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.WhiteSmoke;
            this.emptySpaceItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Firebrick;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.emptySpaceItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(108, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem1.TextVisible = true;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.WhiteSmoke;
            this.emptySpaceItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.emptySpaceItem2.AppearanceItemCaption.Options.UseBackColor = true;
            this.emptySpaceItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.emptySpaceItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.emptySpaceItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(108, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem2.TextVisible = true;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(447, 231);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(155, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(272, 211);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnDownload;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(87, 29);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(155, 55);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.pictureEdit1;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(155, 156);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frm_UpdateInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.Name = "frm_UpdateInfo";
            this.Load += new System.EventHandler(this.frm_UpdateInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReleaseDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraLayout.EmptySpaceItem lblReleaseDate;
        private DevExpress.XtraLayout.EmptySpaceItem lblVersion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.Utils.ImageCollection imgLarge;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}