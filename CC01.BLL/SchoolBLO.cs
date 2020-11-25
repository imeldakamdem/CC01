using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.IO;

namespace CC01.BLL
{
    public class SchoolBLO
    {
        SchoolDAO schoolRepo;
        private string dbFolder;

        public SchoolBLO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            schoolRepo = new SchoolDAO(dbFolder);
        }
        public IEnumerable<School> GetBy(Func<School, bool> predicate)
        {
            return schoolRepo.Find(predicate);
        }
        public void DeleteSchool(School school)
        {
            schoolRepo.Remove(school);
        }

        public void CreateSchool(School newSchool)
        {
            schoolRepo.Add(newSchool);
        }

        public School GetSchool()
        {
                School school = schoolRepo.Get();
                if (school != null)
                    if (!string.IsNullOrEmpty(school.Logo))
                        school.Logo = Path.Combine(dbFolder, "logo", school.Logo);
                return school;
            }

        public static void CreateSchool(School oldSchool, School newSchool)
        {
            throw new NotImplementedException();
        }
        //public void EditSchool(School oldSchool, School newSchool)
        //{
        //    schoolRepo.Set(oldSchool, newSchool);
        //}
    }
}
