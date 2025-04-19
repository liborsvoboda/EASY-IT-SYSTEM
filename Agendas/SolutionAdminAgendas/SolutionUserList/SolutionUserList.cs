using System;
using System.ComponentModel.DataAnnotations;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionUserList {

        public int Id { get; set; } = 0;
        public int RoleId { get; set; }
        public int RoleAccessValue { get; set; }
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
        public string Name { get; set; } = null;
        public string Email { get; set; } = null;
        public string Surname { get; set; } = null;
        public string Description { get; set; } = null;
        public bool Active { get; set; } = false;
        public DateTime? TimeStamp { get; set; } = null;
        public string AccessToken { get; set; } = null;
        public DateTime? Expiration { get; set; } = null;
        public byte[] Photo { get; set; } = null;
        public string PhotoMimeType { get; set; } = null;
        public string PhotoPath { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string UserDbPreffix { get; set; } = null;

        public string Translation { get; set; } = null;
    }
}