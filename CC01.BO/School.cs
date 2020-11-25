using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    public class School
    {
        private string v1;
        private string text;
        private long v2;
        private object p;

        public string NameSchool { get; set; }
        public string EmailSchool { get; set; }
        public long ContactSchool { get; set; }
        public string Logo { get; set; }
        public string Localisation { get; set; }

        public School()
        {

        }
        public School(string logo, string nom, long tel, string email, string localisation)
        {
            Logo = logo;
            NameSchool = nom;
            ContactSchool = tel;
            EmailSchool = email;
            Localisation = localisation;
        }

        public School(string v1, string text, long v2, object p)
        {
            this.v1 = v1;
            this.text = text;
            this.v2 = v2;
            this.p = p;
        }

        public override bool Equals(object obj)
        {
            return obj is School school &&
                   NameSchool.Equals(school.NameSchool, StringComparison.OrdinalIgnoreCase); ;
        }
        public override int GetHashCode()
        {
            return 217408413 + EqualityComparer<string>.Default.GetHashCode(NameSchool);
        }
    }
}
