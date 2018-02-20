using System.Collections.Generic;

namespace SmartAltur.DTO
{
    public class TakeThemHome
    {
        public GeoLoc StartingPoint { get; set; }

        public List<Worker> Workers{ get; set; }
    }
}
