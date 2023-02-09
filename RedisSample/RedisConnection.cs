using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample
{
    public class RedisConnection
    {
        private static ConnectionMultiplexer? REDIS;
        public static IDatabase? DB;
        private static EndPointCollection? EndPointsCollection;
        public static IDatabase Initialize()
        {
            IDatabase? database = null;
            try
            {
                EndPointsCollection = new EndPointCollection();
                EndPointsCollection.Add("127.0.0.1", 6379);
                database = connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return database;
        }

        private static IDatabase connect()
        {
            IDatabase? database = null;
            try
            {
                var configurationOptions = new ConfigurationOptions();
                configurationOptions.SyncTimeout = int.MaxValue;
                configurationOptions.ConfigCheckSeconds = 5;
                configurationOptions.ConnectRetry = 10;

                foreach (var item in EndPointsCollection)
                {
                    configurationOptions.EndPoints.Add(item);
                }

                REDIS = ConnectionMultiplexer.Connect(configurationOptions);
                DB = REDIS.GetDatabase();

                if (REDIS.IsConnected)
                {
                    database = DB;
                    Console.WriteLine("\t Redis Connected ");
                }
                else
                {
                    Console.WriteLine("\t Redis not Connected ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return database;
        }
    }
}
