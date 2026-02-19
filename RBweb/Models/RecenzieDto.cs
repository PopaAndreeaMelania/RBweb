namespace RBweb.Models
{
    public class RecenzieDto
    {
        public int ComandaID { get; set; }
        public string UserName { get; set; } = "";
        public int Rating { get; set; }
        public string Comentariu { get; set; } = "";
        public DateTime DataCreare { get; set; }
    }
}