namespace PearlNecklace
{
    public class Pearl : IPearl
    {
        public const decimal PearlBasePrice = 50M;
        public const int PearlMinSize = 5;
        public const int PearlMaxSize = 25;

        int _size;
        public int Size
        {
            get => _size;
            set
            {
                if (value < PearlMinSize || value > PearlMaxSize)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _size = value;
            }
        }
        public PearlColor Color { get; private set; }
        public PearlShape Shape { get; private set; }
        public PearlType Type { get; private set; }
        public decimal Price
        {
            get
            {
                var price = Size * PearlBasePrice;
                if (Type == PearlType.SaltWater)
                    price *= 2;
                return price;
            }
        }

        #region Property change methods of an immutable type
        public Pearl SetSize(int size)
        {
            var np = new Pearl(this);
            np.Size = size;
            return np;
        }
        public Pearl SetColor(PearlColor color)
        {
            var np = new Pearl(this);
            np.Color = color;
            return np;
        }
        public Pearl SetType(PearlType type)
        {
            var np = new Pearl(this);
            np.Type = type;
            return np;
        }
        public Pearl SetShape(PearlShape shape)
        {
            var np = new Pearl(this);
            np.Shape = shape;
            return np;
        }
        #endregion

        public override string ToString() => $"{Size}mm {Color} {Shape} {Type} pearl. Price: {Price}";

        #region IComparable and IEquatable
        public int CompareTo(IPearl other)
        {
            if (Size != other.Size)
                return Size.CompareTo(other.Size);
            if (Color != other.Color)
                return Color.CompareTo(other.Color);

            return Shape.CompareTo(other.Shape);
        }

        public bool Equals(IPearl other) => (Size, Color, Shape, Type) == (other.Size, other.Color, other.Shape, other.Type);
        public override bool Equals(object obj) => Equals(obj as IPearl);
        public override int GetHashCode() => (Size, Color, Shape, Type).GetHashCode();
        #endregion

        #region Class Factory for creating an instance filled with Random data
        public void RandomInit()
        {
            var rnd = new Random();
            this.Size = rnd.Next(PearlMinSize, PearlMaxSize);
            this.Color = (PearlColor)rnd.Next((int)PearlColor.Black, (int)PearlColor.Pink + 1);
            this.Shape = (PearlShape)rnd.Next((int)PearlShape.Round, (int)PearlShape.DropShaped + 1);
            this.Type = (PearlType)rnd.Next((int)PearlType.FreshWater, (int)PearlType.SaltWater + 1);
        }
        internal static class Factory
        {
            internal static Pearl CreateRandomPearl()
            {
                var p = new Pearl();
                p.RandomInit();
                return p;
            }
        }
        #endregion

        #region Methods for writing a neclacebag to disk
        public string Write(string filename)
        {
            string fn = fname(filename);
            using (FileStream fs = File.Create(fn))
            //using (Stream ds = new GZipStream(fs, CompressionMode.Compress))
            using (TextWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(this);
            }
            return fn;
        }
        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "Examples");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
        #endregion

        #region Copy constructor
        public Pearl()
        { }
        public Pearl(Pearl src)
        {
            this.Color = src.Color;
            this.Shape = src.Shape; 
            this.Type = src.Type;
            this.Size = src.Size;
        }
        #endregion
    }
}
