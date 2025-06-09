
using System.ComponentModel.DataAnnotations;

namespace LogicaNegocio.IntefacesDominio
{
    public interface IEntity
    {

        [Required]
        int Id {  get; set; }
    }
}
