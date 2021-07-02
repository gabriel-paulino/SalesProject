using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Entities
{
    public class OrderFilter : BaseEntity
    {
        public OrderFilter(
            Guid? customerId,
            OrderStatus? status, 
            DateTime? startDate, 
            DateTime? endDate)
        {
            this.CustomerId = customerId;
            this.Status = status;
            this.StartDate = startDate;
            this.EndDate = endDate;

            DoValidations();
        }

        public OrderFilter(
            Guid? customerId,
            DateTime? startDate,
            DateTime? endDate)
        {
            this.CustomerId = customerId;
            this.StartDate = startDate;
            this.EndDate = endDate;

            DoValidations();
        }

        public Guid? CustomerId { get; private set; }
        public OrderStatus? Status { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public bool IsFilledCustomerId() => this.CustomerId is not null;
        public bool IsFilledOrderStatus() => this.Status is not null;
        public bool IsFilledStartDate() => this.StartDate is not null;
        public bool IsFilledEndDate() => this.EndDate is not null;

        public override void DoValidations()
        {
            ValidateRangeDate();            
        }

        private void ValidateRangeDate()
        {
            if (IsFilledStartDate() && IsFilledEndDate() && StartDate > EndDate)
                AddNotification("A data inicial não pode ser maior que a data final.");
        }
    }
}