using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.helper
{
    public class DecomentSetting
    {
        public static string Upload( IFormFile File , string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName);

            string FileName = $"{Guid.NewGuid()}{File.FileName}";

            string Filepath = Path.Combine(FolderPath,FileName);

            using var fs = new FileStream(Filepath ,FileMode.Create);

            File.CopyTo(fs);

            return FileName;
        } 


        public static void Delete( string FileName , string FolderName )
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName ,FileName); 

            if(File.Exists(filePath) )
            {
                File.Delete(filePath);
            } 
        }

    }
}
