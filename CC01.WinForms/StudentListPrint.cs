using System;

namespace CC01.WinForms
{
    public class StudentListPrint
    {
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
       public string Email { get; set; }
        public byte[] Photo { get; set; }

        //public StudentListPrint(string matricule, string nom, string prenom,,string email, byte[] photo)
        //{
        //    Matricule = matricule;
        //    Nom = nom;
        //    Prenom = prenom; 
        //    Email = email;
        //    Photo = photo;
        //}

        public StudentListPrint(string matricule, string nom, string prenom, string email, byte[] photo)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Photo = photo;
        }
    }
}