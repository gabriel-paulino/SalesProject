using SalesProject.Domain.Entities.Base;

namespace SalesProject.Domain.Entities
{
    //Verificar se essa classe pode ser abstrata e fazer as linhas de PV e de NF herdar esses atributos
    public class TaxLine : BaseEntity
    {
        public TaxLine() { }

        public TaxLine(
            string cst,
            string cfop,
            decimal baseCalcIcms,
            decimal icmsValue,
            decimal ipiValue,
            double icmsAliquot,
            double ipiAliquot)
        {
            this.Cst = cst;
            this.Cfop = cfop;
            this.BaseCalcIcms = baseCalcIcms;
            this.IcmsValue = icmsValue;
            this.IpiValue = ipiValue;
            this.IcmsAliquot = icmsAliquot;
            this.IpiAliquot = ipiAliquot;

            DoValidations();
        }

        public string Cst { get; private set; }
        public string Cfop { get; private set; }
        public decimal BaseCalcIcms { get; private set; }
        public decimal IcmsValue { get; private set; }
        public decimal IpiValue { get; private set; }
        public double IcmsAliquot { get; private set; }
        public double IpiAliquot { get; private set; }

        public override void DoValidations()
        {
            //Todo
        }
    }
}