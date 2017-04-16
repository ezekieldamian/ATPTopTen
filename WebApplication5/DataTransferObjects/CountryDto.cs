using System.ComponentModel.DataAnnotations;

namespace WebApplication5.DataTransferObjects
{
    public class CountryDto
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}