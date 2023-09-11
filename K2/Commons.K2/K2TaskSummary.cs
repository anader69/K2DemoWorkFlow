namespace Commons.K2
{
    /// <summary>
    /// K2 Task Summary
    /// </summary>
    public class K2TaskSummary
    {
        public string UserName { get; set; }
        public string ProcessName { get; set; }
        public DateTime StartDate { get; set; }
        public string Folio { get; set; }
        public string ActivityName { get; set; }
        public string RoleKey { get; set; }

        public Guid TeamId { get; set; }
        public string SN { get; set; }
    }

}
