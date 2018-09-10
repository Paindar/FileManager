
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FileManagerProject
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node is DirNode && sender is TreeView)
            {
                this.dataGridView1.Rows.Clear();
                List<FileItem> files = FileMgr.fileMgr.getFiles(((DirNode)e.Node).info.id);
                foreach (var file in files)
                {
                    string tags = "";
                    foreach(var tag in FileMgr.fileMgr.getFileTags(file.id))
                    {
                        tags += tag.name + ' ';
                    }
                    int index = this.dataGridView1.Rows.Add(new string[] { file.name, tags });
                    //this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
    }
    class DirNode : TreeNode
    {
        public DirItem info = null;
        public DirNode(DirItem item)
        {
            this.info = item;
            this.Text = info.name;
            this.Name = item.id + " " + Text;
        }
    }
}
