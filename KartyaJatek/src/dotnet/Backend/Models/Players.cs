using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApp.Backend.Models;

public class Players
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string UserName { get; set; }
    public int Score { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}