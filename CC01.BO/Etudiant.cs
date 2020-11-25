using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Etudiant
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string DateNaissance { get; set; }
        public string LieuNaissance { get; set; }
        public string Matricule { get; set; }
        public long Contact { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }

        public Etudiant()
        {

        }

        public Etudiant(string nom, string prenom, string dateNaissance, string lieuNaissance, string matricule, long contact, string email, byte[] photo)
        {
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            LieuNaissance = lieuNaissance;
            Matricule = matricule;
            Contact = contact;
            Email = email;
            Photo = photo;
        }

        public override bool Equals(object obj)
        {
            return obj is Etudiant etudiant &&
                   Matricule == etudiant.Matricule;
        }

        public override int GetHashCode()
        {
            return 145730772 + EqualityComparer<string>.Default.GetHashCode(Matricule);
        }
    }
}
