using System.IO;
using System.Text;

namespace AspFinal.Utils {
    public static class ApiUtils {
        public static string ApiControllers() {
            string[] filePaths = Directory.GetFiles(@"Z:\Woz U\Projects\AspFinal\Controllers");
            StringBuilder sb = new(filePaths.Length/2);
            foreach(string file in  filePaths) {
                if(file.Contains("Api")) {
                    string fileName = file.Split("\\")[5].Replace("Controller.cs", "").ToLower();
                    sb.Append(fileName + ", ");
                }
            }
            return sb.ToString();
        }
    }
}