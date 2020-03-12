using DSM.UI.Api.Helpers.RemoteDesktop.Models;
using System;
using System.Text;

namespace DSM.UI.Api.Helpers.RemoteDesktop
{
    public static class RDPManager
    {
        public static readonly string IpaddressPatten = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

        public static readonly string FilePath = "connection.rdp";

        public static readonly string templatePath = 
@"screen mode id:i:2
use multimon:i:0
desktopwidth:i:1600
desktopheight:i:900
session bpp:i:32
winposstr:s:0,3,0,0,800,600
compression:i:1
keyboardhook:i:2
audiocapturemode:i:0
videoplaybackmode:i:1
connection type:i:2
networkautodetect:i:1
bandwidthautodetect:i:1
displayconnectionbar:i:1
enableworkspacereconnect:i:0
disable wallpaper:i:1
allow font smoothing:i:0
allow desktop composition:i:0
disable full window drag:i:1
disable menu anims:i:1
disable themes:i:0
disable cursor setting:i:0
bitmapcachepersistenable:i:1
audiomode:i:1
redirectprinters:i:1
redirectcomports:i:0
redirectsmartcards:i:1
redirectclipboard:i:1
redirectposdevices:i:0
autoreconnection enabled:i:1
authentication level:i:2
prompt for credentials:i:0
negotiate security layer:i:1
remoteapplicationmode:i:0
alternate shell:s:
shell working directory:s:
gatewayhostname:s:
gatewayusagemethod:i:4
gatewaycredentialssource:i:4
gatewayprofileusagemethod:i:0
promptcredentialonce:i:0
use redirection server name:i:0
rdgiskdcproxy:i:0
kdcproxyname:s:
drivestoredirect:s:
redirectdirectx:i:1
full address:s:{0}
username:s:{1}
password 51:b:{2}";

        public static LoginInfo rdpLoginInfo = new LoginInfo();

        public static byte[] CreateFile(LoginInfo info)
        {
            if (string.IsNullOrEmpty(info.Username))
            {
                throw new ArgumentNullException("username and password can't be empty");
            }
            string pwstr = BitConverter.ToString(DataProtection.ProtectData(Encoding.Unicode.GetBytes("DSM"), "")).Replace("-", "");
            string rdpInfo = string.Format(templatePath, info.IpAddress, info.Username, pwstr);

            byte[] returnArray = Encoding.ASCII.GetBytes(rdpInfo);
            return returnArray;
        }
    }
}
