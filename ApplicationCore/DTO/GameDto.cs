using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class GameDto
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Key")]
        public string Key { get; set; }
    }

    public class GameRequestDto
    {
        public GameDto Game { get; set; }
        public List<Guid> Genres { get; set; }
        public List<Guid> Platforms { get; set; }
        
    }

    public class GameUpdateDto
    {
        public GameResponseDto Game { get; set; }
        public List<Guid> Genres { get; set; }
        public List<Guid> Platforms { get; set; }

    }

    public class GameResponseDto : GameDto
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
    }

    
}
