namespace PearlNecklace
{
    public class Necklace : INecklace
    {
        List<IPearl> _stringOfPearls = new List<IPearl>();
        public IPearl this[int idx] => _stringOfPearls[idx];
        public decimal Price
        {
            get
            {
                //_stringOfPearls.Sum(x => x.Price);
                var price = 0M;
                foreach (var p in _stringOfPearls)
                {
                    price += p.Price;
                }
                return price;
            }
        }
        public IPearl MostExpensivePearl
        {
            get
            {
                var mostExpensivePrice = decimal.MinValue;
                var mostExpensiveIdx = 0;
                for (int i = 0; i < _stringOfPearls.Count; i++)
                {
                    if (_stringOfPearls[i].Price > mostExpensivePrice)
                    {
                        mostExpensivePrice = _stringOfPearls[i].Price;
                        mostExpensiveIdx = i;
                    }
                }
                return _stringOfPearls[mostExpensiveIdx];
            }
        }

        public int Count() => _stringOfPearls.Count;

        public int Count(PearlColor? color = null)
        {
            //_stringOfPearls.Where(o => o.Type == PearlType.SaltWater).Count();
            int c = 0;
            foreach (var item in _stringOfPearls)
            {
                if (!color.HasValue)
                    c++;

                else if (color.HasValue && color == item.Color)
                    c++;
            }
            return c;
        }
        public int Count(PearlType type)
        {
            //_stringOfPearls.Where(o => o.Type == PearlType.SaltWater).Count();
            int c = 0;
            foreach (var item in _stringOfPearls)
            {
                if (type == item.Type)
                    c++;
            }
            return c;
        }

        public override string ToString()
        {
            string sRet = $"Necklace has the following pearls:\n";
            foreach (var item in _stringOfPearls)
            {
                sRet += $"{item}\n";
            }
            return sRet;
        }


        public void Sort() => _stringOfPearls.Sort();
        public int IndexOf(IPearl pearl) => _stringOfPearls.IndexOf(pearl);
 

        #region Class Factory for creating an instance filled with Random data
        internal static class Factory
        {
            internal static Necklace CreateRandomNecklace(int NrOfPearls)
            {
                var necklace = new Necklace();
                for (int i = 0; i < NrOfPearls; i++)
                {
                    necklace._stringOfPearls.Add(Pearl.Factory.CreateRandomPearl());
                }
                return necklace;
            }
        }
        #endregion

        #region Methods for writing a neclace to disk
        public string Write(string filename)
        {
            string fn = fname(filename);
            using (FileStream fs = File.Create(fn))
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
    }
}
