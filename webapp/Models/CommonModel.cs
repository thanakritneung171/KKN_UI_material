using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
{
    public class IconClassModel
    {
        public string IconClass { get; set; }
        public bool CanReview { get; set; }
    }

    public class FileReviewDocument
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string OriginalName { get; set; }
        public long FileSize { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool CanReview { get; set; }
        public string base64 { get; set; }
    }
}