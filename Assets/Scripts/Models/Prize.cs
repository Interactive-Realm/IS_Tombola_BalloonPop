using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Postgrest.Models;
using Postgrest.Attributes;

[Table("prizes")]
public class Prize : BaseModel
{
    [PrimaryKey("id")]
    public string Id { get; set; }

    [Column("prize_name")]
    public string PrizeName { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

}
