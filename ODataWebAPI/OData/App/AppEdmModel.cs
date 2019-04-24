using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using ODataWebAPI.Models;

namespace ODataWebAPI
{
    public static class AppEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<EmployeeViewModel>("Employee")
                .EntityType.HasKey(e => e.EmployeeId);
            return builder.GetEdmModel();
        }
    }
}