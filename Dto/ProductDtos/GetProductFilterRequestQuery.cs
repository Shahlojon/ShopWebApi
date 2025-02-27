namespace ShopApi.DTO.ProductDtos;

public class GetProductFilterRequestQuery
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int Page { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; }
}