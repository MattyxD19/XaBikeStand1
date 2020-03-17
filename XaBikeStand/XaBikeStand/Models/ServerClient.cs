using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;

namespace XaBikeStand.Models
{
    public class ServerClient { 

    private const String standardAddress = "https://bikerack.azurewebsites.net/";
    public ServerClient()
    {

    }

        //public ObservableCollection<User> GetCurrentProducts()
        //{
        //    String responseFromServer = "";
        //    WebRequest request = WebRequest.Create(standardAddress + "CurrentProducts");
        //    request.Method = "GET";
        //    request.ContentType = "application/json";


        //    responseFromServer = GetResponse(request);
        //    ObservableCollection<Product> tbl_CurrentProduct = JsonConvert.DeserializeObject<ObservableCollection<Product>>(responseFromServer);
        //    return tbl_CurrentProduct;
        //}

        //private String GetResponse(WebRequest request)
        //{
        //    String responseFromServer = "";
        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        using (Stream responseStream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(responseStream))

        //                responseFromServer = reader.ReadToEnd();
        //        }

        //        return responseFromServer;
        //    }
        //}

        //public bool PostData(ISerializable serializable, String target)
        //{
        //    String statusCodeFromServer = "";

        //    String jsonData = JsonConvert.SerializeObject(serializable);
        //    target = standardAddress + target;
        //    Console.WriteLine(jsonData);
        //    WebRequest request = WebRequest.Create(target);
        //    request.Method = "POST";
        //    request.ContentType = "application/json";

        //    using (Stream requestStream = request.GetRequestStream())
        //    {
        //        using (StreamWriter streamWriter = new StreamWriter(requestStream))
        //        {
        //            streamWriter.Write(jsonData);
        //        }

        //        statusCodeFromServer = GetStatusCode(request);
        //        if (statusCodeFromServer.Equals("200"))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        public bool Action(String target)
        {
            String statusCodeFromServer = "";
            int bikestandID = 1;
            if (target.Equals(Target.Lock))
            {
                target = standardAddress + target + "/" + bikestandID;
                Console.WriteLine(target);
            } else if (target.Equals(Target.Unlock))
            {
                target = standardAddress + target;
            }
            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6InVzZXIiLCJpYXQiOjE1ODQzNzk1OTUsImV4cCI6MTU4NDQ2NTk5NX0.gILjWECGtKzrJY7YLiLFc5uWmxPXWJbf1BVwlGal-0k");

            using (Stream requestStream = request.GetRequestStream())
            { 

                statusCodeFromServer = GetStatusCode(request);
                if (statusCodeFromServer.Equals("200"))
                {
                    return true;
                }
                return false;
            }
        }




        private String GetStatusCode(WebRequest request)
    {
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            return response.StatusCode.ToString();
        }
    }
}
}


