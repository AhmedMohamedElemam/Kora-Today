//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kora_Today.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MatchTv
    {
        public System.Guid MatchTvId { get; set; }
        public string MatchVideoId { get; set; }
        public string MatchVideoName { get; set; }
        public int MatchId { get; set; }
    
        public virtual Match Match { get; set; }
    }
}
