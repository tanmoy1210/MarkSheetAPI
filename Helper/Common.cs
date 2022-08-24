using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using MT.DBLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MT.Helper
{
    public class Common
    {
         private static Random random = new Random();
        //public  static string GetSha256Encryption(string baseString)
        //{
        //    System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
        //    byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(baseString);
        //    byte[] cryString = sha256.ComputeHash(sha256Bytes);
        //    string sha256Str = string.Empty;
        //    for (int i = 0; i < cryString.Length; i++)
        //    {
        //        sha256Str += cryString[i].ToString("X");
        //    }
        //   return  sha256Str = sha256Str.ToLower();
        //}

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Method For saving any BASE64 file type
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="path">Path to save the file</param>
        public static string SaveImage(string base64, string path, string filename)
        {
            string fileex = ".jpg";
            if (base64.Contains("image/png;"))
            {
                fileex = ".png";
            }
            else if (base64.Contains("image/jpeg;"))
            {
                fileex = ".jpeg";
            }
            else if (base64.Contains("image/bmp;"))
            {
                fileex = ".bmp";
            }
            else if (base64.Contains("image/gif;"))
            {
                fileex = ".gif";
            }
            else if (base64.Contains("application/pdf;"))
            {
                fileex = ".pdf";
            }

            else if (base64.Contains("application/vnd.ms-excel;"))
            {
                fileex = ".xls";
            }
            else if (base64.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;"))
            {
                fileex = ".xlsx";
            }
            else if (base64.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.template;"))
            {
                fileex = ".xlsx";
            }
            else if (base64.Contains("application/vnd.ms-powerpoint;"))
            {
                fileex = ".ppt";
            }
            else if (base64.Contains("application/vnd.openxmlformats-officedocument.presentationml.presentation;"))
            {
                fileex = ".pptx";
            }
            else if (base64.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.template;"))
            {
                fileex = ".docx";
            }
            else if (base64.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document;"))
            {
                fileex = ".docx";
            }
            else if (base64.Contains("application/msword;"))
            {
                fileex = ".doc";
            }
           

            filename = filename + fileex;
            HostingEnvironment hostingEnvironment = new HostingEnvironment();
             path = hostingEnvironment.ContentRootPath + "wwwroot/"+path;
           // path = HostingEnvironment.MapPath(path);
            System.IO.Directory.CreateDirectory(path);
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(path, filename), Convert.FromBase64String(base64.Split(',').Last()));
            return filename;

        }



        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            //string sh = sha256.ComputeHash(inputString.ToString());
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        //public static string SaveImage(string base64, string path, string filename)
        //{
        //    path = HostingEnvironment.MapPath(path);
        //    System.IO.Directory.CreateDirectory(path);
        //    System.IO.File.WriteAllBytes(System.IO.Path.Combine(path, filename), Convert.FromBase64String(base64));
        //    return filename;
        //}








        //public static string GetBase64ImageString(string path)
        //{

        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //    }

        //    string imageName = ImgName + ".jpg";

        //    //set the image path
        //    string imgPath = Path.Combine(path, imageName);

        //    byte[] imageBytes = Convert.FromBase64String(ImgStr);

        //    File.WriteAllBytes(imgPath, imageBytes);

        //    return true;








        //    using (Image image = Image.FromFile(path))
        //    {
        //        using (MemoryStream m = new MemoryStream())
        //        {
        //            image.Save(m, image.RawFormat);
        //            byte[] imageBytes = m.ToArray();

        //            // Convert byte[] to Base64 String
        //            string base64String = Convert.ToBase64String(imageBytes);
        //            return base64String;
        //        }
        //    }
        //}

        public static void ValidDatePostedData(JObject data, string propertyname)
        {
            object objPostData = data[propertyname];
            JObject joData = (JObject)objPostData;

            var ToHash = "";

            foreach (JProperty property in joData.Properties())
            {
                Console.WriteLine(property.Name + " - " + property.Value);
                ToHash += property.Name + ":" + property.Value + ",";
            }

            if (Common.GenerateSHA256String(ToHash) != data["SHA"]["SHA"].ToString())
            {
                throw new Exception("Invlaid Access");
            }
        }

        #region Generate New Token
        public static object GetToken(string ClientID, IConfiguration _configuration)
        {
            string RandomData = Common.RandomString(4);
            LoginRequestAttempt obj = new LoginRequestAttempt();
            try
            {
                // using (bpr_IGICContext dbcontext =  new bpr_IGICContext())
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                //using (bpr_IGICContext dbcontext = new bpr_IGICContext(options => options.UseMySQL(Configuration.GetConnectionString("IGICConn")))
                {
                    List<LoginRequestAttempt> objul = new List<LoginRequestAttempt>();
                    objul = (from s in dbcontext.LoginRequestAttempt where s.ClientId == ClientID && s.IsActive.Value == 1 select s).ToList<LoginRequestAttempt>();
                    foreach (LoginRequestAttempt obju in objul)
                    {
                        obju.IsActive = 0;
                        obju.AttemptUser = "False / Retry";
                        obju.UseTime = System.DateTime.Now;
                    }

                    obj.ClientId = ClientID;
                    obj.KeyToAuthenticate = RandomData;
                    obj.CreateTime = System.DateTime.Now;
                    obj.IsActive = 1;
                    dbcontext.LoginRequestAttempt.Add(obj);
                    dbcontext.SaveChangesAsync();

                }
            }
            catch
            {
                throw;
            }
            Newtonsoft.Json.Linq.JObject objToken = new Newtonsoft.Json.Linq.JObject
            {
                { "Token", RandomData }
            };


            return objToken;

        }
        #endregion

        #region Update Login Attempts
        public static void UpdateLoginAttempts(mararkContext dbcontext, JObject data)
        {
            List<LoginRequestAttempt> userLoginRequestAttempts =
                                (from s in dbcontext.LoginRequestAttempt
                                 where s.ClientId == data["SHA"]["clientid"].ToString() &&
                                 s.KeyToAuthenticate == data["SHA"]["token"].ToString()
                                 select s).ToList();
            if (userLoginRequestAttempts.Count == 0)
                throw new Exception("Invalid Access");
            userLoginRequestAttempts[0].IsActive = 0;
            userLoginRequestAttempts[0].AttemptUser = "New User";
            userLoginRequestAttempts[0].UseTime = System.DateTime.Now;
        }

        #endregion

    }
}
