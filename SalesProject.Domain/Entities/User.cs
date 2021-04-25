using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }

        public override void DoValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}