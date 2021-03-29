using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Shipping : BaseEntity
    {
        public Shipping() { }

        public Shipping(
            string carrierName,
            string paidBy,
            string anttCode,
            string licensePlate,
            string carrierCnpj,
            string stateRegistration,
            double quantity,
            string type,
            string branch,
            string numeration,
            double grossWeight,
            double netWeight,
            Address carrierAddress)
        {
            CarrierName = carrierName;
            PaidBy = paidBy;
            AnttCode = anttCode;
            LicensePlate = licensePlate;
            CarrierCnpj = carrierCnpj;
            StateRegistration = stateRegistration;
            Quantity = quantity;
            Type = type;
            Branch = branch;
            Numeration = numeration;
            GrossWeight = grossWeight;
            NetWeight = netWeight;
            CarrierAddress = carrierAddress;

            DoValidations();
        }

        public string CarrierName { get; private set; }
        public string PaidBy { get; private set; }
        public string AnttCode { get; private set; }
        public string LicensePlate { get; private set; }
        public string CarrierCnpj { get; private set; }      
        public string StateRegistration { get; private set; }
        public double Quantity { get; private set; }
        public string Type { get; private set; }
        public string Branch { get; private set; }
        public string Numeration { get; private set; }
        public double GrossWeight { get; private set; }
        public double NetWeight { get; private set; }
        public Address CarrierAddress { get; private set; }
        public Guid InvoiceId { get; private set; }

        public override void DoValidations()
        {
            //Todo
        }

        public string GetShippingAddressToInvoice()
            => $@"{CarrierAddress.Street},{CarrierAddress.Number} {CarrierAddress.Neighborhood}";
    }
}
