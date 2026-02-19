namespace RBmaui.Models
{
    public class ComandaAfisare
    {
        public int Id { get; set; }
        public string NumarComanda { get; set; } = "";
        public DateTime DataComanda { get; set; }
        public string Status { get; set; } = "";
        public decimal Total { get; set; }

        public int NrStrigat { get; set; }   // nr  ordine
    }
}
