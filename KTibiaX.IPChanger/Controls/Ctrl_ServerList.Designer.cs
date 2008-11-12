namespace KTibiaX.IPChanger.Controls {
    partial class Ctrl_ServerList {
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem2 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem3 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lookUpEdit1.Location = new System.Drawing.Point(0, 0);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.AutoSearchColumnIndex = 2;
            toolTipTitleItem1.Appearance.Image = global::KTibiaX.IPChanger.Properties.Resources.addbk2_32;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::KTibiaX.IPChanger.Properties.Resources.addbk2_32;
            toolTipTitleItem1.Text = "Add new OT Server";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Adds a new OT Server to IPChanger server list.";
            toolTipTitleItem2.LeftIndent = 6;
            toolTipTitleItem2.Text = "TIP: You can check if the server is online.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.Items.Add(toolTipSeparatorItem1);
            superToolTip1.Items.Add(toolTipTitleItem2);
            toolTipTitleItem3.Appearance.Image = global::KTibiaX.IPChanger.Properties.Resources.close_32;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.Image = global::KTibiaX.IPChanger.Properties.Resources.close_32;
            toolTipTitleItem3.Text = "Delete Current Server";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Delets the current server from otserv list.";
            toolTipTitleItem4.LeftIndent = 6;
            toolTipTitleItem4.Text = "Warning: This action cannot be undone!";
            superToolTip2.Items.Add(toolTipTitleItem3);
            superToolTip2.Items.Add(toolTipItem2);
            superToolTip2.Items.Add(toolTipSeparatorItem2);
            superToolTip2.Items.Add(toolTipTitleItem4);
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "List", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "Add", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, superToolTip1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "Del", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, superToolTip2)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Version", "Version", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 100),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ip", "Ip", 150),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Port", "Port", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Exp", "Exp", 40)});
            this.lookUpEdit1.Properties.DisplayMember = "Name";
            this.lookUpEdit1.Properties.NullText = "[Select or type a server]";
            this.lookUpEdit1.Properties.PopupWidth = 710;
            this.lookUpEdit1.Properties.ValueMember = "Ip";
            this.lookUpEdit1.Size = new System.Drawing.Size(171, 20);
            toolTipTitleItem5.Appearance.Image = global::KTibiaX.IPChanger.Properties.Resources.addbk_32;
            toolTipTitleItem5.Appearance.Options.UseImage = true;
            toolTipTitleItem5.Image = global::KTibiaX.IPChanger.Properties.Resources.addbk_32;
            toolTipTitleItem5.Text = "Server List";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "You can Type, Add or Remove Servers to change the Client Login Server IP, allowin" +
                "g the connection in OT Servers or Custom Servers.";
            toolTipTitleItem6.LeftIndent = 6;
            toolTipTitleItem6.Text = "To Official Tibia Servers dont change this value.";
            superToolTip3.Items.Add(toolTipTitleItem5);
            superToolTip3.Items.Add(toolTipItem3);
            superToolTip3.Items.Add(toolTipSeparatorItem3);
            superToolTip3.Items.Add(toolTipTitleItem6);
            this.lookUpEdit1.SuperTip = superToolTip3;
            this.lookUpEdit1.TabIndex = 0;
            this.lookUpEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpEdit1_ButtonClick);
            this.lookUpEdit1.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.lookUpEdit1_EditValueChanging);
            this.lookUpEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lookUpEdit1_KeyPress);
            this.lookUpEdit1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lookUpEdit1_KeyUp);
            this.lookUpEdit1.TextChanged += new System.EventHandler(this.lookUpEdit1_TextChanged);
            // 
            // Ctrl_ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookUpEdit1);
            this.Name = "Ctrl_ServerList";
            this.Size = new System.Drawing.Size(171, 20);
            this.Load += new System.EventHandler(this.Ctrl_ServerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
    }
}
