using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class SchoolInfo
{
    [Required(ErrorMessage = "Please enter school address.", AllowEmptyStrings = false)]
    public required string SchoolAddress { get; set; }
    [Required(ErrorMessage = "Please enter school contact number.", AllowEmptyStrings = false)]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number.")]
    public required string SchoolContactNumber { get; set; }
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter a valid name.")]
    [Required(ErrorMessage = "Please enter principal name.")]
    public required string PrincipalName { get; set; }
    [RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "Please enter a valid qualification.")]
    [Required(ErrorMessage = "Please enter principal qualification.")]
    public required string PrincipalQualification { get; set; }
}