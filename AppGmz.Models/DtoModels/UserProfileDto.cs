using System.Collections.Generic;

namespace AppGmz.Models.DtoModels
{
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> ListRoles { get; set; }
    }
}