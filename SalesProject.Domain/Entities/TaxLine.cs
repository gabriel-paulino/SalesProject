namespace SalesProject.Domain.Entities
{
    public class TaxLine
    {
        public TaxLine() { }

        public TaxLine(
            string originIcms,
            string cstIcms,
            int determinationMode,
            decimal valueBaseCalcIcms,
            int aliquotIcms,
            decimal valueIcms,
            string cstPis,
            decimal valueBaseCalcPis,
            double aliquotPis,
            decimal valuePis,
            string cstCofins,
            decimal valueBaseCalcCofins,
            double aliquotCofins,
            decimal valueCofins)
        {
            OriginIcms = originIcms;
            CstIcms = cstIcms;
            DeterminationMode = determinationMode;
            ValueBaseCalcIcms = valueBaseCalcIcms;
            AliquotIcms = aliquotIcms;
            ValueIcms = valueIcms;
            CstPis = cstPis;
            ValueBaseCalcPis = valueBaseCalcPis;
            AliquotPis = aliquotPis;
            ValuePis = valuePis;
            CstCofins = cstCofins;
            ValueBaseCalcCofins = valueBaseCalcCofins;
            AliquotCofins = aliquotCofins;
            ValueCofins = valueCofins;
        }

        public string OriginIcms { get; private set; }
        public string CstIcms { get; private set; }
        public int DeterminationMode { get; private set; }
        public decimal ValueBaseCalcIcms { get; private set; }
        public int AliquotIcms { get; private set; }
        public decimal ValueIcms { get; private set; }
        public string CstPis { get; private set; }
        public decimal ValueBaseCalcPis { get; private set; }
        public double AliquotPis { get; private set; }
        public decimal ValuePis { get; private set; }
        public string CstCofins { get; private set; }
        public decimal ValueBaseCalcCofins { get; private set; }
        public double AliquotCofins { get; private set; }
        public decimal ValueCofins { get; private set; }

        public TaxLine GetDefaultTaxes(decimal totalLineOrder) =>
            new TaxLine(
                        originIcms: "0",
                        cstIcms: "00",
                        determinationMode: 0,
                        valueBaseCalcIcms: totalLineOrder,
                        aliquotIcms: 7,
                        valueIcms: totalLineOrder * 0.07m,
                        cstPis: "07",
                        valueBaseCalcPis: 0.0m,
                        aliquotPis: 0.0,
                        valuePis: 0.0m,
                        cstCofins: "07",
                        valueBaseCalcCofins: 0.0m,
                        aliquotCofins: 0.0,
                        valueCofins: 0.0m);
    }
}