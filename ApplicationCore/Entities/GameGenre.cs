using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class GameGenre
    {
        public int Id { get; set; }
        public Guid GameId { get; set; }
        public Guid GenreId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
