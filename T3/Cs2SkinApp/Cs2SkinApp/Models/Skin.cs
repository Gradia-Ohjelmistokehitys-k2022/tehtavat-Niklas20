using System;

public class Skin
{
    public string Name { get; set; }
    public string Hash_Name { get; set; }
    public int Sell_Listings { get; set; }
    public int Sell_Price { get; set; }
    public string Sell_Price_Text { get; set; }
    public string App_Icon { get; set; }
    public string App_Name { get; set; }
    public AssetDescription Asset_Description { get; set; }
    public string Sale_Price_Text { get; set; }
}

public class AssetDescription
{
    public int App_id { get; set; }
    public string Class_id { get; set; }
    public string Instance_id { get; set; }
    public string Background_Color { get; set; }
    public string Icon_Url { get; set; }
    public int Tradable { get; set; }
    public string Name { get; set; }
    public string Name_Color { get; set; }
    public string Type { get; set; }
    public string Market_Name { get; set; }
    public string Market_Hash_Name { get; set; }
    public int Commodity { get; set; }
}

public class SearchResult
{
    public List<Skin> Results { get; set; }
}