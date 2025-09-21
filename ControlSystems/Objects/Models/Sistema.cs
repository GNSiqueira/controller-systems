using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControlSystems.Objects.Models;

[Table("sistema")]
public class Sistema
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("nome")]
	[StringLength(100, ErrorMessage = "O nome tem que ter no máximo 100 caracteres")]
	public string? Nome { get; set; }

	[Required]
	[Column("descricao")]
	public string? Descricao { get; set; }

	[Required]
	[Column("status")]
	public bool Status { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACIONAMENTOS ==================
	[JsonIgnore]
	public ICollection<Plano>? Planos { get; set; }

	public Sistema() { }

	public Sistema(int id, string nome, string descricao, bool status, DateOnly created)
	{
		Id = id;
		Nome = nome;
		Descricao = descricao;
		Status = status;
		Created = created;
	}
}

