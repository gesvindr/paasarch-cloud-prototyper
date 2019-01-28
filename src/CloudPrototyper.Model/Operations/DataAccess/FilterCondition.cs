using Newtonsoft.Json;

namespace CloudPrototyper.Model.Operations.DataAccess
{
    /// <summary>
    /// Specifies filter of query to entity storage.
    /// </summary>
    public class FilterCondition
    {
        /// <summary>
        /// Key shall be used.
        /// </summary>
        public bool UseKey { get; set; }

        private string _onAttribute = "";

        /// <summary>
        /// Query on attribute.
        /// </summary>
        public string OnAttribute
        {
            get
            {
                if (UseKey)
                {
                    return "Id";
                }
                return _onAttribute;
            }

            set => _onAttribute = value;
        }

        private bool _isNominal;
        /// <summary>
        /// Nominal or ordinal.
        /// </summary>
        public bool IsNominal
        {
            get
            {
                if (UseKey)
                {
                    return false;
                }
                return _isNominal;
            }

            set => _isNominal = value;
        }


        /// <summary>
        /// Number of expected resuilts from query.
        /// </summary>
        public int NumberOfResults { get; set; }

        /// <summary>
        /// Min value if !IsNominal.
        /// </summary>
        [JsonIgnore]
        public string MaxValue { get; set; } = "";

        /// <summary>
        /// Min value if !IsNominal.
        /// </summary>
        [JsonIgnore]
        public string MinValue { get; set; } = "";


        /// <summary>
        /// Nominal value if IsNominal.
        /// </summary>
        [JsonIgnore]
        public string NominalValue { get; set; } = "";
    }
}