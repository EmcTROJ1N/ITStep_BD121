using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileExplorerWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorerWebApi.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplorerController : ControllerBase
    {
        [HttpGet("{path}", Name = "Get")]
        public List<FileModel> Get(string path)
        {
            path = WebUtility.UrlDecode(path);
            List<FileModel> files = new List<FileModel>();
            foreach (string fileName in Directory.GetFiles(path).Concat(
                         Directory.GetDirectories(path)))
            {
                try { files.Add(new FileModel(fileName)); }
                catch {}
            }

            return files;
        }

        // POST: api/Explorer
        [HttpPost]
        public void Post([FromBody] string fullName)
        {
            System.IO.File.Create(fullName);
        }

        // PUT: api/Explorer/5
        [HttpPut("{fullName}")]
        public void Put(string fullName, [FromBody] string value)
        {
            System.IO.File.WriteAllText(fullName, value);
        }

        // DELETE: api/Explorer/5
        [HttpDelete("{fullName}")]
        public void Delete(string fullName)
        {
            if (System.IO.File.Exists(fullName))
                System.IO.File.Delete(fullName);
            else if (Directory.Exists(fullName))
                Directory.Delete(fullName, true);
        }
    }
}