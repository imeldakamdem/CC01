using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    public class EcoleListPrint
    {
        private string matricule;
        private string nom;
        private string prenom;
        private DateTime dateTime;
        private byte[] photo;

        public string Intitule { get; set; }
        public long Telephone { get; set; }
        public string Ecomail { get; set; }
        public byte[] Logo { get; set; }
        public EcoleListPrint(string intitule, long telephone,
               string ecomail, byte[] logo)
        {
            Intitule = intitule;
            Telephone = telephone;
            Ecomail = ecomail;
            Logo = logo;
        }

        public EcoleListPrint(string matricule, string nom, string prenom, DateTime dateTime, byte[] photo)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.dateTime = dateTime;
            this.photo = photo;
        }
    }

}
