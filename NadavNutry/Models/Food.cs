//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NadavNutry.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Food
    {
        public string Name { get; set; }
        public string Units { get; set; }
        public Nullable<int> Calories { get; set; }
        public Nullable<int> Proteins { get; set; }
        public Nullable<int> Carbs { get; set; }
        public Nullable<int> Fats { get; set; }
        public string ImagePath { get; set; }
    }
}
