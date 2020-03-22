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

        private SingletonSharedData sharedData;

        public ServerClient()
        {
            sharedData = SingletonSharedData.GetInstance();
        }

        public User Login(String username, String password)
        {
            String responseFromServer = "";

            String target = standardAddress + "users/login";

            String jsonData = "{\"userName\": \"" + username + "\", \"psw\": \"" + password + "\" }";


            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.Write(jsonData);
                }


            }
            User foundUser = null;
            try
            {
                responseFromServer = GetResponse(request);
                Console.WriteLine("response " + responseFromServer);
                foundUser = JsonConvert.DeserializeObject<User>(responseFromServer);
            }
            catch (System.Net.WebException)
            {
            }

            return foundUser;
        }

        public ObservableCollection<BikeStation> GetBikeStations()
        {
            String responseFromServer = "";
            WebRequest request = WebRequest.Create(standardAddress + "bikestations");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


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

        public ISerializable PostData(ISerializable serializable, String target)
        {
            String responseFromServer = "";

            String jsonData = JsonConvert.SerializeObject(serializable);
            target = standardAddress + target;
            Console.WriteLine(jsonData);
            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.Write(jsonData);
                }

                ISerializable foundSerializable = null;
                try
                {
                    responseFromServer = GetResponse(request);
                    Console.WriteLine("response " + responseFromServer);
                    foundSerializable = JsonConvert.DeserializeObject<User>(responseFromServer);
                }
                catch (System.Net.WebException e)
                {
                }

                return foundSerializable;
            }
        }

        public BikeStation Lock(int bikestandID)
        {
            String responseFromServer = "";
            String target = standardAddress + "lock/" + bikestandID;


            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            using (Stream requestStream = request.GetRequestStream())
            {

                try
                {
                    responseFromServer = GetResponse(request);
                    return JsonConvert.DeserializeObject<BikeStation>(responseFromServer);
                }
                catch (System.Net.WebException)
                {
                }
            }
            return null; ;

        }

        public bool Unlock()
        {
            String responseFromServer = "";

            String target = standardAddress + "unlock";

            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            using (Stream requestStream = request.GetRequestStream())
            {

                try
                {
                    responseFromServer = GetResponse(request);
                    return true;
                }
                catch (System.Net.WebException)
                {
                }
            }
            return false;
        }

        public Availability GetAvailability(String bikeStationID)
        {
            String response = "";

            String target = standardAddress + "bikestations/GetAvailability/as";

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


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


        public BikeStand GetLockedBikestand()
        {
            String response = "";

            String target = standardAddress + "bikestandRegistration/getLockedBikestand";

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            BikeStand bikestand = null;
            try
            {
                response = GetResponse(request);
                if (!response.Equals("{}"))
                {
                    bikestand = JsonConvert.DeserializeObject<BikeStand>(response);
                }
            }
            catch (WebException){}
            catch (JsonSerializationException) { }


            return bikestand;
        }

        public BikeStation GetBikeStation(String id)
        {
            String response = "";

            String target = standardAddress + "bikeStations/" + id;

            Console.WriteLine("target" + target);

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            BikeStation bikeStation = null;
            try
            {
                response = GetResponse(request);
                Console.WriteLine("response " + response);
                bikeStation = JsonConvert.DeserializeObject<BikeStation>(response);
            }
            catch (WebException) { }
            catch (JsonSerializationException) { }


            return bikeStation;

        }


        public bool ShareBikestandLock(String username)
        {
            String responseFromServer = "";

            WebRequest request = WebRequest.Create(standardAddress + "bikestandRegistration/AddRegistration");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.Write("{\"userName\": \"" + username + "\"}");
                }
            }
            bool succes = false;
            try
            {
                responseFromServer = GetResponse(request);
                succes = true;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine(e.Message);
            }
            return succes;
        }
    }

}


