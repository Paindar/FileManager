﻿namespace FileManagerProject
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.fileTree = new System.Windows.Forms.TreeView();
            this.explorerLayout = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new FileManagerProject.GridDataViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.borderLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addrLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addrLineText = new System.Windows.Forms.TextBox();
            this.searchText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.explorerLayout)).BeginInit();
            this.explorerLayout.Panel1.SuspendLayout();
            this.explorerLayout.Panel2.SuspendLayout();
            this.explorerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.borderLayoutPanel.SuspendLayout();
            this.addrLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileTree
            // 
            this.fileTree.BackColor = System.Drawing.Color.White;
            this.fileTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.fileTree.ForeColor = System.Drawing.Color.Black;
            this.fileTree.LineColor = System.Drawing.Color.Maroon;
            this.fileTree.Location = new System.Drawing.Point(0, 0);
            this.fileTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fileTree.Name = "fileTree";
            this.fileTree.Size = new System.Drawing.Size(190, 583);
            this.fileTree.TabIndex = 0;
            this.fileTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileTree_NodeMouseClick);
            // 
            // explorerLayout
            // 
            this.explorerLayout.BackColor = System.Drawing.Color.Transparent;
            this.explorerLayout.Location = new System.Drawing.Point(3, 39);
            this.explorerLayout.Name = "explorerLayout";
            // 
            // explorerLayout.Panel1
            // 
            this.explorerLayout.Panel1.Controls.Add(this.fileTree);
            // 
            // explorerLayout.Panel2
            // 
            this.explorerLayout.Panel2.Controls.Add(this.dataGridView1);
            this.explorerLayout.Size = new System.Drawing.Size(921, 583);
            this.explorerLayout.SplitterDistance = 187;
            this.explorerLayout.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑 Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(726, 583);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.CadetBlue;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 50F;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "文件名";
            this.Column1.MinimumWidth = 17;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 69;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.CadetBlue;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 75F;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "文件标签";
            this.Column2.MinimumWidth = 17;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.Width = 81;
            // 
            // borderLayoutPanel
            // 
            this.borderLayoutPanel.AutoSize = true;
            this.borderLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderLayoutPanel.Controls.Add(this.addrLayoutPanel);
            this.borderLayoutPanel.Controls.Add(this.explorerLayout);
            this.borderLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.borderLayoutPanel.Location = new System.Drawing.Point(0, -1);
            this.borderLayoutPanel.Name = "borderLayoutPanel";
            this.borderLayoutPanel.Size = new System.Drawing.Size(927, 625);
            this.borderLayoutPanel.TabIndex = 3;
            // 
            // addrLayoutPanel
            // 
            this.addrLayoutPanel.AutoSize = true;
            this.addrLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addrLayoutPanel.Controls.Add(this.addrLineText);
            this.addrLayoutPanel.Controls.Add(this.searchText);
            this.addrLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.addrLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.addrLayoutPanel.MinimumSize = new System.Drawing.Size(100, 30);
            this.addrLayoutPanel.Name = "addrLayoutPanel";
            this.addrLayoutPanel.Size = new System.Drawing.Size(921, 30);
            this.addrLayoutPanel.TabIndex = 1;
            // 
            // addrLineText
            // 
            this.addrLineText.Dock = System.Windows.Forms.DockStyle.Left;
            this.addrLineText.Location = new System.Drawing.Point(3, 3);
            this.addrLineText.Name = "addrLineText";
            this.addrLineText.ReadOnly = true;
            this.addrLineText.Size = new System.Drawing.Size(739, 23);
            this.addrLineText.TabIndex = 0;
            // 
            // searchText
            // 
            this.searchText.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchText.Location = new System.Drawing.Point(748, 3);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(170, 23);
            this.searchText.TabIndex = 1;
            this.searchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchText_KeyPress);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(933, 637);
            this.Controls.Add(this.borderLayoutPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainWindow";
            this.Text = "迷之文件管理器";
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.explorerLayout.Panel1.ResumeLayout(false);
            this.explorerLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.explorerLayout)).EndInit();
            this.explorerLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.borderLayoutPanel.ResumeLayout(false);
            this.borderLayoutPanel.PerformLayout();
            this.addrLayoutPanel.ResumeLayout(false);
            this.addrLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView fileTree;
        private System.Windows.Forms.SplitContainer explorerLayout;
        public GridDataViewEx dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.FlowLayoutPanel borderLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel addrLayoutPanel;
        private System.Windows.Forms.TextBox addrLineText;
        private System.Windows.Forms.TextBox searchText;
    }
}

