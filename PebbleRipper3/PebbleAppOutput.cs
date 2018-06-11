using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PebbleRipper3
{
    [DataContract]
    class PebbleAppOutput
    {
        [DataMember]
        public string author;
        //Capabilities
        [DataMember]
        public string category_id;
        [DataMember]
        public string category_name;
        [DataMember]
        public string category_color;
        [DataMember]
        public PebbleApp_ChangelogItem[] changelog;
        [DataMember]
        public PebbleApp_Companions companions;
        [DataMember]
        public PebbleApp_Compatibility compatibility;
        [DataMember]
        public string created_at;
        [DataMember]
        public string description;
        [DataMember]
        public string developer_id;
        [DataMember]
        public PebbleApp_HeaderImg[] header_images;
        [DataMember]
        public int hearts;
        [DataMember]
        public PebbleAppOutput_IconImages icon_image;
        [DataMember]
        public string id;
        [DataMember]
        public PebbleAppOutput_Releases latest_release; 
        [DataMember]
        public PebbleApp_Links links;
        [DataMember]
        public PebbleAppOutput_ListImages list_image; 
        [DataMember]
        public string published_date;
        [DataMember]
        public PebbleAppOutput_ScreenshotImages screenshot_images; //To convert
        [DataMember]
        public string source;
        [DataMember]
        public string title;
        [DataMember]
        public string type;
        [DataMember]
        public string uuid;
        [DataMember]
        public string website;

        public static PebbleAppOutput Import(PebbleAppInternal i)
        {
            //This function is really gross!
            PebbleAppOutput o = new PebbleAppOutput();
            //Copy and convert when needed
            PebbleApp r = i.basalt; //Refrence for most data.
            if (r == null)
                r = i.diorite;
            if (r == null)
                r = i.emery;
            if (r == null)
                r = i.chalk;
            if (r == null)
                r = i.aplite;
            if(r==null)
            {
                //I give up. No platforms
                Console.WriteLine("App was skipped because it has NO valid platforms. How did it even get here!?");
            }

            o.author = r.author;
            o.category_id = r.category_id;
            o.category_name = r.category_name;
            o.category_color = r.category_color;
            o.changelog = r.changelog;
            o.companions = r.companions;
            o.compatibility = r.compatibility;
            o.created_at = r.created_at;
            o.description = r.description;
            o.developer_id = r.developer_id;
            o.header_images = r.header_images;
            o.hearts = r.hearts;
            o.id = r.id;
            o.links = r.links;
            o.published_date = r.published_date;
            o.source = r.source;
            o.title = r.title;
            o.type = r.type;
            o.uuid = r.uuid;
            o.website = r.website;

            //Convert
            o.icon_image = new PebbleAppOutput_IconImages();
            if(i.aplite!=null)
                o.icon_image.aplite = i.aplite.icon_image;
            if(i.basalt != null)
                o.icon_image.basalt = i.basalt.icon_image;
            if (i.chalk != null)
                o.icon_image.chalk = i.chalk.icon_image;
            if (i.diorite != null)
                o.icon_image.diorite = i.diorite.icon_image;
            if (i.emery != null)
                o.icon_image.emery = i.emery.icon_image;
            //Convert
            o.latest_release = new PebbleAppOutput_Releases();
            if(i.aplite!=null)
                o.latest_release.aplite = i.aplite.latest_release;
            if (i.basalt != null)
                o.latest_release.basalt = i.basalt.latest_release;
            if (i.chalk != null)
                o.latest_release.chalk = i.chalk.latest_release;
            if (i.diorite != null)
                o.latest_release.diorite = i.diorite.latest_release;
            if (i.emery != null)
                o.latest_release.emery = i.emery.latest_release;
            //Convert
            o.list_image = new PebbleAppOutput_ListImages();
            if(i.aplite != null)
                o.list_image.aplite = i.aplite.list_image;
            if (i.basalt != null)
                o.list_image.basalt = i.basalt.list_image;
            if (i.chalk != null)
                o.list_image.chalk = i.chalk.list_image;
            if (i.diorite != null)
                o.list_image.diorite = i.diorite.list_image;
            if (i.emery != null)
                o.list_image.emery = i.emery.list_image;
            //Convert
            o.screenshot_images = new PebbleAppOutput_ScreenshotImages();
            if(i.aplite != null)
                o.screenshot_images.aplite = i.aplite.screenshot_images;
            if (i.basalt != null)
                o.screenshot_images.basalt = i.basalt.screenshot_images;
            if (i.chalk != null)
                o.screenshot_images.chalk = i.chalk.screenshot_images;
            if (i.diorite != null)
                o.screenshot_images.diorite = i.diorite.screenshot_images;
            if (i.emery != null)
                o.screenshot_images.emery = i.emery.screenshot_images;

            //Queue asset downloads
            if(o.icon_image.aplite != null)
                QueueAndChangeFilename(ref o.icon_image.aplite._48x48, r, WatchHardware.aplite, "icon.png");
            if (o.latest_release.aplite != null)
                QueueAndChangeFilename(ref o.latest_release.aplite.pbw_file, r, WatchHardware.aplite, "app.pbw");
            if (o.list_image.aplite != null)
                QueueAndChangeFilename(ref o.list_image.aplite._144x144, r, WatchHardware.aplite, "list.png");

            if(o.icon_image.basalt != null)
                QueueAndChangeFilename(ref o.icon_image.basalt._48x48, r, WatchHardware.basalt, "icon.png");
            if (o.latest_release.basalt != null)
                QueueAndChangeFilename(ref o.latest_release.basalt.pbw_file, r, WatchHardware.basalt, "app.pbw");
            if (o.list_image.basalt != null)
                QueueAndChangeFilename(ref o.list_image.basalt._144x144, r, WatchHardware.basalt, "list.png");

            if (o.icon_image.chalk != null)
                QueueAndChangeFilename(ref o.icon_image.chalk._48x48, r, WatchHardware.chalk, "icon.png");
            if (o.latest_release.chalk != null)
                QueueAndChangeFilename(ref o.latest_release.chalk.pbw_file, r, WatchHardware.chalk, "app.pbw");
            if (o.list_image.chalk != null)
                QueueAndChangeFilename(ref o.list_image.chalk._144x144, r, WatchHardware.chalk, "list.png");

            if (o.icon_image.diorite != null)
                QueueAndChangeFilename(ref o.icon_image.diorite._48x48, r, WatchHardware.diorite, "icon.png");
            if (o.latest_release.diorite != null)
                QueueAndChangeFilename(ref o.latest_release.diorite.pbw_file, r, WatchHardware.diorite, "app.pbw");
            if (o.list_image.diorite != null)
                QueueAndChangeFilename(ref o.list_image.diorite._144x144, r, WatchHardware.diorite, "list.png");

            if (o.icon_image.emery != null)
                QueueAndChangeFilename(ref o.icon_image.emery._48x48, r, WatchHardware.emery, "icon.png");
            if (o.latest_release.emery != null)
                QueueAndChangeFilename(ref o.latest_release.emery.pbw_file, r, WatchHardware.emery, "app.pbw");
            if (o.list_image.emery != null)
                QueueAndChangeFilename(ref o.list_image.emery._144x144, r, WatchHardware.emery, "list.png");




            int ii = 0;
            while(ii<50)
            {
                //Change each screenshot
                string name = "screenshot_" + ii.ToString() + ".png";

                if (o.screenshot_images.aplite != null)
                {
                    if (ii < o.screenshot_images.aplite.Length)
                        QueueAndChangeFilename(ref o.screenshot_images.aplite[ii]._144x168, r, WatchHardware.aplite, name);
                }
                if (o.screenshot_images.basalt != null)
                {
                    if (ii < o.screenshot_images.basalt.Length)
                        QueueAndChangeFilename(ref o.screenshot_images.basalt[ii]._144x168, r, WatchHardware.basalt, name);
                }
                if (o.screenshot_images.chalk != null)
                {
                    if (ii < o.screenshot_images.chalk.Length)
                        QueueAndChangeFilename(ref o.screenshot_images.chalk[ii]._144x168, r, WatchHardware.chalk, name);
                }
                if (o.screenshot_images.diorite != null)
                {
                    if (ii < o.screenshot_images.diorite.Length)
                        QueueAndChangeFilename(ref o.screenshot_images.diorite[ii]._144x168, r, WatchHardware.diorite, name);
                }
                if (o.screenshot_images.emery != null)
                {
                    if (ii < o.screenshot_images.emery.Length)
                        QueueAndChangeFilename(ref o.screenshot_images.emery[ii]._144x168, r, WatchHardware.emery, name);
                }
                ii++;
            }

            //Return 
            return o;
        }

        private static void QueueAndChangeFilename(ref string path, PebbleApp app, WatchHardware hardware, string name)
        {
            if(path!=null)
            {
                if (path.Length > 2)
                {
                    string save = CreateFilename(name, hardware, app);
                    string newUrl = "%rootUrl%files/" + app.id + "/" + hardware.ToString() + "/" + name;
                    Program.AddWebAsset(save, path);
                    path = newUrl;
                }
            }
        }

        private static string CreateFilename(string name, WatchHardware hardware, PebbleApp app)
        {
            return Program.savePath + "files\\" + app.id + "\\" + hardware.ToString() + "\\" + name;
        }
    }

    [DataContract]
    class PebbleAppOutput_ScreenshotImages
    {
        [DataMember]
        public PebbleApp_ScreenshotImg[] aplite;
        [DataMember]
        public PebbleApp_ScreenshotImg[] basalt;
        [DataMember]
        public PebbleApp_ScreenshotImg[] chalk;
        [DataMember]
        public PebbleApp_ScreenshotImg[] diorite;
        [DataMember]
        public PebbleApp_ScreenshotImg[] emery;
    }

    [DataContract]
    class PebbleAppOutput_Releases
    {
        [DataMember]
        public PebbleApp_Release aplite;
        [DataMember]
        public PebbleApp_Release basalt;
        [DataMember]
        public PebbleApp_Release chalk;
        [DataMember]
        public PebbleApp_Release diorite;
        [DataMember]
        public PebbleApp_Release emery;
    }

    [DataContract]
    class PebbleAppOutput_IconImages
    {
        [DataMember]
        public PebbleApp_IconImg aplite;
        [DataMember]
        public PebbleApp_IconImg basalt;
        [DataMember]
        public PebbleApp_IconImg chalk;
        [DataMember]
        public PebbleApp_IconImg diorite;
        [DataMember]
        public PebbleApp_IconImg emery;
    }

    [DataContract]
    class PebbleAppOutput_ListImages
    {
        [DataMember]
        public PebbleApp_ListImg aplite;
        [DataMember]
        public PebbleApp_ListImg basalt;
        [DataMember]
        public PebbleApp_ListImg chalk;
        [DataMember]
        public PebbleApp_ListImg diorite;
        [DataMember]
        public PebbleApp_ListImg emery;
    }

    class PebbleAppInternal
    {
        //Stores apps for each platform.
        public PebbleApp aplite; //OG Pebble / Pebble Steel
        public PebbleApp basalt; //Pebble Time / Pebble Time Steel
        public PebbleApp chalk; //Pebble Time Round
        public PebbleApp diorite; //Pebble 2
        public PebbleApp emery; //Pebble Time 2

        public void Set(PebbleApp app, WatchHardware hardware)
        {
            switch(hardware)
            {
                case WatchHardware.aplite:
                    aplite = app;
                    break;
                case WatchHardware.basalt:
                    basalt = app;
                    break;
                case WatchHardware.chalk:
                    chalk = app;
                    break;
                case WatchHardware.diorite:
                    diorite = app;
                    break;
                case WatchHardware.emery:
                    emery = app;
                    break;
            }
        }
    }
}
