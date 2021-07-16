using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }
        public bool? Status { get; set; }

        public int? PositionId { get; set; }
    }
}