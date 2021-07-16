using KKN_UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace KKN_UI.Helper
{
    public class UtilityHelper
    {
        public IconClassModel GetIconClassByFileType(string docTypeLowerCase)
        {
            IconClassModel result = new IconClassModel();

            switch (docTypeLowerCase.ToLower())
            {
                case "pdf":
                    result.IconClass = "fa fa-file-pdf-o txt-color-red";
                    result.CanReview = true;
                    break;
                case "docx":
                case "doc":
                    result.IconClass = "fa fa-file-word-o txt-color-blue";
                    result.CanReview = false;
                    break;
                case "png":
                case "jpg":
                case "jpeg":
                    result.IconClass = "fa fa-file-image-o txt-color-yellow";
                    result.CanReview = true;
                    break;
                case "xls":
                case "xlx":
                case "xlsx":
                    result.IconClass = "fa fa-file-excel-o txt-color-green";
                    result.CanReview = false;
                    break;             
                default:
                    result.IconClass = "fa fa-file-o";
                    result.CanReview = false;
                    break;
            }

            return result;
        }

        public FileReviewDocument GetFileForReview(string guId, string originalName, string type)
        {
            var result = new FileReviewDocument();
            DateTime now = DateTime.Now;
            var networkPath = ConfigurationSettings.AppSettings["ContractFilePath"].ToString();
            string fileLocation = now.Year.ToString();
            string newFileName = string.Format("{0}.{1}", guId, type);
            var pathName = System.IO.Path.Combine(Path.Combine(networkPath, fileLocation), newFileName);
            Byte[] bytes = System.IO.File.ReadAllBytes(pathName);

            FileReviewDocument file = new FileReviewDocument();           
            file.Filename = guId;
            file.OriginalName = originalName;         
            file.Type = type;
            file.base64 = fileData(type) + Convert.ToBase64String(bytes);
            switch (type)
            {
                case "pdf":
                    file.Class = "fa-file-pdf-o txt-color-red";
                    file.CanReview = true;
                    break;
                case "docx":
                case "doc":
                    file.Class = "fa-file-word-o txt-color-blue";
                    file.CanReview = false;
                    break;
                case "png":
                case "jpg":
                case "jpeg":
                    file.Class = "fa-file-image-o txt-color-yellow";
                    file.CanReview = true;
                    break;
                case "xlx":
                case "xlsx":
                    file.Class = "fa-file-excel-o txt-color-green";
                    file.CanReview = false;
                    break;
            }
            result = file;
            return result;
        }

        private string fileData(string type)
        {
            string result = "";
            switch (type)
            {
                case "pdf": result = "data:application/pdf;base64,"; break;
                case "docx":
                case "doc": result = "data:application/msword;base64,"; break;
                case "png": 
                case "jpg":
                case "jpeg": result = "data:image/jpeg;base64,"; break;
                case "xlx": 
                case "xlsx": result = "data:application/vnd.ms-excel;base64,"; break;
            }
            return result;
        }

        public class HelplerSort : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                string s1 = x as string;
                if (s1 == null)
                {
                    return 0;
                }
                string s2 = y as string;
                if (s2 == null)
                {
                    return 0;
                }

                int len1 = s1.Length;
                int len2 = s2.Length;
                int marker1 = 0;
                int marker2 = 0;

                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1];
                    char ch2 = s2[marker2];

                    char[] space1 = new char[len1];
                    int loc1 = 0;
                    char[] space2 = new char[len2];
                    int loc2 = 0;

                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                        {
                            ch1 = s1[marker1];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                        {
                            ch2 = s2[marker2];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    string str1 = new string(space1);
                    string str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                    {
                        result = str1.CompareTo(str2);
                    }

                    if (result != 0)
                    {
                        return result;
                    }
                }
                return len1 - len2;
            }
        }
    }
}