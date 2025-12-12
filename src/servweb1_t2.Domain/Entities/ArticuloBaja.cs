using System;

namespace servweb1_t2.Domain.Entities
{
    public class ArticuloBaja
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime FechaBaja { get; set; } = DateTime.Now;
        public string? Motivo { get; set; }
    }
}
