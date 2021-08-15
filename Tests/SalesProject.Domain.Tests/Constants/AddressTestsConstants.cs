using SalesProject.Domain.Enums;

namespace SalesProject.Domain.Tests.Constants
{
    public struct AddressTestsConstants
    {
        public const string 
            ValidDescription = "Balcão 1",
            ValidZipCode = "02248-050",
            ValidStreet = "Rua Francisca Maria de Souza",
            ValidNeighborhood = "Parada Inglesa",
            ValidCity = "São Paulo",
            ValidState = "SP",
            ValidCodeCity = "3550308";

        public const int ValidNumber = 131;
        public const AddressType ValidType = AddressType.Other;
    }
}