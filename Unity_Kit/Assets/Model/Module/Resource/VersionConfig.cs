
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ET
{
    public class FileVersionInfo
    {
        public string File;
        public string MD5;
        public long Size;
    }


    public class VersionConfig : Object
    {
        public int Version;

        public long TotalSize;

        [BsonIgnore]
        public Dictionary<string, FileVersionInfo> FileInfoDict = new Dictionary<string, FileVersionInfo>();

        public override void EndInit()
        {
            base.EndInit();

            foreach (FileVersionInfo fileVersion in FileInfoDict.Values)
            {
                TotalSize += fileVersion.Size;
            }
        }

    }
}