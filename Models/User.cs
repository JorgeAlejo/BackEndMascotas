using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;

    //Navigation proprety for the pets of the owner
    [JsonIgnore]
    public List<Pet> Pets { get; set; } = new List<Pet>();
}