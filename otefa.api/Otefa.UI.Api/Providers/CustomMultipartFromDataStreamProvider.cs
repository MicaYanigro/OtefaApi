using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Otefa.UI.Api.Providers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        string[] uploadsFileTypeNotSupported = (ConfigurationManager.AppSettings["UploadsFileTypeNotSupported"])?.Split(',');
        long? uploadsFileSize = Convert.ToInt64(ConfigurationManager.AppSettings["UploadsFileSize"]);

        public CustomMultipartFormDataStreamProvider(string uploadPath) : base(uploadPath)
        {

        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty);

            //if (filename.IndexOf('.') < 0)
            //    return Stream.Null;

            if (uploadsFileTypeNotSupported.Contains(Path.GetExtension(filename)))
                throw new Exception("Files Type Not Supported: " + string.Join(", ", uploadsFileTypeNotSupported));

            if (filename.Length > uploadsFileSize.Value)
                throw new Exception("Your File " + filename + " is too large, maximum allowed size is : " + uploadsFileSize.Value);

            var extension = filename.Split('.').Last();

            return base.GetStream(parent, headers);
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string fileName = headers.ContentDisposition.FileName;

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = Guid.NewGuid().ToString() + ".data";

            return fileName.Replace("\"", string.Empty);
        }
    }
}