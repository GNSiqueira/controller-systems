using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ControlSystems.Objects.Enums;

namespace ControlSystems.Objects.Models;

[Table("usuario")]
public class Usuario
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("nome")]
	public string? Nome { get; set; }

	[Required]
	[Column("email")]
	public string? Email { get; set; }

	[Required]
	[Column("password")]
	public string? Password { get; set; }

	[Required]
	[Column("tipo_usuario")]
	public TipoUsuario TipoUsuario { get; set; }

	[Required]
	[Column("status")]
	public bool Status { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACIONAMENTOS ==================
	[Column("empresa_id")]
	public int EmpresaId { get; set; }

	[JsonIgnore]
	public Empresa? Empresa { get; set; }

	[JsonIgnore]
	public ICollection<LogUsuario>? LogsUsuario { get; set; }

	public Usuario() { }

	public Usuario(int id, string nome, string email, string password, TipoUsuario tipoUsuario, bool status, DateOnly created, int empresaId)
	{
		Id = id;
		Nome = nome;
		Email = email;
		Password = password;
		TipoUsuario = tipoUsuario;
		Status = status;
		Created = created;
		EmpresaId = empresaId;
	}

}

