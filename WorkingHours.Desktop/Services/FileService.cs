using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces.Services;

namespace WorkingHours.Desktop.Services
{
    public class FileService : IFileService
    {
        public void OpenFile(string fileName)
        {
            Process.Start(fileName);
        }

        public async Task SaveByteArrayToFileAsync(string fileName, byte[] content)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                await fs.WriteAsync(content, 0, content.Length);
                await fs.FlushAsync();
            }
        }
    }
}
