using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ControlSystems.Objects.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Objects.Models;

[Table("assinatura")]
public class Assinatura
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("status")]
	public StatusAssinatura Status { get; set; }

	[Required]
	[Column("data_inicio")]
	public DateOnly DataInicio { get; set; }

	[Column("data_fim")]
	public DateOnly? DataFim { get; set; }

	[Column("data_cancelamento")]
	public DateOnly? DataCancelamento { get; set; }

	[Required]
	[Precision(10, 2)]
	[Column("valor")]
	public decimal Valor { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACINAMENTOS ==================
	[Column("plano_id")]
	public int PlanoId { get; set; }

	[JsonIgnore]
	public Plano? Plano { get; set; }

	[Column("empresa_id")]
	public int EmpresaId { get; set; }

	[JsonIgnore]
	public Empresa? Empresa { get; set; }

	[JsonIgnore]
	public ICollection<Pagamento>? Pagamentos { get; set; }

	public Assinatura() { }

	public Assinatura(StatusAssinatura status, DateOnly dataInicio, decimal valor, int planoId, int empresaId, DateOnly created)
	{
		Status = status;
		DataInicio = dataInicio;
		Valor = valor;
		PlanoId = planoId;
		EmpresaId = empresaId;
		Created = created;
	}
}