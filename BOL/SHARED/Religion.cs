﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.SHARED
{
    [Table("Religion")]
    public class Religion
    {
        public Religion()
        {
            Castes = new HashSet<Caste>();
        }

        [Key]
        [Display(Name = "Religion ID")]
        public int Id { get; set; }

        [Display(Name = "Religion Name", Prompt = "Religion name")]
        [Required(ErrorMessage = "Religion required.")]
        public string Name { get; set; }
        public virtual ICollection<Caste> Castes { get; set; }
    }
}
