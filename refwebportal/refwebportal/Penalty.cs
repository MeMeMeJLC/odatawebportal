//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace refwebportal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Penalty
    {
        public int Id { get; set; }
        public int GamePlayerId { get; set; }
        public int PenaltyTypeId { get; set; }
        public System.TimeSpan PenaltyTime { get; set; }
    
        public virtual GamePlayer GamePlayer { get; set; }
        public virtual PenaltyType PenaltyType { get; set; }
    }
}
