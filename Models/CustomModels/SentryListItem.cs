namespace DSM.UI.Api.Models.CustomModels
{
    public class SentryListItem
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public string Day { get; set; }
        public string Platform { get; set; }
        public string Cloud { get; set; }
        public string Network { get; set; }
        public string Security { get; set; }
        public string DB { get; set; }
        public string ApplicationManagement { get; set; }
        public string CyberSecurity { get; set; }

    }
}