using System;
using System.ComponentModel.DataAnnotations;

namespace ComprasVentas.Dto.auth;

public class AuthRequestDto
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
    public string Username {get; set;} = string.Empty;
    
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string Password {get; set;} = string.Empty;

}
