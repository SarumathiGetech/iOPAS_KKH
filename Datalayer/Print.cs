using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;

namespace Datalayer
{
    public class Print
    {   

        public  int PrintCartridgeDrugLabel(string ItemCode, string ItemName, string Brand, string PackType, string PackSize, string UOM,string BarCode,
            string PrinterName)
        {
            int ItemNamePerLine = 30;
            int NoOFLineForItemName = 0;
            string[] ItemNameArray;
            //-----Temp Initialize-------
            ItemNameArray = new string[1];
            ItemNameArray[0] = "";

            if (ItemName != "")
            {
                string Value = Print.WordWrap(ItemName, ItemNamePerLine);
                string[] parts = Value.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                NoOFLineForItemName = parts.Length;

                if (NoOFLineForItemName >= 1)
                {
                    ItemNameArray = new string[NoOFLineForItemName];
                    for (int i = 0; i < NoOFLineForItemName; i++)
                    {
                        ItemNameArray[i] = "";
                    }

                    for (int i = 0; i < NoOFLineForItemName; i++)
                    {
                        ItemNameArray[i] = parts[i];
                    }
                }
            }
            else
                NoOFLineForItemName = 0;

            if (NoOFLineForItemName <= 5)
            {
                StringBuilder sb = new StringBuilder();

                int LineTwo = 140 + 40;
                int LineThree = LineTwo + ((NoOFLineForItemName - 1) * 40) + 40;
                int LineFour = LineThree + 40;
                int LineTwos = LineTwo;

                sb.AppendLine("^XA");

                //sb.AppendLine("^FO25,80^BY1,10^B3N,N,40,N,N^FD" + BarCode + "^FS");
                sb.AppendLine("^FO25,30^BY1^BQN,2,4^FDMM," + BarCode + "^FS");
                sb.AppendLine("^FO170,140^A0N,29^FD" + ItemCode + "^FS");
                sb.AppendLine("^FO25," + LineTwo + "^A0N,35^FD" + ItemNameArray[0] + "^FS");

                if (NoOFLineForItemName >= 2)
                {
                    for (int i = 1; i < NoOFLineForItemName; i++)
                    {
                        LineTwos = LineTwo + (i * 40);
                        sb.AppendLine("^FO25," + LineTwos + "^A0N,35^FD" + ItemNameArray[i] + "^FS");
                    }
                }

                sb.AppendLine("^FO25," + LineThree + "^A0N,26^FD" + Brand + "^FS");
                //sb.AppendLine("^FO210,90^A0N,29^FD" + PackType + " of " + PackSize + " " + UOM + "^FS");
                sb.AppendLine("^FO170,90^A0N,29^FD" + PackType + " of " + PackSize + " " + UOM + "^FS");
                sb.AppendLine("^XZ");

                RawPrinterHelper.SendStringToPrinter(PrinterName, sb.ToString());

            }//---------if (NoOFLineForItemCode <= 6)-------
            return 1;            
           
        }

        public  int PrintCartonBoxBarcode(string CartBarcode,string ItemName, string PrinterName)
        {

            int _ItemNamePerLine = 40;
            int _NoOFLineForItemName = 0;
            string[] _ItemNameArray;
            _ItemNameArray = new string[1];
            _ItemNameArray[0] = "";

            if (ItemName != "")
            {
                string _Value = Print.WordWrap(ItemName, _ItemNamePerLine);
                string[] _parts = _Value.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                _NoOFLineForItemName = _parts.Length;

                if (_NoOFLineForItemName >= 1)
                {                   
                    _ItemNameArray = new string[_NoOFLineForItemName];
                    for (int i = 0; i < _NoOFLineForItemName; i++)
                    {
                        _ItemNameArray[i] = "";
                    }

                    for (int i = 0; i < _NoOFLineForItemName; i++)
                    {
                        _ItemNameArray[i] = _parts[i];
                    }
                }
            }

            //----------Compile Label info------------
            StringBuilder sb = new StringBuilder();
            int _LineTwo = 32;
            int _LineTwos = _LineTwo;

            sb.AppendLine("^XA");
            sb.AppendLine("^FO25," + _LineTwos + "^A0N,30^FD" + _ItemNameArray[0] + "^FS");
            if (_NoOFLineForItemName >= 2)
            {
                for (int i = 1; i < _NoOFLineForItemName; i++)
                {
                    _LineTwos = _LineTwo + (i * 32);
                    sb.AppendLine("^FO25," + _LineTwos + "^A0N,30^FD" + _ItemNameArray[i] + "^FS");
                    i = _NoOFLineForItemName + 1;
                }
            }

            //sb.AppendLine("^FO35,115^BY2^B3N,N,160,Y,N^FD" + CartBarcode + "^FS");   // Normal (stright)
            sb.AppendLine("^FO115,115^BY2^BCN,150^Y,N,N^FD" + CartBarcode + "^FS");   // Normal (stright)
            sb.AppendLine("^XZ");  // FO30,20^BY1,2.0^B3R,N,50,N,N^FD

            RawPrinterHelper.SendStringToPrinter(PrinterName, sb.ToString());
            return 1;
        }

        //---------------------------------------------Word Wrap---------------------------------------------------------
        static char[] splitChars = new char[] { ' ', '\t' };  // { ' ', '-', '\t' }; // split words based on this

        public static string WordWrap(string str, int width)
        {
            string[] words = Explode(str, splitChars);

            int curLineLength = 0;
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < words.Length; i += 1)
            {
                string word = words[i];
                // If adding the new word to the current line would be too long, 
                // then put it on a new line (and split it up if it's too long). 
                if (curLineLength + word.Length > width)
                {
                    // Only move down to a new line if we have text on the current line. 
                    // Avoids situation where wrapped whitespace causes emptylines in text. 
                    if (curLineLength > 0)
                    {
                        strBuilder.Append(Environment.NewLine);
                        curLineLength = 0;
                    }

                    // If the current word is too long to fit on a line even on it's own then 
                    // split the word up. 
                    while (word.Length > width)
                    {
                        strBuilder.Append(word.Substring(0, width - 1) + "-");
                        word = word.Substring(width - 1);
                        strBuilder.Append(Environment.NewLine);
                    }
                    // Remove leading whitespace from the word so the new line starts flush to the left. 
                    word = word.TrimStart();
                }
                strBuilder.Append(word);
                curLineLength += word.Length;
            }
            return strBuilder.ToString();
        }

        private static string[] Explode(string str, char[] splitChars)
        {
            List<string> parts = new List<string>();
            int startIndex = 0;
            while (true)
            {
                int index = str.IndexOfAny(splitChars, startIndex);

                if (index == -1)
                {
                    parts.Add(str.Substring(startIndex));
                    return parts.ToArray();
                }

                string word = str.Substring(startIndex, index - startIndex);
                char nextChar = str.Substring(index, 1)[0];
                // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to. 
                if (char.IsWhiteSpace(nextChar))
                {
                    parts.Add(word);
                    parts.Add(nextChar.ToString());
                }
                else
                {
                    parts.Add(word + nextChar);
                }
                startIndex = index + 1;
            }
        }
    }

    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document"; // "OPAS PRINTING" ;     // 
            di.pDataType = "RAW";  // "FOR ZEBRA PRINTER"; // 

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}

