using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WebCommunity.Models;
//using Microsoft.AspNetCore.Hosting.IWebHostEnvironment : IHostEnvironment

namespace WebCommunity.Helpers
{

    public class DeleteImages : IDeleteImages
    {
     
        private readonly IWebHostEnvironment _webHostEnv;
        
        public DeleteImages(IWebHostEnvironment hostingEnv)
        {
            _webHostEnv = hostingEnv ?? throw new ArgumentNullException(nameof(hostingEnv));
        }

        public void Delete(string path, string partialFileName)
        {
            string webRoot = _webHostEnv.WebRootPath;//wwwroot

            if (path == null || path == "")
            {
                System.Diagnostics.Debug.WriteLine("hittar inte sökvägen");
                return;
            };

            if (Directory.Exists(Path.Combine(webRoot, path + "/")))
            {
                var _fullPath = Path.Combine(webRoot, path + "/");

                string[] fileList = Directory.GetFiles(_fullPath, "*" + partialFileName + "*");
                foreach (string file in fileList)
                {
                    //todo: Make better dialog to user
                    System.Diagnostics.Debug.WriteLine(file + " kommer att raderas");
                    System.IO.File.Delete(file);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Hittar inte platsen där bilderna finns");

            }


        }
    }
}
