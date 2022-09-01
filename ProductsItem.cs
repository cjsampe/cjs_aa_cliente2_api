namespace cjs_aa_cliente2_api{

    public class ProductsItem
{
     public int id { get; set; }
    public string name { get; set; }
    public string category { get; set; }
    public string short_d { get; set; }
    public string description { get; set; }
    public string price { get; set; }     // es string en backmock, ejemplo:"price": "344.00"
    public string mainImage { get; set; }
    public int disccount { get; set; }
    public ICollection<ProductImageItem> images { get; set; }
}

}