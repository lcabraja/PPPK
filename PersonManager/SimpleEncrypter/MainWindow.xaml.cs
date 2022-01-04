using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Zadatak.Utils;

namespace SimpleEncrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
            => TbEncrypted.Text = EncryptionUtils.Encrypt(TbPlain.Text, "fru1tc@k3");

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "App.config|App.config";
            if (openFileDialog.ShowDialog() == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog.FileName);
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/configuration/connectionStrings/add");
                XmlAttributeCollection attributes = nodes.Item(0).Attributes;
                string value = attributes.GetNamedItem("connectionString").Value;
                TbPlain.Text = value;
            }
        }
    }
}
