﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("City", Schema = "shared")]
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [Display(Name = "Country", Prompt = "Select country")]
        [Required(ErrorMessage = "Select country")]
        public Nullable<int> CountryID { get; set; }

        [Display(Name = "State", Prompt = "Select state")]
        [Required(ErrorMessage = "Select state")]
        public Nullable<int> StateID { get; set; }

        [Display(Name = "Name", Prompt = "Mumbai, Pune etc.")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        // Shafi: Navigation properties
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        // End:

        // Shafi: Show city in
        public IList<Location> Station { get; set; }
        // End:
    }
}
