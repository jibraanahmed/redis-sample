using StackExchange.Redis;
using System;
using System.IO.Pipelines;

namespace RedisSample
{
    internal class Program
    {
        static IDatabase? database = null;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.PingRedis();
            p.RedisSaveDelete();
            Console.Read();
        }

        public async void RedisSaveDelete()
        {
            try
            {
                var res = await database.StringSetAsync("test", "test",TimeSpan.FromSeconds(10));
                var result = await database.StringGetAsync("test");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public async void PingRedis()
        {
            try
            {
                database = RedisConnection.Initialize();
                await database.PingAsync();
                Console.WriteLine(database);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}