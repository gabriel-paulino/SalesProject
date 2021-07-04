using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            this.Id = new Guid();
            this.Valid = true;
            this.Notifications = new List<string>();
        }

        public bool Valid { get; private set; }
        public List<string> Notifications { get; private set; }
        public Guid Id { get; private set; }

        public abstract void DoValidations();

        public void AddNotification(string errorMessage)
        {
            Valid = false;
            Notifications.Add(errorMessage);
        }

        public string GetNotification() 
            => Notifications.FirstOrDefault();

        public IEnumerable<string> GetAllNotifications() => Notifications;        
    }
}