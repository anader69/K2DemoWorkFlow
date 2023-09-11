// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemSetting.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Framework.Core.Data;

namespace Framework.Core.SharedServices.Entities
{
    public class SystemSetting : FullAuditedEntityBase<int>
    {
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public bool IsSecure { get; set; }
        public bool IsSticky { get; set; }
        public bool IsActive { get; set; }
    }
}