using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMarcaModelo.Models;

[Table("Marcas")]
public class MarcaModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Nome { get; set; }

    public ICollection<ModeloModel>? Modelos { get; set; }
}
