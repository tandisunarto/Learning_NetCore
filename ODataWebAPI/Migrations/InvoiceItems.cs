using System;
using System.Collections.Generic;

namespace ODataWebAPI.Migrations
{
    public partial class InvoiceItems
    {
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public long TrackId { get; set; }
        public string UnitPrice { get; set; }
        public long Quantity { get; set; }

        public virtual Invoices Invoice { get; set; }
        public virtual Tracks Track { get; set; }
    }
}
