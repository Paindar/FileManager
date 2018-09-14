using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerProject
{
    public partial class PreviewWindow : Form
    {
        bool isFirst = false;
        Control showObj =null;
        
        public PreviewWindow()
        {
            InitializeComponent();
        }

        private void PreviewWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel=true;
            this.Hide();
        }
        
        public void onPreview(object sender, DataGridViewCellEventArgs e)
        {
            FileDataGridViewRow fd = (sender as GridDataViewEx).Rows[e.RowIndex] as FileDataGridViewRow;
            this.Show();
            if (!isFirst)
            {
                isFirst = true;
                GridDataViewEx gdex = (sender as GridDataViewEx);
                Control ctrl = gdex;
                while (ctrl.Parent is MainWindow == false) ctrl = ctrl.Parent;
                this.Location = new Point(ctrl.Parent.Location.X + ctrl.Parent.Width, ctrl.Parent.Location.Y);
           
            }
            string name = fd.info.name;
            string path = FileMgr.fileMgr.getDirPath(fd.info.parId) + '/' + name;
            Text = name;
            if(showObj != null)
            {
                this.Controls.Remove(showObj);
                showObj.Dispose();
                showObj = null;
            }
            bool result = tryAsImage(path);
            if(!result)
            {
                result = tryAsText(path);
                //TODO other
            }
        }
        public bool tryAsImage(string path)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                PictureBox pb = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Image = img,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                showObj = pb;
                this.Controls.Add(pb);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool tryAsText(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                String line;
                StringBuilder context = new StringBuilder();
                TextBox textBox = new TextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Multiline = true

                };
                while ((line = sr.ReadLine()) != null)
                {
                    context.Append(line).Append('\n');
                    if (context.Length >= 300)
                        break;
                }
                textBox.Text = context.ToString();
                this.Controls.Add(textBox);
                showObj = textBox;
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
