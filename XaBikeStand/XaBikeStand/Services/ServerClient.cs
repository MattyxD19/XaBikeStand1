using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

namespace XaBikeStand.Models
{
    public class ServerClient
    {
        private const String standardAddress = "https://bikerack.azurewebsites.net/";

        private SingletonSharedData sharedData;

        public ServerClient()
        {
            sharedData = SingletonSharedData.GetInstance();
        }

        /// <summary>
        /// Contacts the backend in order to login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
                foundUser = JsonConvert.DeserializeObject<User>(responseFromServer);
            }
            catch (System.Net.WebException)
            {
            }

            return foundUser;
        }


        /// <summary>
        /// Contacts the backend in order to get all bike stations
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the response from the backend
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Contacts the backend in order to add a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            String responseFromServer = "";

            String jsonData = JsonConvert.SerializeObject(user);
            String target = standardAddress + "users/register";
            WebRequest request = WebRequest.Create(target);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.Write(jsonData);
                }

                User addedUser = null;
                try
                {
                    responseFromServer = GetResponse(request);
                    addedUser = JsonConvert.DeserializeObject<User>(responseFromServer);
                }
                catch (System.Net.WebException)
                {
                }
                return addedUser;
            }
        }

        /// <summary>
        /// Contacts the backend in order to delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User DeleteUser(String id)
        {
            String response = "";

            String target = standardAddress + "users/" + id;

            WebRequest request = WebRequest.Create(target);
            request.Method = "Delete";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            User deletedUser = null;
            try
            {
                response = GetResponse(request);
                deletedUser = JsonConvert.DeserializeObject<User>(response);
            }
            catch (System.Net.WebException)
            {
            }

            return deletedUser;
        }

        /// <summary>
        /// Contacts the backend in order to update a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateUser(User user)
        {
            String responseFromServer = "";
            String jsonData = "";
            if (user.psw != null)
            {
                jsonData = JsonConvert.SerializeObject(user);
            }
            else
            {
                jsonData = "{\"userName\": \"" + user.userName + "\", \"email\": \"" + user.email + "\" }";
            }
            String target = standardAddress + "users/" + user.userName;

            WebRequest request = WebRequest.Create(target);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(requestStream))
                {
                    streamWriter.Write(jsonData);
                }

                User updatedUser = null;
                try
                {
                    responseFromServer = GetResponse(request);
                    updatedUser = JsonConvert.DeserializeObject<User>(responseFromServer);
                }
                catch (System.Net.WebException)
                {
                }

                return updatedUser;
            }
        }

        /// <summary>
        /// Contacts the backend in order to lock a bikestand
        /// </summary>
        /// <param name="bikestandID"></param>
        /// <returns></returns>
        public BikeStandRegistration Lock(int bikestandID)
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
                    return JsonConvert.DeserializeObject<BikeStandRegistration>(responseFromServer);
                }
                catch (System.Net.WebException)
                {
                }
            }
            return null; ;

        }

        /// <summary>
        /// Contacts the backend in order unlock a bikestand
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Contacts the backend in order to get the availability of a bike station 
        /// </summary>
        /// <param name="bikeStationID"></param>
        /// <returns></returns>
        public Availability GetAvailability(String bikeStationID)
        {
            String response = "";

            String target = standardAddress + "bikestations/GetAvailability/" + bikeStationID;

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            response = GetResponse(request);
            return JsonConvert.DeserializeObject<Availability>(response);
        }


        /// <summary>
        /// Contacts the backend in order to get a locked bikestand
        /// </summary>
        /// <returns></returns>
        public BikeStandRegistration GetLockedBikestand()
        {
            String response = "";

            String target = standardAddress + "bikestandRegistration/getLockedBikestand";

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            BikeStandRegistration bikestand = null;
            try
            {
                response = GetResponse(request);
                if (!response.Equals("{}"))
                {
                    bikestand = JsonConvert.DeserializeObject<BikeStandRegistration>(response);
                }
            }
            catch (WebException) { }
            catch (JsonSerializationException) { }


            return bikestand;
        }

        /// <summary>
        /// Contacts the backend in order to get a bike station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BikeStation GetBikeStation(String id)
        {
            String response = "";

            String target = standardAddress + "bikeStations/" + id;


            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);

            BikeStation bikeStation = null;
            try
            {
                response = GetResponse(request);
                bikeStation = JsonConvert.DeserializeObject<BikeStation>(response);
            }
            catch (WebException) { }
            catch (JsonSerializationException) { }


            return bikeStation;

        }

        /// <summary>
        /// Contacts the backend in order to add a registration to another user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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
            catch (WebException)
            {
            }
            return succes;
        }


        /// <summary>
        /// Contacts the backend in order to get the username of a shared access to a bikestand
        /// </summary>
        /// <returns></returns>
        public String GetSharedUsername()
        {
            String response = null;

            String target = standardAddress + "bikestandRegistration/getSharedUsername";

            WebRequest request = WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            try
            {
                response = GetResponse(request);
            }
            catch (WebException) { }
            catch (JsonSerializationException) { }

            return response;
        }

        /// <summary>
        /// Contacts the backend in order to delete shared access to a bikestand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteSharedAccess(String id)
        {
            String response = "";

            String target = standardAddress + "bikestandRegistration/deleteFromUsername/" + id;

            WebRequest request = WebRequest.Create(target);
            request.Method = "Delete";
            request.ContentType = "application/json";
            request.Headers.Add("x-access-token", sharedData.LoggedInUser.token);


            bool succes = false;
            try
            {
                response = GetResponse(request);
                succes = true;
            }
            catch (WebException)
            {
            }
            return succes;
        }
    }
}


