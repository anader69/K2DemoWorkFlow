using System;

namespace Framework.Identity.Data.Dtos
{
    public class UserSearchDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
