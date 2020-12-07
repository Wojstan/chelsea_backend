using System.Collections.Generic;
using aiproject.Entities;

namespace aiproject.Dto
{
    public class PlayerResponse
    {
        public string Name { get; set; }
        public List<PlayerEntity> Players { get; set; }
    }
}