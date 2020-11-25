using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class StudentBLO
    {
        StudentDAO studentRepo;
        public StudentBLO(string dbFolder)
        {
            studentRepo = new StudentDAO(dbFolder);
        }
        public void CreateStudent(Student student)
        {
            studentRepo.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            studentRepo.Remove(student);
        }


        public IEnumerable<Student> GetAllEtudiants()
        {
            return studentRepo.Find();
        }


        public IEnumerable<Student> GetByMatricule(string matricule)
        {
            return studentRepo.Find(x => x.Matricule == matricule);
        }

        public IEnumerable<Student> GetBy(Func<Student, bool> predicate)
        {
            return studentRepo.Find(predicate);
        }

        public void EditEtudiant(Student oldEtudiant, Student newEtudiant)
        {
            studentRepo.Set(oldEtudiant, newEtudiant);
        }

        public Student Get()
        {
            throw new NotImplementedException();
        }

        public void EditStudent(Student oldStudent, Student newStudent)
        {
            throw new NotImplementedException();
        }
    }
}

