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
using fileName;

namespace WindowsFormsApplication1
{
    public partial class Cutlist : Form
    {
        public Cutlist()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Menu = new MainMenu();
            Menu.Closed += (s, args) => this.Close();
            Menu.Show();
        }

        private void Cutlist_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            listBoxFill();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_listBox.Text = listBox1.SelectedItem.ToString();
            DirectoryInfo dinfoCL = new DirectoryInfo(@"K:/Cutlist/");
            string selectedCutlist = dinfoCL + listBox1.Text;
            if (File.Exists(selectedCutlist))
            {
                fileName.fileName.SetselectedFile(selectedCutlist);
            }
            else
            {
                MessageBox.Show("File doesn't exist");
            }

        }
        private void listBoxFill()
        {
            DirectoryInfo dinfoCL = new DirectoryInfo(@"K:/Cutlist/");
            FileInfo[] FilesCL = dinfoCL.GetFiles("*.txt");
            foreach (FileInfo file in FilesCL)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void btn_cutlist_Click(object sender, EventArgs e)
        {
         //   List<string> selectedFiles = new List<string>();
            string FileNameCL = fileName.fileName.selectedFile;
            string[] lines = File.ReadAllLines(FileNameCL).ToArray();
            int count = lines.Length;
            int counter = 0;
            for (int x = 0; x < count; x++) // x lines in file 
            {
                counter++;
                if (counter == 3)
                {
                    lbl_customer.Text = lines[3];
                }
                if (counter == 4)
                {
                    lbl_details.Text = lines[4];
                }
                if (counter == 5)
                {
                    lbl_1Item.Text = lines[5];
                }
                if (counter == 6)
                {
                    lbl_itemDetails.Text = lines[6];
                }
                if (counter == 7)
                {
                    lbl_task.Text = lines[7];
                }

                string[] Items = File.ReadAllLines(FileNameCL).Reverse().ToArray();
                if (Items[x].Contains("***") && !string.IsNullOrEmpty(Items[x]))
                {
                    var nextItemIdex = x;
                    if (nextItemIdex > 0)
                    {
                        lbl_2Item.Text = Items[nextItemIdex - 1];
                        lbl_2itemDetails.Text = Items[nextItemIdex - 2];
                        lbl_2task.Text = Items[nextItemIdex - 3];

                    }
                }
                if (lbl_2Item.Text == "lbl_2Item")
                {
                    lbl_2Item.Text = "";
                    lbl_2itemDetails.Text = "";
                    lbl_2task.Text = "";
                }
                
            }
            string FileNameCL1 = fileName.fileName.selectedFile;
            string[] linesi = File.ReadAllLines(FileNameCL1).ToArray();
            int counti = lines.Length;
            for (int i = 0; i < counti; i++) // x lines in file 
            {
                while (!linesi[i].Contains("___") && !linesi[i].Contains("CUTTING") && linesi[i] != " " && linesi[i] != lbl_2Item.Text && linesi[i] != lbl_2itemDetails.Text && linesi[i] != lbl_2task.Text
                        && linesi[i] != lbl_customer.Text && linesi[i] != lbl_details.Text && linesi[i] != lbl_1Item.Text && linesi[i] != lbl_itemDetails.Text && linesi[i] != lbl_task.Text)
                {
                    linesi[i].TrimStart(' ');
                    string[] numbers = Regex.Split(linesi[i], @"\D+");
                    string text = Regex.Replace(linesi[i], @"^\d+", "");
                    textBox1.AppendText(text);
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.Text.Trim(' ');
                    i++;
                    //if (linesi[i] ==)
                    //{
                    //    textBox2.AppendText(text);
                    //    textBox2.AppendText(Environment.NewLine);
                    //    textBox2.Text.Trim(' ');
                    //    i++;
                    //}
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
                    Paragraph para1stItem = new Paragraph(lbl_customer.Text + "\n" + lbl_details.Text + "\n" + lbl_1Item.Text + "\n" + lbl_itemDetails.Text + "\n" + lbl_task.Text + "\n",fBold);
                    Paragraph para2ndItem = new Paragraph("\n" + lbl_2Item.Text + "\n" + lbl_2itemDetails.Text + "\n" + lbl_2task.Text + "\n", fBold);
                    Paragraph paraQuote = new Paragraph("\n" + textBox1.Text, fdefault);
                    Paragraph para2Quote = new Paragraph("\n" + textBox2.Text, fdefault);
                    Paragraph paraOtherDetails = new Paragraph("\n" + "Other Details:"  + "____________________________________________________________________________"
                      + "\n" +"                        "                                + "____________________________________________________________________________"
                      + "\n" +"                        "                                + "____________________________________________________________________________"
                      , fBold);
                    Paragraph paraCompletedBy = new Paragraph("\n" + "Completed By:"    + "____________________________________________________________________________"
                      + "\n" + "                         "                              + "____________________________________________________________________________"
                      + "\n" + "                         "                              + "____________________________________________________________________________"
                      , fBold);
                    PdfPTable tableFP = new PdfPTable(1);
                    tableFP.TotalWidth = 500f;
                    tableFP.DefaultCell.Border = 0;
                  //  tableFP.DefaultCell.SetLeading(3, 1);
                    tableFP.AddCell(paraOtherDetails);
                    tableFP.AddCell(paraCompletedBy);
                    tableFP.WriteSelectedRows(0, -1, 0, 120, myPDFWriter.DirectContent);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var image = iTextSharp.text.Image.GetInstance("I:/Connor/Images/Rogers Fencing Supplies Logo.jpg");
                    var imageCell = new PdfPCell(image);
                    image.ScaleToFit(300f, 300f);
                    image.SetAbsolutePosition(60, 700);
                    PdfPCell imageHeader = new PdfPCell(image);
                    imageHeader.Colspan = 2;
                    imageHeader.HorizontalAlignment = 1;
                    imageHeader.Border = 0;
                    table.DefaultCell.Border = 0;
                    table.AddCell(imageHeader);
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    myDocument.Add(table);
                    myDocument.Add(para1stItem);
                    myDocument.Add(paraQuote);
                    myDocument.Add(para2ndItem);                   
                    myDocument.Add(para2Quote);                                                      
                    myDocument.Close();                                                                                                
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////  
                    byte[] content = myMemoryStream.ToArray();                                                                  
                    DateTime date = DateTime.Parse(System.DateTime.Now.ToString());   
                    string dateForPDF = date.ToString("HHmmss");
                    using (FileStream fs = File.Create("I:/Connor/Cutlist/" + dateForPDF + lbl_listBox.Text + ".pdf"))             
                    {
                        fs.Write(content, 0, (int)content.Length); 
                        DialogResult saved = MessageBox.Show("Saved on Inova Server,Connor Folder", "File Saved", MessageBoxButtons.OK); 
                    }
                    this.Controls.Clear();
                    this.InitializeComponent();
                    Cutlist_Load(sender, e);
                }
              
            } 
           
        }
    }

