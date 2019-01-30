using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.EfStructures.Context;
using DataAccessLayer.EfStructures.Entities;
using DataAccessLayer.EfStructures.Extensions;
using Xunit;

namespace DataAccessLayerTests.B_AdvancedQueries.G_INotifyPropertyChanged
{
    public class NotifyPropertyChangedTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public NotifyPropertyChangedTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //TODO: Update Product to support INotifyPropertyChanged
        [Fact]
        public void ShouldNotCallPropertyChangedOnConstruction()
        {
        }
    }
}
