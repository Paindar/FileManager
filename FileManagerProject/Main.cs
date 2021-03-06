﻿
using FileManagerProject.network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FileManagerProject
{
    public partial class MainWindow : Form
    {
        private List<KeyValuePair<double, double>> sizePer;
        private double splitterDstPer;
        private float progessFloat = 0.00f;
        Hashtable hash = new Hashtable();
        Thread threadPreWin = new Thread(obj =>
        {
            PreviewWindow preWin = new PreviewWindow();
            (obj as MainWindow).dataGridView1.RowEnter += preWin.onPreview;
        })
        {
            IsBackground = true
        };
        public MainWindow()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            threadPreWin.Start(this);
            sizePer = new List<KeyValuePair<double, double>>
            {
                new KeyValuePair<double, double>(1.0 * this.addrLineText.Width / this.Width, 1.0 * this.addrLineText.Height / this.Height),
                new KeyValuePair<double, double>(1.0 * this.searchText.Width / this.Width, 1.0 * this.searchText.Height / this.Height),
                new KeyValuePair<double, double>(1.0 * this.fileTree.Width / this.Width, 1.0 * this.fileTree.Height / this.Height),
                new KeyValuePair<double, double>(1.0 * this.dataGridView1.Width / this.Width, 1.0 * this.dataGridView1.Height / this.Height)
            };
            splitterDstPer = 1.0 * this.explorerLayout.SplitterDistance / this.Width;
        }

        private void fileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node is DirNode && sender is TreeView)
            {
                this.dataGridView1.Rows.Clear();
                this.addrLineText.Text = FileMgr.fileMgr.getDirPath(((DirNode)e.Node).info.id);
                List<FileItem> files = FileMgr.fileMgr.getFiles(((DirNode)e.Node).info.id);
                FileDataGridViewRow[] rows = files.Select(f =>
                {
                    var res = new FileDataGridViewRow();
                    res.CreateCells(dataGridView1, f.name);
                    res.set(f);
                    return res;
                }).ToArray();
                this.dataGridView1.Rows.AddRange(rows);
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
                if (dataGridView1.Rows[e.RowIndex] is FileDataGridViewRow == false)
                {
                    throw new ArgumentException("Argument sender is not type FileDataGridViewRow.");
                }
                FileDataGridViewRow data = (dataGridView1.Rows[e.RowIndex] as FileDataGridViewRow);
                string newData = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                FileMgr.fileMgr.renameFile(data.info, newData);
            }
            else
            { 
                string oldData = (string)hash[e.RowIndex];
                var value = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                string newData = (value==null)?"":value.ToString();
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
            foreach(var tag in newDatas)
            {
                FileMgr.fileMgr.addTag(fileId, tag);
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if(this.dataGridView1.Visible)
            {
                foreach(var r in this.dataGridView1.Rows)
                {
                    if(r is FileDataGridViewRow)
                    {
                        FileDataGridViewRow fd = r as FileDataGridViewRow;
                        if (fd.Displayed)
                        {
                            fd.update();
                        }
                    }
                }
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            this.addrLineText.Width = (int)(this.Width * sizePer[0].Key);
            this.addrLineText.Height = (int)(this.Height * sizePer[0].Value);
            this.searchText.Width = (int)(this.Width * sizePer[1].Key);
            this.searchText.Height = (int)(this.Height * sizePer[1].Value);
            this.fileTree.Width = (int)(this.Width * sizePer[2].Key);
            this.fileTree.Height = (int)(this.Height * sizePer[2].Value);
            this.dataGridView1.Width = (int)(this.Width * sizePer[3].Key);
            this.dataGridView1.Height = (int)(this.Height * sizePer[3].Value);
            this.explorerLayout.SplitterDistance = (int)(splitterDstPer * this.Width);
        }

        private void searchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                string[] context = this.searchText.Text.Split(' ');
                if(context.Count() != 0)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    DirNode node = this.fileTree.SelectedNode as DirNode;
                    int dirRangeIndex = 0;
                    if (node != null)
                        dirRangeIndex = node.info.id;

                    List<int> resultId = new List<int>();
                    foreach (var l in context.Select(tag => FileMgr.fileMgr.getFilesFromTag(tag)))
                    {
                        resultId.AddRange(l);
                    }
                    List<FileItem> files = resultId.Select(f => FileMgr.fileMgr.getFile(f)).ToList();
                    if (dirRangeIndex != 0)
                    {
                        List<FileItem> subFiles = new List<FileItem>();
                        Queue<int> s = new Queue<int>();
                        s.Enqueue(dirRangeIndex);
                        while (s.Count != 0)
                        {
                            int i = s.Dequeue();
                            foreach (var di in FileMgr.fileMgr.getSubDirs(i))
                            {
                                s.Enqueue(di.id);
                            }
                            subFiles.AddRange(FileMgr.fileMgr.getFiles(i));
                        }
                        files = files.Intersect(subFiles).ToList() ;
                    }
                    watch.Stop();
                    this.toolStripStatusLabel1.Text="用时 " + watch.ElapsedMilliseconds.ToString() + "ms.";

                    this.dataGridView1.Rows.Clear();
                    FileDataGridViewRow[] rows = files.Select(f =>
                    {
                        var res = new FileDataGridViewRow();
                        res.CreateCells(dataGridView1, f.name);
                        res.set(f);
                        return res;
                    }).ToArray();
                    this.dataGridView1.Rows.AddRange(rows);
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void allTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Join("|\n", FileMgr.fileMgr.getAllTags()));
        }
        

        private void SyncFromServerButton_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(() =>
            {
                Hashtable hash = new Hashtable();
                Mutex mutex = new Mutex(), tagMutex = new Mutex();
                FileMgr.fileMgr.recurse((FileItem item) =>
                {
                    int parId = item.parId;
                    try
                    {
                        mutex.WaitOne();
                        if (!hash.ContainsKey(parId))
                        {
                            hash.Add(parId, FileMgr.fileMgr.getDirPath(parId));
                        }
                        mutex.ReleaseMutex();
                    }
                    catch (Exception)
                    {

                    }
                    string tags = NetworkSyncer.getTagsFromServer((string)hash[parId] + '/' + item.name, Program.url).Result;
                    tagMutex.WaitOne();
                    foreach (var tag in tags.Split(' '))
                    {
                        if (tag != "")
                        {
                            FileMgr.fileMgr.addTag(item.id, tag);
                        }
                    }
                    tagMutex.ReleaseMutex();
                    return true;
                }, 64, (float prog) => {
                    try
                    {
                        mutex.WaitOne();
                        progessFloat += prog;
                        this.toolStripProgressBar1.Value = (int)(progessFloat * 100);
                        mutex.ReleaseMutex();
                    }
                    catch (Exception)
                    {

                    }
                    return true;
                });
            })
            {
                IsBackground = true
            };
            thr.Start();
        }

        private void SyncToServerButton_Click(object sender, EventArgs e)
        {
            Hashtable hash = new Hashtable();
            Mutex mgrMutex = new Mutex(), mutex = new Mutex();
            FileMgr.fileMgr.recurse((FileItem item) =>
            {
                int parId = item.parId;
                try
                {
                    mgrMutex.WaitOne();
                    if (!hash.ContainsKey(parId))
                    {
                        hash.Add(parId, FileMgr.fileMgr.getDirPath(parId));
                    }
                    mgrMutex.ReleaseMutex();
                }
                catch(Exception)
                {

                }
                var tags = FileMgr.fileMgr.getFileTags(item.id);
                if(tags.Count!=0)
                {
                    bool b = NetworkSyncer.uploadTagsToServer((string)hash[parId] + '/' + item.name, tags, Program.url).Result;
                }
                return true;
            }, 16,(float prog)=> {
                try
                {
                    mutex.WaitOne();
                    progessFloat += prog;
                    this.toolStripProgressBar1.Value = (int)(progessFloat * 100);
                    mutex.ReleaseMutex();
                }
                catch (Exception)
                {

                }
                return true;
            }
            );
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
        private bool isUpdated = false;
        public FileDataGridViewRow(){}
        public FileDataGridViewRow set(FileItem item)
        {
            this.info = item;
            return this;
        }
        public void update()
        {
            if(isUpdated==false)
            {
                isUpdated = true;
                Thread t = new Thread(obj =>
                {
                    FileDataGridViewRow fd = obj as FileDataGridViewRow;
                    List<string> tags = FileMgr.fileMgr.getFileTags(info.id);
                    string tagInfo = string.Join(" ", tags);
                    fd.Cells[1].Value = tagInfo;
                })
                {
                    IsBackground = true
                };
                t.Start(this);
            }
        }
    }
}
