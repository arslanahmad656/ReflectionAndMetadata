using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Resources;
using Props = Resources.Properties;

namespace Resources
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ChangeCurrentCulture("ur");
            InitializeComponent();
            ReadFile();
            ReadImage();
            ReadBasicsFromMainResource();
            ReadImageFromResource();
        }

        public void ReadFile()
        {
            Assembly ass = Assembly.GetEntryAssembly();
            var resourceNames = ass.GetManifestResourceNames();
            byte[] arr = new byte[512];
            using (Stream s = ass.GetManifestResourceStream(typeof(Form1), "res.txt1.txt"))
            {
                s.Read(arr, 0, arr.Length);
            }
            var txt = Encoding.ASCII.GetString(arr);
            txt_File.Text = txt;
        }

        public void ReadImage()
        {
            Image img;
            using(Stream s = Assembly.GetEntryAssembly().GetManifestResourceStream(typeof(Form1), "res.img1.png"))
            {
                img = Image.FromStream(s);
            }
            pic_File.Height = img.Height;
            pic_File.Width = img.Width;
            pic_File.Image = img;
        }

        public void ReadBasicsFromMainResource()
        {
            var resMgr = Props.Resources.ResourceManager;
            resMgr.IgnoreCase = true;
            var string1 = resMgr.GetString("string1");
            var string2 = resMgr.GetObject("string2");
            var int1 = Convert.ToInt32(resMgr.GetString("int1"));

            txt_Types.Text = $"{nameof(string1)}: {string1}{Environment.NewLine}"
                + $"{nameof(string2)}: {string2}{Environment.NewLine}"
                + $"{nameof(int1)}: {int1}{Environment.NewLine}";
        }

        public void ReadImageFromResource()
        {
            var img = (Image)Props.Resources.ResourceManager.GetObject("ResImg1");
            pic_Res.Width = img.Width;
            pic_Res.Height = img.Height;
            pic_Res.Image = img;
        }

        public void ChangeCurrentCulture(string culture = "en")
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
        }
    }
}
