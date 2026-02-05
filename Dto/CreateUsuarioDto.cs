using System;
using System.ComponentModel.DataAnnotations;

namespace ComprasVentas.Dto;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
    [StringLength(50, ErrorMessage = "El correo no puede superar los 50 caracteres")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "La contraseña debe incluir mayúscula, minúscula, número y carácter especial"
    )]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    [RegularExpression("^(Masculino|Femenino|Otro)$", 
        ErrorMessage = "El género debe ser Masculino, Femenino u Otro")]
    public string? Genero { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "La nacionalidad es obligatoria")]
    [StringLength(50, ErrorMessage = "La nacionalidad no puede superar los 50 caracteres")]
    public string? Nacionalidad { get; set; }
    
    public List<int> RolesIds {get; set;}=[];
}
