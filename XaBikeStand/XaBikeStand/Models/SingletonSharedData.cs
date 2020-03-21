using System;
using System.Collections.Generic;
using System.Text;

namespace XaBikeStand.Models
{
    public sealed class SingletonSharedData
    {
        private static SingletonSharedData instance = null;
        private static readonly object padlock = new object();

        public User LoggedInUser { get; set; }

        public String ScannedBikestandID { get; set; }


        private SingletonSharedData()
        {
        }

        /// <summary>
        /// The method creates an instance of the SharedData class or returns the existing instance
        /// if it has already been created. 
        /// </summary>
        /// <returns>Returns the SharedData object</returns>
        public static SingletonSharedData GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new SingletonSharedData();
                }
                return instance;
            }
        }
    }
}
