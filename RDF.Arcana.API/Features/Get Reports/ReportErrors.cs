using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Get_Reports
{
    public class ReportErrors
    {

        public static Error NoPending() => new("NoPending", "Client has no pending transaction");
    }
}
