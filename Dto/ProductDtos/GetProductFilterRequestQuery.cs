namespace ShopApi.DTO.ProductDtos;

public class GetProductFilterRequestQuery
{
    public string? Name { get; set; }
    public int? CategoryId { get; set; }
    public int Page { get; set; } = 1;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = string.Empty;
}