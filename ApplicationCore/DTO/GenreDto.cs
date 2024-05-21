using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class GenreDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name {  get; set; }

        public Guid? ParentGenreId { get; set; }
    }

    public class GenreRequestDto
    {

        [Required]
        public string Name { get; set; }

        public Guid? ParentGenreId { get; set; }
    }


    public class GenreListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
