namespace CA1202
{
    internal class Author
    {
        /*
        Author:class
        Kereszt- és vezeték névvel, valamint GUID-al rendelkezik, 
        a névrészei egyenként minimum 3, maximum 32 karakter hosszúak. 
        Egyetlen konstruktőri van, ahol a vezeték és keresztnevét egyetlen szóközzel tagolt strinkbem kapja. 
        A konstruktor generál neki letrehozáskor egy GUIDet és szetbontja a nevét a megfelelő adattagokba.
        */

        private string keresztNev;
        private string vezetekNev;
        private Guid guid;

        public string KeresztNev
        {
            get => keresztNev; set
            {
                if (value.Length < 3 || value.Length > 32) throw new Exception("Nem megfelelő hosszúságú a keresztnév!");
                keresztNev = value;
            }
        }
        public string VezetekNev
        {
            get => vezetekNev; set
            {
                if (value.Length < 3 || value.Length > 32) throw new Exception("Nem megfelelő hosszúságú a vezetéknév!");
                vezetekNev = value;
            }
        }
        public Guid Guid { get => guid; set => guid = value; }

        public Author(string nev)
        {
            Guid = Guid.NewGuid();
            string[] nevek = nev.Split(' ');
            VezetekNev = nevek[0];
            KeresztNev = nevek[1];
        }
    }
}
