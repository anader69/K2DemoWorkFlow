using System;

namespace Framework.Identity.Data.Dtos
{
    public class UserManagementInsertResultDto
    {
        public Guid? InsertedId { get; set; }
        public string VerificationMSG { get; set; }
    }
}
