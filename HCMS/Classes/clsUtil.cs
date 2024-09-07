using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCMS.Classes
{
    public class clsUtil
    {
        

        private static bool CreateFolderIfDoseNotExist(string SourceFolder)
        {
            if (!Directory.Exists(SourceFolder))
            {
                try
                {
                    Directory.CreateDirectory(SourceFolder);
                    return true;
                }
                catch(Exception ex) {
                    MessageBox.Show("Error Creating Folder! " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private static string GenerateGuid()
        {
            Guid Guid = Guid.NewGuid();

            return Guid.ToString();
        }
        private static string ReplaceNameFileWithGuid(string SourceFile)
        {
            FileInfo Fi = new FileInfo(SourceFile);

            return GenerateGuid() + Fi.Extension;

        }
        public static bool CopyImageToFolderProjectImages(ref string SourseFile)
        {
           string DestinationFolder = "D:/HCMS Project/HCMS-Project-Images/";

            if (!CreateFolderIfDoseNotExist(DestinationFolder))
            {
                MessageBox.Show("Does Not Created", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceNameFileWithGuid(SourseFile);

            try
            {
                File.Copy(SourseFile,DestinationFile,true);
            }
            catch(IOException IO)
            {
                MessageBox.Show(IO.Message,"Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            SourseFile = DestinationFile;
            return true;
        }
    }
}
