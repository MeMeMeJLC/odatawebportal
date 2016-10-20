using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refwebportal.Models
{
    public class ViewPlayers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
  /*      public int SquadNumber { get; set; }
        public bool IsCaptain { get; set; }*/
        public int TeamId { get; set; }

        //public virtual Team Team { get; set; }
       // public virtual Game Game { get; set; }

       /* public string FullNameAndTeam
        {
            get
            {
                return LastName + ", " + FirstName + " - " + Team.Name;
            }
        }*/
    }
}