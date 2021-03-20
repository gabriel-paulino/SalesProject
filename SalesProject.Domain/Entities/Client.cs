using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class Client : BaseEntity
    {
        public Guid Id { get; private set; }
        public string Cnpj { get; private set; }
        public string CompanyName { get; private set; }
        public DateTime Opening { get; private set; }
        public string TelNumber { get; private set; }
        public DateTime ClientSince { get; private set; }
        public string MunicipalRegistration { get; private set; }
        public string StateRegistration { get; private set; }

        public Client(
            string cnpj, 
            string companyName, 
            DateTime opening, 
            string telNumber, 
            string municipalRegistration, 
            string stateRegistration)
        {
            this.Id = Guid.NewGuid();
            this.Cnpj = cnpj;
            this.CompanyName = companyName;
            this.Opening = opening;
            this.TelNumber = telNumber;
            this.ClientSince = DateTime.Today;
            this.MunicipalRegistration = municipalRegistration;
            this.StateRegistration = stateRegistration;

            DoBusinesRulesValidations();
        }

        public override void DoBusinesRulesValidations()
        {
            throw new NotImplementedException();
        }
    }
}