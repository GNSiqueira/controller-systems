using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ControlSystems.Objects.Enums;

namespace ControlSystems.Objects.Models;

[Table("plano")]
public class Plano
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("nome")]
	public string? Nome { get; set; }

	[Required]
	[Column("descricao")]
	public string? Descricao { get; set; }

	[Required]
	[Column("tipo")]
	public TipoPlano Tipo { get; set; }

	[Required]
	[Column("status")]
	public bool Status { get; set; }

	[Required]
	[Column("is_public")]
	public bool IsPublic { get; set; }

	[Required]
	[Column("intervalo_cobranca")]
	public TipoIntervalo IntervaloCobranca { get; set; }

	[Required]
	[Column("intervalo")]
	public int Intervalo { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACIONAMENTOS ==================
	[Column("sistema_id")]
	public int SistemaId { get; set; }

	[JsonIgnore]
	public Sistema? Sistema { get; set; }

	[JsonIgnore]
	public ICollection<Assinatura>? Assinatura { get; set; }

	[JsonIgnore]
	public ICollection<ItemPlano>? Itemplano { get; set; }

	public Plano() { }

	public Plano(int id, string nome, string descricao, TipoPlano tipo, bool status, bool isPublic, TipoIntervalo intervaloCobranca, int intervalo, DateOnly created, int sistemaId)
	{
		Id = id;
		Nome = nome;
		Descricao = descricao;
		Tipo = tipo;
		Status = status;
		IsPublic = isPublic;
		IntervaloCobranca = intervaloCobranca;
		Intervalo = intervalo;
		Created = created;
		SistemaId = sistemaId;
	}

}

