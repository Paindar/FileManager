
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FileManagerProject
{
    public partial class MainWindow : Form
    {
        Hashtable hash = new Hashtable();
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
                    int index = this.dataGridView1.Rows.Add(new FileDataGridViewRow());
                    if (this.dataGridView1.Rows[index] is FileDataGridViewRow)
                        (this.dataGridView1.Rows[index] as FileDataGridViewRow).set(file, FileMgr.fileMgr.getFileTags(file.id).Select(tag => tag.name).ToArray());
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.ColumnIndex==1)
            { 
                string oldData = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                hash[e.RowIndex] = oldData;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (sender is FileDataGridViewRow == false)
                {
                    throw new ArgumentException("Argument sender is not type FileDataGridViewRow.");
                }
                FileDataGridViewRow data = (sender as FileDataGridViewRow);
                string newData = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                FileMgr.fileMgr.renameFile(data.info, newData);
            }
            else
            { 
                string oldData = (string)hash[e.RowIndex];
                string newData = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                if(dataGridView1.Rows[e.RowIndex] is FileDataGridViewRow == false)
                {
                    throw new ArgumentException("Argument sender is not type FileDataGridViewRow.");
                }
                Thread t = new Thread(new ParameterizedThreadStart(updateTag));
                t.Start(new object[] { oldData, newData, (dataGridView1.Rows[e.RowIndex] as FileDataGridViewRow).info.id });
            }
        }
        public static void updateTag(object op)
        {
            string oldData = (op as object[])[0] as string,
                newData = (op as object[])[1] as string;
            int fileId = (int)(op as object[])[2];
            string[] oldDatas = oldData.Split(' ');
            string[] newDatas = newData.Split(' ');
            string[] delDatas = oldDatas.Except(newDatas).ToArray(),
                addDatas = newDatas.Except(oldDatas).ToArray();

            delDatas.Select(tag => FileMgr.fileMgr.removeTagFromFile(fileId, tag));
            newDatas.Select(tag => FileMgr.fileMgr.addTag(fileId, tag));
            MessageBox.Show(string.Join(" ", result.Select(b=>b.ToString())));

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
    class FileDataGridViewRow : DataGridViewRow
    {
        public FileItem info = null;
        public FileDataGridViewRow()
        {

        }
        public void set(FileItem item, params string[] tags)
        {
            this.info = item;
            this.SetValues(item.name, string.Join(" ", tags));
        }
    }
}
