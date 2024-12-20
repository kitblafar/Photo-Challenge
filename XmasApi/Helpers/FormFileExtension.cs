using System.Text.RegularExpressions;

namespace XmasAPI.Helpers
{
        public static class FormFileExtensions
        {
            public const int ImageMinimumBytes = 512;

            public static bool IsImage(this IFormFile postedFile)
            {
                //-------------------------------------------
                //  Check the image mime types
                //-------------------------------------------
                if (postedFile.ContentType.ToLower() != "image/jpg" &&
                            postedFile.ContentType.ToLower() != "image/jpeg" &&
                            postedFile.ContentType.ToLower() != "image/pjpeg" &&
                            postedFile.ContentType.ToLower() != "image/gif" &&
                            postedFile.ContentType.ToLower() != "image/x-png" &&
                            postedFile.ContentType.ToLower() != "image/png")
                {
                    Console.WriteLine("1");
                    return false;
                }

                //-------------------------------------------
                //  Check the image extension
                //-------------------------------------------
                if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                    && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                    && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                    && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
                {
                    Console.WriteLine("2");
                    return false;
                }

                //-------------------------------------------
                //  Attempt to read the file and check the first bytes
                //-------------------------------------------
                try
                {
                    if (!postedFile.OpenReadStream().CanRead)
                    {

                    Console.WriteLine("3");
                        return false;
                    }
                    //------------------------------------------
                    //check whether the image size exceeding the limit or not
                    //------------------------------------------ 
                    if (postedFile.Length < ImageMinimumBytes)
                    {

                    Console.WriteLine("4");
                        return false;
                    }

                    byte[] buffer = new byte[ImageMinimumBytes];
                    postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                    string content = System.Text.Encoding.UTF8.GetString(buffer);
                    if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    {

                    Console.WriteLine("5");
                        return false;
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("6");
                    return false;
                }

                return true;
            }
        
    }
}
