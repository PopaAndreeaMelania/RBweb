namespace RBweb.Models.ViewModels
{
    public class CartItemVM
    {
        public int MeniuID { get; set; }
        public string Denumire { get; set; } = "";
        public decimal Pret { get; set; }
        public string? Imagine { get; set; }
        public int Cantitate { get; set; } = 1;
    }
}
