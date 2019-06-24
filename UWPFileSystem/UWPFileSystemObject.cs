using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

using FileSystemInterface;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace UWPFileSystem
{
    public class UWPFileSystemObject : IFileSystemObject
    {
        protected IStorageItem _fsitem;
        public UWPFileSystemObject(IStorageItem fsitem) { _fsitem = fsitem; }
        public string FileName() => _fsitem.Name;
        public string DirectoryName() => _fsitem.Name;
        public string FullPath() => _fsitem.Path;
        public bool IsDirectory() => _fsitem.IsOfType(StorageItemTypes.Folder);
        public bool IsFile() => _fsitem.IsOfType(StorageItemTypes.File);

        public async Task<IFileSystemObject> GetFile(string file)
        {
            StorageFile fsitem = await StorageFile.GetFileFromPathAsync($"{_fsitem.Path}\\{file}");
            return new UWPFileSystemObject(fsitem);
        }

        public async Task<byte[]> ReadAllBytes()
        {
            return (await FileIO.ReadBufferAsync(_fsitem as StorageFile)).ToArray();
        }

        public async Task<String> ReadAllText()
        {
            return (await Windows.Storage.FileIO.ReadTextAsync(_fsitem as StorageFile));
        }

        public async Task<IEnumerable<IFileSystemObject>> EnumerateItems()
        {
            var fsobjects = new List<IFileSystemObject>();
            foreach (var file in await (_fsitem as StorageFolder).GetItemsAsync())
            {
                fsobjects.Add(new UWPFileSystemObject(file));
            }
            return fsobjects;
        }

        public async Task<Stream> GetReadStream()
        {
            return await (_fsitem as StorageFile).OpenStreamForReadAsync();
        }
    }
}
