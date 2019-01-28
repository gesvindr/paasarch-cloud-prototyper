namespace CloudPrototyper.NET.Interface.Generation
{
    public class QueryGenerator : CodeGeneratorBase
    {
        public QueryGenerator(string projectName) : base(projectName, "Interfaces", "Query", typeof(QueryTemplate))
        {
        }
    }
}
