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
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;
using fileName;
using System.Globalization;// For culture info 
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (Form.ModifierKeys == Keys.None && keyData == Keys.F5)
            {
                this.Controls.Clear();
                this.InitializeComponent();
                this.CenterToScreen();          
                menuStripToolTips();
                btn_pdf.Enabled = false; btn_cutlist.Enabled = false; btn_quoteBreakdown.Enabled = false;
                groupBox1.Visible = false; groupBox3.Visible = false; btn_editQuote.Enabled = false;
                txt_search.Select();
                listBoxFill();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_pdf.Select();   
            if (listBox1.SelectedItem != null)
            {
                lbl_listBox.Text = listBox1.SelectedItem.ToString();
            }
            DirectoryInfo dinfoSAF  =       new DirectoryInfo(@"K:/CSupply And Fit/");
            DirectoryInfo dinfoSO   =       new DirectoryInfo(@"K:/CSupply Only/");
            DirectoryInfo dinfoQF   =       new DirectoryInfo(@"K:/QUOTEfit/");
            DirectoryInfo dinfoQS   =       new DirectoryInfo(@"K:/QUOTEsupply/");
            DirectoryInfo dinfoCL   =       new DirectoryInfo(@"K:/Cutlist/");
            string suppAndFit       =       dinfoSAF    + listBox1.Text;
            string suppOnly         =       dinfoSO     + listBox1.Text;
            string quoteFit         =       dinfoQF     + listBox1.Text;
            string quoteSupply      =       dinfoQS     + listBox1.Text;
            string cutlist          =       dinfoCL     + listBox1.Text;
            //Checking to see if the file exists and if it does store it in selected file and enable pdf and quote breakdown buttons
            if (File.Exists(suppAndFit))
            {
                fileName.fileName.SetselectedFile(suppAndFit);
                btn_pdf.Enabled = true;
                btn_quoteBreakdown.Enabled = true;
                btn_editQuote.Enabled = true;
            }
            else if (File.Exists(suppOnly))
            {
                fileName.fileName.SetselectedFile(suppOnly);
                btn_pdf.Enabled = true;
                btn_quoteBreakdown.Enabled = true;
                btn_editQuote.Enabled = true;
            }
            else if (File.Exists(quoteFit))
            {
                fileName.fileName.SetselectedFile(quoteFit);
                btn_pdf.Enabled = true;
                btn_quoteBreakdown.Enabled = true;
                btn_editQuote.Enabled = true;
            }
            else if (File.Exists(quoteSupply))
            {
                fileName.fileName.SetselectedFile(quoteSupply);
                btn_pdf.Enabled = true;
                btn_quoteBreakdown.Enabled = true;
                btn_editQuote.Enabled = true;
            }         
            else
            {
                MessageBox.Show("Please select a file", "Error");
            }            
            List<string> selectedFiles = new List<string>(); //adding files that exist to a list 
            List<string> selectedFilesSAF = new List<string>();
            string FileName = fileName.fileName.selectedFile;
            if (FileName.Contains("QUOTEsupply") || FileName.Contains("QUOTEfit")) //creating a search key i.e the filename minus the .txt extension 
            {
                string[] shortFileName = FileName.Split('/');
                string searchKey = shortFileName[2].Substring(0, shortFileName[2].Length - 4);
                FileInfo[] FilesSO = dinfoSO.GetFiles(searchKey + "*" + ".txt");
                FileInfo[] FilesSAF = dinfoSAF.GetFiles(searchKey + "*" + ".txt");
                Console.WriteLine("Search Key:" + searchKey);
                foreach (FileInfo file in FilesSO)
                {
                    selectedFiles.Add(file.Name);
                    selectedFiles.ForEach(Console.WriteLine);
                }
                foreach (FileInfo file in FilesSAF)
                {
                    selectedFilesSAF.Add(file.Name);
                }
                lbl_sfdSuppOnly.Text= "No";
                for (int i = 0; i < selectedFiles.Count; i++) // i number of files 
                {
                    FileInfo relatedFiles = new FileInfo(@"K:/CSupply Only/" + selectedFiles[i]);
                    string[] lines = File.ReadLines(@"K:/CSupply Only/" + selectedFiles[i]).ToArray();
                    int count = lines.Length;
                    for (int x = 0; x < count; x++) // x lines in each file 
                    {
                        fileName.fileName.SetcurrentFile(@"K:/CSupply Only/" + selectedFiles[i]);
                        string sfd = (@"K:/CSupply Only/" + selectedFiles[i]);
                        FileInfo sfd1 =  new FileInfo(@"K:/CSupply Only/" + selectedFiles[i]);
                        if (sfd1.Exists)
                        {
                            lbl_sfdSuppOnly.Text = "Yes";
                        }                    
                        if (x == 1)
                        {
                            string[] explainSearchKey = Regex.Split(@"K:/CSupply Only/" + selectedFiles[0], @"\D+"); // searching for RefExplains file 
                            string explain = explainSearchKey[1];
                            fileName.fileName.Setexplain(explain);
                            if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                            {
                                FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                int countExplain = explainLines.Length;
                                for (int y = 0; y < countExplain; y++) // x lines in file 
                                {
                                    if (explainLines[0] == null || explainLines[0] == "")
                                    {
                                        lbl_explain.Text = "No Description Available";
                                    }
                                    else 
                                    {
                                        lbl_explain.Text = explainLines[0];
                                    }
                                }
                            }
                        }
                    }                   
                }
                lbl_sfdSuppFit.Text = "No";
                for (int i = 0; i < selectedFilesSAF.Count; i++) // i number of files 
                {        
                    FileInfo relatedFiles = new FileInfo(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                    string[] lines = File.ReadLines(@"K:/CSupply And Fit/" + selectedFilesSAF[i]).ToArray();
                    int count = lines.Length;
                    for (int x = 0; x < count; x++) // x lines in file 
                    {
                        fileName.fileName.SetcurrentFile(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                        FileInfo sfdf = new FileInfo(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                        if (sfdf.Exists)
                        {
                            lbl_sfdSuppFit.Text = "Yes";
                        }                    
                        if (x == 1)
                        {
                            string[] explainSearchKey = Regex.Split(@"K:/CSupply And Fit/" + selectedFilesSAF[0], @"\D+"); // searching for RefExplains file 
                            string explain = explainSearchKey[1];
                            if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                            {
                                FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                int countExplain = explainLines.Length;
                                for (int y = 0; y < countExplain; y++) //x lines in file 
                                {
                                    if (explainLines[0] == null || explainLines[0] == "")
                                    {
                                        lbl_explain.Text = "No Description Available";
                                    }
                                    else
                                    {
                                        lbl_explain.Text = explainLines[0];
                                    }                                 
                                }
                            }
                        }  
                    }                 
                }               
            }
            if (File.Exists(cutlist))
            {
               btn_cutlist.Enabled = true;
            }
            else if (!File.Exists(cutlist))
            {
                btn_cutlist.Enabled = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            txt_search.Select();
            menuStripToolTips(); 
            btn_pdf.Enabled = false;        btn_cutlist.Enabled = false;        btn_quoteBreakdown.Enabled = false;
            groupBox1.Visible = false;      groupBox3.Visible = false;          btn_editQuote.Enabled = false;
            string changeFiles;
            string changeFiles2;
            string[] filePaths = Directory.GetFiles(@"K:/QUOTEfit/");           
            string[] filePaths2 = Directory.GetFiles(@"K:/QUOTEsupply/"); 

            foreach (string myfile in filePaths)
            {
                changeFiles = Path.ChangeExtension(myfile, ".txt");
                System.IO.File.Move(myfile, changeFiles);
            }
            foreach (string myfile in filePaths2)
            {
                changeFiles2 = Path.ChangeExtension(myfile, ".txt");
                System.IO.File.Move(myfile, changeFiles2);
            }
            listBoxFill();
        }
        private void listBoxFill()
        {
            DirectoryInfo dinfoQF = new DirectoryInfo(@"K:/QUOTEfit/");
            FileInfo[] FilesQF = dinfoQF.GetFiles("*.txt");
            DirectoryInfo dinfoQS = new DirectoryInfo(@"K:/QUOTEsupply/");
            FileInfo[] FilesQS = dinfoQS.GetFiles("*.txt");
            foreach (FileInfo file in FilesQF)
            {
                DateTime dirFiles = file.LastWriteTime;
                DateTime pastWeek = System.DateTime.Today.AddDays(-14);
                int result = DateTime.Compare(dirFiles, pastWeek); //If quote file was last edited 7 days or less add to listbox 
                if (result >= 0)
                {
                    if (!listBox1.Items.Contains(file.Name))
                    {
                        listBox1.Items.Add(file.Name);
                        for (int i = 0; i < listBox1.Items.Count / 2; i++)
                        {
                            var tmp = listBox1.Items[i];
                            listBox1.Items[i] = listBox1.Items[listBox1.Items.Count - i - 1];
                            listBox1.Items[listBox1.Items.Count - i - 1] = tmp;
                        }
                    }
                }
            }
            foreach (FileInfo file in FilesQS)
            {
                DateTime dirFiles = file.LastWriteTime;
                DateTime pastWeek = System.DateTime.Today.AddDays(-14);
                int result = DateTime.Compare(dirFiles, pastWeek);
                if (result >= 0)
                {
                    if (!listBox1.Items.Contains(file.Name))
                    {
                        listBox1.Items.Add(file.Name);
                        for (int i = 0; i < listBox1.Items.Count / 2; i++)
                        {
                            var tmp = listBox1.Items[i];
                            listBox1.Items[i] = listBox1.Items[listBox1.Items.Count - i - 1];
                            listBox1.Items[listBox1.Items.Count - i - 1] = tmp;
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btn_pdf_Click(object sender, EventArgs e)
        {
            if (fileName.fileName.selectedFile != "")
            {
                finalPrice();
                carriage();
                List<string> selectedFiles = new List<string>();
                List<string> selectedFilesSAF = new List<string>();
                string FileName = fileName.fileName.selectedFile;
                if (FileName.Contains("QUOTEsupply") || FileName.Contains("QUOTEfit"))
                {
                    string[] shortFileName = FileName.Split('/');
                    string searchKey = shortFileName[2].Substring(0, shortFileName[2].Length - 4);
                    DirectoryInfo dinfoSO = new DirectoryInfo(@"K:/CSupply Only/");
                    FileInfo[] FilesSO = dinfoSO.GetFiles(searchKey + "*" + ".txt");
                    DirectoryInfo dinfoSAF = new DirectoryInfo(@"K:/CSupply And Fit/");
                    FileInfo[] FilesSAF = dinfoSAF.GetFiles(searchKey + "*" + ".txt");
                    foreach (FileInfo file in FilesSO)
                    {
                        selectedFiles.Add(file.Name);
                    }
                    foreach (FileInfo file in FilesSAF)
                    {
                        selectedFilesSAF.Add(file.Name);
                    }
                    for (int i = 0; i < selectedFiles.Count; i++) // i number of files 
                    {
                        FileInfo relatedFiles = new FileInfo(@"K:/CSupply Only/" + selectedFiles[i]);
                        string[] lines = File.ReadLines(@"K:/CSupply Only/" + selectedFiles[i]).ToArray();
                        int count = lines.Length;
                        for (int x = 0; x < count; x++) // x lines in file 
                        {
                            fileName.fileName.SetcurrentFile(@"K:/CSupply Only/" + selectedFiles[i]);
                            custDetails();
                            siteDetails();
                            if (lines[x].Contains("D/WIRE 868 FENCE Height"))
                            {
                                DialogResult result = MessageBox.Show("You selected Double Wire Fence \nYes for PreGalv ; No for Hot Dipped Galv'd", "Double Wire Fence Selection", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFence();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                }
                                else if (result == DialogResult.No)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFenceHDG();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                }
                                else
                                {
                                    this.Controls.Clear();
                                    this.InitializeComponent();
                                    Form1_Load(sender, e);
                                }
                            }
                            if (lines[x].Contains("D/WIRE 888 FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire888();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN H/CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicSinGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. H.CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicDBGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("HVY.CLASSIC FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicHeavyFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("CLASSIC FENCE") && !lines[x].Contains("HVY"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                doubleWireGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                sinDWGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("AXIS FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicEcoFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PRISON MESH FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                prisonMesh();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Netted B/WIRE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
                            }
                            if (lines[x].Contains("Spect Fence"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                SpectFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PALISADE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                palisadeFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("D/W Ball Stop Fence"))
                            {
                                sinDWWisaGate();
                            }
                            if (x == 1)
                            {
                                string[] explainSearchKey = Regex.Split(@"K:/CSupply Only/" + selectedFiles[0], @"\D+");
                                string explain = explainSearchKey[1];
                                if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                                {
                                    FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                    string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                    int countExplain = explainLines.Length;
                                    for (int y = 0; y < countExplain; y++) // x lines in file 
                                    {
                                        txt_explain.Text = explainLines[0];
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < selectedFilesSAF.Count; i++) // i number of files 
                    {
                        FileInfo relatedFiles = new FileInfo(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                        string[] lines = File.ReadLines(@"K:/CSupply And Fit/" + selectedFilesSAF[i]).ToArray();
                        int count = lines.Length;
                        Console.WriteLine("SAF:" + selectedFilesSAF[i]);
                        for (int x = 0; x < count; x++) // x lines in file 
                        {

                            fileName.fileName.SetcurrentFile(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                            custDetails();
                            siteDetails();
                            if (lines[x].Contains("D/WIRE 868 FENCE Height"))
                            {
                                DialogResult result = MessageBox.Show("You selected Double Wire Fence \nYes for PreGalv ; No for Hot Dipped Galv'd", "Double Wire Fence Selection", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFence();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else if (result == DialogResult.No)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFenceHDG();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else
                                {
                                    this.Controls.Clear();
                                    this.InitializeComponent();
                                    Form1_Load(sender, e);
                                }
                            }
                            if (lines[x].Contains("D/WIRE 888 FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire888();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN H/CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicSinGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. H.CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicDBGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("HVY.CLASSIC FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicHeavyFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("CLASSIC FENCE") && !lines[x].Contains("HVY"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                doubleWireGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                sinDWGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("AXIS FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicEcoFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PRISON MESH FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                prisonMesh();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Netted B/WIRE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Spect Fence"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                SpectFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PALISADE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                palisadeFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("D/W Ball Stop Fence"))
                            {
                                sinDWWisaGate();
                            }
                            if (x == 1)
                            {
                                string[] explainSearchKey = Regex.Split(@"K:/CSupply And Fit/" + selectedFilesSAF[0], @"\D+");
                                string explain = explainSearchKey[1];
                                fileName.fileName.Setexplain(explain);
                                if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                                {
                                    FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                    string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                    int countExplain = explainLines.Length;
                                    for (int y = 0; y < countExplain; y++) // x lines in file 
                                    {
                                        txt_explain.Text = explainLines[0];
                                    }
                                }
                            }
                        }
                    }
                }
                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    iTextSharp.text.Font fdefault = FontFactory.GetFont("HELVETICA", 8, BaseColor.BLACK);
                    iTextSharp.text.Font fdetails = FontFactory.GetFont("HELVETICA", 9, BaseColor.BLACK);
                    iTextSharp.text.Font fBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                    iTextSharp.text.Font fBoldPrices = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            setting up the doc and table 
                    Document myDocument = new Document();
                    PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                    myDocument.Open();
                    PdfPTable table = new PdfPTable(2);                                                                                 // create table with 2 columns 
                    PdfPTable tableFooter = new PdfPTable(2);
                    PdfPTable tableDetails = new PdfPTable(3);
                    PdfPTable tableQuote = new PdfPTable(5);
                    PdfPTable tableTitle = new PdfPTable(5);
                    PdfPTable tableCarriage = new PdfPTable(5);
                    tableQuote.SplitLate = false;
                    tableQuote.SplitRows = false;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            creating header cell
                    Paragraph header = new Paragraph("Description", fBold);
                    header.Alignment = Element.ALIGN_CENTER;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////                   
                    FileInfo file2 = new FileInfo("Header.txt");
                    using (StreamReader sr2 = file2.OpenText())
                    {
                        while (!sr2.EndOfStream)
                        {
                            var image = iTextSharp.text.Image.GetInstance(sr2.ReadLine());
                            var imageCell = new PdfPCell(image);
                            image.ScaleToFit(300f, 300f);
                            image.SetAbsolutePosition(60, 700);
                            PdfPCell imageHeader = new PdfPCell(image);
                            imageHeader.Colspan = 2;
                            imageHeader.HorizontalAlignment = 1;
                            imageHeader.Border = 0;
                            table.DefaultCell.Border = 0;
                            table.AddCell(imageHeader);
                            Phrase phrase = new Phrase();
                            Phrase sdetails = new Phrase();
                            phrase.Add(
                                        new Chunk(Environment.NewLine + lbl_custName.Text + Environment.NewLine + lbl_address.Text + Environment.NewLine
                                        + lbl_town.Text + Environment.NewLine + lbl_county.Text + Environment.NewLine + lbl_postcode.Text + Environment.NewLine
                                        + lbl_telNo.Text + Environment.NewLine + Environment.NewLine, fdetails)
                                        );
                            string test = DateTime.Now.ToString("dd.MM.yyy");
                            sdetails.Add(new Chunk(Environment.NewLine + "Quotation : " + fileName.fileName.explain + Environment.NewLine + Environment.NewLine + lbl_site.Text + Environment.NewLine + Environment.NewLine + "Date: " + test, fdetails));
                            Console.WriteLine("Explain:" + fileName.fileName.explain);
                            int[] intTblWidth = { 70, 2, 28 };
                            tableDetails.SetWidths(intTblWidth);
                            tableDetails.HorizontalAlignment = Element.ALIGN_LEFT;
                            tableDetails.WidthPercentage = 100;
                            tableDetails.DefaultCell.Border = 0;
                            tableDetails.DefaultCell.SetLeading(3, 1);
                            tableDetails.AddCell(phrase);
                            tableDetails.AddCell("");
                            tableDetails.AddCell(sdetails);
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////
                            FileInfo file1 = new FileInfo("Footer.txt");
                            using (StreamReader sr1 = file1.OpenText())
                            {
                                while (!sr1.EndOfStream)
                                {
                                    var imageFooter = iTextSharp.text.Image.GetInstance(sr1.ReadLine());
                                    var imageCellFooter = new PdfPCell(imageFooter);
                                    imageFooter.ScaleToFit(400f, 400f);
                                    PdfPCell imageFooterCell = new PdfPCell(imageFooter);
                                    imageFooterCell.Colspan = 2;
                                    imageFooterCell.HorizontalAlignment = 1;
                                    imageFooterCell.Border = 0;
                                    imageFooter.SetAbsolutePosition(80, 0);
                                    tableFooter.DefaultCell.Border = 0;
                                    tableFooter.AddCell(imageFooterCell);
                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            adding paragraphs 
                                    txt_quote.Text = txt_quotePara.Text;
                                    Paragraph paraQuote = new Paragraph(Environment.NewLine + txt_quote.Text, fdefault);
                                    txt_quote1.Text = txt_quotePara1.Text;
                                    Paragraph paraQuote1 = new Paragraph(Environment.NewLine + txt_quote1.Text + Environment.NewLine, fBoldPrices);
                                    txt_quote2.Text = txt_quotePara2.Text;
                                    Paragraph paraQuote2 = new Paragraph(Environment.NewLine + txt_quotePara2.Text + Environment.NewLine, fBoldPrices);
                                    txt_quote3.Text = txt_quotePara3.Text;
                                    Paragraph paraQuote3 = new Paragraph(Environment.NewLine + txt_quotePara3.Text + Environment.NewLine, fBoldPrices);                                   
                                    Paragraph paraExplain = new Paragraph(txt_explain.Text);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    //PdfContentByte cb = myPDFWriter.DirectContent;
                                    var paraFinalPrices = new Paragraph("£"+ fileName.fileName.tNA2
                                                                        + Environment.NewLine + "£" + fileName.fileName.tVA2
                                                                        + Environment.NewLine + "£" + fileName.fileName.t2, fBoldPrices);                                  
                                    var paraFPText = new Paragraph(fileName.fileName.tNA
                                                                        + Environment.NewLine   + fileName.fileName.tVA
                                                                        + Environment.NewLine   + fileName.fileName.t, fBoldPrices);
                                    PdfPTable tableFP = new PdfPTable(4);
                                    float[] intTblFPWidth = { 45, 1,32,22 };
                                    tableFP.SetWidths(intTblFPWidth);
                                    tableFP.WidthPercentage = 100;                              
                                    tableFP.DefaultCell.Border = 0;
                                    tableFP.DefaultCell.SetLeading(3, 1);                               
                                    tableFP.AddCell("");
                                    tableFP.AddCell("");
                                    PdfPCell fpt = new PdfPCell(paraFPText) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                                    fpt.Border = 0; fpt.SetLeading(3, 1);
                                    tableFP.AddCell(fpt);
                                    PdfPCell fp = new PdfPCell(paraFinalPrices) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    fp.Border = 0; fp.SetLeading(3, 1);
                                    tableFP.AddCell(fp);                                   
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    int[] intTblQuoteWidth = { 64, 1, 11,11,11 };
                                    tableQuote.SetWidths(intTblQuoteWidth);
                                    tableQuote.WidthPercentage = 100;
                                    tableQuote.DefaultCell.Border = 0;
                                    tableQuote.DefaultCell.SetLeading(3, 1);
                                    PdfPCell q = new PdfPCell(paraQuote) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED};
                                    q.Border = 0; q.SetLeading(3, 1);
                                    tableQuote.AddCell(q);
                                    tableQuote.AddCell("");
                                    PdfPCell q1 = new PdfPCell(paraQuote1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    q1.Border = 0; q1.SetLeading(3, 1);
                                    tableQuote.AddCell(q1);
                                    PdfPCell q2 = new PdfPCell(paraQuote2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    q2.Border = 0; q2.SetLeading(3, 1);
                                    tableQuote.AddCell(q2);
                                    PdfPCell q3 = new PdfPCell(paraQuote3) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    q3.Border = 0; q3.SetLeading(3, 1);
                                    tableQuote.AddCell(q3);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    int[] intTblCarriageWidth = { 64, 1, 11, 11, 11 };
                                    Paragraph paraCText = new Paragraph("Carriage to Site ", fBoldPrices);
                                    Paragraph paraCPrice = new Paragraph("1 Nr.", fBoldPrices);
                                    Paragraph paraCPrice1 = new Paragraph("£"+label2.Text, fBoldPrices);                          
                                    tableCarriage.SetWidths(intTblCarriageWidth);
                                    tableCarriage.HorizontalAlignment = Element.ALIGN_LEFT;
                                    tableCarriage.WidthPercentage = 100;
                                    tableCarriage.DefaultCell.Border = 0;
                                    tableCarriage.DefaultCell.SetLeading(3, 1);
                                    tableCarriage.AddCell(paraCText);
                                    tableCarriage.AddCell("");
                                    PdfPCell nr = new PdfPCell(paraCPrice) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    nr.Border = 0; nr.SetLeading(3, 1);
                                    tableCarriage.AddCell(nr);
                                    PdfPCell p1 = new PdfPCell(paraCPrice1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    p1.Border = 0; p1.SetLeading(3, 1);
                                    tableCarriage.AddCell(p1);
                                    tableCarriage.AddCell(p1);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    Paragraph paraDescription = new Paragraph("Description", fBold);
                                    Paragraph paraTitle = new Paragraph("Quantity", fBold);
                                    Paragraph paraTitle1 = new Paragraph("Unit Price", fBold);
                                    Paragraph paraTitle2 = new Paragraph("Net Amount", fBold);                                   
                                    int[] intTbltitleWidth = { 64, 1, 11, 11, 11 };
                                    tableTitle.SetWidths(intTbltitleWidth);                                 
                                    tableTitle.WidthPercentage = 100;
                                    tableTitle.DefaultCell.Border = 0;
                                    tableTitle.DefaultCell.SetLeading(3, 1);
                                    tableTitle.AddCell(paraDescription);
                                    tableTitle.AddCell("");
                                    PdfPCell t = new PdfPCell(paraTitle) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    t.Border = 0; t.SetLeading(3, 1);
                                    tableTitle.AddCell(t);
                                    PdfPCell t1 = new PdfPCell(paraTitle1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    t1.Border = 0;  t1.SetLeading(3, 1);
                                    tableTitle.AddCell(t1);
                                    PdfPCell t2 = new PdfPCell(paraTitle2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    t2.Border = 0;  t2.SetLeading(3, 1);
                                    tableTitle.AddCell(t2);
                                    ///////////////////////////////////////////////////////////////////////////////////////////////////////             
                                    myDocument.Add(table);
                                    myDocument.Add(tableDetails);
                                    myDocument.Add(tableTitle);
                                    myDocument.Add(tableQuote);
                                    myDocument.Add(tableCarriage);
                                    myDocument.Add(tableFP);
                                    //tableFP.WriteSelectedRows(0, -1, 0, (myDocument.BottomMargin + 790), myPDFWriter.DirectContent);
                                    myDocument.Add(imageFooter);                               
                                    myDocument.Close();
                                }
                            }
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////  
                    byte[] content = myMemoryStream.ToArray();
                    string output = lbl_listBox.Text;
                    string listbox = output.Remove(output.Length - 4);
                    DateTime date = DateTime.Parse(System.DateTime.Now.ToString());
                    string dateForPDF = date.ToString("HHmmss");
                    FileInfo filepdfPath = new FileInfo("pdfPath.txt");
                    using (StreamReader sr3 = filepdfPath.OpenText())
                    {
                        while (!sr3.EndOfStream)
                        {
                            var pathString = Path.Combine(sr3.ReadLine() + listbox);
                            var finalPS = pathString.Replace("\\", "/");
                            System.IO.Directory.CreateDirectory(finalPS);
                            using (FileStream fs = File.Create(finalPS + "/" + "/" + listbox  + ".pdf"))
                            {
                                fs.Write(content, 0, (int)content.Length); // writing out pdf from the content array
                                DialogResult saved = MessageBox.Show("Saved Successfully!", "Quote Saved", MessageBoxButtons.OK);//Dialog box to show file has been saved 
                            }
                            this.Controls.Clear();
                            this.InitializeComponent();
                            Form1_Load(sender, e);
                        }
                    }
                }
            }
        }
        private void finalPrice()
        {
            if (fileName.fileName.selectedFile != "")
            {
                Console.WriteLine("selected file:" + fileName.fileName.selectedFile);
                string[] lines = File.ReadAllLines(fileName.fileName.selectedFile).Reverse().ToArray();
                int count = lines.Length;
                for (int x = 0; x < count; x++) // x lines in file 
                {
                    ///////////////////////////////////////////////////////////////////////////// TOTAL
                    string total = lines[0];
                    Regex.Replace(total, @"\D+", "");
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex("[ ]{2,}", options);
                    total = regex.Replace(total, " ");

                    string inputT = total;
                    string patternT = @"([^\w]*Stg[^\w]*)+|[|\\^&\r\n]+";
                    string replacementT = " \t £: ";
                    Regex rgx = new Regex(patternT);
                    string resultT = rgx.Replace(inputT, replacementT);
                    lbl_finalTotal.Text = resultT;

                    string[] spaceSplit = resultT.Split(' ');
                    string t = spaceSplit[1];
                    string currency = spaceSplit[3];
                    string t2 = spaceSplit[4];
                    fileName.fileName.Sett(t);
                    fileName.fileName.Setcurrency(currency);
                    fileName.fileName.Sett2(t2);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////     VAT          
                    string vat = lines[1];
                    Regex.Replace(vat, @"\D+", "");
                    RegexOptions options1 = RegexOptions.None;
                    Regex regex1 = new Regex("[ ]{2,}", options1);
                    vat = regex1.Replace(vat, " ");

                    string inputV = vat;
                    string patternV = @"([^\w]*Stg[^\w]*)+|[|\\^&\r\n]+";
                    string replacementV = " %  \t £: ";
                    Regex rgxV = new Regex(patternV);
                    string resultV = rgxV.Replace(inputV, replacementV);
                    lbl_finalVAT.Text = resultV;

                    string[] spaceSplitVAT = resultV.Split(' ');
                    string tVA = spaceSplitVAT[1] + " " + spaceSplitVAT[2] + " " + spaceSplitVAT[3] + " " + spaceSplitVAT[4];
                    string tVA2 = spaceSplitVAT[8];
                    fileName.fileName.SettVA(tVA);
                    fileName.fileName.SettVA2(tVA2);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////  NET AMOUNT
                    string netAmount = lines[2];
                    Regex.Replace(netAmount, @"\D+", "");
                    RegexOptions options2 = RegexOptions.None;
                    Regex regex2 = new Regex("[ ]{2,}", options2);
                    netAmount = regex2.Replace(netAmount, " ");
                    lbl_finalNetAmount.Text = netAmount;
                    string input = netAmount;
                    string pattern = @"([^\w]*Erecting[^\w]*)+|[|\\^&\r\n]+";
                    string replacement = " ";
                    Regex rgxN = new Regex(pattern);
                    string result = rgxN.Replace(input, replacement);
                    lbl_finalNetAmount.Text = result;

                    string inputA = result;
                    string patternA = @"([^\w]*Stg[^\w]*)+|[|\\^&\r\n]+";
                    string replacementA = "  £: ";
                    Regex rgxA = new Regex(patternA);
                    string resultA = rgxV.Replace(inputA, replacementA);
                    resultA.PadRight(200);
                    lbl_finalNetAmount.Text = resultA;

                    string[] spaceSplitNA = resultA.Split(' ');
                    string tNA = spaceSplitNA[1] + " " + spaceSplitNA[2] + " " + spaceSplitNA[3];
                    string tNA2 = spaceSplitNA[6];
                    fileName.fileName.SettNA(tNA);
                    fileName.fileName.SettNA2(tNA2);
                    Console.WriteLine("tNA: " + tNA);
                    Console.WriteLine("tNA2: " + tNA2);
                }
            }
        }
        private void custDetails()
        {
            string line;
            int counter = 0;         
            FileInfo selectedFile = new FileInfo(fileName.fileName.selectedFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    counter++;
                    if (counter == 5)
                    {
                        string custName = line;    
                        string outputCustName = custName.Trim().Trim(' ');
                        lbl_custName.Text = outputCustName;
                    }
                    if (counter == 6)
                    {
                        string address = line;
                        string outputAddress = address.Trim().Trim(' ');
                        lbl_address.Text = outputAddress;
                    }
                    if (counter == 7)
                    {
                        string town = line;
                        string outputTown = town.Trim().Trim(' ');
                        lbl_town.Text = outputTown;
                    }
                    if (counter == 8)
                    {
                        string county = line;
                        string outputCounty = county.Trim().Trim(' ');
                        lbl_county.Text = outputCounty;
                    }
                    if (counter == 9)
                    {
                        string postcode = line;
                        string outputPostcode = postcode.Trim().Trim(' ');
                        lbl_postcode.Text = outputPostcode;
                    }
                    if (counter == 12)
                    {
                        string telNo = line;
                        string outputTelNo = telNo.Trim().Trim(' ');
                        lbl_telNo.Text = outputTelNo;
                    }
                }
            }
        }
        private void siteDetails()
        {
            string line;
            int counter = 0;

            FileInfo selectedFile = new FileInfo(fileName.fileName.selectedFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    counter++;
                    if (counter == 11)
                    {
                        string site = line;
                        string outputSite = site.Trim().Trim(' ');
                        if (lbl_explain.Text != "No Description Available")
                            {
                                lbl_site.Text = outputSite + " " + lbl_explain.Text;
                            }
                        else
                            {
                                lbl_site.Text = outputSite;
                            }
                    }
                }
            }
        } 
        private void carriage()
        {
            if (fileName.fileName.selectedFile != "") //lbl_sfdSuppFit.Text == "Yes" &&  
            {
                string line;
                FileInfo quoteFile = new FileInfo(@"K:/QUOTEfit/" + lbl_listBox.Text);
                if (quoteFile.Exists)
                {
                    using (StreamReader sR = quoteFile.OpenText())
                    {
                        while ((line = sR.ReadLine()) != null)
                        {
                            if (line.Contains("Delivery To Site"))
                            {
                                string[] spaceSplit = line.Split(' ');
                                if (spaceSplit[42] == "") { label2.Text = spaceSplit[43]; }
                                else { label2.Text = spaceSplit[42]; }
                            }
                        }
                    }
                }
            }
            else if (lbl_sfdSuppOnly.Text == "Yes" && fileName.fileName.selectedFile != "")
            {
                string line;
                FileInfo quoteFile = new FileInfo(@"K:/QUOTEsupply/" + lbl_listBox.Text);
                using (StreamReader sR = quoteFile.OpenText())
                {
                    while ((line = sR.ReadLine()) != null)
                    {
                        if (line.Contains("Delivery To Site"))
                            {
                                string[] spaceSplit = line.Split(' ');
                                if (spaceSplit[42] == "") { label2.Text = spaceSplit[43]; }
                                else { label2.Text = spaceSplit[42]; }
                            }
                    }
                }
            }
        }
        private void classicHeavyFence()
        { 
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Ht"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("POSTS") && ! line.Contains("Extra"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[5]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                       
                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Heavy-Duty Fencing System to stand " + lbl_height.Text + " " + "above ground level,"
                           + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                           + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                           + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                           + " having 4 No reinforcing crimps 200 x 50 mm,"
                           + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                           + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                           + " Caltec clips + internal nut inserts."
                           + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                           ); 
                            txt_quotePara.AppendText(txt_quote.Text);
                            txt_quote1.Text = (lbl_netAmount.Text );
                            string[] splitString = txt_quote1.Text.Split(' ');
                            string q1Split = (splitString[0]+splitString[1] ) ;
                            var q2 = (splitString[4]);                    
                            var q3 = (splitString[7]);
                            string formattedQ2 = String.Format("£" + q2);
                            String formattedQ3 = String.Format("£"+ q3);
                            txt_quotePara1.AppendText(q1Split);
                            txt_quotePara2.AppendText(formattedQ2);
                            txt_quotePara3.AppendText(formattedQ3);
                }
            }
        }
        private void classicFence()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Ht"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        string[] numbers = Regex.Split(outputHeight1, @"\D+");
                        string pHeight = (numbers[0]);
                        lbl_height.Text = pHeight;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        string[] numbers = Regex.Split(outputHeight1, @"\D+");
                        string pHeight = (numbers[0]);
                        lbl_height.Text = pHeight;
                    }
                    if (line.Contains("POSTS") && !line.Contains("Extra"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[5]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {                  
                    int cfHeight = Int32.Parse(lbl_height.Text);
                    if (cfHeight <= 1230)
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std Fencing System to stand  " + lbl_height.Text + " " + "above ground level,"
                               + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                               + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                               + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                               + " having 4 No reinforcing crimps 200 x 50 mm,"
                               + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                               + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                               + " Caltec clips + internal nut inserts."
                               + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                               );
                                txt_quotePara.AppendText(txt_quote.Text);
                                txt_quote1.Text = (lbl_netAmount.Text);
                                string[] splitString = txt_quote1.Text.Split(' ');
                                string q1Split = (splitString[0] + splitString[1]);
                                string q2 = (splitString[3] + splitString[4]);
                                string q3 = (splitString[7]);
                                string formattedQ2 = String.Format("£" + q2);
                                String formattedQ3 = String.Format("£" + q3);
                                txt_quotePara1.AppendText(q1Split);
                                txt_quotePara2.AppendText(formattedQ2);
                                txt_quotePara3.AppendText(formattedQ3);
                    }
                    else if (cfHeight >1231 && cfHeight <=1830)
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std Fencing System to stand  " + lbl_height.Text + " mm" + " above ground level,"
                               + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                               + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                               + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                               + " having 4 No reinforcing crimps 200 x 50 mm,"
                               + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                               + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                               + " Caltec clips + internal nut inserts."
                               + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                               );
                                txt_quotePara.AppendText(txt_quote.Text);
                                txt_quote1.Text = (lbl_netAmount.Text);
                                string[] splitString = txt_quote1.Text.Split(' ');
                                string q1Split = (splitString[0] + splitString[1]);
                                string q2 = (splitString[3] + splitString[4]);
                                string q3 = (splitString[7]);
                                string formattedQ2 = String.Format("£" + q2);
                                String formattedQ3 = String.Format("£" + q3);
                                txt_quotePara1.AppendText(q1Split);
                                txt_quotePara2.AppendText(formattedQ2);
                                txt_quotePara3.AppendText(formattedQ3);
                    }
                    else if (cfHeight > 1831 && cfHeight <=2030)
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std Fencing System to stand  " + lbl_height.Text + " " + "above ground level,"
                               + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                               + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                               + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                               + " having 4 No reinforcing crimps 200 x 50 mm,"
                               + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                               + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                               + " Caltec clips + internal nut inserts."
                               + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                               );
                                txt_quotePara.AppendText(txt_quote.Text);
                                txt_quote1.Text = (lbl_netAmount.Text);
                                string[] splitString = txt_quote1.Text.Split(' ');
                                string q1Split = (splitString[0] + splitString[1]);
                                string q2 = (splitString[3] + splitString[4]);
                                string q3 = (splitString[7]);
                                string formattedQ2 = String.Format("£" + q2);
                                String formattedQ3 = String.Format("£" + q3);
                                txt_quotePara1.AppendText(q1Split);
                                txt_quotePara2.AppendText(formattedQ2);
                                txt_quotePara3.AppendText(formattedQ3);
                    }
                    else if (cfHeight > 2031)
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std Fencing System to stand  " + lbl_height.Text + " " + "above ground level,"
                               + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                               + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                               + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                               + " having 4 No reinforcing crimps 200 x 50 mm,"
                               + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                               + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                               + " Caltec clips + internal nut inserts."
                               + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                               );
                                txt_quotePara.AppendText(txt_quote.Text);
                                txt_quote1.Text = (lbl_netAmount.Text);
                                string[] splitString = txt_quote1.Text.Split(' ');
                                string q1Split = (splitString[0] + splitString[1]);
                                string q2 = (splitString[3] + splitString[4]);
                                string q3 = (splitString[7]);
                                string formattedQ2 = String.Format("£" + q2);
                                String formattedQ3 = String.Format("£" + q3);
                                txt_quotePara1.AppendText(q1Split);
                                txt_quotePara2.AppendText(formattedQ2);
                                txt_quotePara3.AppendText(formattedQ3);
                    }
                }
            }
        }
        private void classicEcoFence()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');                        
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("POSTS") && !line.Contains("Extra"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[5]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies  Classic ECO Fencing System to stand nominally  " + lbl_height.Text + " " + "above ground level,"
                           + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                           + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                           + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                           + " having 4 No reinforcing crimps 200 x 50 mm,"
                           + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                           + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                           + " Caltec clips + internal nut inserts."
                           + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                           );
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] splitString = txt_quote1.Text.Split(' ');
                    string q1Split = (splitString[0] + splitString[1]);
                    string q2 = (splitString[3] + splitString[4]);
                    string q3 = (splitString[7]);
                    string formattedQ2 = String.Format("£" + q2);
                    String formattedQ3 = String.Format("£" + q3);
                    txt_quotePara1.AppendText(q1Split);
                    txt_quotePara2.AppendText(formattedQ2);
                    txt_quotePara3.AppendText(formattedQ3);
                }
            }
        } 
        private void DBWireFence()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("POSTS"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[8]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                }
                
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine  + lbl_supply.Text + " " + "RFS 'Double Wire' 868 Fencing System to stand " + lbl_height.Text + " " + "  high above ground level comprising of " + lbl_posts.Text + " " + " RHS fence posts,"
                             + " set into and including concrete bases 450 x 450 x 800mm deep at 2.523m centres. Rogers Fencing Supplies Double Wire 868 mesh panels" + lbl_height.Text + " " + " high x 2506mm long,"
                             + " 200 x 50mm mesh size with double twinned 8mm diameter horizontal wire and 6mm diameter vertical wire, vertical wires protrude 22mm above top horizontal wire."
                             + " Panels fixed to posts with Cal-tech clips."
                             + " Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator.");
                    
                    string[] splitString = txt_quote1.Text.Split(' ');
                    if (splitString.Length >8)
                    {
                       // string q1Split = (splitString[0] + splitString[1] + "             " + splitString[3] + splitString[5] + "             " + splitString[8]);
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[3] + splitString1[4]);
                        string q3 = (splitString1[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                    else
                    {
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[3] + splitString1[4]);
                        string q3 = (splitString1[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                }
            }
        }
        private void  DBWireFenceHDG()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("POSTS"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[8]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                }

                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS ' Hot Dip Galvanized ' Double Wire 868 Fencing System to stand " + lbl_height.Text + " " + "  high above ground level comprising of " + lbl_posts.Text + " " + " ' Hot Dip Galvanized ' RHS fence posts,"
                             + " set into and including concrete bases 450 x 450 x 800mm deep at 2.523m centres. Rogers Fencing Supplies ' Hot Dip Galvanized ' Double Wire 868 mesh panels" + lbl_height.Text + " " + " high x 2506mm long,"
                             + " 200 x 50mm mesh size with double twinned 8mm diameter horizontal wire and 6mm diameter vertical wire, vertical wires protrude 22mm above top horizontal wire."
                             + " Panels fixed to posts with Cal-tech clips."
                             + " Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator.");
                    string[] splitString = txt_quote1.Text.Split(' ');
                    if (splitString.Length > 8)
                    {
                        // string q1Split = (splitString[0] + splitString[1] + "             " + splitString[3] + splitString[5] + "             " + splitString[8]);
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString[0] + splitString[1]);
                        string q2 = (splitString[3] + splitString[4]);
                        string q3 = (splitString[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                    else
                    {
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString[0] + splitString[1]);
                        string q2 = (splitString[3] + splitString[4]);
                        string q3 = (splitString[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                }
            }
        }
        private void prisonMesh() 
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("SUPPLY"))
                    {
                        string supply = Regex.Match(line, @"^[^0-9]*").Value.Trim();
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Height"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("Ht "))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[12];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("POSTS") && !line.Contains("Extra"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[2]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        string outputPPM1 = outputPPM.Trim().Trim(' ');
                        lbl_total.Text = outputPPM1;
                    }
                }
                    if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS 358 High Security fencing system to stand " + "" + lbl_height.Text  + " high above ground level "
                        + "on steel posts" + "" + lbl_posts.Text + "" + " mm 3770mm long, set into and including concrete bases 450 x 450 x 900mm deep at 2440mm centres."
                        + "Rogers Fencing Supplies 358 High Security mesh panels 2515mm wide x 3000mm high, 76.2 x 12.7mm mesh size, 4.00mm diameter,"
                        + "overlapped at posts and secured to same using flat clamp bar 3000mm long, post and clamp bar holed for M8 long spun galvanised"
                        + " cup square bolts and tamper-proof permacone fixings. "
                        + "Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator.");
                        string[] splitString = txt_quote1.Text.Split(' ');
                        if (splitString.Length > 8)
                        {
                            txt_quotePara.AppendText(txt_quote.Text);
                            txt_quote1.Text = (lbl_netAmount.Text);
                            string[] splitString1 = txt_quote1.Text.Split(' ');
                            string q1Split = (splitString1[0] + splitString1[1]);
                            string q2 = (splitString1[5]);
                            string q3 = (splitString1[8]);
                            txt_quotePara1.AppendText(q1Split);
                            txt_quotePara2.AppendText("£" + q2);
                            txt_quotePara3.AppendText("£" + q3);
                        }
                        else
                        {
                            txt_quotePara.AppendText(txt_quote.Text);
                            txt_quote1.Text = (lbl_netAmount.Text);
                            string[] splitString1 = txt_quote1.Text.Split(' ');
                            string q1Split = (splitString1[0] + splitString1[1]);
                            string q2 = (splitString1[5]);
                            string q3 = (splitString1[7]);
                            txt_quotePara1.AppendText(q1Split);
                            txt_quotePara2.AppendText("£" + q2);
                            txt_quotePara3.AppendText("£" + q3);
                        }
                    }
                }
            }       
        private void SpectFence()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
               
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');        
                        lbl_supply.Text = outputSupply+ " " + and +" " + fit +" ";
                    }
                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("Ht "))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[10];
                        string[] outputHeight1 = height.ToString().Split('.');
                       
                        lbl_height.Text = outputHeight1[0];
                    }
                    if (line.Contains("POSTS "))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[2] +"x" + numbers[3] +"x"+numbers[4] + "x" + numbers[5] +"L");
                        lbl_posts.Text = pHeight;
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        string outputPPM1 = outputPPM.Trim().Trim(' ');
                        lbl_total.Text = outputPPM1;
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Spectator fencing system to stand " + "" + lbl_height.Text + "" + " high above ground level "
                    + "on " + "" + lbl_posts.Text + " " + " posts set in concrete bases 350x350x500mm deep completely filled with 20N concrete at centres no greater than 2500mm."
                    + " Posts to be fitted with M8 threaded inserts every 400mm tensioned only by hydraulic tool. Posts to be fitted with 60x60mm RFS Top Rail angle bracket."
                    + "Finish: Galvanized to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved RFS applicator.");
                    string[] splitString = txt_quote1.Text.Split(' ');
                    if (splitString.Length > 8)
                    {
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[5]);
                        string q3 = (splitString1[8]);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText("£" + q2);
                        txt_quotePara3.AppendText("£" + q3);
                    }
                    else 
                    {
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[5]);
                        string q3 = (splitString1[7]);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText("£" + q2);
                        txt_quotePara3.AppendText("£" + q3);
                    }
                }
            }
        } 
        private void palisadeFence() 
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("Height"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_height.Text = numbers[3];
                        lbl_stiles.Text = numbers[2]; // Number of Bays 
                    }
                    if (line.Contains("Security"))
                    {
                        lbl_postsCN.Text = "Security";

                    }
                    if (line.Contains("G/Purpose"))
                    {
                        lbl_postsCN.Text = "G/Purpose";

                    }
                    if (line.Contains("SUPPLY") && line.Contains("ONLY"))
                    {
                        lbl_supply.Text = "Supply Only";

                    }
                    if (line.Contains("SUPPLY") && line.Contains("FIT"))
                    {
                        lbl_supply.Text = "Supply and Fit";
                    }
                    if (line.Contains(". @"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');

                        string input = outputPPM;
                        string pattern = @"([^\w]*Per Metre[^\w]*)+|[|\\^&\r\n]+";
                        string replacement = " = £: ";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replacement);
                        lbl_netAmount.Text = result;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        string outputPPM1 = outputPPM.Trim().Trim(' ');
                        lbl_total.Text = outputPPM1;
                    }
                }
                    if (lbl_postsCN.Text == "G/Purpose")
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + lbl_height.Text + "  mm high Palisade GP Spec Rails 45 x 45 x 6mm RSA, fish plated to posts."
                                            + "Pales D section pales having Triple Point Splayed 3.0mm thick, fixed to rails at 152mm centres"
                                            + "Posts 100 x 55 IPE set in concrete bases 450x450x750mm deep at centres no greater than 2750mm."
                                            + "Fixings Rails to be fixed to posts using fish plates and M12 x 40 Permacone bolts and saddle head nuts."
                                            + "Pales to be fixed to rails using M8 x 40 Saddle head and permacone bolted connections."
                                            + "\nFinish: Hot dipped Galvanised to BS EN ISO 1461: 1999 galvanised only");
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[3] + splitString1[4]);
                        string q3 = (splitString1[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                    if (lbl_postsCN.Text == "Security")
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + lbl_height.Text + "  mm high Palisade Security Spec Rails 45 x 45 x 6mm RSA, fish plated to posts."
                                            + "Pales D section pales having Triple Point Splayed 3.0mm thick, fixed to rails at 152mm centres"
                                            + "Posts 100 x 55 IPE set in concrete bases 450x450x750mm deep at centres no greater than 2750mm."
                                            + "Fixings Rails to be fixed to posts using fish plates and M12 x 40 Permacone bolts and saddle head nuts."
                                            + "Pales to be fixed to rails using M8 x 40 Saddle head and permacone bolted connections."
                                            + "\nFinish: Hot dipped Galvanised to BS EN ISO 1461: 1999 galvanised only");
                        txt_quotePara.AppendText(txt_quote.Text);
                        txt_quote1.Text = (lbl_netAmount.Text);
                        string[] splitString1 = txt_quote1.Text.Split(' ');
                        string q1Split = (splitString1[0] + splitString1[1]);
                        string q2 = (splitString1[3] + splitString1[4]);
                        string q3 = (splitString1[7]);
                        string formattedQ2 = String.Format("£" + q2);
                        String formattedQ3 = String.Format("£" + q3);
                        txt_quotePara1.AppendText(q1Split);
                        txt_quotePara2.AppendText(formattedQ2);
                        txt_quotePara3.AppendText(formattedQ3);
                    }
                }          
        }
        private void nettedDBWire() 
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("D/W PANEL ") && !line.Contains("Gv"))// || line.Contains("D/W PANEL 666") || line.Contains("D/W PANEL 888"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[4]);
                        txt_finalHeight.AppendText("." + pHeight);
                        string height = txt_finalHeight.Text;
                        Console.WriteLine("final height:" + height);
                        Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                        Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                        lbl_stiles.Text = height.TrimStart('.');
                        lbl_width.Text = lbl_stiles.Text; // height 
                        if (height.Length > 6)
                        {
                            string height1 = txt_finalHeight.Text;
                            string[] spaceSplit = height1.Split('.');
                            int one = Convert.ToInt32(spaceSplit[1]);
                            int two = Convert.ToInt32(spaceSplit[2]);
                            int finalHeight = one + two;
                            fileName.fileName.Setpanel1(one.ToString());
                            fileName.fileName.Setpanel2(two.ToString());
                            Console.WriteLine("final height:" + finalHeight);
                            Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                            Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                            lbl_stiles.Text = finalHeight.ToString(); // Height
                        }
                    }
                    if (line.Contains("D/W PANEL ") && line.Contains("Gv"))// || line.Contains("D/W PANEL 666") || line.Contains("D/W PANEL 888"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[3]);
                        txt_finalHeight.AppendText("." + pHeight);
                        string height = txt_finalHeight.Text;
                        Console.WriteLine("final height:" + height);
                        Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                        Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                        lbl_stiles.Text = height.TrimStart('.');
                        lbl_width.Text = lbl_stiles.Text; // height 
                        if (height.Length > 6)
                        {
                            string height1 = txt_finalHeight.Text;
                            string[] spaceSplit = height1.Split('.');
                            int one = Convert.ToInt32(spaceSplit[1]);
                            int two = Convert.ToInt32(spaceSplit[2]);
                            int finalHeight = one + two;
                            fileName.fileName.Setpanel1(one.ToString());
                            fileName.fileName.Setpanel2(two.ToString());
                            Console.WriteLine("final height:" + finalHeight);
                            Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                            Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                            lbl_stiles.Text = finalHeight.ToString(); // Height
                        }
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    }
                    if (line.Contains("Net") && line.Contains("Mts"))
                    {
                        string[] spaceSplit = line.Split('.');
                        lbl_1Item.Text = spaceSplit[0] + ". consisting of " + spaceSplit[1];
                    }
                    if (line.Contains("SUPPLY") && (line.Contains("FIT")))
                    {
                        lbl_supply.Text = line.Trim().Trim(' ');
                    }
                    if (line.Contains("Ln POSTS") && !line.Contains("Ext"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[6]); // sometimes 6 sometimes 7
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains("PANEL")) // panel height
                    {
                        string[] numbers    = Regex.Split(line, @"\D+");
                        string pHeight      = (numbers[4]);
                        lbl_height.Text     = pHeight;
                    }
                    if (line.Contains("Cn POSTS"))
                    {
                        string[] splitString    = line.Split(' ');
                        string posts            = (splitString[7]);
                        string outputPosts      = posts.Trim().Trim('"');
                        string outputPosts1     = outputPosts.Trim().Trim(' ');
                        lbl_postsCN.Text        = outputPosts1;
                    }
                    if (line.Contains("Price"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                }
                if (fileName.fileName.panel1 != null && fileName.fileName.panel1 == fileName.fileName.panel2)
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-6-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels "
                                         + " " + lbl_width.Text + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                         + "\nNetting to be attached with plastic ties and span" + lbl_1Item.Text + " high"
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text);
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 != "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-6-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels. "
                                         + "The first " + fileName.fileName.panel1 + " mm high x 2506mm wide with 200 x 30mm mesh aperature and the second " + fileName.fileName.panel2 + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 == "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-6-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + "mm. high overall above ground level and comprising of "
                                       + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 1 D/Wire 8-6-8 mesh panel "
                                       + lbl_width.Text + "mm high x 2506mm wide with 200 x 30mm mesh aperature."
                                       + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                       + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                       + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
            }
        }
        private void nettedDBWire666()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                   
                    if (line.Contains("D/W PANEL 666"))// || line.Contains("D/W PANEL 666") || line.Contains("D/W PANEL 888"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[3]);
                        txt_finalHeight.AppendText("." + pHeight);
                        string height = txt_finalHeight.Text;
                        Console.WriteLine("final height:" + height);
                        Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                        Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                        lbl_stiles.Text = height.TrimStart('.');
                        lbl_width.Text = lbl_stiles.Text; // height 
                        if (height.Length > 6)
                        {
                            string height1 = txt_finalHeight.Text;
                            string[] spaceSplit = height1.Split('.');
                            int one = Convert.ToInt32(spaceSplit[1]);
                            int two = Convert.ToInt32(spaceSplit[2]);
                            int finalHeight = one + two;
                            fileName.fileName.Setpanel1(one.ToString());
                            fileName.fileName.Setpanel2(two.ToString());
                            Console.WriteLine("final height:" + finalHeight);
                            Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                            Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                            lbl_stiles.Text = finalHeight.ToString(); // Height
                        }
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    }
                    if (line.Contains("SUPPLY") && (line.Contains("FIT")))
                    {
                        lbl_supply.Text = line.Trim().Trim(' ');
                    }
                    if (line.Contains("Ln POSTS") && !line.Contains("Ext"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[6]); // sometimes 6 sometimes 7
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text = outputPosts1;
                    }
                    if (line.Contains("PANEL")) // panel height
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[4]);
                        lbl_height.Text = pHeight;
                    }
                    if (line.Contains("Cn POSTS"))
                    {
                        string[] splitString = line.Split(' ');
                        string posts = (splitString[7]);
                        string outputPosts = posts.Trim().Trim('"');
                        string outputPosts1 = outputPosts.Trim().Trim(' ');
                        lbl_postsCN.Text = outputPosts1;
                    }
                    if (line.Contains("Price"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                }
                if (fileName.fileName.panel1 != null && fileName.fileName.panel1 == fileName.fileName.panel2)
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 6-6-6 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels "
                                         + " " + lbl_width.Text + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text);
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 != "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 6-6-6 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels. "
                                         + "The first " + fileName.fileName.panel1 + " mm high x 2506mm wide with 200 x 30mm mesh aperature and the second " + fileName.fileName.panel2 + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 == "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 6-6-6 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + "mm. high overall above ground level and comprising of "
                                       + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 1 D/Wire 8-6-8 mesh panel "
                                       + lbl_width.Text + "mm high x 2506mm wide with 200 x 30mm mesh aperature."
                                       + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                       + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                       + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
            }
        }
        private void nettedDBWire888()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("D/W PANEL 888"))// || line.Contains("D/W PANEL 666") || line.Contains("D/W PANEL 888"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pHeight = (numbers[4]);
                        txt_finalHeight.AppendText("." + pHeight);
                        string height = txt_finalHeight.Text;
                        Console.WriteLine("final height:" + height);
                        Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                        Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                        lbl_stiles.Text = height.TrimStart('.');
                        lbl_width.Text = lbl_stiles.Text; // height 
                        if (height.Length > 6)
                        {
                            string height1 = txt_finalHeight.Text;
                            string[] spaceSplit = height1.Split('.');
                            int one = Convert.ToInt32(spaceSplit[1]);
                            int two = Convert.ToInt32(spaceSplit[2]);
                            int finalHeight = one + two;
                            fileName.fileName.Setpanel1(one.ToString());
                            fileName.fileName.Setpanel2(two.ToString());
                            Console.WriteLine("final height:" + finalHeight);
                            Console.WriteLine("Panel 1:" + fileName.fileName.panel1);
                            Console.WriteLine("Panel 2:" + fileName.fileName.panel2);
                            lbl_stiles.Text = finalHeight.ToString(); // Height
                        }
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    } 
                    if (line.Contains("NET"))
                    {
                        lbl_1Item.Text = line;
                    }
                    if (line.Contains("SUPPLY") && (line.Contains("FIT")))
                    {
                        lbl_supply.Text = line.Trim().Trim(' ');
                    }
                    if (line.Contains("Ln POSTS") && !line.Contains("Ext"))
                    {
                        string[] splitString        = line.Split(' ');
                        string posts                = (splitString[6]); // sometimes 6 sometimes 7
                        string outputPosts          = posts.Trim().Trim('"');
                        string outputPosts1         = outputPosts.Trim().Trim(' ');
                        lbl_posts.Text              = outputPosts1;
                    }
                    if (line.Contains("PANEL")) // panel height
                    {
                        string[] numbers            = Regex.Split(line, @"\D+");
                        string pHeight              = (numbers[4]);
                        lbl_height.Text             = pHeight;
                    }
                    if (line.Contains("Cn POSTS"))
                    {
                        string[] splitString        = line.Split(' ');
                        string posts                = (splitString[7]);
                        string outputPosts          = posts.Trim().Trim('"');
                        string outputPosts1         = outputPosts.Trim().Trim(' ');
                        lbl_postsCN.Text            = outputPosts1;
                    }
                    if (line.Contains("Price"))
                    {
                        string[] numbers            = Regex.Split(line, @"\D+");
                        string pPerMetre            = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text          = pPerMetre;
                    }
                }
                if (fileName.fileName.panel1 != null && fileName.fileName.panel1 == fileName.fileName.panel2)
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-8-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels "
                                         + " " + lbl_width.Text + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts." 
                                         + lbl_1Item.Text + ""
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text);
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 != "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-8-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + " mm high overall above ground level and comprising of "
                                         + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 2 D/Wire 8-6-8 panels. "
                                         + "The first " + fileName.fileName.panel1 + " mm high x 2506mm wide with 200 x 30mm mesh aperature and the second " + fileName.fileName.panel2 + " mm high x 2506mm wide with 200 x 30mm mesh aperature. "
                                         + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                         + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                         + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
                else if (fileName.fileName.panel2 == "")
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " Rogers pro-sport 8-8-8 D/W rebound mesh panel fence system to stand " + lbl_stiles.Text + " " + "mm. high overall above ground level and comprising of "
                                       + lbl_posts.Text + " " + "long 'RHS' fence posts, set into and including concrete bases 600 x 600 x 900 mm deep at 2.523m centres clad with 1 D/Wire 8-6-8 mesh panel "
                                       + lbl_width.Text + "mm high x 2506mm wide with 200 x 30mm mesh aperature."
                                       + "Panel is made up of 2 - Number 8mm horizontal wires and 1-number 6mm vertical wire, vertical wires protrude 22mm above top horizontal wire."
                                       + "Panels fixed to posts with Hi Density PVC Cal-tech clips and M8 Pin head security bolts."
                                       + "\nFinish: Galvanised to BS EN ISO 1461: 1999 and polyester powder coated to BS6497 by an approved applicator. ");
                    txt_quotePara.AppendText(txt_quote.Text);
                    txt_quote1.Text = (lbl_netAmount.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    var quantity = Convert.ToDecimal(lbl_stiles.Text) / 100;
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    string q1Split = (quantity + " l/m.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
            }
        } 
        private void doubleWireGate()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("Ht"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string height = spaceSplit[11];
                        string outputHeight1 = height.Trim().Trim('.');
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("No.") && !(line.Contains("for")))
                    {
                        string[] spaceSplit = line.Split('.');
                        string supply = spaceSplit[0] + "." + spaceSplit[1];
                        string inputA = supply;
                        string patternA = @"([^\w]*DB[^\w]*)+|[|\\^&\r\n]+";
                        string replacementA = " ";
                        Regex rgxA = new Regex(patternA);
                        string resultA = rgxA.Replace(inputA, replacementA);
                        Console.WriteLine("Number:" + resultA);
                        resultA.Replace("  ", string.Empty);
                        
                        fileName.fileName.SetdbgNumber(resultA);
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " "+ and +" " + fit;
                    }
                    if (line.Contains("Clear"))
                    {

                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_numberOf.Text = numbers[1].ToString();
                        lbl_width.Text = numbers[2].ToString();
                        lbl_height.Text = numbers[3].ToString();
                    }
                    if (line.Contains("STILES"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_stiles.Text = numbers[2].ToString() + "x" + numbers[3].ToString();
                    }
                    if (line.Contains("GATE POSTS"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_gatePosts.Text = numbers[1].ToString() + "Nr" + numbers[2].ToString() + "x" + numbers[3].ToString() + "x" + numbers[4].ToString() + "x" + numbers[5].ToString() + "mm";
                    }
                    if (line.Contains("Price Supply"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                    if (line.Contains("Finish"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        if (numbers[1] == "6005")
                        {
                            lbl_postFinish.Text = "6005 Green";
                        }
                        else if (numbers[1] == "9005")
                        {
                            lbl_postFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_postFinish.Text = "Custom colour";
                        }
                        if (numbers[2] == "6005")
                        {
                            lbl_gateFinish.Text = "6005 Green";
                        }
                        else if (numbers[2] == "9005")
                        {
                            lbl_gateFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_gateFinish.Text = "Custom colour";
                        }
                    }
                }
                    if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" ") &&
                        lbl_width.Text != null && lbl_width.Text != (" ") && lbl_stiles.Text != null && lbl_stiles.Text != (" ") && lbl_postFinish.Text != null && lbl_postFinish.Text != (" ") &&
                         lbl_gatePosts.Text != null && lbl_gatePosts.Text != (" ") && lbl_gateFinish.Text != null && lbl_gateFinish.Text != (" "))
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Double Leaf Vehicular Gate:" + "" + lbl_width.Text + "mm wide x " + lbl_height.Text + "mm high comprising " + " "
                       + lbl_stiles.Text + "" + "SHS framing, hot dip galvanized and powder coated RAL Colour " + " " + lbl_postFinish.Text + "" + ", clad with 868 mesh hung on " + " "
                       + lbl_gatePosts.Text + " " + "long posts complete with all fixings galvanised and PPC RAL " + " " + lbl_gateFinish.Text);
                        txt_quotePara.AppendText(txt_quote.Text);
                        string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                        string[] numbers2 = Regex.Split(fileName.fileName.dbgNumber, @"\D+");
                        var quantity = Convert.ToDouble(numbers2[1]);
                        var netAmount = Convert.ToDouble(numbers[1] + "." + numbers[2]);
                        double unitPrice1 = netAmount / quantity;
                        fileName.fileName.dbgNumber.Trim();
                        var unitPrice = unitPrice1;
                        Console.WriteLine("dgbnumber: " + fileName.fileName.dbgNumber);
                        string q1Split = (numbers2[1] + "Nr.");
                        txt_quotePara1.AppendText(q1Split);
                        string q2 = unitPrice.ToString("C2");
                        txt_quotePara2.AppendText(q2);
                        string q3 = netAmount.ToString("C2");
                        txt_quotePara3.AppendText(q3);
                    }
                } 
                
            }
        private void ClassicSinGate()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {

                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("No.") && !(line.Contains("for")))
                    
                    {       
                        string[] slashSplit = line.Split('/');
                        string supply = slashSplit[0];
                        string[] spaceSplit = supply.Split('.');
                        string No = spaceSplit[1];
             
                       
                        string finalNo = "No." + No;
                        Console.WriteLine("No...:" + No);
                        string inputA = finalNo;
                        string patternA = @"([^\w]*SIN H[^\w]*)+|[|\\^&\r\n]+";
                        string replacementA = " ";
                        Regex rgxA = new Regex(patternA);
                        string resultA = rgxA.Replace(inputA, replacementA);
                        Console.WriteLine("Number:" + resultA);
                        resultA.Replace("  ", string.Empty);
                        fileName.fileName.SetdbgNumber(resultA);
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    }
                    if (line.Contains("Clear"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_numberOf.Text = numbers[1].ToString();
                        lbl_width.Text = numbers[2].ToString();
                        lbl_height.Text = numbers[3].ToString();
                    }
                    if (line.Contains("STILES"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_stiles.Text = numbers[2].ToString() + "x" + numbers[3].ToString();
                    }
                    if (line.Contains("GATE POSTS"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_gatePosts.Text = numbers[1].ToString() + "Nr" + numbers[2].ToString() + "x" + numbers[3].ToString() + "x" + numbers[4].ToString() + "x" + numbers[5].ToString() + "mm";
                    }
                    if (line.Contains("Price Supply"))
                    {
                      
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                    if (line.Contains("Finish"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        if (numbers[1] == "6005")
                        {
                            lbl_postFinish.Text = "6005 Green";
                        }
                        else if (numbers[1] == "9005")
                        {
                            lbl_postFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_postFinish.Text = "CUSTOM COLOUR";
                        }
                        if (numbers[2] == "6005")
                        {
                            lbl_gateFinish.Text = "6005 Green";
                        }
                        else if (numbers[2] == "9005")
                        {
                            lbl_gateFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_gateFinish.Text = "CUSTOM COLOUR";
                        }
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" ") &&
                   lbl_width.Text != null && lbl_width.Text != (" ") && lbl_stiles.Text != null && lbl_stiles.Text != (" ") && lbl_postFinish.Text != null && lbl_postFinish.Text != (" ") &&
                    lbl_gatePosts.Text != null && lbl_gatePosts.Text != (" ") && lbl_gateFinish.Text != null && lbl_gateFinish.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std - Single Leaf Gate to stand " + "" + lbl_height.Text + "mm  high above ground level, clear opening 1000m, comprising gate posts " + " "
                   + lbl_gatePosts.Text + "" + " RHS set into concrete bases 600 x 600 x 750mm deep, gate frame 50 x 50mm RHS having panels fixed with Cal-tech Clips to gate frame. Gate complete with Rogers Fencing Supplies adjustable hinges, drop bolt and ground sockets, Rogers Fencing Supplies latch incorporating slipbolt for client's padlock" +
                   " \n Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator. Colour RAL " + " " + lbl_gateFinish.Text);
                    txt_quotePara.AppendText(txt_quote.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    string[] numbers2 = Regex.Split(fileName.fileName.dbgNumber, @"\D+");
                    var quantity = Convert.ToDecimal(numbers2[1]);
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    fileName.fileName.dbgNumber.Trim();
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    Console.WriteLine("dgbnumber: " + fileName.fileName.dbgNumber);
                    string q1Split = (numbers2[1] + "Nr.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
            }
        }
        private void ClassicDBGate()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {

                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("No.") && !(line.Contains("for")))
                    {
                        string[] spaceSplit = line.Split('.');
                        string supply = spaceSplit[0] + "." + spaceSplit[1];
                        string inputA = supply;
                        string patternA = @"([^\w]*DB[^\w]*)+|[|\\^&\r\n]+";
                        string replacementA = " ";
                        Regex rgxA = new Regex(patternA);
                        string resultA = rgxA.Replace(inputA, replacementA);
                        Console.WriteLine("Number:" + resultA);
                        resultA.Replace("  ", string.Empty);
                        
                        fileName.fileName.SetdbgNumber(resultA);
                    
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    }
                    if (line.Contains("Clear"))
                    {

                        string[] numbers =  Regex.Split(line, @"\D+");
                        lbl_numberOf.Text = numbers[1].ToString();
                        lbl_width.Text =    numbers[2].ToString();
                        lbl_height.Text =   numbers[3].ToString();
                    }
                    if (line.Contains("STILES"))
                    {
                        string[] numbers =  Regex.Split(line, @"\D+");
                        lbl_stiles.Text =   numbers[2].ToString() + "x" + numbers[3].ToString();
                    }
                    if (line.Contains("GATE POSTS"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_gatePosts.Text = numbers[1].ToString() + "Nr" + numbers[2].ToString() + "x" + numbers[3].ToString() + "x" + numbers[4].ToString() + "x" + numbers[5].ToString() + "mm";
                    }
                    if (line.Contains("Price Supply"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                    if (line.Contains("Finish"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        if (numbers[1] == "6005")
                        {
                            lbl_postFinish.Text = "6005 Green";
                        }
                        else if (numbers[1] == "9005")
                        {
                            lbl_postFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_postFinish.Text = "UNKNOWN COLOUR";
                        }
                        if (numbers[2] == "6005")
                        {
                            lbl_gateFinish.Text = "6005 Green";
                        }
                        else if (numbers[2] == "9005")
                        {
                            lbl_gateFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_gateFinish.Text = "UNKNOWN COLOUR";
                        }
                    }
                }
                if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" ") &&
                   lbl_width.Text != null && lbl_width.Text != (" ") && lbl_stiles.Text != null && lbl_stiles.Text != (" ") && lbl_postFinish.Text != null && lbl_postFinish.Text != (" ") &&
                    lbl_gatePosts.Text != null && lbl_gatePosts.Text != (" ") && lbl_gateFinish.Text != null && lbl_gateFinish.Text != (" "))
                {
                    txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Classic Std - Double Leaf Gate to stand " + "" + lbl_height.Text + "  mm high above ground level, clear opening 4000mm, comprising gate posts " + " "
                   + lbl_gatePosts.Text + "" + " RHS, set into concrete bases 600 x 600 x 600mm deep, gate frame 50x50x3 having panels fixed with Cal-tech Clips to gate frame. Gate complete with Rogers Fencing Supplies adjustable hinges, drop bolt and ground sockets, Rogers Fencing Supplies latch incorporating slipbolt for clients padlock." +
                   " \n Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator. Colour RAL " + " " + lbl_gateFinish.Text);
                    txt_quotePara.AppendText(txt_quote.Text);
                    string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                    string[] numbers2 = Regex.Split(fileName.fileName.dbgNumber, @"\D+");
                    var quantity = Convert.ToDecimal(numbers2[1]);
                    var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                    decimal unitPrice1 = netAmount / quantity;
                    fileName.fileName.dbgNumber.Trim();
                    decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                    Console.WriteLine("dgbnumber: " + fileName.fileName.dbgNumber);
                    string q1Split = (numbers2[1] + "Nr.");
                    txt_quotePara1.AppendText(q1Split);
                    string q2 = unitPrice.ToString("C2");
                    txt_quotePara2.AppendText(q2);
                    string q3 = netAmount.ToString("C2");
                    txt_quotePara3.AppendText(q3);
                }
            }
        }
        private void sinDWGate()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                { 
                     if (line.Contains("Ht"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string outputHeight1 = numbers[3];
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("No.") && !(line.Contains("for")))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string resultA = numbers[1];
                        Console.WriteLine("No for sin sw gate:" + resultA);
                        fileName.fileName.SetdbgNumber(resultA);
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " "+ and +" " + fit;
                    }
                    if (line.Contains("Clear"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_numberOf.Text = numbers[1].ToString();
                        lbl_width.Text = numbers[2].ToString();
                        lbl_height.Text = numbers[3].ToString();
                    }
                    if (line.Contains("STILES"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_stiles.Text = numbers[2].ToString() + "x" + numbers[3].ToString();
                    }
                    if (line.Contains("GATE POSTS"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        lbl_gatePosts.Text = numbers[1].ToString() + "Nr" + numbers[2].ToString() + "x" + numbers[3].ToString() + "x" + numbers[4].ToString() + "x" + numbers[5].ToString() + "mm";
                    }
                    if (line.Contains("Price Supply"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string pPerMetre = ("£:" + numbers[1] + "." + numbers[2]);
                        lbl_netAmount.Text = pPerMetre;
                    }
                    if (line.Contains("VAT"))
                    {
                        string[] splitString = line.Split('£');
                        string pPerMetre = (splitString[0]);
                        string outputPPM = pPerMetre.Trim().Trim(' ');
                        lbl_total.Text = outputPPM;
                    }
                    if (line.Contains("Finish"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        if (numbers[1] == "6005")
                        {
                            lbl_postFinish.Text = "6005 Green";
                        }
                        else if (numbers[1] == "9005")
                        {
                            lbl_postFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_postFinish.Text = "Custom colour";
                        }
                        if (numbers[2] == "6005")
                        {
                            lbl_gateFinish.Text = "6005 Green";
                        }
                        else if (numbers[2] == "9005")
                        {
                            lbl_gateFinish.Text = "9005 Black";
                        }
                        else
                        {
                            lbl_gateFinish.Text = "Custom colour";
                        }
                    }
                }
                    if (lbl_supply.Text != null && lbl_supply.Text != (" ") && lbl_height.Text != null && lbl_height.Text != (" ") && lbl_posts.Text != null && lbl_posts.Text != (" ") &&
                        lbl_width.Text != null && lbl_width.Text != (" ") && lbl_stiles.Text != null && lbl_stiles.Text != (" ") && lbl_postFinish.Text != null && lbl_postFinish.Text != (" ") &&
                         lbl_gatePosts.Text != null && lbl_gatePosts.Text != (" ") && lbl_gateFinish.Text != null && lbl_gateFinish.Text != (" "))
                    {
                        txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Single Leaf Vehicular Gate:" + "" + lbl_width.Text + "mm wide x " + lbl_height.Text + "mm high comprising " + " "
                       + lbl_stiles.Text + "" + "SHS framing, hot dip galvanized and powder coated RAL Colour " + " " + lbl_postFinish.Text + "" + ", clad with 868 mesh hung on " + " "
                       + lbl_gatePosts.Text + " " + "long posts complete with all fixings galvanised and PPC RAL " + " " + lbl_gateFinish.Text);
                        txt_quotePara.AppendText(txt_quote.Text);
                        string[] numbers = Regex.Split(lbl_netAmount.Text, @"\D+");
                        string[] numbers2 = Regex.Split(fileName.fileName.dbgNumber, @"\D+");
                        var quantity = Convert.ToDecimal(fileName.fileName.dbgNumber);
                        var netAmount = Convert.ToDecimal(numbers[1] + "." + numbers[2]);
                        decimal unitPrice1 = netAmount / quantity;
                        fileName.fileName.dbgNumber.Trim();
                        decimal unitPrice = decimal.Round(unitPrice1, 2, MidpointRounding.AwayFromZero);
                        Console.WriteLine("dgbnumber: " + fileName.fileName.dbgNumber);
                        string q1Split = (numbers2[0] + "Nr.");
                        txt_quotePara1.AppendText(q1Split);
                        string q2 = unitPrice.ToString("C2");
                        txt_quotePara2.AppendText(q2);
                        string q3 = netAmount.ToString("C2");
                        txt_quotePara3.AppendText(q3);
                    }
                }            
        }
        private void sinDWWisaGate()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {
                    if (line.Contains("Ht"))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string outputHeight1 = numbers[3];
                        lbl_height.Text = outputHeight1;
                    }
                    if (line.Contains("SUPPLY") && !(line.Contains("FIT")))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string outputSupply = supply.Trim().Trim('.');
                        lbl_supply.Text = outputSupply;
                    }
                    if (line.Contains("No.") && !(line.Contains("for")))
                    {
                        string[] numbers = Regex.Split(line, @"\D+");
                        string resultA = numbers[1];
                        Console.WriteLine("No for sin sw gate:" + resultA);
                        fileName.fileName.SetdbgNumber(resultA);
                    }
                    if (line.Contains("SUPPLY"))
                    {
                        string[] spaceSplit = line.Split(' ');
                        string supply = spaceSplit[6];
                        string and = spaceSplit[7];
                        string fit = spaceSplit[8];
                        string outputSupply = supply.Trim().Trim(' ');
                        lbl_supply.Text = outputSupply + " " + and + " " + fit;
                    }
                }

            }
        }   
        private void DWBallStop()
        {
            string line;
            FileInfo selectedFile = new FileInfo(fileName.fileName.currentFile);
            using (StreamReader sR = selectedFile.OpenText())
            {
                while ((line = sR.ReadLine()) != null)
                {

                }

            }
        }   
        private void  doubleWireGateCustom() 
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Double Leaf Vehicular Gate:" + "" + lbl_width.Text + "mm wide x " + lbl_height.Text + "mm high comprising " 
                             + lbl_stiles.Text + "" + " SHS framing, hot dip galvanized and powder coated RAL Colour " + " " + lbl_postFinish.Text + "" + ", clad with 868 mesh hung on " 
                             + lbl_gatePosts.Text + " " + "long posts complete with all fixings galvanised and PPC RAL " + " " + lbl_gateFinish.Text);
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine );
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
          
        }
        private void classicFenceCustom()
        {
            txt_quote.Text =     (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Std Fencing System to stand  " + lbl_height.Text + "mm " + "above ground level,"
                                 + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                                 + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                                 + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                                 + " having 4 No reinforcing crimps 200 x 50 mm,"
                                 + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                                 + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                                 + " Caltec clips + internal nut inserts."
                                 + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                                 );
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void classicFenceHeavyCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "RFS Classic Heavy-Duty Fencing System to stand " + lbl_height.Text + " " + "above ground level,"
                           + "comprising of " + " " + lbl_posts.Text + " " + "RHS Posts (resistance to bending 8.30cm3) having PVC insert cap,"
                           + " Posts set into concrete foundations 300 x 300 x 700mm deep at 3025 mm centres."
                           + " Clad with RFS Classic H-D panels " + " " + lbl_height.Text + " " + "high x 3005 mm wide, each panel"
                           + " having 4 No reinforcing crimps 200 x 50 mm,"
                           + " incorporating 5.0 mm diameter horizontal wires, 5.0 mm vertical wires,"
                           + " secured to post with 7 No M8 Anti-Vandal tamper resistant bolts and"
                           + " Caltec clips + internal nut inserts."
                           + "\nFinish: Galvanized and polyester powder coated to BS EN 13438-2005 by approved Powder Coating Company"
                           );
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void spectFenceCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Spectator fencing system to stand " + "" + lbl_height.Text + "" + " high above ground level "
                    + "on " + "" + lbl_posts.Text + " " + " posts set in concrete bases 350x350x500mm deep completely filled with 20N concrete at centres no greater than 2500mm."
                    + " Posts to be fitted with M8 threaded inserts every 400mm tensioned only by hydraulic tool. Posts to be fitted with 60x60mm RFS Top Rail angle bracket."
                    + "Finish: Galvanized to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved RFS applicator.");
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        } 
        private void prisonMeshCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies 358 High Security fencing system to stand " + "" + lbl_height.Text + "" + " high above ground level "
                       + "on steel posts" + "" + lbl_posts.Text + "" + " mm 3770mm long, set into and including concrete bases 450 x 450 x 900mm deep at 2440mm centres."
                       + "Rogers Fencing Supplies 358 High Security mesh panels 2515mm wide x 3000mm high, 76.2 x 12.7mm mesh size, 4.00mm diameter,"
                       + "overlapped at posts and secured to same using flat clamp bar 3000mm long, post and clamp bar holed for M8 long spun galvanised"
                       + " cup square bolts and tamper-proof permacone fixings. "
                       + "Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator.");
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void doubleWireFenceCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Double Wire 868 Fencing System to stand " + lbl_height.Text + " " + "  high above ground level comprising of " + lbl_posts.Text + " " + " RHS fence posts,"
                            + " set into and including concrete bases 450 x 450 x 800mm deep at 2.523m centres. Rogers Fencing Supplies Double Wire 868 mesh panels" + lbl_height.Text + " " + " high x 2506mm long,"
                            + " 200 x 50mm mesh size with double twinned 8mm diameter horizontal wire and 6mm diameter vertical wire, vertical wires protrude 22mm above top horizontal wire."
                            + " Panels fixed to posts with Cal-tech clips."
                            + " Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator.");
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        } 
        private void singleDoubleWireGateCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Single Leaf Vehicular Gate:" + "" + lbl_width.Text + "mm wide x " + lbl_height.Text + "mm high comprising " + " "
                       + lbl_stiles.Text + "" + "SHS framing, hot dip galvanized and powder coated RAL Colour " + " " + lbl_postFinish.Text + "" + ", clad with 868 mesh hung on " + " "
                       + lbl_gatePosts.Text + " " + "long posts complete with all fixings galvanised and PPC RAL " + " " + lbl_gateFinish.Text);
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void singleClassicGateCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Classic Std Fencing System to stand " + "" + lbl_height.Text + "mm  high above ground level, clear opening 1000m, comprising gate posts " + " "
                  + lbl_gatePosts.Text + "" + " RHS set into concrete bases 600 x 600 x 750mm deep, gate frame 50 x 50mm RHS having panels fixed with Cal-tech Clips to gate frame. Gate complete with Rogers Fencing Supplies adjustable hinges, drop bolt and ground sockets, Rogers Fencing Supplies latch incorporating slipbolt for client's padlock" +
                  " \n Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator. Colour RAL " + " " + lbl_gateFinish.Text);
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void doubleClassicGateCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + "Rogers Fencing Supplies Classic Std - Double Leaf Gate to stand " + "" + lbl_height.Text + "  mm high above ground level, clear opening 4000mm, comprising gate posts " + " "
                  + lbl_gatePosts.Text + "" + " RHS, set into concrete bases 600 x 600 x 600mm deep, gate frame 50x50x3having panels fixed with Cal-tech Clips to gate frame. Gate complete with Rogers Fencing Supplies adjustable hinges, drop bolt and ground sockets, Rogers Fencing Supplies latch incorporating slipbolt for clients padlock." +
                  " \n Finish: Galvanised to BS EN ISO 1461:1999 and polyester powder coated to BS6497 by an approved applicator. Colour RAL " + " " + lbl_gateFinish.Text);
                   txt_quotePara.AppendText(txt_quote.Text);
                   string q1Split = (lbl_quantity.Text);// + "                " + lbl_unitPrice.Text + "                    " + lbl_netAmountCQ.Text);
                   string q2 = (lbl_unitPrice.Text);
                   string q3 = lbl_netAmountCQ.Text;
                   txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                   txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                   txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3 + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void palisadeFenceCustom()
        {
            txt_quote.Text = (Environment.NewLine + Environment.NewLine + lbl_supply.Text + " " + lbl_height.Text + "  mm high Palisade GP Spec Rails 45 x 45 x 6mm RSA, fish plated to posts."
                                            + "Pales D section pales having Triple Point Splayed 3.0mm thick, fixed to rails at 152mm centres"
                                            + "Posts 100 x 55 IPE set in concrete bases 450x450x750mm deep at centres no greater than 2750mm."
                                            + "Fixings Rails to be fixed to posts using fish plates and M12 x 40 Permacone bolts and saddle head nuts."
                                            + "Pales to be fixed to rails using M8 x 40 Saddle head and permacone bolted connections."
                                            + "\nFinish: Hot dipped Galvanised to BS EN ISO 1461: 1999 galvanised only");
            txt_quotePara.AppendText(txt_quote.Text);
            string q1Split = (lbl_quantity.Text);
            string q2 = (lbl_unitPrice.Text);
            string q3 = lbl_netAmountCQ.Text;
            txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + q1Split   + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + q2        + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + q3        + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }
        private void classicFenceCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Classic Fence", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Classic Fence", "2400");
            lbl_height.Text = height;
            var posts = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Size", "Classic Fence", "60x60x2");
            lbl_posts.Text = posts;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Classic Fence", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");          
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);    
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void doubleWireGateCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Double Wire Gate", "Supply Only");
            lbl_supply.Text = supply;
            var width = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Width", "Double Wire Gate", "4000");
            lbl_width.Text = width;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Height", "Double Wire Gate", "2400");
            lbl_height.Text = height;
            var stiles = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate stiles", "Double Wire Gate", "80x80x3x3150");
            lbl_stiles.Text = stiles;
            var postFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Finish", "Double Wire Gate", "6005 Green");
            lbl_postFinish.Text = postFinish;
            var gatePosts = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Posts", "Double Wire Gate", "150x150x6x3290");
            lbl_gatePosts.Text = gatePosts;
            var gateFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Finish", "Double Wire Gate", "6005 Green");
            lbl_gateFinish.Text = gateFinish;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Quantity", "Double Wire Gate", "1");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void spectFenceCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Spect Fence", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Spect Fence", "2400");
            lbl_height.Text = height;
            var posts = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Size", "Spect Fence", "60x60x2");
            lbl_posts.Text = posts;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Spect Fence", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        } 
        private void prisonMeshCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Prison Mesh Fence", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Prison Mesh Fence", "2400");
            lbl_height.Text = height;
            var posts = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Size", "Prison Mesh Fence", "60x60x2");
            lbl_posts.Text = posts;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Prison Mesh Fence", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void doubleWireFenceCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Double Wire Fence", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Double Wire Fence", "2400");
            lbl_height.Text = height;
            var posts = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Size", "Double Wire Fence", "60x60x2");
            lbl_posts.Text = posts;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Double Wire Fence", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void classicFenceHeavyCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Classic Heavy Fence", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Classic Heavy Fence", "2400");
            lbl_height.Text = height;
            var posts = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Size", "Classic Heavy Fence", "60x60x2");
            lbl_posts.Text = posts;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Classic Heavy Fence", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void singleDoubleWireGateCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Single Double Wire Gate", "Supply Only");
            lbl_supply.Text = supply;
            var width = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Width", "Single Double Wire Gate", "4000");
            lbl_width.Text = width;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Height", "Single Double Wire Gate", "2400");
            lbl_height.Text = height;
            var stiles = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate stiles", "Single Double Wire Gate", "80x80x3x3150");
            lbl_stiles.Text = stiles;
            var postFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Post Finish", "Single Double Wire Gate", "6005 Green");
            lbl_postFinish.Text = postFinish;
            var gatePosts = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Posts", "Single Double Wire Gate", "150x150x6x3290");
            lbl_gatePosts.Text = gatePosts;
            var gateFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Finish", "Single Double Wire Gate", "6005 Green");
            lbl_gateFinish.Text = gateFinish;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Quantity", "Single Double Wire Gate", "1");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void singleClassicGateCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Single Classic Gate", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Single Classic Gate", "2400");
            lbl_height.Text = height;
            var gatePosts = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Posts", "Single Classic Gate", "150x150x6x3290");
            lbl_gatePosts.Text = gatePosts;
            var gateFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Finish", "Single Classic Gate", "6005 Green");
            lbl_gateFinish.Text = gateFinish;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Single Classic Gate", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void doubleClassicGateCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Double Classic Gate", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Double Classic Gate", "2400");
            lbl_height.Text = height;
            var gatePosts = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Posts", "Double Classic Gate", "150x150x6x3290");
            lbl_gatePosts.Text = gatePosts;
            var gateFinish = Microsoft.VisualBasic.Interaction.InputBox("Enter Gate Finish", "Double Classic Gate", "6005 Green");
            lbl_gateFinish.Text = gateFinish;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Double Classic Gate", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2)+ Convert.ToDouble(total) ;
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void palisadeFenceCustomQs()
        {
            var supply = Microsoft.VisualBasic.Interaction.InputBox("Enter Supply Type", "Double Classic Gate", "Supply Only");
            lbl_supply.Text = supply;
            var height = Microsoft.VisualBasic.Interaction.InputBox("Enter Fence Height", "Double Classic Gate", "2400");
            lbl_height.Text = height;
            var quantity = Microsoft.VisualBasic.Interaction.InputBox("Enter Distance (in metres)", "Double Classic Gate", "100");
            lbl_quantity.Text = quantity;
            var netAmount = Microsoft.VisualBasic.Interaction.InputBox("Enter Price", "Classic Fence", "5000.00");
            var unitPrice1 = Convert.ToDouble(netAmount) / Convert.ToDouble(quantity);
            var netAmountFinal = netAmount.ToString();
            lbl_netAmountCQ.Text = netAmountFinal;
            var total = netAmountFinal;
            var total1 = Convert.ToDouble(fileName.fileName.QQtNA2) + Convert.ToDouble(total);
            fileName.fileName.SetQQtNA2(total1.ToString("#0.00"));
            var tva = total1 * .2;
            fileName.fileName.SetQQtVA2(tva.ToString("#0.00"));
            var finalTotal = tva + total1;
            fileName.fileName.SetQQt2(finalTotal.ToString("#0.00"));
            double QQTotalPrice = Convert.ToDouble(fileName.fileName.QQTotalPrice) + Convert.ToDouble(lbl_netAmountCQ.Text);
            fileName.fileName.SetQQTotalPrice(Convert.ToInt32(QQTotalPrice));
            lbl_unitPrice.Text = unitPrice1.ToString();
        }
        private void tbx_search_TextChanged(object sender, EventArgs e)
        {               
                    DirectoryInfo dinfoQF = new DirectoryInfo(@"K:/QUOTEfit/");
                    FileInfo[] FilesQF = dinfoQF.GetFiles("*.txt");
                    DirectoryInfo dinfoQS = new DirectoryInfo(@"K:/QUOTEsupply/");
                    FileInfo[] FilesQS = dinfoQS.GetFiles("*.txt");
                    listBox1.Items.Clear();
                    foreach (FileInfo file in FilesQF)
                    {
                        if (file.Name.ToUpper().Contains(txt_search.Text.ToUpper()))
                        {
                            listBox1.Items.Add(file.Name);
                        }
                    }
                    foreach (FileInfo file in FilesQS)
                    {
                        if (file.Name.ToUpper().Contains(txt_search.Text.ToUpper()))
                        {
                            listBox1.Items.Add(file.Name);
                        }
                    }
                    FileInfo filepdfPath = new FileInfo("pdfPath.txt");
                    using (StreamReader sr3 = filepdfPath.OpenText())
                    {
                        while (!sr3.EndOfStream)
                        {
                            var finalPS = sr3.ReadLine().Replace("\\", "/");
                            DirectoryInfo dinfoPDFS = new DirectoryInfo(finalPS);
                            FileInfo[] FilesPDFS = dinfoPDFS.GetFiles("*.pdf");
                            foreach (FileInfo file in FilesPDFS)
                            {
                                if (file.Name.ToUpper().Contains(txt_search.Text.ToUpper()))
                                {
                                    listBox1.Items.Add(file.Name);
                                }
                            }
                        }
                    }
        }
        private void btn_custom_Click(object sender, EventArgs e)
        {        
            var custname = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Name", "Customer Details", "");
            lbl_custName.Text = custname;
            var address = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Address", "Customer Details", "");
            lbl_address.Text = address;
            var town = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Town", "Customer Details", "");
            lbl_town.Text = town;
            var county = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer County", "Customer Details", "Co.");
            lbl_county.Text = county;
            var postcode = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Postcode", "Customer Details", "BT");
            lbl_postcode.Text = postcode;
            var telNo = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Tel. No.", "Customer Details", "");
            lbl_telNo.Text = telNo;
            var site = Microsoft.VisualBasic.Interaction.InputBox("Enter Site Name", "Site Details", "");
            lbl_site.Text = site;
            if (cb_doubleWireGate.Checked)
            {              
               doubleWireGateCustomQs();
               doubleWireGateCustom();
               if (txt_dwgAmount.Text != "" && Convert.ToInt32(txt_dwgAmount.Text) > 0)
               {
                   for (int i = 0; i < Convert.ToInt32(txt_dwgAmount.Text) - 1; i++)
                   {
                       doubleWireGateCustomQs();
                       doubleWireGateCustom();
                   }
               }
            } 
            if (cb_classicFence.Checked)
            {              
               classicFenceCustomQs();
               classicFenceCustom();
               if (txt_cfAmount.Text != "" && Convert.ToInt32(txt_cfAmount.Text) > 0)
                    {
                        for(int i = 0; i <Convert.ToInt32(txt_cfAmount.Text) - 1; i++)
                        {
                            classicFenceCustomQs();
                            classicFenceCustom();
                        }
                    }
            }
            if (cb_spectFence.Checked)
            {            
                spectFenceCustomQs();
                spectFenceCustom();
                if (txt_sfAmount.Text != "" && Convert.ToInt32(txt_sfAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_sfAmount.Text) - 1; i++)
                    {
                        spectFenceCustomQs();
                        spectFenceCustom(); 
                    }
                }
            }
            if (cb_prisonMesh.Checked)
            {           
                prisonMeshCustomQs();
                prisonMeshCustom();
                if (txt_pmAmount.Text != "" && Convert.ToInt32(txt_pmAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_pmAmount.Text) - 1; i++)
                    {
                        prisonMeshCustomQs();
                        prisonMeshCustom();
                    }
                }
            }
            if (cb_doubleWireFence.Checked)
            {            
                doubleWireFenceCustomQs();
                doubleWireFenceCustom();
                if (txt_dwfAmount.Text != "" && Convert.ToInt32(txt_dwfAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_dwfAmount.Text) - 1; i++)
                    {
                        doubleWireFenceCustomQs();
                        doubleWireFenceCustom();
                    }
                }
            }
            if (cb_classicFenceHeavy.Checked)
            {              
                classicFenceHeavyCustomQs();
                classicFenceHeavyCustom();
                if (txt_cfHeavyAmount.Text != "" && Convert.ToInt32(txt_cfHeavyAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_cfHeavyAmount.Text) - 1; i++)
                    {
                        classicFenceHeavyCustomQs();
                        classicFenceHeavyCustom();
                    }
                }
            }
            if (cb_singleDWGate.Checked)
            {
                singleDoubleWireGateCustomQs();
                singleDoubleWireGateCustom();
                if (txt_sdwgAmount.Text != "" && Convert.ToInt32(txt_sdwgAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_sdwgAmount.Text) - 1; i++)
                    {
                        singleDoubleWireGateCustomQs();
                        singleDoubleWireGateCustom();
                    }
                }
            }
            if (cb_singleClassicGate.Checked)
            {
                singleClassicGateCustomQs();
                singleClassicGateCustom();
                if (txt_scgAmount.Text != "" && Convert.ToInt32(txt_scgAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_scgAmount.Text) - 1; i++)
                    {
                        singleClassicGateCustomQs();
                        singleClassicGateCustom();
                    }
                }
            }
            if (cb_doubleClassicGate.Checked)
            {
                doubleClassicGateCustomQs();
                doubleClassicGateCustom();
                if (txt_dcgAmount.Text != "" && Convert.ToInt32(txt_dcgAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_dcgAmount.Text) - 1; i++)
                    {
                        doubleClassicGateCustomQs();
                        doubleClassicGateCustom();
                    }
                }
            }
            if (cb_palisadeFence.Checked)
            {
                palisadeFenceCustomQs();
                palisadeFenceCustom();
                if (txt_pfAmount.Text != "" && Convert.ToInt32(txt_pfAmount.Text) > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(txt_pfAmount.Text) - 1; i++)
                    {
                        palisadeFenceCustomQs();
                        palisadeFenceCustom();
                    }
                }
            } 
                button2_Click(sender, e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                iTextSharp.text.Font fdefault = FontFactory.GetFont("HELVETICA", 8, BaseColor.BLACK);
                iTextSharp.text.Font fdetails = FontFactory.GetFont("HELVETICA", 9, BaseColor.BLACK);
                iTextSharp.text.Font fBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////            setting up the doc and table 
                Document myDocument = new Document();
                PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                myDocument.Open();
                PdfPTable table             = new PdfPTable(2);                                                                                 // create table with 2 columns 
                PdfPTable tableFooter       = new PdfPTable(2);
                PdfPTable tableDetails      = new PdfPTable(3);
                PdfPTable tableQuote        = new PdfPTable(5);
                PdfPTable tableTitle        = new PdfPTable(5);
                PdfPTable tableCarriage     = new PdfPTable(5);
                ////////////////////////////////////////////////////////////////////////////////////////////////////////            creating header cell
                Paragraph header = new Paragraph("Description", fBold);
                header.Alignment = Element.ALIGN_CENTER;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    FileInfo file2 = new FileInfo("Header.txt");
                    using (StreamReader sr2 = file2.OpenText())
                    {
                        while (!sr2.EndOfStream)
                        {
                            var image = iTextSharp.text.Image.GetInstance(sr2.ReadLine());
                            var imageCell = new PdfPCell(image);
                            image.ScaleToFit(300f, 300f);
                            image.SetAbsolutePosition(60, 700);
                            PdfPCell imageHeader = new PdfPCell(image);
                            imageHeader.Colspan = 2;
                            imageHeader.HorizontalAlignment = 1;
                            imageHeader.Border = 0;
                            table.DefaultCell.Border = 0;
                            table.AddCell(imageHeader);
                            Phrase phrase = new Phrase();
                            Phrase sdetails = new Phrase();
                            phrase.Add(
                                new Chunk(Environment.NewLine + lbl_custName.Text + Environment.NewLine + lbl_address.Text + Environment.NewLine + lbl_town.Text + Environment.NewLine + lbl_county.Text + Environment.NewLine + lbl_postcode.Text + Environment.NewLine + lbl_telNo.Text + Environment.NewLine + Environment.NewLine, fdetails)
                              );
                            String test = DateTime.Now.ToString("dd.MM.yyy");
                            sdetails.Add(new Chunk(Environment.NewLine + "Quotation : "  + Environment.NewLine + Environment.NewLine + lbl_site.Text + Environment.NewLine + Environment.NewLine + "Date: " + test, fdetails));
                            int[] intTblWidth = { 70, 2, 28 };
                            tableDetails.SetWidths(intTblWidth);
                            tableDetails.HorizontalAlignment = Element.ALIGN_LEFT;
                            tableDetails.WidthPercentage = 100;
                            tableDetails.DefaultCell.Border = 0;
                            tableDetails.DefaultCell.SetLeading(3, 1);
                            tableDetails.AddCell(phrase);
                            tableDetails.AddCell("");
                            tableDetails.AddCell(sdetails);
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////
                            FileInfo file1 = new FileInfo("Footer.txt");
                            using (StreamReader sr1 = file1.OpenText())
                            {
                                while (!sr1.EndOfStream)
                                {
                                    var imageFooter = iTextSharp.text.Image.GetInstance(sr1.ReadLine());
                                    var imageCellFooter = new PdfPCell(imageFooter);
                                    imageFooter.ScaleToFit(300f, 300f);
                                    PdfPCell imageFooterCell = new PdfPCell(imageFooter);
                                    imageFooterCell.Colspan = 2;
                                    imageFooterCell.HorizontalAlignment = 1;
                                    imageFooterCell.Border = 0;
                                    imageFooter.SetAbsolutePosition(80, 0);
                                    tableFooter.DefaultCell.Border = 0;
                                    tableFooter.AddCell(imageFooterCell);
                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            adding paragraphs 
                                    txt_quote.Text = txt_quotePara.Text;
                                    Paragraph paraQuote = new Paragraph(Environment.NewLine + txt_quote.Text, fdefault);
                                    txt_quote1.Text = txt_quotePara1.Text;
                                    Paragraph paraQuote1 = new Paragraph(Environment.NewLine + txt_quote1.Text + Environment.NewLine, fdefault);
                                    txt_quote2.Text = txt_quotePara2.Text;
                                    Paragraph paraQuote2 = new Paragraph(Environment.NewLine + txt_quotePara2.Text + Environment.NewLine, fdefault);
                                    txt_quote3.Text = txt_quotePara3.Text;
                                    Paragraph paraQuote3 = new Paragraph(Environment.NewLine + txt_quotePara3.Text + Environment.NewLine, fdefault);
                                    Paragraph paraExplain = new Paragraph(txt_explain.Text);                                  
                                    Paragraph paraPrices = new Paragraph(Environment.NewLine + Environment.NewLine + Environment.NewLine + lbl_total.Text); //+ Environment.NewLine + lbl_VAT.Text + Environment.NewLine + lbl_total.Text + Environment.NewLine);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    var paraFinalPrices = new Paragraph(                                                             "£" + fileName.fileName.QQTotalPrice
                                                                                                            + Environment.NewLine +  "£" + fileName.fileName.QQtVA2
                                                                                                            + Environment.NewLine +  "£" + fileName.fileName.QQt2, fBold);
                                    var paraFPText = new Paragraph(                                                                  "Total Net Amount"
                                                                                                            + Environment.NewLine +  "VAT @ 20 %"
                                                                                                            + Environment.NewLine +  "Total", fBold);                                   
                                    PdfPTable tableFP = new PdfPTable(4);
                                    int[] intTblFPWidth = { 45, 1, 32, 22 };
                                    tableFP.SetWidths(intTblFPWidth);
                                    tableFP.WidthPercentage = 100;
                                    tableFP.DefaultCell.Border = 0;
                                    tableFP.DefaultCell.SetLeading(3, 1);
                                    tableFP.AddCell("");
                                    tableFP.AddCell("");
                                    PdfPCell fpt = new PdfPCell(paraFPText) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                                    fpt.Border = 0; fpt.SetLeading(3, 1);
                                    tableFP.AddCell(fpt);
                                    PdfPCell fp = new PdfPCell(paraFinalPrices) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    fp.Border = 0; fp.SetLeading(3, 1);
                                    tableFP.AddCell(fp);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    int[] intTblQuoteWidth = { 64, 1, 11, 11, 11 };
                                    tableQuote.SetWidths(intTblQuoteWidth);
                                    tableQuote.WidthPercentage = 100;
                                    tableQuote.DefaultCell.Border = 0;
                                    tableQuote.DefaultCell.SetLeading(3, 1);
                                    PdfPCell q = new PdfPCell(paraQuote) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED };
                                    q.Border = 0; q.SetLeading(3, 1);
                                    tableQuote.AddCell(q);
                                    tableQuote.AddCell("");
                                    PdfPCell q1 = new PdfPCell(paraQuote1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    q1.Border = 0; q1.SetLeading(3, 1);
                                    tableQuote.AddCell(q1);
                                    PdfPCell q2 = new PdfPCell(paraQuote2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    q2.Border = 0; q2.SetLeading(3, 1);
                                    tableQuote.AddCell(q2);
                                    PdfPCell q3 = new PdfPCell(paraQuote3) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                    q3.Border = 0; q3.SetLeading(3, 1);
                                    tableQuote.AddCell(q3);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    Paragraph paraDescription = new Paragraph("Description" , fBold);
                                    Paragraph paraTitle = new Paragraph("Quantity"          , fBold);
                                    Paragraph paraTitle1 = new Paragraph("Unit Price"       , fBold);
                                    Paragraph paraTitle2 = new Paragraph("Net Amount"       , fBold);
                                    int[] intTbltitleWidth = { 64, 1, 11, 11, 11 };
                                    tableTitle.SetWidths(intTbltitleWidth);
                                    tableTitle.WidthPercentage = 100;
                                    tableTitle.DefaultCell.Border = 0;
                                    tableTitle.DefaultCell.SetLeading(3, 1);
                                    tableTitle.AddCell(paraDescription);
                                    tableTitle.AddCell("");
                                    PdfPCell t = new PdfPCell(paraTitle)    {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    t.Border = 0; t.SetLeading(3, 1);
                                    tableTitle.AddCell(t);
                                    PdfPCell t1 = new PdfPCell(paraTitle1)  {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    t1.Border = 0; t1.SetLeading(3, 1);
                                    tableTitle.AddCell(t1);
                                    PdfPCell t2 = new PdfPCell(paraTitle2)  {HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT};
                                    t2.Border = 0; t2.SetLeading(3, 1);
                                    tableTitle.AddCell(t2);
                                    ///////////////////////////////////////////////////////////////////////////////////////////////////////             
                                    myDocument.Add(table);
                                    myDocument.Add(tableDetails);
                                    myDocument.Add(tableTitle);
                                    myDocument.Add(tableQuote);
                                    myDocument.Add(tableFP);
                                    myDocument.Add(imageFooter);
                                    myDocument.Close();
                                }
                            }
                        }
                    }
                    byte[] content = myMemoryStream.ToArray();
                    string output = lbl_listBox.Text;
                    string listbox = output.Remove(output.Length - 4);
                    DateTime date = DateTime.Parse(System.DateTime.Now.ToString());
                    string dateForPDF = date.ToString("HHmmss");
                    FileInfo filepdfPath = new FileInfo("pdfPath.txt");
                    using (StreamReader sr3 = filepdfPath.OpenText())
                    {
                        while (!sr3.EndOfStream)
                        {
                            var pathString = Path.Combine(sr3.ReadLine() + lbl_custName.Text);
                            var finalPS = pathString.Replace("\\", "/");
                            System.IO.Directory.CreateDirectory(finalPS);
                            using (FileStream fs = File.Create(finalPS + "/" + "/" + lbl_custName.Text + ".pdf"))
                            {
                                fs.Write(content, 0, (int)content.Length); // writing out pdf from the content array
                                DialogResult saved = MessageBox.Show("Saved Successfully!", "Quote Saved", MessageBoxButtons.OK);//Dialog box to show file has been saved 
                            }
                            this.Controls.Clear();
                            this.InitializeComponent();
                            Form1_Load(sender, e);
                        }
                    }
                                } 

        }
        private void changeHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.InitialDirectory = "c:\\";
            oFD.Filter = "Image files (*.jpg)|*.jpg|All files (*.png)|*.jpg";
            oFD.FilterIndex = 1;
            oFD.RestoreDirectory = true;
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                string headerFileName = Path.GetFileName(oFD.FileName);
                string headerDir = Path.GetDirectoryName(oFD.FileName);
                fileName.fileName.Setheader(headerDir + "\\" + headerFileName);
                Console.WriteLine(fileName.fileName.header);
                using (StreamWriter sw1 = new StreamWriter("Header.txt"))
                {
                    sw1.WriteLine(fileName.fileName.header);
                }
            }
        }
        private void changeFooterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.InitialDirectory = "c:\\";
            oFD.Filter = "Image files (*.jpg)|*.jpg|All files (*.png)|*.jpg";
            oFD.FilterIndex = 1;
            oFD.RestoreDirectory = true;
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                string headerFileName = Path.GetFileName(oFD.FileName);
                string headerDir = Path.GetDirectoryName(oFD.FileName);
                fileName.fileName.Setfooter(headerDir + "\\" + headerFileName);
                using (StreamWriter sw1 = new StreamWriter("Footer.txt"))
                {
                    sw1.WriteLine(fileName.fileName.footer);
                }
            }
        }
        private void changePDFSaveLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    using (StreamWriter sw1 = new StreamWriter("pdfPath.txt"))
                    {
                        var pdfPath = fbd.SelectedPath.Replace("\\","/");
                        fileName.fileName.SetpdfPath(pdfPath +"/");
                        sw1.WriteLine(fileName.fileName.pdfPath);                        
                    }
                }
            }
        }
        private void changeCutlistSaveLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    using (StreamWriter sw1 = new StreamWriter("clPath.txt"))
                    {
                        var pdfPath = fbd.SelectedPath.Replace("\\", "/");
                        fileName.fileName.SetpdfPath(pdfPath + "/");
                        sw1.WriteLine(fileName.fileName.pdfPath);
                    }
                }
            }
        }
        private void viewQuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FileInfo filepdfPath = new FileInfo("pdfPath.txt");
           using (StreamReader sr3 = filepdfPath.OpenText())
           {
               while (!sr3.EndOfStream)
               {
                   var pathString = Path.Combine(sr3.ReadLine());
                   var finalPS = pathString.Replace("\\", "/");
                   if (Directory.Exists(finalPS))
                   {
                       System.Diagnostics.Process.Start(finalPS);
                   }
                   else
                   {
                       Directory.CreateDirectory(finalPS);
                   }
               }            
           }
        }
        private void viewCutlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo filepdfPath = new FileInfo("clPath.txt");
            using (StreamReader sr3 = filepdfPath.OpenText())
            {
                while (!sr3.EndOfStream)
                {
                    var pathString = Path.Combine(sr3.ReadLine());
                    var finalPS = pathString.Replace("\\", "/");
                    System.Diagnostics.Process.Start(finalPS);
                    this.CenterToScreen();
                }
            }
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Form1_Load(sender, e);
        }
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("QuoteHelp.txt");
        }
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:/fence/warnxxx.exe");
        }
        private void viewQuoteBreakdownFromWilliamsProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:/fence/viewoldq.exe");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void menuStripToolTips()
        {
            menuStrip1.ShowItemToolTips                                             =   true;
            viewQuotesToolStripMenuItem.ToolTipText                                 =   "View completed quotes (opens dialog browser)";
            viewCutlistToolStripMenuItem.ToolTipText                                =   "View completed cutlists (opens dialog browser)";
            refreshToolStripMenuItem.ToolTipText                                    =   "Reloads the program";
            changeFooterToolStripMenuItem.ToolTipText                               =   "Change the Footer of the PDF(s) produced";
            changeHeaderToolStripMenuItem.ToolTipText                               =   "Change the Header of the PDF(s) produced";
            helpToolStripMenuItem1.ToolTipText                                      =   "Open help document";
            exitToolStripMenuItem.ToolTipText                                       =   "Exits the program";
            changePDFSaveLocationToolStripMenuItem.ToolTipText                      =   "Change where PDFs are saved";
            changeCutlistSaveLocationToolStripMenuItem.ToolTipText                  =   "Change where Cutlists are saved";
            testToolStripMenuItem.ToolTipText                                       =   "Open Williams pricing program";
            viewQuoteBreakdownFromWilliamsProgramToolStripMenuItem.ToolTipText      =   "View quote breakdown from Williams program";
            ToolTip tooltip                                                         =   new ToolTip(); 
            tooltip.SetToolTip(btn_editQuote,                                           "Allows for the change of customer details and adding additional items");
        }
        private void btn_cutlist_Click(object sender, EventArgs e)
        {
            DirectoryInfo dinfoCL = new DirectoryInfo(@"K:/Cutlist/");
            string selectedCutlist = dinfoCL + listBox1.Text;
            if (File.Exists(selectedCutlist))
            {
                fileName.fileName.SetselectedFile(selectedCutlist);

                string[] linesF = File.ReadLines(fileName.fileName.selectedFile).ToArray();
                int count = linesF.Length;
                int counter = 0;
                for (int x = 0; x < count; x++) // x lines in file 
                {
                    counter++;
                    if (counter == 3)
                    {
                        lbl_customer.Text = linesF[3];
                    }
                    if (counter == 4)
                    {
                        lbl_details.Text = linesF[4];
                    }
                    if (counter == 5)
                    {
                        lbl_1Item.Text = linesF[5];
                    }
                    if (counter == 6)
                    {
                        lbl_itemDetails.Text = linesF[6];
                    }
                    if (counter == 7)
                    {
                        lbl_task.Text = linesF[7];
                    }
                }
                string FileNameCL1 = fileName.fileName.selectedFile;
                string[] linesi = File.ReadAllLines(FileNameCL1).ToArray();
                int counti = linesF.Length;
                for (int i = 0; i < counti; i++) 
                {
                    while (!linesi[i].Contains("___") && !linesi[i].Contains("CUTTING") && linesi[i] != " ")
                    {
                        if (linesi[i].Contains("PLY"))
                        {
                             fileName.fileName.SetafterSupply(i);
                        }
                        if (i <= fileName.fileName.afterSupply)
                        {
                            string text = Regex.Replace(linesi[i], @"^\d+", "");
                            var cltext = text.TrimStart();
                            textBox1.AppendText(cltext);
                            textBox1.AppendText(Environment.NewLine);
                            textBox1.Text.Trim(' ');                              
                        }
                        if (i > fileName.fileName.afterSupply)
                        {
                            textBox1.AppendText(Environment.NewLine);
                            linesi[i].TrimStart(' ');
                            string[] numbers = Regex.Split(linesi[i], @"\D+");
                            string text = Regex.Replace(linesi[i], @"^\d+", "");
                            var cltext = text.TrimStart();
                            var numLength = numbers[1].Length;
                            textBox1.AppendText(numbers[1] + "                                                             " + cltext.Remove(0, numLength));
                            textBox1.AppendText(Environment.NewLine);
                            textBox1.Text.Trim(' ');
                        }
                     }
                                            
                }
                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    iTextSharp.text.Font fdefault = FontFactory.GetFont("HELVETICA", 8, BaseColor.BLACK);
                    iTextSharp.text.Font fdetails = FontFactory.GetFont("HELVETICA", 9, BaseColor.BLACK);
                    iTextSharp.text.Font fBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////           
                    Document myDocument = new Document();
                    PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                    myDocument.Open();
                    PdfPTable table = new PdfPTable(2);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    Paragraph para1stItem = new Paragraph(lbl_customer.Text + "\n" + lbl_details.Text + "\n" + lbl_1Item.Text + "\n" + lbl_itemDetails.Text + "\n" + lbl_task.Text + "\n", fBold);
                    Paragraph paraQuote = new Paragraph("\n" + textBox1.Text, fdefault);
                    Paragraph para2Quote = new Paragraph("\n" + textBox2.Text, fBold);
                    Paragraph paraOtherDetails = new Paragraph("\n" + "Other Details:" + " ____________________________________________________________________________ "
                      + "\n" + "                        " + " ____________________________________________________________________________ "
                      + "\n" + "                        " + " ____________________________________________________________________________ "
                      , fBold);
                    Paragraph paraCompletedBy = new Paragraph("\n" + "Completed By:" + " ____________________________________________________________________________ "
                      + "\n" + "                         "                           + " ____________________________________________________________________________ "
                      + "\n" + "                         "                           + " ____________________________________________________________________________ "
                      , fBold);
                    PdfPTable tableFP = new PdfPTable(1);
                    tableFP.TotalWidth = 500f;
                    tableFP.DefaultCell.Border = 0;
                    tableFP.AddCell(paraOtherDetails);
                    tableFP.AddCell(paraCompletedBy);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    myDocument.Add(paraQuote);
                    myDocument.Add(para2Quote);
                    tableFP.WriteSelectedRows(0, -1, 0, 120, myPDFWriter.DirectContent);
                    myDocument.Close();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////  
                    byte[] content = myMemoryStream.ToArray();
                    string output = lbl_listBox.Text;
                    string listbox = output.Remove(output.Length - 4);
                    DateTime date = DateTime.Parse(System.DateTime.Now.ToString());
                    string dateForPDF = date.ToString("HHmmss");
                    FileInfo filepdfPath = new FileInfo("pdfPath.txt");
                    using (StreamReader sr3 = filepdfPath.OpenText())
                    {
                        while (!sr3.EndOfStream)
                        {
                            var pathString = Path.Combine(sr3.ReadLine() + lbl_custName.Text);
                            var finalPS = pathString.Replace("\\", "/");
                            System.IO.Directory.CreateDirectory(finalPS);
                            using (FileStream fs = File.Create(finalPS + "/" + "/" + lbl_custName.Text + ".pdf"))
                            {
                                fs.Write(content, 0, (int)content.Length); // writing out pdf from the content array
                                DialogResult saved = MessageBox.Show("Saved Successfully!", "Quote Saved", MessageBoxButtons.OK);//Dialog box to show file has been saved 
                            }
                            this.Controls.Clear();
                            this.InitializeComponent();
                            Form1_Load(sender, e);
                        }                   
                    }
                }
            }
            else
            {
                MessageBox.Show("You have not created a cutlist in williams program for the selected quote.", "Error");
            }
        }
        private void grpBoxQQ_Enter(object sender, EventArgs e)
        {  
            btn_custom.Focus();
        }

        private void btn_quoteBreakdown_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(fileName.fileName.selectedFile);
        }

        private void btn_editQuote_Click(object sender, EventArgs e)
        {
            if (fileName.fileName.selectedFile != "")
            {
                finalPrice();
                carriage();
                List<string> selectedFiles = new List<string>();
                List<string> selectedFilesSAF = new List<string>();
                string FileName = fileName.fileName.selectedFile;
                if (FileName.Contains("QUOTEsupply") || FileName.Contains("QUOTEfit"))
                {
                    string[] shortFileName = FileName.Split('/');
                    string searchKey = shortFileName[2].Substring(0, shortFileName[2].Length - 4);
                    DirectoryInfo dinfoSO = new DirectoryInfo(@"K:/CSupply Only/");
                    FileInfo[] FilesSO = dinfoSO.GetFiles(searchKey + "*" + ".txt");
                    DirectoryInfo dinfoSAF = new DirectoryInfo(@"K:/CSupply And Fit/");
                    FileInfo[] FilesSAF = dinfoSAF.GetFiles(searchKey + "*" + ".txt");
                    foreach (FileInfo file in FilesSO)
                    {
                        selectedFiles.Add(file.Name);
                    }
                    foreach (FileInfo file in FilesSAF)
                    {
                        selectedFilesSAF.Add(file.Name);
                    }
                    for (int i = 0; i < selectedFiles.Count; i++) // i number of files 
                    {
                        FileInfo relatedFiles = new FileInfo(@"K:/CSupply Only/" + selectedFiles[i]);
                        string[] lines = File.ReadLines(@"K:/CSupply Only/" + selectedFiles[i]).ToArray();
                        int count = lines.Length;
                        for (int x = 0; x < count; x++) // x lines in file 
                        {
                            fileName.fileName.SetcurrentFile(@"K:/CSupply Only/" + selectedFiles[i]);
                            custDetails();
                            siteDetails();
                            if (lines[x].Contains("D/WIRE 868 FENCE Height"))
                            {
                                DialogResult result = MessageBox.Show("You selected Double Wire Fence \nYes for PreGalv ; No for Hot Dipped Galv'd", "Double Wire Fence Selection", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFence();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else if (result == DialogResult.No)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFenceHDG();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else
                                {
                                    this.Controls.Clear();
                                    this.InitializeComponent();
                                    Form1_Load(sender, e);
                                }
                            }
                            if (lines[x].Contains("D/WIRE 888 FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire888();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN H/CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicSinGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. H.CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicDBGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("HVY.CLASSIC FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicHeavyFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("CLASSIC FENCE") && !lines[x].Contains("HVY"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                doubleWireGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                sinDWGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("AXIS FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicEcoFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PRISON MESH FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                prisonMesh();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Netted B/WIRE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Spect Fence"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                SpectFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PALISADE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                palisadeFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("D/W Ball Stop Fence"))
                            {
                                sinDWWisaGate();
                            }
                            if (x == 1)
                            {
                                string[] explainSearchKey = Regex.Split(@"K:/CSupply Only/" + selectedFiles[0], @"\D+");
                                string explain = explainSearchKey[1];
                                if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                                {
                                    FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                    string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                    int countExplain = explainLines.Length;
                                    for (int y = 0; y < countExplain; y++) // x lines in file 
                                    {
                                        txt_explain.Text = explainLines[0];
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < selectedFilesSAF.Count; i++) // i number of files 
                    {
                        FileInfo relatedFiles = new FileInfo(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                        string[] lines = File.ReadLines(@"K:/CSupply And Fit/" + selectedFilesSAF[i]).ToArray();
                        int count = lines.Length;
                        Console.WriteLine("SAF:" + selectedFilesSAF[i]);
                        for (int x = 0; x < count; x++) // x lines in file 
                        {

                            fileName.fileName.SetcurrentFile(@"K:/CSupply And Fit/" + selectedFilesSAF[i]);
                            custDetails();
                            siteDetails();
                            if (lines[x].Contains("D/WIRE 868 FENCE Height"))
                            {
                                DialogResult result = MessageBox.Show("You selected Double Wire Fence \nYes for PreGalv ; No for Hot Dipped Galv'd", "Double Wire Fence Selection", MessageBoxButtons.YesNoCancel);
                                if (result == DialogResult.Yes)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFence();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else if (result == DialogResult.No)
                                {
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                    DBWireFenceHDG();
                                    txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                    txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                }
                                else
                                {
                                    this.Controls.Clear();
                                    this.InitializeComponent();
                                    Form1_Load(sender, e);
                                }
                            }
                            if (lines[x].Contains("D/WIRE 888 FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire888();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN H/CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicSinGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. H.CLASSIC GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                ClassicDBGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("HVY.CLASSIC FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicHeavyFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("CLASSIC FENCE") && !lines[x].Contains("HVY"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("DB. D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                doubleWireGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("SIN D/WIRE GATE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                sinDWGate();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("AXIS FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                classicEcoFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PRISON MESH FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                prisonMesh();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Netted B/WIRE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                nettedDBWire();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("Spect Fence"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                SpectFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("PALISADE FENCE"))
                            {
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine);
                                palisadeFence();
                                txt_quotePara1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara2.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                                txt_quotePara3.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                            if (lines[x].Contains("D/W Ball Stop Fence"))
                            {
                                sinDWWisaGate();
                            }
                            if (x == 1)
                            {
                                string[] explainSearchKey = Regex.Split(@"K:/CSupply And Fit/" + selectedFilesSAF[0], @"\D+");
                                string explain = explainSearchKey[1];
                                fileName.fileName.Setexplain(explain);
                                if (File.Exists(@"K:/Ref Explains/" + explain + ".txt"))
                                {
                                    FileInfo explainFile = new FileInfo(@"K:/Ref Explains/" + explain + ".txt");
                                    string[] explainLines = File.ReadLines(@"K:/Ref Explains/" + explain + ".txt").ToArray();
                                    int countExplain = explainLines.Length;
                                    for (int y = 0; y < countExplain; y++) // x lines in file 
                                    {
                                        txt_explain.Text = explainLines[0];
                                    }
                                }
                            }
                        }
                    }
                }
                if (cb_doubleWireGate.Checked)
                {
                    doubleWireGateCustomQs();
                    doubleWireGateCustom();
                    if (txt_dwgAmount.Text != "" && Convert.ToInt32(txt_dwgAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_dwgAmount.Text) - 1; i++)
                        {
                            doubleWireGateCustomQs();
                            doubleWireGateCustom();
                        }
                    }
                }
                if (cb_classicFence.Checked)
                {
                    classicFenceCustomQs();
                    classicFenceCustom();
                    if (txt_cfAmount.Text != "" && Convert.ToInt32(txt_cfAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_cfAmount.Text) - 1; i++)
                        {
                            classicFenceCustomQs();
                            classicFenceCustom();
                        }
                    }
                }
                if (cb_spectFence.Checked)
                {
                    spectFenceCustomQs();
                    spectFenceCustom();
                    if (txt_sfAmount.Text != "" && Convert.ToInt32(txt_sfAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_sfAmount.Text) - 1; i++)
                        {
                            spectFenceCustomQs();
                            spectFenceCustom();
                        }
                    }
                }
                if (cb_prisonMesh.Checked)
                {
                    prisonMeshCustomQs();
                    prisonMeshCustom();
                    if (txt_pmAmount.Text != "" && Convert.ToInt32(txt_pmAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_pmAmount.Text) - 1; i++)
                        {
                            prisonMeshCustomQs();
                            prisonMeshCustom();
                        }
                    }
                }
                if (cb_doubleWireFence.Checked)
                {
                    doubleWireFenceCustomQs();
                    doubleWireFenceCustom();
                    if (txt_dwfAmount.Text != "" && Convert.ToInt32(txt_dwfAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_dwfAmount.Text) - 1; i++)
                        {
                            doubleWireFenceCustomQs();
                            doubleWireFenceCustom();
                        }
                    }
                }
                if (cb_classicFenceHeavy.Checked)
                {
                    classicFenceHeavyCustomQs();
                    classicFenceHeavyCustom();
                    if (txt_cfHeavyAmount.Text != "" && Convert.ToInt32(txt_cfHeavyAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_cfHeavyAmount.Text) - 1; i++)
                        {
                            classicFenceHeavyCustomQs();
                            classicFenceHeavyCustom();
                        }
                    }
                }
                if (cb_singleDWGate.Checked)
                {
                    singleDoubleWireGateCustomQs();
                    singleDoubleWireGateCustom();
                    if (txt_sdwgAmount.Text != "" && Convert.ToInt32(txt_sdwgAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_sdwgAmount.Text) - 1; i++)
                        {
                            singleDoubleWireGateCustomQs();
                            singleDoubleWireGateCustom();
                        }
                    }
                }
                if (cb_singleClassicGate.Checked)
                {
                    singleClassicGateCustomQs();
                    singleClassicGateCustom();
                    if (txt_scgAmount.Text != "" && Convert.ToInt32(txt_scgAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_scgAmount.Text) - 1; i++)
                        {
                            singleClassicGateCustomQs();
                            singleClassicGateCustom();
                        }
                    }
                }
                if (cb_doubleClassicGate.Checked)
                {
                    doubleClassicGateCustomQs();
                    doubleClassicGateCustom();
                    if (txt_dcgAmount.Text != "" && Convert.ToInt32(txt_dcgAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_dcgAmount.Text) - 1; i++)
                        {
                            doubleClassicGateCustomQs();
                            doubleClassicGateCustom();
                        }
                    }
                }
                if (cb_palisadeFence.Checked)
                {
                    palisadeFenceCustomQs();
                    palisadeFenceCustom();
                    if (txt_pfAmount.Text != "" && Convert.ToInt32(txt_pfAmount.Text) > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(txt_pfAmount.Text) - 1; i++)
                        {
                            palisadeFenceCustomQs();
                            palisadeFenceCustom();
                        }
                    }
                } 
            
                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    iTextSharp.text.Font fdefault = FontFactory.GetFont("HELVETICA", 8, BaseColor.BLACK);
                    iTextSharp.text.Font fdetails = FontFactory.GetFont("HELVETICA", 9, BaseColor.BLACK);
                    iTextSharp.text.Font fBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                    iTextSharp.text.Font fBoldPrices = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            setting up the doc and table 
                    Document myDocument = new Document();
                    PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                    myDocument.Open();
                    PdfPTable table = new PdfPTable(2);                                                                                 // create table with 2 columns 
                    PdfPTable tableFooter = new PdfPTable(2);
                    PdfPTable tableDetails = new PdfPTable(3);
                    PdfPTable tableQuote = new PdfPTable(5);
                    PdfPTable tableTitle = new PdfPTable(5);
                    PdfPTable tableCarriage = new PdfPTable(5);
                    tableQuote.SplitLate = false;
                    tableQuote.SplitRows = false;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            creating header cell
                    Paragraph header = new Paragraph("Description", fBold);
                    header.Alignment = Element.ALIGN_CENTER;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////                   
                    FileInfo file2 = new FileInfo("Header.txt");
                    using (StreamReader sr2 = file2.OpenText())
                    {
                        while (!sr2.EndOfStream)
                        {
                            var image = iTextSharp.text.Image.GetInstance(sr2.ReadLine());
                            var imageCell = new PdfPCell(image);
                            image.ScaleToFit(300f, 300f);
                            image.SetAbsolutePosition(60, 700);
                            PdfPCell imageHeader = new PdfPCell(image);
                            imageHeader.Colspan = 2;
                            imageHeader.HorizontalAlignment = 1;
                            imageHeader.Border = 0;
                            table.DefaultCell.Border = 0;
                            table.AddCell(imageHeader);
                            Phrase phrase = new Phrase();
                            Phrase sdetails = new Phrase();
                            var custname = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Name", "New Customer Details", "");
                            lbl_custName.Text = custname;
                            var address = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Address", "New Customer Details", "");
                            lbl_address.Text = address;
                            var town = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Town", "New Customer Details", "");
                            lbl_town.Text = town;
                            var county = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer County", "New Customer Details", "Co.");
                            lbl_county.Text = county;
                            var postcode = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Postcode", "New Customer Details", "BT");
                            lbl_postcode.Text = postcode;
                            var telNo = Microsoft.VisualBasic.Interaction.InputBox("Enter Customer Tel. No.", "New Customer Details", "");
                            lbl_telNo.Text = telNo;
                            var site = Microsoft.VisualBasic.Interaction.InputBox("Enter Site Name", "New Site Details", "");
                            lbl_site.Text = site;
                            var quotationNo = Microsoft.VisualBasic.Interaction.InputBox("Enter New Quotation No.", "New Quotation No.", "");
                            fileName.fileName.Setexplain(quotationNo);
                            phrase.Add(
                                        new Chunk(Environment.NewLine + lbl_custName.Text + Environment.NewLine + lbl_address.Text + Environment.NewLine
                                        + lbl_town.Text + Environment.NewLine + lbl_county.Text + Environment.NewLine + lbl_postcode.Text + Environment.NewLine
                                        + lbl_telNo.Text + Environment.NewLine + Environment.NewLine, fdetails)
                                        );
                            string test = DateTime.Now.ToString("dd.MM.yyy");
                            sdetails.Add(new Chunk(Environment.NewLine + "Quotation : " + fileName.fileName.explain + Environment.NewLine + Environment.NewLine + lbl_site.Text + Environment.NewLine + Environment.NewLine + "Date: " + test, fdetails));
                            Console.WriteLine("Explain:" + fileName.fileName.explain);
                            int[] intTblWidth = { 70, 2, 28 };
                            tableDetails.SetWidths(intTblWidth);
                            tableDetails.HorizontalAlignment = Element.ALIGN_LEFT;
                            tableDetails.WidthPercentage = 100;
                            tableDetails.DefaultCell.Border = 0;
                            tableDetails.DefaultCell.SetLeading(3, 1);
                            tableDetails.AddCell(phrase);
                            tableDetails.AddCell("");
                            tableDetails.AddCell(sdetails);
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////
                            FileInfo file1 = new FileInfo("Footer.txt");
                            using (StreamReader sr1 = file1.OpenText())
                            {
                                while (!sr1.EndOfStream)
                                {
                                    var imageFooter = iTextSharp.text.Image.GetInstance(sr1.ReadLine());
                                    var imageCellFooter = new PdfPCell(imageFooter);
                                    imageFooter.ScaleToFit(400f, 400f);
                                    PdfPCell imageFooterCell = new PdfPCell(imageFooter);
                                    imageFooterCell.Colspan = 2;
                                    imageFooterCell.HorizontalAlignment = 1;
                                    imageFooterCell.Border = 0;
                                    imageFooter.SetAbsolutePosition(80, 0);
                                    tableFooter.DefaultCell.Border = 0;
                                    tableFooter.AddCell(imageFooterCell);
                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////            adding paragraphs 
                                    txt_quote.Text = txt_quotePara.Text;
                                    Paragraph paraQuote = new Paragraph(Environment.NewLine + txt_quote.Text, fdefault);
                                    txt_quote1.Text = txt_quotePara1.Text;
                                    Paragraph paraQuote1 = new Paragraph(Environment.NewLine + txt_quote1.Text + Environment.NewLine, fBoldPrices);
                                    txt_quote2.Text = txt_quotePara2.Text;
                                    Paragraph paraQuote2 = new Paragraph(Environment.NewLine + txt_quotePara2.Text + Environment.NewLine, fBoldPrices);
                                    txt_quote3.Text = txt_quotePara3.Text;
                                    Paragraph paraQuote3 = new Paragraph(Environment.NewLine + txt_quotePara3.Text + Environment.NewLine, fBoldPrices);
                                    Paragraph paraExplain = new Paragraph(txt_explain.Text);
                                    //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                    Console.WriteLine("TEST QQTVA2:" + (fileName.fileName.QQtVA2)); //Convert.ToDecimal(fileName.fileName.QQtVA2) != 0 ||
                                    if ( (fileName.fileName.QQtVA2) == "0.00")//|| (fileName.fileName.QQtVA2) != "")
                                    {                                  
                                        //var QQtNA = Convert.ToDecimal(fileName.fileName.tNA2) + Convert.ToDecimal(fileName.fileName.QQtNA2);
                                        //Console.WriteLine("QQtNA:" + fileName.fileName.tNA2);
                                        //Console.WriteLine("QQtNA:" + fileName.fileName.QQtNA2);
                                        //var QQtVA = Convert.ToDecimal(fileName.fileName.tVA2) + Convert.ToDecimal(fileName.fileName.QQtVA2);
                                        //var QQt2 = Convert.ToDecimal(fileName.fileName.t2) + Convert.ToDecimal(fileName.fileName.QQt2);
                                        //var paraFinalPrices = new Paragraph("£" + QQtNA
                                        //+ Environment.NewLine + "£" + QQtVA
                                        //+ Environment.NewLine + "£" + QQt2, fBoldPrices);
                                        var paraFinalPrices = new Paragraph(    "£" + fileName.fileName.tNA2
                                        + Environment.NewLine               +   "£" + fileName.fileName.tVA2
                                        + Environment.NewLine               +   "£" + fileName.fileName.t2, fBoldPrices);
                                        var paraFPText = new Paragraph(fileName.fileName.tNA
                                                                            + Environment.NewLine + fileName.fileName.tVA
                                                                            + Environment.NewLine + fileName.fileName.t, fBoldPrices);
                                        PdfPTable tableFP = new PdfPTable(4);
                                        float[] intTblFPWidth = { 45, 1, 32, 22 };
                                        tableFP.SetWidths(intTblFPWidth);
                                        tableFP.WidthPercentage = 100;
                                        tableFP.DefaultCell.Border = 0;
                                        tableFP.DefaultCell.SetLeading(3, 1);
                                        tableFP.AddCell("");
                                        tableFP.AddCell("");
                                        PdfPCell fpt = new PdfPCell(paraFPText) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                                        fpt.Border = 0; fpt.SetLeading(3, 1);
                                        tableFP.AddCell(fpt);
                                        PdfPCell fp = new PdfPCell(paraFinalPrices) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        fp.Border = 0; fp.SetLeading(3, 1);
                                        tableFP.AddCell(fp);
                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        int[] intTblQuoteWidth = { 64, 1, 11, 11, 11 };
                                        tableQuote.SetWidths(intTblQuoteWidth);
                                        tableQuote.WidthPercentage = 100;
                                        tableQuote.DefaultCell.Border = 0;
                                        tableQuote.DefaultCell.SetLeading(3, 1);
                                        PdfPCell q = new PdfPCell(paraQuote) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED };
                                        q.Border = 0; q.SetLeading(3, 1);
                                        tableQuote.AddCell(q);
                                        tableQuote.AddCell("");
                                        PdfPCell q1 = new PdfPCell(paraQuote1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q1.Border = 0; q1.SetLeading(3, 1);
                                        tableQuote.AddCell(q1);
                                        PdfPCell q2 = new PdfPCell(paraQuote2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q2.Border = 0; q2.SetLeading(3, 1);
                                        tableQuote.AddCell(q2);
                                        PdfPCell q3 = new PdfPCell(paraQuote3) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q3.Border = 0; q3.SetLeading(3, 1);
                                        tableQuote.AddCell(q3);
                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        int[] intTblCarriageWidth = { 64, 1, 11, 11, 11 };
                                        Paragraph paraCText = new Paragraph("Carriage to Site ", fBoldPrices);
                                        Paragraph paraCPrice = new Paragraph("1 Nr.", fBoldPrices);
                                        Paragraph paraCPrice1 = new Paragraph("£" + label2.Text, fBoldPrices);
                                        tableCarriage.SetWidths(intTblCarriageWidth);
                                        tableCarriage.HorizontalAlignment = Element.ALIGN_LEFT;
                                        tableCarriage.WidthPercentage = 100;
                                        tableCarriage.DefaultCell.Border = 0;
                                        tableCarriage.DefaultCell.SetLeading(3, 1);
                                        tableCarriage.AddCell(paraCText);
                                        tableCarriage.AddCell("");
                                        PdfPCell nr = new PdfPCell(paraCPrice) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        nr.Border = 0; nr.SetLeading(3, 1);
                                        tableCarriage.AddCell(nr);
                                        PdfPCell p1 = new PdfPCell(paraCPrice1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        p1.Border = 0; p1.SetLeading(3, 1);
                                        tableCarriage.AddCell(p1);
                                        tableCarriage.AddCell(p1);
                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        Paragraph paraDescription = new Paragraph("Description", fBold);
                                        Paragraph paraTitle = new Paragraph("Quantity", fBold);
                                        Paragraph paraTitle1 = new Paragraph("Unit Price", fBold);
                                        Paragraph paraTitle2 = new Paragraph("Net Amount", fBold);
                                        int[] intTbltitleWidth = { 64, 1, 11, 11, 11 };
                                        tableTitle.SetWidths(intTbltitleWidth);
                                        tableTitle.WidthPercentage = 100;
                                        tableTitle.DefaultCell.Border = 0;
                                        tableTitle.DefaultCell.SetLeading(3, 1);
                                        tableTitle.AddCell(paraDescription);
                                        tableTitle.AddCell("");
                                        PdfPCell t = new PdfPCell(paraTitle) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t.Border = 0; t.SetLeading(3, 1);
                                        tableTitle.AddCell(t);
                                        PdfPCell t1 = new PdfPCell(paraTitle1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t1.Border = 0; t1.SetLeading(3, 1);
                                        tableTitle.AddCell(t1);
                                        PdfPCell t2 = new PdfPCell(paraTitle2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t2.Border = 0; t2.SetLeading(3, 1);
                                        tableTitle.AddCell(t2);
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////             
                                        myDocument.Add(table);
                                        myDocument.Add(tableDetails);
                                        myDocument.Add(tableTitle);
                                        myDocument.Add(tableQuote);
                                        myDocument.Add(tableCarriage);
                                        myDocument.Add(tableFP);
                                        myDocument.Add(imageFooter);
                                        myDocument.Close();
                                    }
                                    else
                                    {
                                        var QQtNA = Convert.ToDecimal(fileName.fileName.tNA2) + Convert.ToDecimal(fileName.fileName.QQtNA2);
                                        Console.WriteLine("QQtNA:" + fileName.fileName.tNA2);
                                        Console.WriteLine("QQtNA:" + fileName.fileName.QQtNA2);
                                        var QQtVA = Convert.ToDecimal(fileName.fileName.tVA2) + Convert.ToDecimal(fileName.fileName.QQtVA2);
                                        var QQt2 = Convert.ToDecimal(fileName.fileName.t2) + Convert.ToDecimal(fileName.fileName.QQt2);
                                        var paraFinalPrices = new Paragraph("£" + QQtNA
                                        + Environment.NewLine + "£" + QQtVA
                                        + Environment.NewLine + "£" + QQt2, fBoldPrices);
                                 //       var paraFinalPrices = new Paragraph("£" + fileName.fileName.tNA2
                                 //+ Environment.NewLine + "£" + fileName.fileName.tVA2
                                 //+ Environment.NewLine + "£" + fileName.fileName.t2, fBoldPrices);

                                        var paraFPText = new Paragraph(fileName.fileName.tNA
                                                                            + Environment.NewLine + fileName.fileName.tVA
                                                                            + Environment.NewLine + fileName.fileName.t, fBoldPrices);
                                        PdfPTable tableFP = new PdfPTable(4);
                                        float[] intTblFPWidth = { 45, 1, 32, 22 };
                                        tableFP.SetWidths(intTblFPWidth);
                                        tableFP.WidthPercentage = 100;
                                        tableFP.DefaultCell.Border = 0;
                                        tableFP.DefaultCell.SetLeading(3, 1);
                                        tableFP.AddCell("");
                                        tableFP.AddCell("");
                                        PdfPCell fpt = new PdfPCell(paraFPText) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT };
                                        fpt.Border = 0; fpt.SetLeading(3, 1);
                                        tableFP.AddCell(fpt);
                                        PdfPCell fp = new PdfPCell(paraFinalPrices) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        fp.Border = 0; fp.SetLeading(3, 1);
                                        tableFP.AddCell(fp);

                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        int[] intTblQuoteWidth = { 64, 1, 11, 11, 11 };
                                        tableQuote.SetWidths(intTblQuoteWidth);
                                        tableQuote.WidthPercentage = 100;
                                        tableQuote.DefaultCell.Border = 0;
                                        tableQuote.DefaultCell.SetLeading(3, 1);
                                        PdfPCell q = new PdfPCell(paraQuote) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED };
                                        q.Border = 0; q.SetLeading(3, 1);
                                        tableQuote.AddCell(q);
                                        tableQuote.AddCell("");
                                        PdfPCell q1 = new PdfPCell(paraQuote1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q1.Border = 0; q1.SetLeading(3, 1);
                                        tableQuote.AddCell(q1);
                                        PdfPCell q2 = new PdfPCell(paraQuote2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q2.Border = 0; q2.SetLeading(3, 1);
                                        tableQuote.AddCell(q2);
                                        PdfPCell q3 = new PdfPCell(paraQuote3) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        q3.Border = 0; q3.SetLeading(3, 1);
                                        tableQuote.AddCell(q3);
                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        int[] intTblCarriageWidth = { 64, 1, 11, 11, 11 };
                                        Paragraph paraCText = new Paragraph("Carriage to Site ", fBoldPrices);
                                        Paragraph paraCPrice = new Paragraph("1 Nr.", fBoldPrices);
                                        Paragraph paraCPrice1 = new Paragraph("£" + label2.Text, fBoldPrices);
                                        tableCarriage.SetWidths(intTblCarriageWidth);
                                        tableCarriage.HorizontalAlignment = Element.ALIGN_LEFT;
                                        tableCarriage.WidthPercentage = 100;
                                        tableCarriage.DefaultCell.Border = 0;
                                        tableCarriage.DefaultCell.SetLeading(3, 1);
                                        tableCarriage.AddCell(paraCText);
                                        tableCarriage.AddCell("");
                                        PdfPCell nr = new PdfPCell(paraCPrice) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        nr.Border = 0; nr.SetLeading(3, 1);
                                        tableCarriage.AddCell(nr);
                                        PdfPCell p1 = new PdfPCell(paraCPrice1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        p1.Border = 0; p1.SetLeading(3, 1);
                                        tableCarriage.AddCell(p1);
                                        tableCarriage.AddCell(p1);
                                        //////////////////////////////////////////////////////////////////////////////////////////////////////// 
                                        Paragraph paraDescription = new Paragraph("Description", fBold);
                                        Paragraph paraTitle = new Paragraph("Quantity", fBold);
                                        Paragraph paraTitle1 = new Paragraph("Unit Price", fBold);
                                        Paragraph paraTitle2 = new Paragraph("Net Amount", fBold);
                                        int[] intTbltitleWidth = { 64, 1, 11, 11, 11 };
                                        tableTitle.SetWidths(intTbltitleWidth);
                                        tableTitle.WidthPercentage = 100;
                                        tableTitle.DefaultCell.Border = 0;
                                        tableTitle.DefaultCell.SetLeading(3, 1);
                                        tableTitle.AddCell(paraDescription);
                                        tableTitle.AddCell("");
                                        PdfPCell t = new PdfPCell(paraTitle) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t.Border = 0; t.SetLeading(3, 1);
                                        tableTitle.AddCell(t);
                                        PdfPCell t1 = new PdfPCell(paraTitle1) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t1.Border = 0; t1.SetLeading(3, 1);
                                        tableTitle.AddCell(t1);
                                        PdfPCell t2 = new PdfPCell(paraTitle2) { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT };
                                        t2.Border = 0; t2.SetLeading(3, 1);
                                        tableTitle.AddCell(t2);
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////             
                                        myDocument.Add(table);
                                        myDocument.Add(tableDetails);
                                        myDocument.Add(tableTitle);
                                        myDocument.Add(tableQuote);
                                        myDocument.Add(tableCarriage);
                                        myDocument.Add(tableFP);
                                        myDocument.Add(imageFooter);
                                        myDocument.Close();


                                        
                                    }
                                }
                            }
                        }
                    }
                    byte[] content = myMemoryStream.ToArray();
                    string output = lbl_listBox.Text;
                    string listbox = output.Remove(output.Length - 4);
                    DateTime date = DateTime.Parse(System.DateTime.Now.ToString());
                    string dateForPDF = date.ToString("HHmmss");
                    FileInfo filepdfPath = new FileInfo("pdfPath.txt");
                    using (StreamReader sr3 = filepdfPath.OpenText())
                    {
                        while (!sr3.EndOfStream)
                        {
                            var pathString = Path.Combine(sr3.ReadLine() + listbox);
                            var finalPS = pathString.Replace("\\", "/");
                            System.IO.Directory.CreateDirectory(finalPS);
                            using (FileStream fs = File.Create(finalPS + "/" + "/" + listbox + "Edited" + ".pdf"))
                            {
                                fs.Write(content, 0, (int)content.Length); // writing out pdf from the content array
                                DialogResult saved = MessageBox.Show("Saved Successfully!", "Quote Saved", MessageBoxButtons.OK);//Dialog box to show file has been saved 
                            }
                            this.Controls.Clear();
                            this.InitializeComponent();
                            Form1_Load(sender, e);
                        }
                    }
                }

            }
        }
     }
 } 
//private void getPath()
//        {
//            string output = lbl_listBox.Text;
//            string listbox = output.Remove(output.Length - 4);
//            DateTime date = DateTime.Parse(System.DateTime.Now.ToString());
//            string dateForPDF = date.ToString("HHmmss");
//            FileInfo filepdfPath = new FileInfo("pdfPath.txt");
//            using (StreamReader sr3 = filepdfPath.OpenText())
//            {
//                while (!sr3.EndOfStream)
//                {
//                    var pathString = Path.Combine(sr3.ReadLine());
//                    var finalPS = pathString.Replace("\\", "/");
//                    fileName.fileName.SetpdfPath(finalPS);
//                }
//            }
//        }
