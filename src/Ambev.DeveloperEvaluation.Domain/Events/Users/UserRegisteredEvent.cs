using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Domain.Events.Users
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
