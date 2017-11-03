using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityUi.Test.Repository
{
    public class TestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {
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
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=1045,Title="Calculus",Credits=4,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=2021,Title="Composition",Credits=3,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Course{CourseID=2042,Title="Literature",Credits=4, Created=DateTime.UtcNow, Modified=DateTime.UtcNow}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=3,CourseID=1050, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=4,CourseID=1050,Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=6,CourseID=1045, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A, Created=DateTime.UtcNow, Modified=DateTime.UtcNow},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}
