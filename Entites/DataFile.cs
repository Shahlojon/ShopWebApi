namespace ShopApi.Entites;

public abstract class DataFile
{
    public string Name { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
}