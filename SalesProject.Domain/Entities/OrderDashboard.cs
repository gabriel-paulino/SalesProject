using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class OrderDashboard : BaseEntity
    {
        public OrderDashboard(
            DateTime start, 
            DateTime end,
            int openOrders, 
            int approvedOrders, 
            int canceledOrders, 
            int billedOrders, 
            decimal biggestOrder, 
            decimal lowestOrder, 
            decimal averageOrders, 
            decimal totalSales)
        {
            Start = start;
            End = end;
            OpenOrders = openOrders;
            ApprovedOrders = approvedOrders;
            CanceledOrders = canceledOrders;
            BilledOrders = billedOrders;
            BiggestOrder = biggestOrder;
            LowestOrder = lowestOrder;
            AverageOrders = averageOrders;
            TotalSales = totalSales;

            DoValidations();
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public int OpenOrders { get; private set; }
        public int ApprovedOrders{ get; private set; }
        public int CanceledOrders { get; private set; }
        public int BilledOrders { get; private set; }
        public decimal BiggestOrder { get; private set; }
        public decimal LowestOrder { get; private set; }
        public decimal AverageOrders { get; private set; }
        public decimal TotalSales { get; private set; }

        public override void DoValidations()
        {
            ValidateRangeDate();
        }

        private void ValidateRangeDate()
        {
            if (Start > End)
                AddNotification("A data inicial não pode ser maior que a data final.");
        }
    }
}