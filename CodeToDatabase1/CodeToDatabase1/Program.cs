


using CodeToDatabase1;
using System.Data.Entity;
using System.Reflection.Metadata;

namespace CodeToDataBase1
{
    class Program
    {
        static void Main()
        {

            using (var database = new GradebookContext())
            {
                // Create and save a new student

                var student = new Student();

                Console.WriteLine("Enter the student's name:");

                student.Name = Console.ReadLine();

                Console.WriteLine("Enter the student's GPA (0.0 - 5.0):");


                student.GPA = Convert.ToDecimal(Console.ReadLine());


                database.Students.Add(student);
                database.SaveChanges();

                // Display all students from the database
                var query = from b in database.Students
                            orderby b.Id
                            select b;

                Console.WriteLine("All students in the database:");
                Console.WriteLine("Id | Name | GPA");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Id + " | " + item.Name + " | " + item.GPA);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }




    }
}