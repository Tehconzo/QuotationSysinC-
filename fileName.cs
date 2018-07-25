using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fileName
{
    public static class fileName
    {
        static fileName() { selectedFile = ""; currentFile = ""; currency = ""; tNA = ""; tNA2 = ""; tVA = ""; tVA2 = ""; t = ""; t2 = ""; dbgNumber = ""; panel1 = "";
        panel2 = ""; header = ""; footer = ""; explain = ""; QQTotalPrice = 0; pdfPath = ""; clPath = ""; afterSupply = 0; greatest = 0; quoteGreatest = 0; 
        QQtVA2 = "0.00"; QQtNA2 = "0.00"; QQt2 = "0.00";
                          }
        public static string selectedFile
        {
            get;
            private set;
        }
        public static void SetselectedFile(string newselectedFile)
        {
            selectedFile = newselectedFile;
        }
        public static string currentFile
        {
            get;
            private set;
        }
        public static void SetcurrentFile(string newcurrentFile)
        {
            currentFile = newcurrentFile;
        }
        public static int afterSupply
        {
            get;
            private set;
        }
        public static void SetafterSupply(int newafterSupply)
        {
            afterSupply = newafterSupply;
        }
        public static string explain
        {
            get;
            private set;
        }
        public static void Setexplain(string newExplain)
        {
            explain = newExplain;
        }
        public static string pdfPath
        {
            get;
            private set;
        }
        public static void SetpdfPath(string newpdfPath)
        {
            pdfPath = newpdfPath;
        }
        public static string clPath
        {
            get;
            private set;
        }
        public static void SetclPath(string newclPath)
        {
            clPath = newclPath;
        }
        public static int greatest
        {
            get;
            private set;
        }
        public static void Setgreatest(int newgreatest)
        {
            greatest = newgreatest;
        }
        public static int quoteGreatest
        {
            get;
            private set;
        }
        public static void SetquoteGreatest(int newquoteGreatest)
        {
            quoteGreatest = newquoteGreatest;
        }
        public static string currency
        {
            get;
            private set;
        }
        public static void Setcurrency(string newcurrency)
        {
            currency = newcurrency;
        }
        public static string tNA
        {
            get;
            private set;
        }
        public static void SettNA(string newtNA)
        {
            tNA = newtNA;
        }
        public static string tNA2
        {
            get;
            private set;
        }
        public static void SettNA2(string newtNA2)
        {
            tNA2 = newtNA2;
        }
        public static string tVA
        {
            get;
            private set;
        }
        public static void SettVA(string newtVA)
        {
            tVA = newtVA;
        }
        public static string tVA2
        {
            get;
            private set;
        }
        public static void SettVA2(string newtVA2)
        {
            tVA2 = newtVA2;
        }
        public static string t
        {
            get;
            private set;
        }
        public static void Sett(string newt)
        {
            t = newt;
        }
        public static string t2
        {
            get;
            private set;
        }
        public static void Sett2(string newt2)
        {
            t2 = newt2;
        }
        public static string dbgNumber
        {
            get;
            private set;
        }
        public static void SetdbgNumber(string newdbgNumber)
        {
            dbgNumber = newdbgNumber;
        }
        public static string panel1
        {
            get;
            private set;
        }
        public static void Setpanel1(string newpanel1)
        {
            panel1 = newpanel1;
        }
        public static string panel2
        {
            get;
            private set;
        }
        public static void Setpanel2(string newpanel2)
        {
            panel2 = newpanel2;
        }
        public static string header
        {
            get;
            private set;
        }
        public static void Setheader(string newHeader)
        {
            header = newHeader;
        }
        public static string footer
        {
            get;
            private set;
        }
        public static void Setfooter(string newFooter)
        {
            footer = newFooter;
        }
        public static int QQTotalPrice
        {
            get;
            private set;
        }
        public static void SetQQTotalPrice(int newQQTotalPrice)
        {
            QQTotalPrice = newQQTotalPrice;
        }
        public static string QQtNA2
        {
            get;
            private set;
        }
        public static void SetQQtNA2(string newQQtNA2)
        {
            QQtNA2 = newQQtNA2;
        }
        public static string QQtVA2
        {
            get;
            private set;
        }
        public static void SetQQtVA2(string newQQtVA2)
        {
            QQtVA2 = newQQtVA2;
        }
        public static string QQt2
        {
            get;
            private set;
        }
        public static void SetQQt2(string newQQt2)
        {
            QQt2 = newQQt2;
        }
    }
}
