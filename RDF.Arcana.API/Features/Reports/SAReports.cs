using Microsoft.AspNetCore.Mvc;

namespace RDF.Arcana.API.Features.Reports
{
    public class SAReports
    {
        public class SAReportsCommand : IRequest<IActionResult>
        {
            public int MyProperty { get; set; }
        }
    }
}
