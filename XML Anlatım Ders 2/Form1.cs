using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XML_Anlatım_Ders_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                string link = listView1.SelectedItems[0].SubItems[1].Text;


                //listviewdeki linke tıkladığımızda linki açacak komut

                System.Diagnostics.Process.Start(link);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            listView1.Items.Clear();

            string url = string.Format("https://www.youtube.com/feeds/videos.xml?channel_id=UCGvj8kfUV5Q6lzECIrGY19g", textBox1.Text);

            WebClient wc = new WebClient(); // webclient ile siteden indrilmesini sağlıyoruz aşağıdaki kodlar bu işe yarıyor.

            string XmlData = wc.DownloadString(url);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(XmlData);

            //eğer xml in kodları karışık ise düzgün yazılmamış ise;

            XmlNodeList entries = xDoc.DocumentElement.GetElementsByTagName("entry"); // burda çekeceğimiz bilginin hangi başlık altında olduğunu belirtiyoruz.

            foreach (XmlNode entry in entries)
            {

                string title = entry.ChildNodes[3].InnerText;

                if (!string.IsNullOrEmpty(title) && title.ToLower().Contains(textBox1.Text.ToLower()))
                {

                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = title;
                    lvi.SubItems.Add(entry.ChildNodes[4].Attributes["href"].InnerText);

                    listView1.Items.Add(lvi);




                }


            }
        }
    }
}
