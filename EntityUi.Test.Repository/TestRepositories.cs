using System;
using System.Collections.Generic;
using EntityUi.Data;

namespace EntityUi.Test.Repository
{


    public class StudentRepository : RepositoryBase<Student, TestContext>
    {
        protected override TestContext GetContext()
        {
            return new TestContext();
        }

        public void ResetData()
        {
            using (var context = GetContext())
            {
                context.Students.RemoveRange(context.Students);
                context.SaveChanges();

                var students = new List<Student>
                {
                new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
                new Student{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), Created=DateTime.UtcNow, Modified=DateTime.UtcNow}
                };

                students.ForEach(s => context.Students.Add(s));
                context.SaveChanges();
            }
        }
    }

    public class CourseRepository : RepositoryBase<Course, TestContext>
    {
        protected override TestContext GetContext()
        {
            return new TestContext();
        }
    }

}
