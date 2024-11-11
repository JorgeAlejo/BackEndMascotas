using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public DateTime BirthDate { get; set; }

    public bool IsActive { get; set; } = true;

    //foreign key of the owner
    [Required]
    public Guid UserId { get; set; }

    //Navigation proprety for the user
    [ForeignKey("UserId")]
    public User user {get; set; } = null!;

    //Validation of the date of birth
    public static ValidationResult? ValidateBirthDate(DateTime birthDate, ValidationContext context){
        if (birthDate > DateTime.Today) return new ValidationResult("La fecha de nacimiento no puede ser en el futuro.");
        return ValidationResult.Success;
    }
}