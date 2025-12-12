namespace servweb1_t2.Domain.Exceptions
{
    public class BookNoStockException : DomainException
    {
        public int EntityId { get; }

        public string EntityName { get; }
        
        public BookNoStockException(int entityId, string entityName)
            : base($"Book '{entityName}' with Id '{entityId}' has no stock available.")
        {
            EntityId = entityId;
            EntityName = entityName;
        }
    }
}