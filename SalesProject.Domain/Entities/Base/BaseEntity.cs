using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public bool Valid { get; private set; }
        public IEnumerable<string> Notifications { get; private set; }
        protected BaseEntity()
        {
            this.Valid = true;
            this.Notifications = new List<string>();
        }

        public abstract void DoBusinesRulesValidations();

        public void AddNotification(string errorMessage)
        {
            Valid = false;
            Notifications.ToList().Add(errorMessage);
        }

        public string GetNotification() 
            => Notifications.FirstOrDefault();    
    }
}