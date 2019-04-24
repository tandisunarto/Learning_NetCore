using System;
using System.Collections.Generic;

namespace ODataWebAPI.Migrations
{
    public partial class Genres
    {
        public Genres()
        {
            Tracks = new HashSet<Tracks>();
        }

        public long GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tracks> Tracks { get; set; }
    }
}
