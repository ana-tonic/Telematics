using System;
using Newtonsoft.Json;

namespace Server.Models
{
    /// <summary>
    /// A C# representation of the location udt in the Astra database
    /// </summary>
    public class location
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

}