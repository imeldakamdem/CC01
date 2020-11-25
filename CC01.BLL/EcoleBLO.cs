using CC01.BO;
using System;
using CC01.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class EcoleBLO
    {
        EcoleDAO ecoleRepo;

        public EcoleBLO(string dbFolder)
        {
            ecoleRepo = new EcoleDAO(dbFolder);
        }
        public void CreateEtudiant(Ecole ecole)
        {
            ecoleRepo.Add(ecole);
        }

        public void DeleteEtudiant(Ecole ecole)
        {
            ecoleRepo.Remove(ecole);
        }


        public IEnumerable<Ecole> GetAllProducts()
        {
            return ecoleRepo.Find();
        }


        public IEnumerable<Ecole> GetByReference(string intitule)
        {
            return ecoleRepo.Find(x => x.Intitule == intitule);
        }

        public IEnumerable<Ecole> GetBy(Func<Ecole, bool> predicate)
        {
            return ecoleRepo.Find(predicate);
        }

        public void EditEtudiant(Ecole oldEcole, Ecole newEcole)
        {
            ecoleRepo.Set(oldEcole, newEcole);
        }
    }
}
