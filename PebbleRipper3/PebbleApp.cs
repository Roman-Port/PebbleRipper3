using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PebbleRipper3
{
    [DataContract]
    class PebbleApp
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
        public PebbleApp_IconImg icon_image;
        [DataMember]
        public string id;
        [DataMember]
        public PebbleApp_Release latest_release;
        [DataMember]
        public PebbleApp_Links links;
        [DataMember]
        public PebbleApp_ListImg list_image;
        [DataMember]
        public string published_date;
        [DataMember]
        public string screenshot_hardware;
        [DataMember]
        public PebbleApp_ScreenshotImg[] screenshot_images;
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
    }

    [DataContract]
    class PebbleApp_Page
    {
        //OG appstore page
        [DataMember]
        public PebbleApp[] data;
        [DataMember]
        public int limit;
        [DataMember]
        public int offset;
    }

    [DataContract]
    class PebbleApp_ChangelogItem
    {
        [DataMember]
        public string version;
        [DataMember]
        public string published_date;
        [DataMember]
        public string release_notes;
    }

    [DataContract]
    class PebbleApp_Companions
    {
        [DataMember]
        public PebbleApp_Compatibility_Phone ios;
        [DataMember]
        public PebbleApp_Compatibility_Phone android;
        [DataMember]
        public PebbleApp_Compatibility_Hardware aplite;
        [DataMember]
        public PebbleApp_Compatibility_Hardware basalt;
        [DataMember]
        public PebbleApp_Compatibility_Hardware chalk;
        [DataMember]
        public PebbleApp_Compatibility_Hardware diorite;
        [DataMember]
        public PebbleApp_Compatibility_Hardware emery;
    }

    [DataContract]
    class PebbleApp_Compatibility
    {
        [DataMember]
        public PebbleApp_Compatibility_Phone ios;
        [DataMember]
        public PebbleApp_Compatibility_Phone android;
    }

    [DataContract]
    class PebbleApp_Compatibility_Hardware
    {
        [DataMember]
        public PebbleApp_Compatibility_Hardware_Firmware firmware;
        [DataMember]
        public bool supported;
    }

    [DataContract]
    class PebbleApp_Compatibility_Hardware_Firmware
    {
        [DataMember]
        public float major;
    }

    [DataContract]
    class PebbleApp_Compatibility_Phone
    {
        [DataMember]
        public bool supported;
    }

    [DataContract]
    class PebbleApp_HeaderImg
    {
        [DataMember]
        public string orig;
    }

    [DataContract]
    class PebbleApp_IconImg
    {
        [DataMember(Name = "48x48")]
        public string _48x48;
    }

    [DataContract]
    class PebbleApp_ListImg
    {
        [DataMember(Name = "144x144")]
        public string _144x144;
    }

    [DataContract]
    class PebbleApp_ScreenshotImg
    {
        [DataMember(Name = "144x168")]
        public string _144x168;
    }

    [DataContract]
    class PebbleApp_Release
    {
        [DataMember]
        public string id;
        [DataMember]
        public float js_version;
        [DataMember]
        public string pbw_file;
        [DataMember]
        public string published_date;
        [DataMember]
        public string release_notes;
        [DataMember]
        public string version;
    }

    [DataContract]
    class PebbleApp_Links
    {
        [DataMember]
        public string add;
        [DataMember]
        public string remove;
        [DataMember]
        public string add_heart;
        [DataMember]
        public string remove_heart;
        [DataMember]
        public string add_flag;
        [DataMember]
        public string remove_flag;
        [DataMember]
        public string share;
    }
}
