using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlSystems.Objects.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Objects.Models;

[Table("pagamento")]
public class Pagamento
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[Required]
	[Column("valor")]
	[Precision(10, 2)]
	public decimal Valor { get; set; }

	[Required]
	[Column("tipo")]
	public TipoPagamento Tipo { get; set; }

	[Required]
	[Column("statsu")]
	public StatusPagamento Status { get; set; }

	[Required]
	[Column("data_pagamento")]
	public DateTime DataPagamento { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACIONAMENTOS ==================
	public int AssinaturaId { get; set; }
	public Assinatura? Assinatura { get; set; }

	public Pagamento() { }

	public Pagamento(int id, decimal valor, TipoPagamento tipo, StatusPagamento status, DateTime dataPagamento, int assinaturaId, DateOnly created)
	{
		Id = id;
		Valor = valor;
		Tipo = tipo;
		Status = status;
		DataPagamento = dataPagamento;
		AssinaturaId = assinaturaId;
		Created = created;
	}


}

