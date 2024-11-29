using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;


public class Pet{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Se requiere un nombre.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Se requiere un tipo de mascota.")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "Se requiere un genero.")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "Se requiere una fecha de nacimiento.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Pet), nameof(ValidateBirthDate))]
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }

    public bool IsActive { get; set; } = true;

    //foreign key of the owner
    [Required]
    public Guid UserId { get; set; }

    //Navigation proprety for the user
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User? user {get; set; }

    //Validation of the date of birth
    public static ValidationResult? ValidateBirthDate(DateTime birthDate, ValidationContext context){
        if (birthDate > DateTime.Today) return new ValidationResult("La fecha de nacimiento no puede ser en el futuro.");
        return ValidationResult.Success;
    }

    public class JsonDateConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "dd/MM/yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }
        throw new JsonException("Invalid date format.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}
}