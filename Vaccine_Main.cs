using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIntroDemo
{
    class Vaccine_Main
    {
         public static void HandleMainAppMenu(AppDBContext db)
         {
            int numberOfRecords = db.Vaccine.Select(x => x).Count();
            
            string choice = Console.ReadLine();

            try
            {

                if (int.Parse(choice) > 0 && int.Parse(choice) <= numberOfRecords)
                {
                    Console.Clear();
                    DisplayAddNoOfDoses(db, choice);

                }
                else if (int.Parse(choice) == 0 || int.Parse(choice) > numberOfRecords)
                {
                    Console.Clear();
                    Console.WriteLine("Records not found,Try again");

                }
            }
            catch (FormatException fe)
            {


            }
            finally
            {
                if ("a".Equals(choice.ToLower()))
                {
                    Console.Clear();
                    DisplayAddNewVaccine(db, choice);

                }
                else if ("x".Equals(choice.ToLower()))
                {
                   
                    System.Environment.Exit(0);

                }
                else
                {
                    Console.WriteLine("Invalid Input,Try Again");
                    PrintMainAppMenu(db);
                   // System.Environment.Exit(0);
                }
            }

        }

        private static void DisplayAddNoOfDoses(AppDBContext db, string choice)
        {
            int convert_choice = int.Parse(choice) ;
            
            var name_dt = db.Vaccine.Where(e => e.Id == convert_choice).Select(e => e.Name).FirstOrDefault();
            
            Console.WriteLine($"Vaccine Manager - {name_dt} ");
            Console.Write("Please Enter the how many new Doses received: ");
            int n_doses;
            bool check = int.TryParse(Console.ReadLine(), out n_doses);
            if (check)
            {
                var Doses_Recieved_dt=db.Vaccine.Where(e => e.Id == convert_choice).Select(e => e.Doses_Recieved).FirstOrDefault();

                var new_Doses_Recieved_dt = Doses_Recieved_dt + n_doses;

                //Update the Doses Recieved
                var update_doses = db.Vaccine.Find(convert_choice);
                update_doses.Doses_Recieved = new_Doses_Recieved_dt;
                db.SaveChanges();
                

                Console.WriteLine("\nTotal doses update...");
                PrintMainAppMenu(db);

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nInvalid Input...\n");
                DisplayAddNoOfDoses(db, choice);

            }
        }

        private static void DisplayAddNewVaccine(AppDBContext db, string choice)
        {
            
            Console.WriteLine("Enter Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Is it more than 1 doses? (y/n): ");
            string userinput_doses = Console.ReadLine();
            int total_doses_required;
            int days_btw_doses;
            if (userinput_doses == "y")
            {
                Console.WriteLine("Enter Total Doses required: ");
                total_doses_required = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter Days_btw_doses required: ");
                 days_btw_doses = Int32.Parse(Console.ReadLine());
            }
            else
            {
                total_doses_required = 1;
                days_btw_doses = 0;
            }
           
            Console.WriteLine("Enter Total Doses received: ");
            int total_doses_received;

            bool check = int.TryParse(Console.ReadLine(), out total_doses_received);
            if (check)
            {

                //Add new row
                var new_record = new Records
                {
                   
                    Name = Name,
                    DosesRequired = total_doses_required,
                    Doses_between_Days = days_btw_doses,
                    Doses_Recieved= total_doses_received

                };
                db.Vaccine.Add(new_record);
                db.SaveChanges();
              
                Console.WriteLine(format: "total Rows:",db.Vaccine.Select(x => x).Count());
                Console.WriteLine("\nnew Vaccine record Added...");
                PrintMainAppMenu(db);

                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("\nInvalid Input...\n");
                DisplayAddNewVaccine(db, choice);

            }
        }

        public static void PrintMainAppMenu(AppDBContext db)
         {
             Console.Clear();
             Console.WriteLine("VACCINE MANAGEMENT");
            
           
             Console.WriteLine("Name\t\tTotalDoses\tDosesBtwDays\tTotalReceived ");
             Console.WriteLine("--------------------------------------------------------------");
             printDataTable(db);
             Console.WriteLine("a) Add new Vaccine\n");
             Console.WriteLine("x) Exit\n");
             Console.WriteLine("Please enter your choice( Try again if same page appears)");

         }

         public static void printDataTable(AppDBContext db)
         {
            
            var qry = db.Vaccine.Select(x => x).ToList();
            
            foreach (var val in qry)
             {
               Console.WriteLine(val);

             }
           

        }

       public static void Main(string[] args)
       {
            using var db = new AppDBContext();

            while (true)
            {
                PrintMainAppMenu(db);
                HandleMainAppMenu(db);
                
            }
        }

    }
}
       