using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace sanic_test
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            //url 範例 http://111.253.224.213:9000/api
            //ip 可能過幾天就會跳掉
            //以下url都是localhost的測試,使用前要改

            //get方法
            string url_get = "http://127.0.0.1:1234/test";
            HttpResponseMessage response_get = await client.GetAsync(url_get);
            response_get.EnsureSuccessStatusCode();
            string result = await response_get.Content.ReadAsStringAsync();

            //post方法
            string url_post = "http://127.0.0.1:1234/json_test";
            HttpWebRequest post = (HttpWebRequest)WebRequest.Create(url_post);
            post.Method = "POST";
            post.ContentType = "application/json";
            var postData = new
            {
                user = "test01",
                password = "0928"
            };
            string postBody = JsonConvert.SerializeObject(postData);
            byte[] byteArray = Encoding.UTF8.GetBytes(postBody);
            using (Stream reqStream = post.GetRequestStream())//這種用法比較好
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
                reqStream.Close();
            }
            string response_str = "";
            using (WebResponse response = post.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    response_str = reader.ReadToEnd();
                }
            }

            //取得user的物品清單
            string url_item = "http://127.0.0.1:1234/user_item";
            HttpWebRequest post_item = (HttpWebRequest)WebRequest.Create(url_item);
            post_item.Method = "POST";
            post_item.ContentType = "application/json";
            var Data_item = new
            {
                user = "test01",
            };
            string Body_item = JsonConvert.SerializeObject(Data_item);
            byte[] byteArray_item = Encoding.UTF8.GetBytes(Body_item);
            using (Stream itemStream = post_item.GetRequestStream())
            {
                itemStream.Write(byteArray_item, 0, byteArray_item.Length);
                itemStream.Close();
            }
            string response_item = "";
            using (WebResponse response = post_item.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    response_item = reader.ReadToEnd();
                }
            }

            //取得user的技能清單
            string url_skill = "http://127.0.0.1:1234/user_skill";
            HttpWebRequest post_skill = (HttpWebRequest)WebRequest.Create(url_skill);
            post_skill.Method = "POST";
            post_skill.ContentType = "application/json";
            var Data_skill = new
            {
                user = "test01",
            };
            string Body_skill = JsonConvert.SerializeObject(Data_skill);
            byte[] byteArray_skill = Encoding.UTF8.GetBytes(Body_skill);
            Stream skillStream = post_skill.GetRequestStream();//這種用法比較不好,要一直想變數名稱
            skillStream.Write(byteArray_skill, 0, byteArray_skill.Length);
            skillStream.Close();
            
            string response_skill = "";
            WebResponse response_sk = post_skill.GetResponse();
            StreamReader reader_sk = new StreamReader(response_sk.GetResponseStream(), Encoding.UTF8);
            response_skill = reader_sk.ReadToEnd();
            
            Console.WriteLine(response_str);
            Console.WriteLine(response_item);
            Console.WriteLine(response_skill);
        }
    }
}
