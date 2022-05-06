namespace SOUNDPLAYER.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NLog;
    using RTCV.CorruptCore;

    using RTCV.Common;
    using RTCV.UI;
    using static RTCV.CorruptCore.RtcCore;
    using RTCV.Vanguard;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Runtime.InteropServices;
    using System.Drawing.Imaging;
    using RTCV.NetCore;
    using System.Diagnostics;
    using System.Net;
    using System.Collections.Specialized;

    using System.IO.Compression;
    using System.Windows.Documents.Serialization;
    using RTCV.UI.Modular;
    using RTCV.NetCore.NetCoreExtensions;

    public partial class PluginForm : ComponentForm, IColorize
    {
        public SOUNDPLAYER plugin;

        public volatile bool HideOnClose = true;

        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        WebClient wc = new WebClient();

        //This dictionary will inflate forever but it would take quite a while to be noticeable.
        Dictionary<string, bool> encounteredIds = new Dictionary<string, bool>();

        List<Button> registeredButtons = new List<Button>();

        public PluginForm(SOUNDPLAYER _plugin)
        {
            plugin = _plugin;

            this.InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.PluginForm_FormClosing);
            this.Text = "Shortcut Bar";// CORRUPTCLOUD_LIVE.CamelCase(nameof(CORRUPTCLOUD_LIVE).Replace("_", " ")); //automatic window title

        }


        private void PluginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HideOnClose)
            {
                e.Cancel = true;
                this.Hide();
            }
        }



        [DllImport("winmm.dll")]
        public static extern uint mciSendString(
    string lpstrCommand,
    StringBuilder lpstrReturnString,
    int uReturnLength,
    IntPtr hWndCallback
        );

        public void PlaySound(string filename)
        {
            //get the full location of the assembly with DaoTests in it
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(SOUNDPLAYER)).Location;

            //get the folder that's in
            string dlldir = Path.GetDirectoryName(fullPath);

            var mp3file = Path.Combine(dlldir, "SOUNDPLAYER", filename);

            //play file
            mciSendString(@"close temp_alias", null, 0, IntPtr.Zero);
            mciSendString($"open \"{mp3file}\" alias temp_alias", null, 0, IntPtr.Zero);
            mciSendString("play temp_alias", null, 0, IntPtr.Zero);
        }

        public void SaveToParams()
        {
            string value = String.Join("|", registeredButtons.Select(it => it.Tag.ToString()));
            Params.SetParam("PLUGIN_SHORTCUTBAR", value);

        }

        public List<RTCV.UI.Input.Binding> GetBindingsFromStrings(string[] values)
        {
            var Hotkeys = UICore.HotkeyBindings;
            List<RTCV.UI.Input.Binding> binds = new List<RTCV.UI.Input.Binding>();

            foreach(var value in values)
            {
                var bind = UICore.HotkeyBindings.FirstOrDefault(iterator => iterator.DisplayName == value);
                if (bind != null)
                    binds.Add(bind);
            }

            return binds;
        }

    }

}
