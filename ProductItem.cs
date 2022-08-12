namespace cjs_aa_cliente2_api{

    public class ProductItem
{
     public int id { get; set; }
    public string? name { get; set; }
    public string? category { get; set; }
    public string? short_d { get; set; }
    public string? description { get; set; }
    public string? price { get; set; } // string en mock
    public string? mainImage { get; set; }
    public int disccount { get; set; }
    //como son varias ¿vale así?
    public int images { get; set; }
}

}