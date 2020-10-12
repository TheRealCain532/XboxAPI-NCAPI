using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCAppInterface;
using XDevkit;

namespace XboxAPI_NCAPI
{
    public class API : IAPI
    {
        private IXboxManager xbManager = null;
        private IXboxConsole xbCon = null;
        public uint Xbox;
        string debuggerName = null;
        string userName = null;
        private static uint outInt = 0;

        public API()
        {

        }

        //Declarations of all our internal API variables
        string myName = "Xbox API";
        string myDescription = "Should do most of the same stuff it does on PS3, but now for Xbox!\n\nMade this to get the Waffle House off my back ;)";
        string myAuthor = "Cain532";
        string myVersion = "1.0";
        string myPlatform = "Xbox360";
        string myContactLink = "http://www.google.com";

        //If you want an Icon, use resources to load an image
        //System.Drawing.Image myIcon = Properties.Resources.ico;
        System.Drawing.Image myIcon = null;

        /// <summary>
        /// Website link to contact info or download (leave "" if no link)
        /// </summary>
        public string ContactLink
        {
            get { return myContactLink; }
        }

        /// <summary>
        /// Name of the API (displayed on title bar of NetCheat)
        /// </summary>
        public string Name
        {
            get { return myName; }
        }

        /// <summary>
        /// Description of the API's purpose
        /// </summary>
        public string Description
        {
            get { return myDescription; }
        }

        /// <summary>
        /// Author(s) of the API
        /// </summary>
        public string Author
        {
            get { return myAuthor; }

        }

        /// <summary>
        /// Current version of the API
        /// </summary>
        public string Version
        {
            get { return myVersion; }
        }

        /// <summary>
        /// Name of platform (abbreviated, i.e. PC, PS3, XBOX, iOS)
        /// </summary>
        public string Platform
        {
            get { return myPlatform; }
        }

        /// <summary>
        /// Returns whether the platform is little endian by default
        /// </summary>
        public bool isPlatformLittleEndian
        {
            get { return false; }
        }

        /// <summary>
        /// Icon displayed along with the other data in the API tab, if null NetCheat icon is displayed
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return myIcon; }
        }
    

        /// <summary>
        /// Read bytes from memory of target process.
        /// Returns read bytes into bytes array.
        /// Returns false if failed.
        /// </summary>
        ///

        public bool GetBytes(ulong address, ref byte[] bytes)
        {
            xbCon.DebugTarget.GetMemory((uint)address, (uint)bytes.Length, bytes, out outInt);
            xbCon.DebugTarget.InvalidateMemoryCache(true, (uint)address, (uint)bytes.Length);
            return bytes.Length >= 0;
        }

        /// <summary>
        /// Write bytes to the memory of target process.
        /// </summary>
        public void SetBytes(ulong address, byte[] bytes)
        {
            xbCon.DebugTarget.SetMemory((uint)address, (uint)bytes.Length, bytes, out outInt);
        }

        /// <summary>
        /// Shutdown game or platform
        /// </summary>
        public void Shutdown()
        {
            //Shutdown game or platform
        }


        /// <summary>
        /// Connects to target.
        /// If platform doesn't require connection, just return true.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Connect()
        {
            this.xbManager = new XboxManager();
            this.xbCon = this.xbManager.OpenConsole(this.xbManager.DefaultConsole);
            this.Xbox = this.xbCon.OpenConnection(null);
            return this.xbCon.Name != "";
        }

        /// <summary>
        /// Disconnects from target.
        /// </summary>
        public void Disconnect()
        {
            this.xbCon.DebugTarget.DisconnectAsDebugger();
            //Disconnect code
            //Reset API (for connect and attach user input)
        }

        /// <summary>
        /// Attaches to target process.
        /// This should automatically continue the process if it is stopped.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Attach()
        {
            if (!this.xbCon.DebugTarget.IsDebuggerConnected(out this.debuggerName, out this.userName))
                this.xbCon.DebugTarget.ConnectAsDebugger("NetCheat", XboxDebugConnectFlags.Force);

            return this.xbCon.DebugTarget.IsDebuggerConnected(out this.debuggerName, out this.userName);
        }

        /// <summary>
        /// Pauses the attached process (return false if not available feature)
        /// </summary>
        public bool PauseProcess()
        {
            return false;
        }

        /// <summary>
        /// Continues the attached process (return false if not available feature)
        /// </summary>
        public bool ContinueProcess()
        {
            return false;
        }

        /// <summary>
        /// Tells NetCheat if the process is currently stopped (return false if not available feature)
        /// </summary>
        public bool isProcessStopped()
        {
            return false;
        }

        /// <summary>
        /// Called by user.
        /// Should display options for the API.
        /// Can be used for other things.
        /// </summary>
        public void Configure()
        {

        }

        /// <summary>
        /// Called on initialization
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Called when disposed
        /// </summary>
        public void Dispose()
        {
            //Put any cleanup code in here for when the program is stopped
        }
    }
}
