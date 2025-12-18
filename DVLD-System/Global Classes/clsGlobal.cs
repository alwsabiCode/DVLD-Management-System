using Business_DVLD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static object Dictionory { get; private set; }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string filePath = CurrentDirectory + "\\data.txt";
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                string dataToSave = Username + "#//#" + Password;
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string filePath = CurrentDirectory + "\\data.txt";
                if (File.Exists(filePath))
                {


                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string data;
                        while ((data = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(data); // Output each line of data to the console

                            string[] result = data.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
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
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
