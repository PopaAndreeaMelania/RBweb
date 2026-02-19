namespace RBmaui.Models
{
    public class Meniu
    {
        public int Id { get; set; }
        public string Denumire { get; set; } = "";
        public decimal Pret { get; set; }
        public string? Imagine { get; set; }
        public string? Ingrediente { get; set; }
    }
}
