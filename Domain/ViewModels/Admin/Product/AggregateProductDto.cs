namespace Domain.ViewModels.Admin.Product
{
    public record AggregateProductsDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImageName { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string ColorId { get; set; }
        public string ColorTitle { get; set; }
        public string SizeId{ get; set; }
        public string SizeTitle { get; set; }
    }
}
