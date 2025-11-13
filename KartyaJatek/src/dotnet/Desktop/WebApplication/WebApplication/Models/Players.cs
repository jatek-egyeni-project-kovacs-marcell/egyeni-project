using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication.Models;

public class Players
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
}