using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace ODataWebAPI
{
    public static class BooksEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}