using MetroSet_UI.Forms;

namespace P5RFlagComparer
{
    partial class FlagHistory : MetroSetForm
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
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl_MappingType = new MetroSet_UI.Controls.MetroSetTabControl();
            this.tabPage_FlagMappings = new System.Windows.Forms.TabPage();
            this.listBox_FlagMappings = new System.Windows.Forms.ListBox();
            this.tabPage_CountMappings = new System.Windows.Forms.TabPage();
            this.listBox_CountMappings = new System.Windows.Forms.ListBox();
            this.listBox_History = new System.Windows.Forms.ListBox();
            this.tlp_Main.SuspendLayout();
            this.tabControl_MappingType.SuspendLayout();
            this.tabPage_FlagMappings.SuspendLayout();
            this.tabPage_CountMappings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.tabControl_MappingType, 0, 0);
            this.tlp_Main.Controls.Add(this.listBox_History, 1, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(2, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Size = new System.Drawing.Size(586, 471);
            this.tlp_Main.TabIndex = 0;
            // 
            // tabControl_MappingType
            // 
            this.tabControl_MappingType.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            this.tabControl_MappingType.AnimateTime = 200;
            this.tabControl_MappingType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabControl_MappingType.Controls.Add(this.tabPage_FlagMappings);
            this.tabControl_MappingType.Controls.Add(this.tabPage_CountMappings);
            this.tabControl_MappingType.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl_MappingType.IsDerivedStyle = true;
            this.tabControl_MappingType.ItemSize = new System.Drawing.Size(100, 38);
            this.tabControl_MappingType.Location = new System.Drawing.Point(3, 3);
            this.tabControl_MappingType.Name = "tabControl_MappingType";
            this.tabControl_MappingType.SelectedIndex = 1;
            this.tabControl_MappingType.SelectedTextColor = System.Drawing.Color.White;
            this.tabControl_MappingType.Size = new System.Drawing.Size(287, 465);
            this.tabControl_MappingType.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_MappingType.Speed = 100;
            this.tabControl_MappingType.Style = MetroSet_UI.Enums.Style.Dark;
            this.tabControl_MappingType.StyleManager = null;
            this.tabControl_MappingType.TabIndex = 0;
            this.tabControl_MappingType.ThemeAuthor = "Narwin";
            this.tabControl_MappingType.ThemeName = "MetroDark";
            this.tabControl_MappingType.UnselectedTextColor = System.Drawing.Color.Gray;
            this.tabControl_MappingType.UseAnimation = false;
            // 
            // tabPage_FlagMappings
            // 
            this.tabPage_FlagMappings.Controls.Add(this.listBox_FlagMappings);
            this.tabPage_FlagMappings.Location = new System.Drawing.Point(4, 42);
            this.tabPage_FlagMappings.Name = "tabPage_FlagMappings";
            this.tabPage_FlagMappings.Size = new System.Drawing.Size(279, 419);
            this.tabPage_FlagMappings.TabIndex = 0;
            this.tabPage_FlagMappings.Text = "Flag Mappings";
            // 
            // listBox_FlagMappings
            // 
            this.listBox_FlagMappings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBox_FlagMappings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_FlagMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_FlagMappings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listBox_FlagMappings.ForeColor = System.Drawing.Color.Silver;
            this.listBox_FlagMappings.FormattingEnabled = true;
            this.listBox_FlagMappings.ItemHeight = 20;
            this.listBox_FlagMappings.Location = new System.Drawing.Point(0, 0);
            this.listBox_FlagMappings.Name = "listBox_FlagMappings";
            this.listBox_FlagMappings.Size = new System.Drawing.Size(279, 419);
            this.listBox_FlagMappings.TabIndex = 2;
            this.listBox_FlagMappings.SelectedIndexChanged += new System.EventHandler(this.SelectedFlag_Changed);
            // 
            // tabPage_CountMappings
            // 
            this.tabPage_CountMappings.Controls.Add(this.listBox_CountMappings);
            this.tabPage_CountMappings.Location = new System.Drawing.Point(4, 42);
            this.tabPage_CountMappings.Name = "tabPage_CountMappings";
            this.tabPage_CountMappings.Size = new System.Drawing.Size(279, 419);
            this.tabPage_CountMappings.TabIndex = 1;
            this.tabPage_CountMappings.Text = "Count Mappings";
            // 
            // listBox_CountMappings
            // 
            this.listBox_CountMappings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBox_CountMappings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_CountMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_CountMappings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listBox_CountMappings.ForeColor = System.Drawing.Color.Silver;
            this.listBox_CountMappings.FormattingEnabled = true;
            this.listBox_CountMappings.ItemHeight = 20;
            this.listBox_CountMappings.Location = new System.Drawing.Point(0, 0);
            this.listBox_CountMappings.Name = "listBox_CountMappings";
            this.listBox_CountMappings.Size = new System.Drawing.Size(279, 419);
            this.listBox_CountMappings.TabIndex = 2;
            this.listBox_CountMappings.SelectedIndexChanged += new System.EventHandler(this.SelectedCount_Changed);
            // 
            // listBox_History
            // 
            this.listBox_History.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBox_History.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_History.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listBox_History.ForeColor = System.Drawing.Color.Silver;
            this.listBox_History.FormattingEnabled = true;
            this.listBox_History.ItemHeight = 20;
            this.listBox_History.Location = new System.Drawing.Point(296, 3);
            this.listBox_History.Name = "listBox_History";
            this.listBox_History.Size = new System.Drawing.Size(287, 465);
            this.listBox_History.TabIndex = 1;
            // 
            // FlagHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(590, 473);
            this.Controls.Add(this.tlp_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.HeaderHeight = -40;
            this.Name = "FlagHistory";
            this.Opacity = 0.99D;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowHeader = true;
            this.ShowLeftRect = false;
            this.Style = MetroSet_UI.Enums.Style.Dark;
            this.Text = "Flag History";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.tlp_Main.ResumeLayout(false);
            this.tabControl_MappingType.ResumeLayout(false);
            this.tabPage_FlagMappings.ResumeLayout(false);
            this.tabPage_CountMappings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private MetroSet_UI.Controls.MetroSetTabControl tabControl_MappingType;
        private System.Windows.Forms.TabPage tabPage_FlagMappings;
        private System.Windows.Forms.TabPage tabPage_CountMappings;
        private System.Windows.Forms.ListBox listBox_FlagMappings;
        private System.Windows.Forms.ListBox listBox_History;
        private System.Windows.Forms.ListBox listBox_CountMappings;
    }
}