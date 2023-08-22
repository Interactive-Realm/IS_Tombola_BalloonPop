using Postgrest.Models;
using Postgrest.Attributes;

[Table("prices")]
public class Price : BaseModel
{
    [PrimaryKey("id")]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    [Column("chance")]
    public int Chance { get; set; }

}
