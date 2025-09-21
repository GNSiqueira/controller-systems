using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControlSystems.Objects.Models;

[Table("log_usuario")]
public class LogUsuario
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("metrica_name")]
	public string? MetricaName { get; set; }

	[Required]
	[Column("quantidade_consumida")]
	public int QuantidadeConsumida { get; set; }

	[Required]
	[Column("data_evento")]
	public DateTime DataEvento { get; set; }

	// ================== RELACIONAMENTOS ==================
	[Column("usuario_id")]
	public int UsuarioId { get; set; }

	[JsonIgnore]
	public Usuario? Usuario { get; set; }

	public LogUsuario() { }

	public LogUsuario(int id, string metricaName, int quantidadeConsumida, DateTime dataEvento, int usuarioId)
	{
		Id = id;
		MetricaName = metricaName;
		QuantidadeConsumida = quantidadeConsumida;
		DataEvento = dataEvento;
		UsuarioId = usuarioId;
	}
}

