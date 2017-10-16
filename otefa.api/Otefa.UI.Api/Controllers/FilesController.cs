using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net;
using NLog;
using System.Configuration;
using Otefa.UI.Api.Controllers;
using Otefa.UI.Api.Providers;
using Otefa.UI.Api.ViewModel.Files.Requests;
using Otefa.UI.Api.ViewModel.Files.Responses;

namespace UI.Api.Controllers
{
    //   [Authorize]
    [RoutePrefix("v1/files")]
    public class FilesController : ApiControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string RelativePathUploads = ConfigurationManager.AppSettings["RelativePathUploads"]?.ToString();

        /// <summary>
        /// Get all files by folder path.
        /// </summary>
        /// <returns>List files</returns>
     //   [Authorize]
        [Route("GetByFolderPath")]
        public async Task<IHttpActionResult> Get([FromUri]GetByFolderPathViewModel request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is null.");

                DirectoryInfo folder = null;

                var path = HttpRuntime.AppDomainAppPath + RelativePathUploads + request.FolderPath;

                var files = new List<FileViewModel>();

                if (Directory.Exists(path))
                {
                    folder = new DirectoryInfo(path);

                    //await Task.Factory.StartNew(() =>
                    //{
                        files = folder.GetFiles()
                        .Select(fi => new FileViewModel
                        {
                            Name = fi.Name,
                            Created = fi.CreationTime,
                            Modified = fi.LastWriteTime,
                            Size = fi.Length / 1024,
                            Type = MimeMapping.GetMimeMapping(fi.Name)
                        }).ToList();
                    //});
                }

                return Ok(new { Files = files, Message = path });
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Get one file by file name path.
        /// </summary>
        /// <returns>File</returns>
     //   [Authorize]
        [Route("GetByFileNamePath")]
        public async Task<IHttpActionResult> Get([FromUri]GetByFileNamePathViewModel request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is null.");

                FileViewModel file = null;
                var path = HttpRuntime.AppDomainAppPath + RelativePathUploads + request.FileNamePath;

                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);

                    await Task.Factory.StartNew(() =>
                    {
                        file = new FileViewModel
                        {
                            Name = fileInfo.Name,
                            Created = fileInfo.CreationTime,
                            Modified = fileInfo.LastWriteTime,
                            Size = fileInfo.Length / 1024,
                            Type = MimeMapping.GetMimeMapping(fileInfo.Name)
                        };
                    });
                }

                return Ok(new { Files = file });
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
     //   [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string FileNamePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FileNamePath))
                    return BadRequest("The FileNamePath is mandatory.");

                var path = HttpRuntime.AppDomainAppPath + RelativePathUploads + FileNamePath;

                if (!File.Exists(path))
                    return NotFound();

                if (IsFileLocked(new FileInfo(path)))
                    return BadRequest("The file is unavailable because it is: still being written to or being processed by another thread or does not exist (has already been processed).");

                await Task.Factory.StartNew(() => { File.Delete(path); });

                return Ok(new { Message = Path.GetFileName(path) + " deleted successfully." });
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Add Files.
        /// </summary>
        /// <returns></returns>
     //   [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromUri]PostFileViewModel request)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                return BadRequest("The request doesn't contain valid content.");

            try
            {
                var path = HttpRuntime.AppDomainAppPath + RelativePathUploads + request.UploadsFolder;

                if (Request.Content.IsMimeMultipartContent())
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var provider = new CustomMultipartFormDataStreamProvider(path);

                    await Request.Content.ReadAsMultipartAsync(provider);

                    return Ok(new
                    {
                        Message = "Files uploaded ok.",
                        Files = provider.FileData.Select(i =>
                        {
                            var fileInfo = new FileInfo(i.LocalFileName);
                            return new FileViewModel
                            {
                                Name = fileInfo.Name,
                                Created = fileInfo.CreationTime,
                                Modified = fileInfo.LastWriteTime,
                                Size = fileInfo.Length / 1024,
                                Type = MimeMapping.GetMimeMapping(fileInfo.Name)
                            };
                        })
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(ex.InnerException?.InnerException?.Message))
                {
                    logger.Error(ex.InnerException.InnerException.Message);
                    return BadRequest(ex.InnerException.InnerException.Message);
                }

                logger.Error(ex.Message);
                return InternalServerError();
            }
        }

        /// <summary>
        /// This function is used to check specified file being used or not
        /// </summary>
        /// <param name="file">FileInfo of required file</param>
        /// <returns>If that specified file is being processed
        /// or not found is return true</returns>
        private static Boolean IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                //Don't change FileAccess to ReadWrite,
                //because if a file is in readOnly, it fails.
                stream = file.Open
                (
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None
                );
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}