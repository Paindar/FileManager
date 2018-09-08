using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerProject
{
    static class Program
    {
        static string rootPath = "E:\\GDriver";
        static MainWindow mainWindow;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainWindow = new MainWindow();
            FileMgr.fileMgr = new FileMgr(rootPath);
            initialize(new DirectoryInfo(rootPath), 0);
            DirNode rootNode = new DirNode(FileMgr.fileMgr.getDirItem(0));
            mainWindow.fileTree.Nodes.Add(rootNode);
            initDirsData(0, rootNode);
            Application.Run(mainWindow);
        }
        public static void initialize(DirectoryInfo path, int dirId)
        {
            int curDir = dirId;
            foreach (DirectoryInfo NextFolder in path.GetDirectories())
            {
                int i = FileMgr.fileMgr.addDir(NextFolder.Name, curDir) ;
                initialize(NextFolder, i);
            }
            foreach (FileInfo NextFile in path.GetFiles())
            {
                FileMgr.fileMgr.addFile(NextFile.Name, dirId);
            }
        }
        public static void initDirsData(int dirId, DirNode node)
        {
            foreach(var subDir in FileMgr.fileMgr.getSubDirs(dirId))
            {
                DirNode subNode = new DirNode(subDir);
                node.Nodes.Add(subNode);
                initDirsData(subDir.id, subNode);
            }
        }
    }
}
