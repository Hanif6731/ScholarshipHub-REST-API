using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ScholarshipHubRestApi.Controllers
{
    [RoutePrefix("api/upload/files")]
    public class FileUploadController : ApiController
    {
        [Route("guided")]
        public Task<HttpResponseMessage> Uploadfile()
        {
            List<string> saveFilePath = new List<string>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string rootpath = HttpContext.Current.Server.MapPath("~/Media/Files");
            var provider = new MultipartFileStreamProvider(rootpath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if(t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);

                    }
                    foreach(MultipartFileData item in provider.FileData)
                    {
                        try
                        {
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            string newFilename = Guid.NewGuid() + Path.GetExtension(name);
                            File.Move(item.LocalFileName, Path.Combine(rootpath, newFilename));

                            Uri baseUri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileReletivePath = "~/Media/Files/" + newFilename;
                            Uri fileFullPath = new Uri(baseUri, VirtualPathUtility.ToAbsolute(fileReletivePath));
                            saveFilePath.Add(fileFullPath.ToString());

                        }
                        catch(Exception ex)
                        {
                            throw ex;
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.Created, saveFilePath);
                });
            return task;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Post()
        {
            // Check whether the POST operation is MultiPart?
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form
            // data will be loaded.
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/Media/Files");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        // We implement MultipartFormDataStreamProvider to override the filename of File which
        // will be stored on server, or else the default name will be of the format like Body-
        // Part_{GUID}. In the following implementation we simply get the FileName from 
        // ContentDisposition Header of the Request Body.
        
    }
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }


}
