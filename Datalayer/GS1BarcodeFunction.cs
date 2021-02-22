using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Datalayer;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace Datalayer
{
    public class GS1BarcodeFunction
    {
        // GS1 Barcode Declaration
        string SymbolIdentifier = "";
        string GTIN = "";
        int rtnValue = 0;
        string Format = "",SI;
        string ExpDate = "", BatchNumber = "", SerialNumber = "", ManufacturedDate = "";
    
        // GS1 Barcode AI
        string AI_GTIN = "01";
        string AI_BATCH_NUMBER = "10";
        string AI_MFR_PRODUCTION = "11";
        string AI_DATE_EXPIRATION = "17";
        string AI_SERIAL_NUMBER = "21";

        string AI_GTIN_FORMAT = "(01)";
        string AI_BATCH_NUMBER_FORMAT = "(10)";
        string AI_MFR_PRODUCTION_FORMAT = "(11)";
        string AI_DATE_EXPIRATION_FORMAT = "(17)";
        string AI_SERIAL_NUMBER_FORMAT = "(21)";


        public int GS1CodeCheck(string Barcode, ref string Expdate, ref string Btchnumber, ref string SrialNumber, ref string MfrDate)
        {
            string BarcodeFNC1 = "", BarcodeFNC2 = "";

            try
            {
                SymbolIdentifier = Barcode.Substring(0, 3);
                if ((SymbolIdentifier == "]E0") || (SymbolIdentifier == "]E1") || (SymbolIdentifier == "]E2") || (SymbolIdentifier == "]E3") || (SymbolIdentifier == "]E4") ||
                         (SymbolIdentifier == "]I1") || (SymbolIdentifier == "]C1") || (SymbolIdentifier == "]e0") || (SymbolIdentifier == "]e1") || (SymbolIdentifier == "]e2") ||
                         (SymbolIdentifier == "]d1") || (SymbolIdentifier == "]d2") || (SymbolIdentifier == "]Q3"))
                {

                    if (SymbolIdentifier == "]E0")
                    {
                        Format = "EAN-13,UPC-A,UPC-E";
                    }
                    else if (SymbolIdentifier == "]E1")
                    {
                        Format = "Two-digit Add-On Symbol";
                    }
                    else if (SymbolIdentifier == "]E2")
                    {
                        Format = "Five-digit Add-On Symbol";
                    }
                    else if (SymbolIdentifier == "]E3")
                    {
                        Format = "EAN-13,UPC-A,UPC-E with Add-On Symbol";
                    }
                    else if (SymbolIdentifier == "]E4")
                    {
                        Format = "EAN-8";
                    }
                    else if (SymbolIdentifier == "]I1")
                    {
                        Format = "ITF-14";
                    }
                    else if (SymbolIdentifier == "]C1")
                    {
                        Format = "GS1-128";
                    }
                    else if (SymbolIdentifier == "]e0")
                    {
                        Format = "GS1-Databar";
                    }
                    else if (SymbolIdentifier == "]e1")
                    {
                        Format = "GS1-Composite";
                    }
                    else if (SymbolIdentifier == "]e2")
                    {
                        Format = "GS1-Composite";
                    }
                    else if (SymbolIdentifier == "]d1")
                    {
                        Format = "ISO DataMatrix";
                    }
                    else if (SymbolIdentifier == "]d2")
                    {
                        Format = "GS1 DataMatrix";
                    }
                    else if (SymbolIdentifier == "]Q3")
                    {
                        Format = "GS1 QR Code";
                    }

                    if (Barcode.Substring(3, 2) == AI_GTIN)
                    {
                        GTIN = Barcode.Substring(5, 14);

                        BarcodeFNC1 = Barcode.Substring(19);
                        BarcodeFNC2 = Barcode.Remove(0, 19);
                        if (BarcodeFNC2 != "")
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                int blenght1 = BarcodeFNC2.Length;
                                if (blenght1 > 0)
                                {
                                    #region ExpiryDate
                                    if (BarcodeFNC2.Substring(0, 2) == AI_DATE_EXPIRATION)
                                    {
                                        ExpiryDate(BarcodeFNC2.Substring(2), ref Expdate);

                                        BarcodeFNC2 = BarcodeFNC2.Remove(0, 8);
                                    }
                                    #endregion
                                    #region BatchNumber
                                    else if (BarcodeFNC2.Substring(0, 2) == AI_BATCH_NUMBER)
                                    {
                                        Batchnumber(BarcodeFNC2.Substring(2), ref Btchnumber);
                                        if (BarcodeFNC2.Contains('['))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf(']') + 1);
                                        }
                                        else if (BarcodeFNC2.Contains('<'))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf('>') + 1);
                                        }
                                        else if (BarcodeFNC2.Contains('{'))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf('}') + 1);
                                        }
                                        else
                                        {
                                            int blenght = BarcodeFNC2.Length;
                                            if (blenght < 20)
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, blenght);
                                            }
                                            else
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, 20);
                                            }
                                        }

                                    }
                                    #endregion
                                    #region MFRDate
                                    else if (BarcodeFNC2.Substring(0, 2) == AI_MFR_PRODUCTION)
                                    {
                                        MFRDate(BarcodeFNC2.Substring(2), ref MfrDate);

                                        BarcodeFNC2 = BarcodeFNC2.Remove(0, 8);
                                    }
                                    #endregion
                                    #region SerialNumber
                                    else if (BarcodeFNC2.Substring(0, 2) == AI_SERIAL_NUMBER)
                                    {
                                        Serialnumber(BarcodeFNC2.Substring(2), ref SrialNumber);
                                        if (BarcodeFNC2.Contains('['))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf(']') + 1);
                                        }
                                        else if (BarcodeFNC2.Contains('<'))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf('>') + 1);
                                        }
                                        else if (BarcodeFNC2.Contains('{'))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, BarcodeFNC2.IndexOf('}') + 1);
                                        }
                                        else
                                        {
                                            int blenght = BarcodeFNC2.Length;
                                            if (blenght < 20)
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, blenght);
                                            }
                                            else
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, 20);
                                            }
                                        }
                                    }
                                    #endregion

                                }

                            }
                        }
                        else
                        {
                            return rtnValue = 1;
                        }
                    }
                    else if (Barcode.Substring(3, 4) == AI_GTIN_FORMAT)
                    {
                        GTIN = Barcode.Substring(7, 14);

                        BarcodeFNC1 = Barcode.Substring(21);
                        BarcodeFNC2 = Barcode.Remove(0, 21);
                        if (BarcodeFNC2 != "")
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                int blenght1 = BarcodeFNC2.Length;
                                if (blenght1 > 0)
                                {
                                    #region ExpiryDate
                                    if (BarcodeFNC2.Substring(0, 4) == AI_DATE_EXPIRATION_FORMAT)
                                    {
                                        ExpiryDate(BarcodeFNC2.Substring(4), ref Expdate);

                                        BarcodeFNC2 = BarcodeFNC2.Remove(0, 10);
                                    }
                                    #endregion
                                    #region BatchNumber
                                    else if (BarcodeFNC2.Substring(0, 4) == AI_BATCH_NUMBER_FORMAT)
                                    {
                                        Batchnumber(BarcodeFNC2.Substring(4), ref Btchnumber);
                                        int batchNumLength = Btchnumber.Length + 4;
                                        if (BarcodeFNC2.Contains('('))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, batchNumLength);
                                        }
                                        else
                                        {
                                            int blenght = BarcodeFNC2.Length;
                                            if (blenght < 20)
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, blenght);
                                            }
                                            else
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, 20);
                                            }
                                        }

                                    }
                                    #endregion
                                    #region MFRDate
                                    else if (BarcodeFNC2.Substring(0, 4) == AI_MFR_PRODUCTION_FORMAT)
                                    {
                                        MFRDate(BarcodeFNC2.Substring(4), ref MfrDate);

                                        BarcodeFNC2 = BarcodeFNC2.Remove(0, 10);
                                    }
                                    #endregion
                                    #region SerialNumber
                                    else if (BarcodeFNC2.Substring(0, 4) == AI_SERIAL_NUMBER_FORMAT)
                                    {
                                        Serialnumber(BarcodeFNC2.Substring(4), ref SrialNumber);
                                        int serialNumLength = SrialNumber.Length + 4;
                                        if (BarcodeFNC2.Contains('('))
                                        {
                                            BarcodeFNC2 = BarcodeFNC2.Remove(0, serialNumLength);
                                        }
                                        else
                                        {
                                            int blenght = BarcodeFNC2.Length;
                                            if (blenght < 20)
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, blenght);
                                            }
                                            else
                                            {
                                                BarcodeFNC2 = BarcodeFNC2.Remove(0, 20);
                                            }
                                        }
                                    }
                                    #endregion

                                }

                            }
                        }
                        else
                        {
                            return rtnValue = 1;
                        }
                    }
                    else
                    {
                        return rtnValue = 2;
                    }
                }

                else
                {
                    return rtnValue = 3;
                }

            }
            catch (Exception ex)
            {


            }
            return rtnValue = 0;

        }

        public string ExpiryDate(string Barcode, ref string ExpDate)
        {
            string year = "", Month = "", Date = "", Date1 = "", Expdate1 = "";

            int yer = 0, Mon = 0, Day = 0;
            try
            {
                int blenght = Barcode.Length;
                if (blenght >= 6)
                {
                    year = Barcode.Substring(0, 2);
                    yer = Convert.ToInt32(year);
                    Month = Barcode.Substring(2, 2);
                    Mon = Convert.ToInt32(Month);
                    Date = Barcode.Substring(4, 2);
                    Day = Convert.ToInt32(Date);
                    if ((Mon < 13) && (Day < 32))
                    {
                        if (Date == "00")
                        {
                            Date1 = DateTime.DaysInMonth(yer, Mon).ToString();
                            Expdate1 = Date1 + "/" + Month + "/" + year;
                        }
                        else
                        {
                            Expdate1 = Date + "/" + Month + "/" + year;
                        }

                        DateTime dt = DateTime.ParseExact(Expdate1, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
                        ExpDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ExpDate = "Invalid Expiry Date";
                    }


                }

            }
            catch (Exception ex)
            {

            }
            return ExpDate;
        }
        public string Batchnumber(string Barcode, ref string BatchNumber)
        {
            try
            {
                if (Barcode.Contains('('))
                {
                    BatchNumber = Barcode.Substring(0, Barcode.IndexOf('('));
                }
                else if (Barcode.Contains('['))
                {
                    BatchNumber = Barcode.Substring(0, Barcode.IndexOf('['));
                }
                else if (Barcode.Contains('<'))
                {
                    BatchNumber = Barcode.Substring(0, Barcode.IndexOf('<'));
                }
                else if (Barcode.Contains('{'))
                {
                    BatchNumber = Barcode.Substring(0, Barcode.IndexOf('{'));
                }
                else
                {
                    int blenght = Barcode.Length;
                    if (blenght < 20)
                    {
                        BatchNumber = Barcode.Substring(0, blenght);
                    }
                    else
                    {
                        BatchNumber = Barcode.Substring(0, 20);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return BatchNumber;
        }
        public string MFRDate(string Barcode, ref string ManufacturedDate)
        {
            string MFRYear = "", MFRMonth = "", ManufacturedDate1 = "", MfrDate = "", MfrDate1 = "";
            int y1 = 0, M1 = 0, D1 = 0;
            try
            {
                int blenght = Barcode.Length;
                if (blenght >= 6)
                {
                    MFRYear = Barcode.Substring(0, 2);
                    y1 = Convert.ToInt32(MFRYear);
                    MFRMonth = Barcode.Substring(2, 2);
                    M1 = Convert.ToInt32(MFRMonth);
                    MfrDate = Barcode.Substring(4, 2);
                    D1 = Convert.ToInt32(MfrDate);
                    if ((M1 < 13) && (D1 < 32))
                    {
                        if (MfrDate == "00")
                        {
                            MfrDate1 = DateTime.DaysInMonth(y1, M1).ToString();
                            ManufacturedDate1 = MfrDate1 + "/" + MFRMonth + "/" + MFRYear;
                        }
                        else
                        {
                            ManufacturedDate1 = MfrDate + "/" + MFRMonth + "/" + MFRYear;
                        }
                        DateTime dt = DateTime.ParseExact(ManufacturedDate1, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
                        ManufacturedDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ManufacturedDate = "Invalid MFR Date";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ManufacturedDate;
        }
        public string Serialnumber(string Barcode, ref string SerialNumber)
        {
            try
            {
                if (Barcode.Contains('('))
                {
                    SerialNumber = Barcode.Substring(0, Barcode.IndexOf('('));
                }
                else if (Barcode.Contains('['))
                {
                    SerialNumber = Barcode.Substring(0, Barcode.IndexOf('['));
                }
                else if (Barcode.Contains('<'))
                {
                    SerialNumber = Barcode.Substring(0, Barcode.IndexOf('<'));
                }
                else if (Barcode.Contains('{'))
                {
                    SerialNumber = Barcode.Substring(0, Barcode.IndexOf('{'));
                }
                else
                {
                    int blenght = Barcode.Length;
                    if (blenght < 20)
                    {
                        SerialNumber = Barcode.Substring(0, blenght);
                    }
                    else
                    {
                        SerialNumber = Barcode.Substring(0, 20);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return SerialNumber;
        }
    }
}
