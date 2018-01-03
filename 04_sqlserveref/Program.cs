using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

//Create C# apps using SQL Server on RHEL
//https://www.microsoft.com/en-us/sql-server/developer-get-started/csharp/rhel/

namespace _04_sqlserveref
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** C# CRUD sample with Entity Framework Core and SQL Server **\n");
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "centos.localsandbox.com";
                builder.UserID = "sa";           
                builder.Password = "pAssw0rd123$";   
                builder.InitialCatalog = "EFSampleDB";

                using (EFSampleContext context = new EFSampleContext(builder.ConnectionString))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    Console.WriteLine("Created database schema from C# classes.");
                    PressAnyKey("continue");

                    // Create demo: Create a User instance and save it to the database
                    User newUser = new User { FirstName = "Anna", LastName = "Shrestinian" };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated User: " + newUser.ToString());
                    PressAnyKey("continue");

                    // Create demo: Create a Task instance and save it to the database
                    Task newTask = new Task() { Title = "Ship Helsinki", IsComplete = false, DueDate = DateTime.Parse("04-01-2017") };
                    context.Tasks.Add(newTask);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated Task: " + newTask.ToString());
                    PressAnyKey("continue");

                    // Association demo: Assign task to user
                    newTask.AssignedTo = newUser;
                    context.SaveChanges();
                    Console.WriteLine("\nAssigned Task: '" + newTask.Title + "' to user '" + newUser.GetFullName() + "'");
                    PressAnyKey("continue");

                    // Create demo: Create a 2nd User instance and save it to the database
                    User newUser2 = new User { FirstName = "Bert", LastName = "Reynolds" };
                    context.Users.Add(newUser2);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated User: " + newUser2.ToString());

                    // Create demo: Create a Task instance and save it to the database
                    Task newTask2 = new Task() { Title = "Ship Acapulco", IsComplete = false, DueDate = DateTime.Parse("12-01-2016") };
                    context.Tasks.Add(newTask2);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated Task: " + newTask2.ToString());

                    // Association demo: Assign task to 2nd user
                    newTask.AssignedTo = newUser2;
                    context.SaveChanges();
                    Console.WriteLine("\nAssigned Task: '" + newTask2.Title + "' to user '" + newUser2.GetFullName() + "'");
                    PressAnyKey("continue");

                    // Read demo: find incomplete tasks assigned to user 'Anna'
                    Console.WriteLine("\nIncomplete tasks assigned to 'Anna':");
                    var query = from t in context.Tasks
                                where t.IsComplete == false &&
                                t.AssignedTo.FirstName.Equals("Anna")
                                select t;
                    foreach(var t in query)
                    {
                        Console.WriteLine(t.ToString());
                    }

                    // Update demo: change the 'dueDate' of a task
                    Task taskToUpdate = context.Tasks.First(); // get the first task
                    Console.WriteLine("\nUpdating task: " + taskToUpdate.ToString());
                    taskToUpdate.DueDate = DateTime.Parse("06-30-2016");
                    context.SaveChanges();
                    Console.WriteLine("dueDate changed: " + taskToUpdate.ToString());
                    PressAnyKey("continue");

                    // Delete demo: delete all tasks with a dueDate in 2016
                    Console.WriteLine("\nDeleting all tasks with a dueDate in 2016");
                    DateTime dueDate2016 = DateTime.Parse("12-31-2016");
                    query = from t in context.Tasks
                            where t.DueDate < dueDate2016
                            select t;
                    foreach(Task t in query)
                    {
                        Console.WriteLine("Deleting task: " + t.ToString());
                        context.Tasks.Remove(t);
                    }
                    context.SaveChanges();

                    // Show tasks after the 'Delete' operation - there should be 0 tasks
                    Console.WriteLine("\nTasks after delete:");
                    List<Task> tasksAfterDelete = (from t in context.Tasks select t).ToList<Task>();
                    if (tasksAfterDelete.Count == 0)
                    {
                        Console.WriteLine("[None]");
                    }
                    else
                    {
                        foreach (Task t in query)
                        {
                            Console.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            PressAnyKey("finish");
        }

        public static void PressAnyKey(string ToWhat)
        {
            Console.WriteLine("Press any key to {0}...", ToWhat);
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}