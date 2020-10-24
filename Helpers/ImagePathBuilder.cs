using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Helpers
{
    public static class ImagePathBuilder
    {

        public static string NewPath(string webRoot)
        {

            string sourceDirectory = System.IO.Path.Combine("Upload", "Images");
            string pictureFolderDirectory;
            string folderYear;
            string folderMonth;
            string folderDay;

            folderYear = Convert.ToString(DateTime.Now.Year);
            folderMonth = Convert.ToString(DateTime.Now.Month);
            folderDay = Convert.ToString(DateTime.Now.Day);

            pictureFolderDirectory = System.IO.Path.Combine(webRoot, sourceDirectory, folderYear, folderMonth, folderDay);

            if (!System.IO.Directory.Exists(pictureFolderDirectory))
            {
                System.IO.Directory.CreateDirectory(pictureFolderDirectory);
                return pictureFolderDirectory;
            }
            else
            {
                return pictureFolderDirectory;
            }
            //    ImagePath = DateTime.Now.ToString("yyy-MM-dd").Replace("-", @"\");
        }
    }
}

