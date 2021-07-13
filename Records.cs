using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIntroDemo
{
    
    public class Records
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DosesRequired { get; set; }
        public int Doses_between_Days { get; set; }
        public int Doses_Recieved { get; set; }

        public override string ToString()
        {
           return $"{Id}){Name}\t\t{DosesRequired}\t\t{Doses_between_Days}\t{Doses_Recieved}";
        }



    }
}

