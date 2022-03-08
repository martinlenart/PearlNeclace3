using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearlNecklace
{
    public enum PearlColor { Black, White, Pink }
    public enum PearlShape { Round, DropShaped}
    public enum PearlType { FreshWater, SaltWater }
    public interface IPearl : IEquatable<IPearl>, IComparable<IPearl>
    {
        public int Size { get; }
        public PearlColor Color { get; }
        public PearlShape Shape { get; }
        public PearlType Type { get; } 
        public decimal Price { get; }
    }
}
