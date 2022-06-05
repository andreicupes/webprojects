namespace A9.Models
{
    public class LogReport
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Severity { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public string Log { get; set; }

    }
}
