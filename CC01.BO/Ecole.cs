using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    public class Ecole
    {
        public string Intitule { get; set; }
        public long Telephone { get; set; }
        public string Ecomail { get; set; }
        public byte[] Logo { get; set; }

        public Ecole()
        {
        }

        public Ecole(string intitule, long téléphone, string ecomail, byte[] logo)
        {
            Intitule = intitule;
            Telephone = téléphone;
            Ecomail = ecomail;
            Logo = logo;
        }
        public override bool Equals(object obj)
        {
            return obj is Ecole ecole &&
                   Intitule == ecole.Intitule;
        }

        public override int GetHashCode()
        {
            return 145730772 + EqualityComparer<string>.Default.GetHashCode(Intitule);
        }
    }
}
