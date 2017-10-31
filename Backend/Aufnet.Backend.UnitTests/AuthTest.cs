using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Aufnet.Backend.UnitTests
{ 
    //[TestFixture]
    //public class AuthTest
    //{
    //    [Test]
    //    public async Task BearerTokenTest()
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            var cnt=new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
    //            {
    //                new KeyValuePair<string, string>("grant_type", "password"),
    //                new KeyValuePair<string, string>("username", "sa"),
    //                new KeyValuePair<string, string>("password", "Potatohair51!"),
    //            });

    //            var n = 10;
    //            var wr=new char[n];
    //            var result=await client.PostAsync("http://10.125.3.12:5000/connect/token", cnt);

    //            var response = await result.Content.ReadAsStringAsync();

    //            Console.WriteLine(response);
    //        }
    //    }
    //}
}
