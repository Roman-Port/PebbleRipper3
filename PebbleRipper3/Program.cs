using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PebbleRipper3
{
    class Program
    {
        static Dictionary<string, PebbleAppInternal> rawApps =
            new Dictionary<string, PebbleAppInternal>();

        public static List<AssetToDownload> assets = new List<AssetToDownload>();
        public static List<string> assetFails = new List<string>();
        public static string savePath = @"E:\FullPebbleArchives\6-4-18\";

        static void Main(string[] args)
        {
            SeekAll();
            Console.WriteLine("Converting apps...");
            List<PebbleAppOutput> output = new List<PebbleAppOutput>();
            rawApps.Values.AsParallel().ForAll(delegate (PebbleAppInternal app)
            {
                var o = PebbleAppOutput.Import(app);
                lock (output)
                {
                    output.Add(o);
                }

            });
            Console.WriteLine("\r\nExporting and saving database... ");
            //Save
            string ser = SerializeObject(output);
            File.WriteAllText(savePath + "data.json", ser);


            Console.WriteLine("\r\nDownloading assets... (" + assets.Count + " assets queued)");

            Console.Write("\r\nPlease wait.");
            Thread updateThread = new Thread(new ThreadStart(UpdateDownloadProgress));
            updateThread.Start();

            assets.AsParallel().ForAll(delegate (AssetToDownload app)
            {
                try
                {
                    //Make folder
                    string[] paths = app.filename.Split('\\');
                    Directory.CreateDirectory(app.filename.Substring(0, app.filename.Length - paths[paths.Length - 1].Length));
                    //Download
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(app.url, app.filename);
                    }
                    app.done = true;
                }
                catch (Exception ex)
                {
                    assetFails.Add("Failed to download URL at " + app.url + " with filename " + app.filename + " with error " + ex.Message+"\r\n");
                    app.done = true;
                }
            });
            //Write errors
            File.WriteAllLines(savePath + "errors.txt", assetFails.ToArray());
            //Stop thread
            updateThread.Abort();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void UpdateDownloadProgress()
        {
            while(true)
            {
                int noComplete = assets.Where(x => x.done).Count();
                int noTotal = assets.Count;
                float done = ((float)noComplete / (float)noTotal) * 100;
                double r = Math.Round(done, 0);

                Console.Write("\rProgress: " + r.ToString() + "% (" + noComplete.ToString() + "/" + noTotal.ToString() + ") - "+assetFails.ToString()+" assets failed.");
                Thread.Sleep(100);
            }
        }

        public static void AddWebAsset(string filename, string url)
        {
            lock(assets)
            {
                bool exists = assets.Where(x => x.filename == filename && x.url == url).Count() != 0;
                if (!exists)
                {
                    assets.Add(new AssetToDownload(filename, url));
                }
            }
        }

        public static void SeekAll()
        {
            Console.WriteLine("Starting seek of watchapps...\r\n");
            SeekEntireType(FetchType.watchapps_and_companions);
            Console.WriteLine("\r\nStarting seek of watchfaces...\r\n");
            SeekEntireType(FetchType.watchfaces);
            Console.WriteLine("\r\nFound " + rawApps.Count + " apps!");
        }

        public static void SeekEntireType(FetchType type)
        {
            int platform = 0;
            while(platform<5)
            {
                //Ran for each platform
                WatchHardware hw = (WatchHardware)platform;
                Console.WriteLine("Getting apps for " + type.ToString() + " on hardware " + hw.ToString());
                //Get apps and add them
                foreach(PebbleApp app in GetAppsForHardware(type,hw))
                {
                    UpdateApp(app, hw);
                }
                platform++;
            }
        }

        public static PebbleApp[] GetAppsForHardware(FetchType type, WatchHardware hardware, int max=-1)
        {
            List<PebbleApp> found = new List<PebbleApp>();
            while(true)
            {
                PebbleApp[] got = GetPage(found.Count, hardware, type);
                found.AddRange(got);
                //Refresh the UI or something
                if ((max != -1 && found.Count > max) || got.Length<100)
                    break;
            }
            return found.ToArray();
        }

        public static void UpdateApp(PebbleApp app, WatchHardware hardware)
        {
            if(rawApps.ContainsKey(app.id))
            {
                //Update
                rawApps[app.id].Set(app, hardware);
            } else
            {
                //Add
                PebbleAppInternal i = new PebbleAppInternal();
                i.Set(app, hardware);
                rawApps.Add(app.id, i);
            }
        }

        public static PebbleApp[] GetPage(int offset, WatchHardware hardware, FetchType type, int limit = 100)
        {
            //Create a GET request here and get the data.
            string url = "https://api2.getpebble.com/v2/apps/collection/all/"+type.ToString().Replace('_','-')+"?platform=all&hardware="+hardware.ToString()+"&filter_hardware=false&image_ratio=1&limit="+limit.ToString()+"&offset=" + offset.ToString();
            //Request
            string raw = DoGetRequest(url);
            //Deserialize JSON.
            PebbleApp_Page page = (PebbleApp_Page)DeserializeObject(raw, typeof(PebbleApp_Page));
            //Extract data and return it
            return page.data;
        }
        
        public static string DoGetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            string data = "";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            return data;
        }

        public static object DeserializeObject(string value, Type objType)
        {
            try
            {
                //Get a data stream
                MemoryStream mainStream = GenerateStreamFromString(value);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(objType);
                //Load it in.
                mainStream.Position = 0;
                var obj = ser.ReadObject(mainStream);
                return Convert.ChangeType(obj, objType);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        public static string SerializeObject(object obj)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
            ser.WriteObject(stream1, obj);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            return sr.ReadToEnd();
        }
    }

    public enum WatchHardware
    {
        aplite, //OG Pebble / Pebble Steel
        basalt, //Pebble Time / Pebble Time Steel
        chalk, //Pebble Time Round
        diorite, //Pebble 2
        emery //Pebble Time 2
    }

    public enum FetchType
    {
        watchapps_and_companions,
        watchfaces
    }
}
