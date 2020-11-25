using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    public class EtudiantPrint
    {
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaiss { get; set; }
        public byte[] Photo { get; set; }

        public EtudiantPrint(string matricule,string nom,
            string prenom,DateTime dateNaiss ,byte[] photo)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            DateNaiss = dateNaiss;
            Photo = photo;
        }
    }
}
