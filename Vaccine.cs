using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Vaccine_Management
{
    public class Records
    {

        private static int idseed = 1;

        
        public string Name { get; set; }
        public int total_doses_required { get; set; }
        public int days_btw_doses { get; set; }
        public int total_doses_received { get; set; }

        Records() { }
        public Records(string Name,int total_doses_required, int days_btw_doses, int total_doses_received)
        {
            
            this.Name = Name;
            this.total_doses_required = total_doses_required;
            this.days_btw_doses = days_btw_doses;
            this.total_doses_received = total_doses_received;
        }
        public void print() { Console.WriteLine($"{Name}\t{total_doses_required}\t{days_btw_doses}\t{total_doses_received}"); }
        public override string ToString()
        {
            return $"{Name}\t{total_doses_required}\t{days_btw_doses}\t{total_doses_received}";
        }
    }
    public class Vaccine
    {
        private static void DisplayAddNoOfDoses(DataTable records_dt, string choice)
        {
            int convert_choice =int.Parse( choice)-1;
            //int totalcolumn = records_dt.Columns.Count;
            var name_dt = records_dt.Rows[convert_choice].Field<String>("Name");
            Console.WriteLine($"Vaccine Manager - {name_dt} ");
            Console.Write("Please Enter the how many new Doses received: ");
            int n_doses;
            bool check = int.TryParse(Console.ReadLine(), out n_doses);
            if (check)
            {
                var Doses_Recieved_dt = records_dt.Rows[convert_choice].Field<int>("Total_Doses_Recieved");
                var new_Doses_Recieved_dt = Doses_Recieved_dt + n_doses;

                for (int i = 0; i < records_dt.Columns.Count; i++)
                {
                    if (records_dt.Rows[convert_choice][i].ToString() == Doses_Recieved_dt.ToString())
                    {
                        
                        records_dt.Rows[convert_choice][i] = new_Doses_Recieved_dt; }
                   
                }

                Console.WriteLine("\nTotal doses update...");
                PrintMainAppMenu(records_dt);
               
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nInvalid Input...\n");
                DisplayAddNoOfDoses(records_dt, choice);

            }
            

        }

        private static void DisplayAddNewVaccine(DataTable records_dt,string choice)
        {
            int idseed = records_dt.Rows.Count+1;
            DataRow row;

            Console.WriteLine("Enter Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter Total Doses required: ");
            int total_doses_required= Int32.Parse(Console.ReadLine()) ;
            Console.WriteLine("Enter Days_btw_doses required: ");
            int days_btw_doses = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter Total Doses received: ");
            int total_doses_received;

            bool check = int.TryParse(Console.ReadLine(), out total_doses_received);
            if (check)
            {
                
                    row = records_dt.NewRow();
                    row["id"] = idseed;
                    row["Name"] = Name;
                    row["Doses_Required"] = total_doses_required;
                    row["Doses_between_Days"] = days_btw_doses;
                    row["Total_Doses_Recieved"] = total_doses_received;

                    records_dt.Rows.Add(row);

                    DataRow row2 = records_dt.NewRow();
                    records_dt.Rows.Add(row2);


               
                Console.WriteLine(records_dt.Rows.Count);
                Console.WriteLine("\nnew Vaccine record Added...");
                PrintMainAppMenu(records_dt);
                
                Console.ReadLine();
                
            }
            else
            {
                Console.WriteLine("\nInvalid Input...\n");
                DisplayAddNewVaccine(records_dt,choice);

            }

            
        }
        
        private static void HandleMainAppMenu(DataTable records_dt)
        {
           
            int numberOfRecords = records_dt.Rows.Count;
            string choice = Console.ReadLine();
          
            try {
               
                if (int.Parse(choice) > 0 && int.Parse(choice) <= numberOfRecords)
                {
                    Console.Clear();
                    DisplayAddNoOfDoses(records_dt, choice);

                }
                else if (int.Parse(choice) == 0 || int.Parse(choice) > numberOfRecords)
                {
                    Console.Clear();
                    Console.WriteLine("Records not found,Try again");
                    
                }
            }
            catch(FormatException fe)
            {
                
                
            }
            finally
            {
                 if ("a".Equals(choice.ToLower()))
                {
                   Console.Clear();
                    DisplayAddNewVaccine(records_dt,choice);

                }
                else if ("x".Equals(choice.ToLower()))
                {
                    Console.Clear();
                    System.Environment.Exit(0);

                }
            }
            
            
        }

        
        private static void PrintMainAppMenu(DataTable dt)
        {
            Console.Clear();
            Console.WriteLine("VACCINE MANAGEMENT");
            Console.WriteLine("Id\tName  Total  DosesBtwDays  TotalReceived ");
            Console.WriteLine("---------------------------------------------");
            printDataTable(dt);
            Console.WriteLine("a) Add new Vaccine\n");
            Console.WriteLine("x) Exit\n");
            Console.WriteLine("Please enter your choice( Try again if same page appears)");
           
           

        }
        private static void printDataTable(DataTable dt)
        {

            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {

                    Console.Write(item);
                    Console.Write("\t");
                }
                Console.WriteLine("\n");
            }

        }
        private static DataTable VaccineTable()
        {
            DataTable dt = new DataTable("Vaccine");

            //Adding columns to table Vaccine
            DataColumn colEmpid = new DataColumn("id", typeof(System.Int32));
            DataColumn colName = new DataColumn("Name", typeof(System.String));
            DataColumn colDosesRequired = new DataColumn("Doses_Required", typeof(System.Int32));
            DataColumn colDoses_between_Days = new DataColumn("Doses_between_Days", typeof(System.Int32));
            DataColumn colDoses_Recieved = new DataColumn("Total_Doses_Recieved", typeof(System.Int32));

            //Adding columns to datatable
            dt.Columns.AddRange(new DataColumn[] { colEmpid, colName, colDosesRequired, colDoses_between_Days, colDoses_Recieved });

            //Adding data
            dt.Rows.Add(1, "pfizer",  2,  21,  10000);
            dt.Rows.Add(2,"J&J", 1,  0, 5000);

            return dt;
        }

        static void Main(string[] args)
        {
            DataTable dt = VaccineTable();
           // Console.Clear();
            while (true)
            {
                PrintMainAppMenu(dt);
                HandleMainAppMenu(dt);
            }
        }

    }
}