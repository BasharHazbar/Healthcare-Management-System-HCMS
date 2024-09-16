using HCMS_Buisness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCMS.Classes
{
    public class clsGlobal
    {

        public static clsUser CurrentUser { get; set; }   

        public static string HashPassword(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            string CurrentDirector = System.IO.Directory.GetCurrentDirectory();

            string FilePath = CurrentDirector + "\\Data.txt";

            if (UserName == "" && File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }

            try {

                string DataToSave = UserName + "#//#" + Password;

                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.Write(DataToSave);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An Erorr Occureed {ex.Message}");
                return false;
            }
            
        }

        public static bool GetStoredCredential(ref string UserName, ref string Password)
        {
            try
            {
                string CurrentDirector = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirector + "\\Data.txt";

                if (File.Exists(FilePath))
                {
                    using (StreamReader Reader = new StreamReader(FilePath))
                    {
                        string Line;

                        while ((Line = Reader.ReadLine()) != null)
                        {
                            string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            UserName = Result[0];
                            Password = Result[1];
                            
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Erorr Occureed {ex.Message}");
                return false;
            }
        }
        

    }
}
