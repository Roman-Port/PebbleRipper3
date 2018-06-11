using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PebbleRipper3
{
    class AssetToDownload
    {
        public string url;
        public string filename;
        public bool done = false;

        public AssetToDownload(string _filename, string _url)
        {
            url = _url;
            filename = _filename;
        }
    }
}
