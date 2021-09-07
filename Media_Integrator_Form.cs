using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace MediaIntegrator_Lab5
{
    public partial class Media_Integrator_Main : Form
    {
        public static string CSV_file_dir;
        public static string XML_file_dir;

        public static string CSV_file_name;
        public static string XML_file_name;

        public Media_Integrator_Main()
        {
            InitializeComponent();
            CSV_file_dir = string.Empty;
            XML_file_dir = string.Empty;
            CSV_file_name = string.Empty;
            XML_file_name = string.Empty;
        }

        private void Btn_Csv_dir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    CSV_file_dir = fbd.SelectedPath.Trim();
                    Btn_Csv_dir.Enabled = false;
                    Get_Watcher_Ready();
                }
            }

        }

        private void Btn_xml_dir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    XML_file_dir = fbd.SelectedPath.Trim();
                    Btn_xml_dir.Enabled = false;
                    Get_Watcher_Ready();
                }
            }
        }

        private void Get_Watcher_Ready()
        {
            if (CSV_file_dir.Length > 0 & XML_file_dir.Length > 0)
            {
                Watcher_Run();
            }
        }

        private void Watcher_Run()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
                    watcher.Path = CSV_file_dir;

                    // Watch for changes in 
                    watcher.NotifyFilter = NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.FileName;

                    // Only watch csv files.
                    watcher.Filter = "*.csv";

                    // Add event handlers.
                   
                    watcher.Created += new FileSystemEventHandler(FilSkapad);
                    
                    // Begin watching.
                    watcher.EnableRaisingEvents = true;
            
        }
  
        private static void FilSkapad(object source, FileSystemEventArgs e)
        {
            CSV_to_XML(e.FullPath);
        }
        
        private static void CSV_to_XML(string CSV_name)
        {
            // först ta bort alla *.xml filer i katalogen, det ska finnas bara en

            string[] XML_files = System.IO.Directory.GetFiles(XML_file_dir, "*.xml");

            foreach (string file_xml in XML_files)
            {
                System.IO.File.Delete(file_xml);
            }

            string XML_dir = XML_file_dir;//new FileInfo(CSV_name).Directory.FullName;
            string filename = Path.GetFileNameWithoutExtension(CSV_name);

            string[] lines = new string[1] { "" };

            try
            {
                lines = System.IO.File.ReadAllLines(CSV_name);
            }
            catch
            {
                System.Threading.Thread.Sleep(5000); // 3s - det räcker i mina tester, efter kopieringen

                try
                {
                    lines = System.IO.File.ReadAllLines(CSV_name);
                }
                catch
                {
                    MessageBox.Show("Fil " + CSV_name + " är upptagen, kan inte komma åt den just nu.");
                    return;
                }

            }

            lines = lines.Skip(1).ToArray(); // skip headers

            XmlWriterSettings XML_inst = new XmlWriterSettings();
            XML_inst.Indent = true;

            XmlWriter XML_Wrt = XmlWriter.Create(XML_dir + Path.DirectorySeparatorChar +
                 filename + ".xml", XML_inst);

            XML_Wrt.WriteStartDocument();
            XML_Wrt.WriteStartElement("Inventory");

            string[] values;

            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    values = line.Split(';');

                    if (values.Length == 11)
                    {
                        XML_Wrt.WriteStartElement("Item");

                        XML_Wrt.WriteStartElement("Name");
                        XML_Wrt.WriteString(values[1]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Count");
                        values[10] = values[10].Replace("\"", "");
                        XML_Wrt.WriteString(values[10]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Price");
                        //values[2] = values[2].Replace(",", ".");
                        values[2] = values[2].Replace("\"", "");
                        values[2] = values[2].Replace(".", ",");

                        XML_Wrt.WriteString(values[2]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Comment");
                        XML_Wrt.WriteString("From My_store");
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Artist");
                        XML_Wrt.WriteString(values[4]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Publisher");
                        XML_Wrt.WriteString("");
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Genre");
                        XML_Wrt.WriteString(values[5]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("Year");
                        XML_Wrt.WriteString("2020");
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteStartElement("ProductID");
                        values[0] = values[0].Replace("\"", "");
                        XML_Wrt.WriteString(values[0]);
                        XML_Wrt.WriteFullEndElement();

                        XML_Wrt.WriteEndElement();

                    }
                    else
                    {
                        MessageBox.Show("Wrong number of items in CSV file row. "+ line);
                    }

                }
                else
                {
                    MessageBox.Show("Empty line in CSV file is found. ");
                }
                
            }

            XML_Wrt.WriteEndElement();
            XML_Wrt.Close();

            File.Delete(CSV_name);
        }
    }
}
