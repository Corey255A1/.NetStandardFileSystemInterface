using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSystemInterface;
namespace FrameworkFileSystem
{
    public class FrameworkFileSystemObject : IFileSystemObject
    {
        protected string _path;
        public FrameworkFileSystemObject(string path) { _path = path; }
        public string FileName() => Path.GetFileName(_path);
        public string DirectoryName() => Path.GetDirectoryName(_path);
        public string FullPath() => _path;
        public bool IsDirectory() => Directory.Exists(_path);
        public bool IsFile() => File.Exists(_path);

        public Task<IFileSystemObject> GetFile(string file)
        {
            return Task.Run(() =>
            {
                if (File.Exists($"{_path}\\{file}"))
                {
                    return (new FrameworkFileSystemObject($"{_path}\\{file}")) as IFileSystemObject;
                }
                else
                {
                    return null;
                }
            });
        }

        public Task<byte[]> ReadAllBytes()
        {
            return Task.Run(() =>
            {
                return File.ReadAllBytes(_path);
            });
        }

        public Task<String> ReadAllText()
        {
            return Task.Run(() =>
            {
                return File.ReadAllText(_path);
            });
        }

        public Task<IEnumerable<IFileSystemObject>> EnumerateItems()
        {
            return Task.Run(() => {
                var fsobjects = new List<IFileSystemObject>();
                foreach (var file in Directory.EnumerateFileSystemEntries(_path))
                {
                    fsobjects.Add(new FrameworkFileSystemObject(file));

                }
                return fsobjects.AsEnumerable();
            });

        }

        public Task<Stream> GetReadStream()
        {
            return Task.Run(() => {
                return File.Open(_path, FileMode.Open) as Stream;
            });
        }

    }
}
