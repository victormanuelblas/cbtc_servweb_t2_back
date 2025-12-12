using System;

namespace servweb1_t2.Domain.Entities
{
    public class ArticuloLiquidacion
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public DateTime FechaLiquidacion { get; set; } = DateTime.Now;
    }
}
