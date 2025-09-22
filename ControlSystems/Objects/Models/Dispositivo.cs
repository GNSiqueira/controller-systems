using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControlSystems.Objects.Models;

[Table("dispositivo")]
public class Dispositivo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("informacao")]
    [Required]
    public string? Informacao { get; set; }

    [Required]
    [Column("logado")]
    public bool Logado { get; set; }

    // Relacionamentos

    [Column("usuario_id")]
    public int UsuarioId { get; set; }

    [JsonIgnore]
    public Usuario? Usuario { get; set; }

    [JsonIgnore]
    public ICollection<LogUsuario>? LogsUsuario { get; set; }

    public Dispositivo() { }

    public Dispositivo(int id, string informacao, bool logado, int usuarioId)
    {
        Id = id;
        Informacao = informacao;
        Logado = logado;
        UsuarioId = usuarioId;
    }

}
