//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task_Logger_Pro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uzer
    {
        public Uzer()
        {
            this.Applications = new HashSet<Aplication>();
            this.AppsToBlocks = new HashSet<AppsToBlock>();
            this.BlockedApps = new HashSet<BlockedApp>();
            this.FileLogs = new HashSet<FileLog>();
            this.Usages = new HashSet<Usage>();
        }
    
        public int UserID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Aplication> Applications { get; set; }
        public virtual ICollection<AppsToBlock> AppsToBlocks { get; set; }
        public virtual ICollection<BlockedApp> BlockedApps { get; set; }
        public virtual ICollection<FileLog> FileLogs { get; set; }
        public virtual ICollection<Usage> Usages { get; set; }
    }
}
