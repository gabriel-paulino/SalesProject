using SalesProject.Domain.Entities.Base;
using System.Collections.Generic;

namespace SalesProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public override void DoValidations()
        {
            throw new System.NotImplementedException();
        }
    }
}