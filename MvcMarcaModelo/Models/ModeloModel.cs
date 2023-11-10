using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMarcaModelo.Models;

[Table("Modelos")]
public class ModeloModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Nome { get; set; }

    [ForeignKey("Marca")]
    public int MarcaId { get; set; }
    public MarcaModel? Marca { get; set;}
}
