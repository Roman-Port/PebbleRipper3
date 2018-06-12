# PebbleRipper3

This is a Pebble Appstore backup utility. This is an improvement over my old backup utility because it will now save the screenshot images for all hardware.

This program exports all of the apps and their data into one big (over 50 MB!) JSON file. It will also save the raw pages in case something goes horribly wrong.

## Usage
If you'd like to compile it yourself, clone it. If you would like to download a compiled version, click on the "Releases" tab and download the binary. Once that is done, open it. Paste in the path to an empty folder by right clicking inside of the command window. Press enter and let it do it's thing. 

When it is done, you will have a folder with three items. A "data.json" file contains the metadata of each app. The "files" folder will contain all of the app assets. The "raw_pages" folder contains the raw data from the Pebble database.
