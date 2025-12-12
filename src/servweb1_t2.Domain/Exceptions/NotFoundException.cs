namespace servweb1_t2.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public int EntityId { get; }
        public string EntityName { get; }

        public NotFoundException(int entityId, string entityName)
            : base($"{entityName} with Id '{entityId}' was not found.")
        {
            EntityId = entityId;
            EntityName = entityName;
        }
    }
}   