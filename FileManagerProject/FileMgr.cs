using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace FileManagerProject
{
    [Serializable]
    class DirItem : ISerializable
    {
        public int id { get; }
        public string name { get; set; }
        public int parId { get; set; }
        public DirItem(int id, string name, int parId)
        {
            this.id = id;
            this.name = name;
            this.parId = parId;
        }
        protected DirItem(SerializationInfo info, StreamingContext context)
        {
            id = info.GetInt32("id");
            name = info.GetString("name");
            parId = info.GetInt32("parId");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", id);
            info.AddValue("name", name);
            info.AddValue("parId", parId);
        }
    }
    [Serializable]
    class FileItem : ISerializable
    {
        public int id { get; }
        public string name { get; set; }
        public int parId { get; set; }
        public FileItem(int id, string name, int parId)
        {
            this.id = id;
            this.name = name;
            this.parId = parId;
        }
        protected FileItem(SerializationInfo info, StreamingContext context)
        {
            id = info.GetInt32("id");
            name = info.GetString("name");
            parId = info.GetInt32("parId");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", id);
            info.AddValue("name", name);
            info.AddValue("parId", parId);
        }
    }
    [Serializable]
    class TagItem : ISerializable
    {
        public int id { get; set; }
        public string name { get; set; }
        public TagItem(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        protected TagItem(SerializationInfo info, StreamingContext context)
        {
            id = info.GetInt32("id");
            name = info.GetString("name");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("id", id);
            info.AddValue("name", name);
        }

    }
    [Serializable]
    class TagPair : ISerializable
    {
        public int fileId { get; set; }
        public int tagId { get; set; }
        public TagPair(int fileId, int tagId)
        {
            this.fileId = fileId;
            this.tagId = tagId;
        }
        protected TagPair(SerializationInfo info, StreamingContext context)
        {
            fileId = info.GetInt32("fileId");
            tagId = info.GetInt32("tagId");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fileId", fileId);
            info.AddValue("tagId", tagId);
        }
    }

    class FileMgr
    {
        public static FileMgr fileMgr = null;
        private List<DirItem> dirs = new List<DirItem>();
        private List<FileItem> files = new List<FileItem>();
        private List<TagItem> tags = new List<TagItem>();
        private List<TagPair> tagPairs = new List<TagPair>();
        private bool isDirsDirty = false;
        private bool isFilesDirty = false;
        private bool isTagsDirty = false;

        public FileMgr(string root)
        {
            dirs.Add(new DirItem(0, root, -1));
        }
        public int addDir(string dirName, int parId)
        {
            if (dirs.FindIndex(i => i.id == parId) != -1)
            {
                int dirId = allocNewDir();
                dirs.Add(new DirItem(dirId, dirName, parId));
                return dirId;
            }
            return -1;
        }
        public int addFile(string fileName, int parId)
        {
            if(dirs.FindIndex( i=> i.id==parId )!=-1)
            {
                int fileId = allocNewFileId();
                files.Add(new FileItem(fileId, fileName, parId));
                return fileId;
            }
            return -1;
        }
        public bool addTag(int fileId, string tag)
        {
            if (tag == "")
                return false;
            int index = files.FindIndex(i => i.id == fileId);
            if(index != -1)
            {
                int tagId = tags.FindIndex(i => i.name.Equals(tag));
                if(tagId == -1)
                {
                    tagId = allocNewTag();
                    tags.Add(new TagItem(tagId, tag));
                    tagPairs.Add(new TagPair(fileId, tagId));
                    return true;
                }
                foreach(var i in tagPairs)
                {
                    if(i.tagId==tagId && i.fileId == fileId)
                    {
                        return false;
                    }
                }
                tagPairs.Add(new TagPair(fileId, tagId));
                return true;
            }
            return false;
        }
        private int allocNewDir()
        {
            if (isDirsDirty)
            {
                dirs.Sort((a, b) => a.id - b.id);
            }
            int i = 0;
            for (; i < dirs.Count; i++)
            {
                if (dirs[i].id != i)
                    return i;
            }
            isDirsDirty = false;
            return i;
        }

        private int allocNewFileId()
        {
            if (isFilesDirty)
            {
                files.Sort((a, b) =>  a.id - b.id);
            }
            int i = 0;
            for(;i<files.Count;i++)
            {
                if (files[i].id != i)
                    return i;
            }
            isFilesDirty = false;
            return i;
        }
        private int allocNewTag()
        {
            if (isTagsDirty)
            {
                tags.Sort((a, b) => a.id - b.id);
            }
            int i = 0;
            for (; i < tags.Count; i++)
            {
                if (tags[i].id != i)
                    return i;
            }
            isTagsDirty = false;
            return i;
        }

        public bool deleteDir(int id)
        {
            int index = dirs.FindIndex(i => i.id == id);
            if(index !=-1)
            {
                foreach(var dir in dirs.FindAll(d=>d.parId==id))
                {
                    deleteDir(dir.id);
                    foreach(var file in files.FindAll(f=>f.parId==id))
                    {
                        deleteFile(file.id);
                    }
                }
                dirs.RemoveAt(index);
                return true;
            }
            return false;
        }
        public bool deleteFile(int id)
        {
            int index = files.FindIndex(i => i.id == id);
            if(index!=-1)
            {
                files.RemoveAt(index);
                tagPairs.RemoveAll(i => i.fileId == id);
                return true;
            }
            return false;
        }
        public bool deleteTag(int id)
        {
            int index = tags.FindIndex(i => i.id == id);
            if (index != -1)
            {
                tagPairs.RemoveAll(d => d.tagId == id);
                tags.RemoveAt(index);
                return true;
            }
            return false;
        }
        public string getDirPath(int id)
        {
            int parId = id;
            string path = "";
            while (parId != -1)
            {
                DirItem parent = dirs.Find(i => i.id == parId);
                path = parent.name + '/' + path;
                parId = parent.parId;
            }
            return path;
        }
        public DirItem getDirItem(int id)
        {
            return dirs.Find(i => i.id == id);
        }
        public FileItem getFile(int fileId)
        {
             return files.Find(i => i.id == fileId);
        }
        public string getFilePath(int id)
        {
            int index = files.FindIndex(i => i.id == id);
            if(index!=-1)
            {
                int parId = files[index].parId;
                string path = files[index].name;
                return getDirPath(parId) + path;
            }
            return null;
        }
        public List<int> getFilesFromTag(string tagName)
        {
            int index = tags.FindIndex(i => i.name == tagName);
            if (index != -1)
                return tagPairs.FindAll(i => i.tagId == index).Select(i => i.fileId).ToList(); ;
            return new List<int>();
        }
        public List<string> getFileTags(int id)
        {
            return tagPairs.FindAll(i => i.fileId == id)
                .Select(pair => tags.Find(tag => tag.id == pair.tagId).name).ToList();
        }
        public List<TagItem> getFileTagsId(int id)
        {
            return tagPairs.FindAll(i => i.fileId == id)
                .Select(pair=>tags.Find(tag=>tag.id==pair.tagId)).ToList();
        }
        public List<DirItem> getSubDirs(int id)
        {
            return dirs.FindAll(i => i.parId == id);
        }
        public List<FileItem> getFiles(int id)
        {
            return files.FindAll(i => i.parId == id);
        }
        public List<int> getFilesIndex(int id)
        {
            return files.FindAll(i => i.parId == id).Select(i => i.id).ToList();
        }
        public bool removeTagFromFile(int fileId, int tagId)
        {
            return tagPairs.RemoveAll(p => { return p.fileId == fileId && p.tagId == tagId; })>0;
        }
        public bool removeTagFromFile(int fileId, string tag)
        {
            int index = tags.FindIndex(i => i.name.Equals(tag));
            if(index!=-1)
            {
                int tagId = tags[index].id;
                return removeTagFromFile(fileId, tagId);
            }
            return false;
        }
        public bool renameFile(int fileId, string newName)
        {
            FileItem item = files.Find(f => f.id == fileId);
            if(item.name.Count()==0)
            {
                return false;
            }
            else
            {
                return renameFile(item, newName);
            }
        }
        public bool renameFile(FileItem item, string newName)
        {
            string dirPath = getDirPath(item.parId);
            try
            {
                File.Move(dirPath + '/' + item.name, dirPath + "/" + newName);
            }
            catch(Exception e)
            {
                //TODO Msg report.
                return false;
            }
            return true;
        }

        public void save(string file)
        {
            Stream fStream = new FileStream(file, FileMode.Create, FileAccess.Write);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            Hashtable hash = new Hashtable
            {
                { "files", files },
                { "dirs", dirs },
                { "tags", tags },

                { "tagPairs", tagPairs }
            };
            binFormat.Serialize(fStream, hash);
            fStream.Close();
        }
        public void load(string file)
        {
            Hashtable hash = new Hashtable();
            Stream fStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            hash = (Hashtable)binFormat.Deserialize(fStream);//反序列化对象
            files = (List<FileItem>)hash["files"];
            dirs = (List<DirItem>)hash["dirs"];
            tags = (List<TagItem>)hash["tags"];
            tagPairs = (List<TagPair>)hash["tagPairs"];
            fStream.Close();
            List<TagItem> availableTags = tagPairs.Select(tp => tags.Find(tag=>tag.id==tp.tagId)).ToList();
            tags = availableTags;
        }
    }
}
