using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace PearlNecklace
{
    //public delegate void NecklaceDelegate(INecklace necklaceDelegate);    
    // Replaced by .NET delegate void Action<INecklace>
    public class NecklaceBag : INecklaceBag
    {
        List<INecklace> _bagOfNecklaces = new List<INecklace>();  
        public INecklace this[int idx] => _bagOfNecklaces[idx];
        public decimal Price
        {
            get
            {
                //_stringOfPearls.Sum(x => x.Price);
                var price = 0M;
                foreach (var p in _bagOfNecklaces)
                {
                    price += p.Price;
                }
                return price;   
            }
        }

        public int Count() => _bagOfNecklaces.Count;

        public IPearl MostExpensivePearl
        {
            get
            {
                var mostExpensivePrice = decimal.MinValue;
                IPearl mostExpensivePearl = null;
                foreach (var necklace in _bagOfNecklaces)
                {
                    if (necklace.MostExpensivePearl.Price > mostExpensivePrice)
                    {
                        mostExpensivePrice = necklace.MostExpensivePearl.Price;
                        mostExpensivePearl = necklace.MostExpensivePearl;
                    }
                }
                return mostExpensivePearl;
            }
        }

        public int CountPearls(PearlColor? color = null)
        {
            var count = 0;  
            foreach (var p in _bagOfNecklaces)
            {
                if (!color.HasValue)
                    count += p.Count();

                else if (color.HasValue)
                    count += p.Count(color);
            }
            return count;   
        }

        public void ForEachPearl(Action<IPearl> pearlAction)
        {
            foreach (var necklace in _bagOfNecklaces)
            {
                necklace.ForEachPearl(pearlAction);
            }
        }

        public override string ToString()
        {
            string sRet = $"My bag had the following necklaces\n";
            int c = 0;
            foreach (var item in _bagOfNecklaces)
            {
                sRet += $"Necklace nr {++c}:\n{item}\n";
            }
            sRet += $"\nNumber of Necklaces: {Count()}";
            sRet += $"\nNumber of Pearls: {CountPearls()}";
            sRet += $"\nNumber of {PearlColor.Black} Pearls: {CountPearls(PearlColor.Black)}";
            sRet += $"\nMost expensive pearls in the bag:\n{MostExpensivePearl}";

            sRet += $"\nTotal value of Bag: {Price}";

            return sRet;
        }

        #region Class Factory for creating an instance filled with Random data
        internal static class Factory
        {
            internal static NecklaceBag CreateRandomNecklaceBag(int NrOfNecklaces, Action<INecklace> NewNecklaceAction)
            {
                var rnd = new Random();
                var necklaceBag = new NecklaceBag();
                for (int i = 0; i < NrOfNecklaces; i++)
                {
                    var necklace = Necklace.Factory.CreateRandomNecklace(rnd.Next(5, 50));
                    necklaceBag._bagOfNecklaces.Add(necklace);
                    NewNecklaceAction(necklace);
                }
                return necklaceBag;    
            }
         }
        #endregion

        #region Methods for writing a neclacebag to disk
        public string Write(string filename)
        {
            string fn = fname(filename);
            using (FileStream fs = File.Create(fn))
            using (Stream ds = new GZipStream(fs, CompressionMode.Compress))
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
