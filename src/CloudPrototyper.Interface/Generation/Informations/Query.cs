namespace CloudPrototyper.Interface.Generation.Informations
{
    public class Query
    {
        public string PropertyName { get; set; }
        public bool IsNominal { get; set; }
        public string NominalParameter { get; set; }
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public bool DoUseKey { get; set; }
    }
}
