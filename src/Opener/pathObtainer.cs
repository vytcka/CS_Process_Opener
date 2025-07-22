using System;
using Microsoft.Win32;

namespace path
{

    public class Browsers
    {

        public void GetEdge()
        {
            string path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            string valname = "MicrosoftEdgeAutoLaunch_B9E62D1FAC80BA0C04C39030B772A86B";

            object testval = Registry.GetValue(path, valname, null);

            if (testval != null)
            {
                Console.WriteLine("val :" + testval.ToString());
            }
            else
            {
                Console.WriteLine("nothing was found");
            }
        }

    }
}