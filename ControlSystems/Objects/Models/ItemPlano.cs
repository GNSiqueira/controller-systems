using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ControlSystems.Objects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControlSystems.Objects.Models;

[Table("item_plano")]
public class ItemPlano
{
	[Key]
	[Column("id")]
	[Required]
	public int Id { get; set; }

	[StringLength(100, ErrorMessage = "Limite de 100 caracteres atingido!")]
	[Column("metrica")]
	public string Metrica { get; set; }

	[Required]
	[Column("tipo")]
	public TipoItemPlano Tipo { get; set; }

	[Column("limite")]
	public int Limite { get; set; }

	[Column("descricao")]
	public string? Descricao { get; set; }

	[Required]
	[Column("fixo")]
	public YesNo Fixo { get; set; }

	[Required]
	[Column("valor")]
	[Precision(10, 2)]
	public decimal Valor { get; set; }

	[Required]
	[Column("moeda")]
	[StringLength(3, ErrorMessage = "O campo tem que ter no máximo 3 caracteres.")]
	public string? Moeda { get; set; }

	
	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }
	// ================== RELACIONAMENTOS ==================
	[Column("plano_id")]
	public int PlanoId { get; set; }

	[JsonIgnore]
	public Plano? Plano { get; set; }

	public ItemPlano() { }

	public ItemPlano(int id, string metrica, TipoItemPlano tipo, int limite, string descricao, YesNo fixo, decimal valor, string moeda, int planoId, DateOnly created)
	{
		Id = id;
		Metrica = metrica;
		Tipo = tipo;
		Limite = limite;
		Descricao = descricao;
		Fixo = fixo;
		Valor = valor;
		Moeda = moeda;
		PlanoId = planoId;
		Created = created; 
	}
}

