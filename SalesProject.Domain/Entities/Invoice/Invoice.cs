using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Invoice() { }

        public Invoice(
            int number,
            int series,
            string originOperation,
            DateTime releaseDate,
            DateTime leaveDate,
            string leaveHour,
            string additionalInformation,
            Client client,
            Shipping shipping)
        {
            Number = number;
            Series = series;
            OriginOperation = originOperation;
            ReleaseDate = releaseDate;
            LeaveDate = leaveDate;
            LeaveHour = leaveHour;

            //Todo: Validar quais parametros fazem sentido serem passados pelo construtor
            CarrierName = shipping.CarrierName;
            PaidBy = shipping.PaidBy;
            AnttCode = shipping.AnttCode;
            LicensePlate = shipping.LicensePlate;
            CarrierCnpj = shipping.CarrierCnpj;
            StateRegistration = shipping.StateRegistration;
            Quantity = shipping.Quantity;
            Type = shipping.Type;
            Branch = shipping.Branch;
            Numeration = shipping.Numeration;
            GrossWeight = shipping.GrossWeight;
            NetWeight = shipping.NetWeight;
            CarrierAddress = shipping.GetShippingAddressToInvoice();
            CarrierCity = shipping.CarrierAddress.City;
            CarrierState = shipping.CarrierAddress.State;

            AdditionalInformation = additionalInformation;
            Client = client;
            Shipping = shipping;
            _invoiceLines = new List<InvoiceLines>();

            DoValidations();
            SetCompanyInformations();
        }

        private IList<InvoiceLines> _invoiceLines;

        public Client Client { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid SalesOrderId { get; private set; }
        public int Number { get; private set; }
        public int Series { get; private set; }
        public string OriginOperation { get; private set; }
        public Company Company { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public DateTime LeaveDate { get; private set; }
        public string LeaveHour { get; private set; }
        public Shipping Shipping { get; private set; }

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
        public string CarrierAddress { get; private set; }
        public string CarrierCity { get; private set; }
        public string CarrierState { get; private set; }

        public decimal BaseCalcIcms { get; private set; }
        public decimal TotalIcms { get; private set; }
        public decimal BaseCalcOtherIcms { get; private set; }
        public decimal TotalOtherIcms { get; private set; }
        public decimal TotalProducts { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal InsuranceCost { get; private set; }
        public decimal TotalDiscont { get; private set; }
        public decimal OtherCosts { get; private set; }
        public decimal IpiValue { get; private set; }
        public decimal TotalInvoice { get; private set; }
        public string AdditionalInformation { get; private set; }
        public string ReservedFisco { get; private set; }
        public IReadOnlyCollection<InvoiceLines> InvoiceLines { get => _invoiceLines.ToArray(); }

        public void AddInvoiceLine(InvoiceLines invoiceLine)
        {
            _invoiceLines.Add(invoiceLine);
            UpdateInvoiceValues();
        }

        public override void DoValidations()
        {
            //Todo
        }

        private void SetCompanyInformations()
        {
            Company =
                new Company(
                            cnpj: CompanyConstants.Cnpj,
                            name: CompanyConstants.Name,
                            stateRegistration: CompanyConstants.StateRegistration,
                            new Address(
                                        zipCode: CompanyConstants.ZipCode,
                                        street: CompanyConstants.Street,
                                        neighborhood: CompanyConstants.Neighborhood,
                                        number: CompanyConstants.Number,
                                        city: CompanyConstants.City,
                                        state: CompanyConstants.State
                                        )
                            );

            if (!Company.Valid)
                AddNotification($@"Erro ao criar nota fiscal. O campo {Company.GetNotification()}");
        }

        private void UpdateInvoiceValues()
        {
            //Atualizar Frete, Impostos no geral, Produtos, Desconto e Valor total basedo nas linhas
            //Em debug verificar se será necessario resetar os totais antes do foreach
        }
    }
}