using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Keyless]
public class Book
{
   
    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("authorLastName")]
    public string AuthorLastName { get; set; }

    [JsonPropertyName("authorFirstName")]
    public string AuthorFirstName { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
}

[Keyless]
public class AuthorList

{
     [JsonPropertyName("publisher")]
     [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Publisher { get; set; }

     [JsonPropertyName("author")]
    
    public string Author { get; set; }
     [JsonPropertyName("title")]
    public string Title { get; set; }

}