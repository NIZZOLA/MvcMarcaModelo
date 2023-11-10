using System.ComponentModel;

namespace MvcMarcaModelo.Models
{
    public class MarcaModeloViewModel
    {
        [DisplayName("Marca")]
        public int MarcaId { get; set; }
        [DisplayName("Modelo")]
        public int ModeloId { get; set; }
    }
}
