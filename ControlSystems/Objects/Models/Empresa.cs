using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ControlSystems.Objects.Enums;

namespace ControlSystems.Objects.Models;

[Table("empresa")]
public class Empresa
{
	[Key]
	[Column("id")]
	public int Id { get; set;}

	[Required]
	[Column("status")]
	public YesNo Status { get; set; }

	[Required]
	[Column("cnpj_cpf")]
	public string? CnpjCpf { get; set; }

	[Required]
	[Column("nome_fantasia")]
	public string? NomeFantasia { get; set; }

	[Required]
	[Column("razao_social")]
	public string? RazaoSocial { get; set; }

	[Required]
	[Column("telefone")]
	public string? Telefone { get; set; }

	[Required]
	[Column("email")]
	public string? Email { get; set; }

	[Required]
	[Column("created")]
	public DateOnly Created { get; set; }

	// ================== RELACIONAMENTOS ==================
	[JsonIgnore]
	public ICollection<Assinatura>? Assinaturas { get; set; }

	[JsonIgnore]
	public ICollection<Usuario>? Usuarios { get; set;}

	public Empresa() {}

	public Empresa(int id, YesNo status, string cnpjCpf, string nameFantasia, string razaoSocial, string telefone, string email, DateOnly created)
	{
		Id = id;
		Status = status;
		CnpjCpf = cnpjCpf;
		NomeFantasia = nameFantasia;
		RazaoSocial = razaoSocial;
		Telefone = telefone;
		Email = email;
		Created = created;
	}

}

