using DSM.UI.Api.Helpers.RemoteDesktop.Models;
using System;
using System.Text;

namespace DSM.UI.Api.Helpers.RemoteDesktop
{
    public static class RDPManager
    {
        public static byte[] CreateFile(LoginInfo info)
        {
            if (string.IsNullOrEmpty(info.Username))
            {
                throw new ArgumentNullException("username and password can't be empty");
            }
            string pwstr = BitConverter.ToString(DataProtection.ProtectData(Encoding.Unicode.GetBytes("DSM"), "")).Replace("-", "");
            string rdpInfo = string.Format(RDPTemplate.templatePath, info.IpAddress, info.Username, pwstr);

            byte[] returnArray = Encoding.ASCII.GetBytes(rdpInfo);
            return returnArray;
        }
    }
}
