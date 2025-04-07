using System.ComponentModel.DataAnnotations;

namespace InzynierkaApiReact.Server.Models
{
    public class Planogram
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string PdfPath { get;set; } 
        public DateOnly Date { get; set; }
    }
}
