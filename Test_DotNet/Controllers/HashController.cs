using Microsoft.AspNetCore.Mvc;
using System;  
using System.Text;  
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Test_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashController : Controller
    {
        [HttpGet("{firstName}")]
        public JsonResult GetHash(string firstName)
        {
            string input = firstName;
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            using (SHA256 hash = SHA256.Create())
            {
               
                byte[] hashedBytes = hash.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < hashedBytes.Length; i++)  
                {  
                    builder.Append(hashedBytes[i].ToString("x2"));  
                }  
                string apurv= builder.ToString();  
                
                // Return a HashResponse Object which contains the hashString (alphanumeric, without '-') converted from hashedBytes
                var response = new HashResponse
                {
                    Hash = apurv
                };
               
                return new JsonResult(response);
            }
            
        }

    }

    public class HashResponse
    {
        public string Hash { get; set; }
    }

}
