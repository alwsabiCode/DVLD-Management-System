using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Global_Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();

        }
        public static bool CreateFolderOfDoesNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }
        public static string ReplaceFileNameWithGUID(string filePath)
        {
            string fileName = filePath;
            FileInfo fi = new FileInfo(filePath);
            string newFileName = fi.Extension;
            return GenerateGUID() + newFileName;
        }
        public static bool CopyImageToProjectImagesFolder(ref string imagePath)
        {

            string imagesFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderOfDoesNotExist(imagesFolder))
            {
                return false;
            }
            string newFileName = imagesFolder + ReplaceFileNameWithGUID(imagePath);
            try
            {
                File.Copy(imagePath, newFileName, true);
            }

            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            imagePath = newFileName;
            return true;
        }
    }
}
