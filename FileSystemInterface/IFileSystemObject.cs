using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileSystemInterface
{
    public interface IFileSystemObject
    {
        bool IsDirectory();
        bool IsFile();
        string FullPath();
        string FileName();
        string DirectoryName();
        Task<IFileSystemObject> GetFile(string file);
        Task<byte[]> ReadAllBytes();

        Task<String> ReadAllText();

        Task<Stream> GetReadStream();


        Task<IEnumerable<IFileSystemObject>> EnumerateItems();
    }
}
