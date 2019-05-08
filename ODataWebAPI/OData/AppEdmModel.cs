using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using ODataWebAPI.Migrations;
using ODataWebAPI.Models;

namespace ODataWebAPI
{
    public static class AppEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            // builder.EntitySet<EmployeeViewModel>("Employee")
            //     .EntityType.HasKey(e => e.EmployeeId);
            builder.EntitySet<Employees>("Employees")
                .EntityType.HasKey(e => e.EmployeeId);
            builder.EntitySet<Customers>("Customer")
                .EntityType.HasKey(e => e.CustomerId);
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}