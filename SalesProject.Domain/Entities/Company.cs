using SalesProject.Domain.Constants;
using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    public class Company : BaseEntity
    {
        public Company() { }

        public Company(
            string cnpj,
            string name,
            string stateRegistration,
            Address address)
        {
            Cnpj = cnpj;
            Name = name;
            StateRegistration = stateRegistration;
            Address = address;

            DoValidations();
        }

        public string Cnpj { get; private set; }
        public string Name { get; private set; }
        public string StateRegistration { get; private set; }
        public Address Address { get; private set; }

        public override void DoValidations()
        {
            if (Cnpj != CompanyConstants.Cnpj)
                AddNotification("'Cnpj' está inválido");
            if (Name != CompanyConstants.Name)
                AddNotification("'Nome' está inválido");
            if (StateRegistration != CompanyConstants.StateRegistration)
                AddNotification("'Inscrição estadual' está inválido");
            if (Address.ZipCode != CompanyConstants.ZipCode)
                AddNotification("'Cep' está inválido");
            if (Address.Street != CompanyConstants.Street)
                AddNotification("'Logradouro' está inválido");
            if (Address.Neighborhood != CompanyConstants.Neighborhood)
                AddNotification("'Bairro' está inválido");
            if (Address.Number != CompanyConstants.Number)
                AddNotification("'Número' está inválido");
            if (Address.City != CompanyConstants.City)
                AddNotification("'Cidade' está inválido");
            if (Address.State != CompanyConstants.State)
                AddNotification("'Uf' está inválido");
        }
    }
}