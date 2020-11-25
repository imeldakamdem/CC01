using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    public class Student
    {
        private string v1;
        private string text1;
        private string text2;
        private string text3;
        private string text4;
        private long v2;
        private object p;

        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNais { get; set; }
        public string Lieu { get; set; }
        public long Contact { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }


        public Student()
        {
        }
        public Student(string matricule, string nom, string prenom, DateTime  dateNais, string lieu, string email, long contact, byte[] photo)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            DateNais = dateNais;
            Lieu = lieu;
            Email = email;
            Contact = contact;
            Photo = photo;

        }

        public Student(string v1, string text1, string text2, string text3, string text4, long v2, object p)
        {
            this.v1 = v1;
            this.text1 = text1;
            this.text2 = text2;
            this.text3 = text3;
            this.text4 = text4;
            this.v2 = v2;
            this.p = p;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
               Matricule.Equals(student.Matricule, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return 797189699 + EqualityComparer<string>.Default.GetHashCode(Matricule);
        }
    }
}
