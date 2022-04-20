namespace MeetupTelegramBot.DataAccess.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, Guid id)
            : base($"Entity [{entityName}] with id [{id}] cannot be found!")
        {

        }
        
        public EntityNotFoundException(string message)
            : base(message)
        {

        }
    }
}
