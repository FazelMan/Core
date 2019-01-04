using System.ComponentModel.DataAnnotations;

namespace FazelMan.Dto.Api
{
    public class PaginationDto
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; } = 10;
    }
}
