using System.Security.Cryptography;
using ZXing.QrCode;
using ZXing;
using System.Drawing;
using Microsoft.VisualBasic;

namespace LabManagement.Helpers
{
    public class Helper
    {
        public static string EncodePassword(string originalPassword)
        {
            byte[] originalBytes;
            byte[] encodedBytes;

            var vmd5 = new MD5CryptoServiceProvider();
            originalBytes = System.Text.Encoding.Default.GetBytes(originalPassword);
            encodedBytes = vmd5.ComputeHash(originalBytes);

            string encodePass = BitConverter.ToString(encodedBytes);
            return encodePass.Replace("-", "").ToUpper();
        }
        public static async Task<string> GenerateBarcode(string data,BarcodeFormat format,int W, int H)
        {
            string res = "";
            var barCodeData = new BarcodeWriterPixelData
            {
                Format = format,
                Options = new QrCodeEncodingOptions
                {
                    Height = H,
                    Width = W,
                    Margin = 1,
                    PureBarcode=false,
                    CharacterSet = data
                }
            };

            var pixelData = barCodeData.Write(data);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                using (var memoryStream = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                        System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);

                    bitmap.UnlockBits(bitmapData);
                    bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    res = "data:image/png;base64, " + Convert.ToBase64String(memoryStream.ToArray());
                    //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/barcode.png");

                    //await File.WriteAllBytesAsync(path, memoryStream.ToArray());
                }
            }
            return res;
        }

        public static int InArray(string[] Arr, string t)
        {
            var res = -1;
            for (int i = 0; i <Arr.Length; i++)
            {
                if (Arr[i] == t)
                    res = i;
            }
            return res;
        }

        public static string ChangeTeeth(string t)
        {
            if (t == "")
                return "NEED";
            try
            {
                string[] arUSA;
                string[] arEUR;
                string strUSA, strEUR;

                strUSA = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32";
                strEUR = "18,17,16,15,14,13,12,11,21,22,23,24,25,26,27,28,38,37,36,35,34,33,32,31,41,42,43,44,45,46,47,48";

                arUSA = strUSA.Split(',');
                arEUR = strEUR.Split(',');

                int index;
                string ch;
                string nextch;
                string teeth;
                string EURTeeth;

                string Res = "";
                index = 1;

                while (index <= Strings.Len(t))
                {
                    ch = Strings.Mid(t, index, 1);
                    if (Information.IsNumeric(ch))
                    {
                        nextch = Strings.Mid(t, index + 1, 1);
                        if (Information.IsNumeric(nextch))
                        {
                            teeth = ch + nextch;

                            EURTeeth = arEUR[InArray(arUSA, Convert.ToInt32(teeth).ToString())];
                            Res = Res + EURTeeth;
                            index = index + 2;
                        }
                        else
                        {
                            teeth = ch;
                            EURTeeth = arEUR[InArray(arUSA, teeth)];
                            Res = Res + EURTeeth;
                            index = index + 1;
                        }
                    }
                    else
                    {
                        if (ch == ",")
                            Res = Res + ch + " ";
                        else
                            Res = Res + ch;
                        index = index + 1;
                    }
                }
                return Res;
            }
            catch (Exception ex)
            {
                return "NEED";
            }
        }


        public static int CountAllTeeth(string TeethUS)
        {
            int countTeeth = 0;

            string listTeeth = ",1,01,2,02,3,03,4,04,5,05,6,06,7,07,8,08,9,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,UPPER,LOWER,";

            if (TeethUS.Trim().IndexOf(",") <= 0 & TeethUS.Trim().IndexOf("-") <= 0 & TeethUS.Trim().IndexOf("=") <= 0)
            {
                if (TeethUS.IndexOf("X") > 0)
                {
                    string tech1 = TeethUS.Substring(0, TeethUS.IndexOf("X"));
                    string tech2 = TeethUS.Substring(TeethUS.IndexOf("X") + 1, TeethUS.Length - TeethUS.IndexOf("X") - 1);
                    if (listTeeth.Contains("," + tech1 + ",") == true)
                        countTeeth += 1;
                    if (listTeeth.Contains("," + tech2 + ",") == true)
                        countTeeth += 1;
                }
                else if (listTeeth.Contains("," + TeethUS.Trim() + ",") == true)
                    return 1;
            }


            if (TeethUS.IndexOf("-") <= 0)
            {
                if (TeethUS.IndexOf("=") > 0)
                {
                    if (TeethUS.IndexOf("X") > 0)
                    {
                        string modifyTeeth = TeethUS.Replace("=", ",");
                        string[] arr = modifyTeeth.Split(",");
                        if (arr.Length > 0)
                        {
                            for (int i = 0; i <= arr.Length - 1; i++)
                            {
                                if (listTeeth.Contains("," + arr[i].Trim() + ",") == true)
                                    countTeeth += 1;
                            }
                        }
                    }
                    else
                    {
                        string modifyTeeth = TeethUS.Replace("=", "-"); // 3=5 replace 3-5
                        countTeeth += CountAllTeeth(modifyTeeth);
                    }
                }
                else
                {
                    // 3,4,5

                    string modifyTeeth = TeethUS.Replace("=", ",");
                    string[] arr = modifyTeeth.Split(",");
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i <= arr.Length - 1; i++)
                        {
                            if (listTeeth.Contains("," + arr[i].Trim() + ",") == true)
                                countTeeth += 1;
                        }
                    }
                }
            }
            else if (TeethUS.IndexOf("=") > 0 | TeethUS.IndexOf(",") > 0)
            {
                string modifyTeeth = TeethUS.Replace("=", ",");
                string[] arr = modifyTeeth.Split(",");
                for (int i = 0; i <= arr.Length - 1; i++)
                    countTeeth += CountAllTeeth(arr[i].Trim());
            }
            else if (TeethUS.IndexOf("X") > 0)
            {
                // 13-X-15'
                string modifyTeeth = TeethUS.Replace("-", ",");
                string[] arr = modifyTeeth.Split(",");
                for (int i = 0; i <= arr.Length - 1; i++)
                    countTeeth += CountAllTeeth(arr[i].Trim());
            }
            else
            {

                // 13-15'
                int countChar = TeethUS.Split("-").Length - 1;
                if (countChar == 1)
                {
                    // 13-15'
                    string[] arr = TeethUS.Split("-");
                    try
                    {
                        int iStart = int.Parse(arr[0]);
                        int iEnd = int.Parse(arr[1]);
                        if (iStart < iEnd)
                        {
                            for (int i = iStart; i <= iEnd; i++)
                            {
                                if (listTeeth.Contains("," + i + ",") == true)
                                    countTeeth += 1;
                            }
                        }
                        else
                            for (int i = iEnd; i <= iStart; i++)
                            {
                                if (listTeeth.Contains("," + i + ",") == true)
                                    countTeeth += 1;
                            }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    string modifyTeeth = TeethUS.Replace("-", ",");
                    string[] arr = modifyTeeth.Split(",");
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i <= arr.Length - 1; i++)
                        {
                            if (listTeeth.Contains("," + arr[i].Trim() + ",") == true)
                                countTeeth += 1;
                        }
                    }
                }
            }

            return countTeeth;
        }



    }
}
