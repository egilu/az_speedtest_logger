using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SpeedTest;
using SpeedTestLogger.Models;


namespace SpeedTestLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Hello SpeedTestLogger!");

            var config = new LoggerConfiguration();
            var runner = new SpeedTestRunner(config.LoggerLocation);
            runner.RunSpeedTest();


            var testData = runner.RunSpeedTest();
            var results = new TestResult
            {
                SessionId = new Guid(),
                User = config.UserId,
                Device = config.LoggerId,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Data = testData
            };


            var success = false;
            using (var client = new SpeedTestApiClient(config.ApiUrl))
            {
                success = await client.PublishTestResult(results);
            }

            if (success)
            {
                Console.WriteLine("Speedtest complete!");
            }
            else
            {
                Console.WriteLine("Speedtest failed!");
            }


            //var results = new TestResult
            //{
            //    SessionId = new Guid(),
            //    User = "cloud-101-testuser",
            //    Device = 1,
            //    Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            //    Data = testData
            //};


            //v2
            //Console.WriteLine("Hello SpeedTestLogger!");

            //var location = new RegionInfo("nb-NO");
            //var runner = new SpeedTestRunner(location);
            //runner.RunSpeedTest();


            //v1
            //var client = new SpeedTestClient();
            //var settings = client.GetSettings();

            //Console.WriteLine("Finding best test servers");

            //var location = new RegionInfo("nb-NO");
            //var tenLocalServers = settings.Servers
            //    .Where(s => s.Country.Equals(location.EnglishName))
            //    .Take(10);

            //var serversOrdersByLatency = tenLocalServers
            //    .Select(s =>
            //    {
            //        s.Latency = client.TestServerLatency(s);
            //        return s;
            //    })
            //    .OrderBy(s => s.Latency);

            //var server = serversOrdersByLatency.First();


            //Console.WriteLine("Testing download speed");
            //var downloadSpeed = client.TestDownloadSpeed(server, settings.Download.ThreadsPerUrl);
            //Console.WriteLine("Download speed was: {0} Mbps", Math.Round(downloadSpeed / 1024, 2));

            //Console.WriteLine("Testing upload speed");
            //var uploadSpeed = client.TestUploadSpeed(server, settings.Upload.ThreadsPerUrl);
            //Console.WriteLine("Upload speed was: {0} Mbps", Math.Round(uploadSpeed / 1024, 2));



            //Console.ReadKey();



        }
    }
}
