using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;

namespace XaBikeStand.Models
{
    public class ServerClient
    {
        private const String local = "http://localhost:3000/";
        private const String azure = "https://bikerack.azurewebsites.net/";
        private const String standardAddress = azure;

        private String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6InVzZXIiLCJpYXQiOjE1ODQ0NjYzNzAsImV4cCI6MTU4NDU1Mjc3MH0.HmxXqRGl4ZgiRydIsihHWuNE6l8pg_iEC5nm48RLuJw";
        public ServerClient()
        {

        }

        public ObservableCollection<BikeStation> GetBikeStations()
        {
            String responseFromServer = "";
            WebRequest request = WebRequest.Create(standardAddress + "bikestations");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", token);


            responseFromServer = GetResponse(request);
            ObservableCollection<BikeStation> bikeStations = JsonConvert.DeserializeObject<ObservableCollection<BikeStation>>(responseFromServer);
            return bikeStations;
        }

        private String GetResponse(WebRequest request)
        {
            String responseFromServer = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))

                        responseFromServer = reader.ReadToEnd();
                }

                return responseFromServer;
            }
        }

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

        public bool Lock(int bikestandID)
        {
            String statusCodeFromServer = "";
            String target = standardAddress + "lock/" + bikestandID;



            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", token);

            using (Stream requestStream = request.GetRequestStream())
            {
                statusCodeFromServer = GetStatusCode(request);

                if (statusCodeFromServer.Equals("OK"))
                {
                    return true;
                }
            }
            return false;

        }

        public bool Unlock()
        {
            String statusCodeFromServer = "";

            String target = standardAddress + "unlock";

            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", token);

            using (Stream requestStream = request.GetRequestStream())
            {
                statusCodeFromServer = GetStatusCode(request);
                if (statusCodeFromServer.Equals("OK"))
                {
                    return true;
                }
                return false;
            }
        }

        public Availability GetAvailability(String bikeStationID)
        {
            String response = "";

            String target = standardAddress + "bikestations/GetAvailability/as";

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", token);


            response = GetResponse(request);


            return JsonConvert.DeserializeObject<Availability>(response);

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


