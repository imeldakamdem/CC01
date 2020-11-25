using CC01.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.DAL
    {
        public class SchoolDAO
        {
            private static List<School> schools;
            private const string FILE_NAME = @"school.json";
            private readonly string dbFolder;
            private FileInfo file;

            public SchoolDAO(string dbFolder)
            {
                this.dbFolder = dbFolder;
                file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                    file.Refresh();
                }
                if (file.Length > 0)
                {
                    using (StreamReader sr = new StreamReader(file.FullName))
                    {
                        string json = sr.ReadToEnd();
                        schools = JsonConvert.DeserializeObject<List<School>>(json);
                    }
                }
                if (schools == null)
                {
                    schools = new List<School>();
                }
            }

        public School Get()
        {
            throw new NotImplementedException();
        }

        public void Add(School school)
            {
                var index = schools.IndexOf(school);
                schools.Add(school);
                Save();
            }

            public IEnumerable<School> Find(Func<School, bool> predicate)
            {
                return new List<School>(schools.Where(predicate).ToArray());
            }

            public IEnumerable<School> Find()
            {
                return new List<School>(schools);
            }

            public void Set(School oldSchool, School newSchool)
            {
                var oldIndex = schools.IndexOf(oldSchool);
                var newIndex = schools.IndexOf(newSchool);
                schools[oldIndex] = newSchool;
                Save();
            }

            public void Remove(School school)
            {
                schools.Remove(school);
                Save();
            }

            private void Save()
            {
                using (StreamWriter sw = new StreamWriter(file.FullName, false))
                {
                    string json = JsonConvert.SerializeObject(schools);
                    sw.WriteLine(json);
                }
            }

        public static void EditSchool(School oldSchool, School newSchool)
        {
            throw new NotImplementedException();
        }
    }
    }
