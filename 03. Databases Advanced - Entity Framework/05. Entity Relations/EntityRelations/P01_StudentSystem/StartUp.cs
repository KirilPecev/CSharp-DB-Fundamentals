namespace P01_StudentSystem
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Data.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (StudentSystemContext db = new StudentSystemContext())
            {
                var students = StudentsSeedMethod();
                var courses = CoursesSeedMethod();
                var resources = ResourcesSeedMethod();
                var homeworkSubmissions = HomeworkSubmissionsSeedMethod();

                db.Students.AddRange(students);
                db.Courses.AddRange(courses);
                db.Resources.AddRange(resources);
                db.HomeworkSubmissions.AddRange(homeworkSubmissions);

                db.SaveChanges();
            }
        }

        private static List<Homework> HomeworkSubmissionsSeedMethod()
        {
            var list = new List<Homework>();

            var homework1 = new Homework()
            {
                Content = "Info",
                ContentType = ContentType.Pdf,
                CourseId = 1,
                StudentId = 1,
                SubmissionTime = DateTime.Now
            };

            var homework2 = new Homework()
            {
                Content = "Info",
                ContentType = ContentType.Application,
                CourseId = 2,
                StudentId = 3,
                SubmissionTime = DateTime.Now
            };

            list.Add(homework1);
            list.Add(homework2);

            return list;
        }

        private static List<Resource> ResourcesSeedMethod()
        {
            var list = new List<Resource>();

            var resource1 = new Resource()
            {
                CourseId = 1,
                Name = "Resource_1",
                ResourceType = ResourceType.Presentation,
                Url = "www.resource.com"
            };

            var resource2 = new Resource()
            {
                CourseId = 2,
                Name = "Resource_2",
                ResourceType = ResourceType.Document,
                Url = "www.resource.com"
            };

            list.Add(resource1);
            list.Add(resource2);

            return list;
        }

        private static List<Course> CoursesSeedMethod()
        {
            var list = new List<Course>();

            var course1 = new Course()
            {
                Name = "C# OOP",
                Description = "OOP",
                Price = 350,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2),
            };

            var course2 = new Course()
            {
                Name = "C# OOP Advanced",
                Description = "OOP",
                Price = 350,
                StartDate = DateTime.Today.AddMonths(2),
                EndDate = DateTime.Today.AddMonths(4),
            };

            list.Add(course1);
            list.Add(course2);

            return list;
        }

        private static List<Student> StudentsSeedMethod()
        {
            var list = new List<Student>();

            var student1 = new Student()
            {
                Name = "Pesho",
                Birthday = DateTime.Parse("01.03.1995"),
                PhoneNumber = "0886528742",
                RegisteredOn = DateTime.Today
            };

            var student2 = new Student()
            {
                Name = "Gosho",
                Birthday = DateTime.Parse("04.12.1999"),
                PhoneNumber = "0888196842",
                RegisteredOn = DateTime.Today
            };

            var student3 = new Student()
            {
                Name = "Mimi",
                Birthday = DateTime.Parse("21.05.1990"),
                PhoneNumber = "0883674296",
                RegisteredOn = DateTime.Today
            };

            list.Add(student1);
            list.Add(student2);
            list.Add(student3);

            return list;
        }
    }
}
