using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GameGenre> Genres { get; set; }

        public virtual ICollection<GamePlatform> Platforms { get; set; }

    }
}
