using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF.ListeFromBusinessProcess.Dto
{
    public class RkiDataElement
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int Cases { get; set; }

        public int Deaths { get; set; }

        public int CasesPerWeek { get; set; }

        public int Recovered { get; set; }

        public double WeekIncidence { get; set; }


    }


}
