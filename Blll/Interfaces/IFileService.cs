using System;
using System.IO;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IFileService
        {
            Task<Uri> SaveFile(Stream stream, string fileExtension);
            Task RemoveFile(string fileUrl);
        }
}
