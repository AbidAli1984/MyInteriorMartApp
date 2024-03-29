﻿using BOL.LISTING.UploadImage;
using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("Listing", Schema = "listing")]
    public class Listing
    {
        [Key]
        [Display(Name = "Listing ID")]
        public int ListingID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        // Shafi: Date and time
        [Display(Name = "Created Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created Time")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "Name", Prompt = "Full Name")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        //[Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "Gender", Prompt = "Male, Female etc")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        //[Required(ErrorMessage = "Gender required.")]
        public string Gender { get; set; }

        [Display(Name = "Company Name", Prompt = "Xyz Pvt. Ltd. etc")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters allowed.")]
        [MinLength(5, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Company name required.")]
        public string CompanyName { get; set; }

        [Display(Name = "Year Of Establishment")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Year of establishment required.")]
        public DateTime YearOfEstablishment { get; set; }

        [Display(Name = "Number of Employees", Prompt = "Upto 5, Upto 50 etc.")]
        [Required(ErrorMessage = "Number of employees required.")]
        [Range(1, 100000, ErrorMessage = "Employees must be between 1 to 1,00,000")]
        public int NumberOfEmployees { get; set; }

        [Display(Name = "Designation", Prompt = "Proprietor, MD etc")]
        //[Required(ErrorMessage = "Designation required.")]
        public string Designation { get; set; }

        [Display(Name = "Nature of Business", Prompt = "Pvt., Ltd. etc")]
        [Required(ErrorMessage = "Nature of business required.")]
        public string NatureOfBusiness { get; set; }

        [Display(Name = "Turnover", Prompt = "1 lac or above etc.")]
        public string Turnover { get; set; }

        [Display(Name = "Listing URL")]
        [Required(ErrorMessage = "Listing URL required.")]
        [RegularExpression("^[a-zA-Z0-9_-]*$")]
        [MaxLength(1000, ErrorMessage = "Maximum 1000 characters allowed.")]
        [MinLength(5, ErrorMessage = "Minimum 5 characters rerquired.")]
        public string ListingURL { get; set; }

        [Display(Name = "BusinessC ategory")]
        [Required(ErrorMessage = "Business Category required.")]
        public string BusinessCategory { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status", Prompt = "Select Status")]
        public int Status { get; set; }

        [Display(Name = "Rejected", Prompt = "Rejected")]
        public bool Rejected { get; set; }

        [Display(Name = "Approved OR Rejected By", Prompt = "Approved OR Rejected By")]
        public bool ApprovedOrRejectedBy { get; set; }

        [Display(Name = "GST Number")]
        public string GSTNumber { get; set; }
        public int Steps { get; set; }

        public virtual LogoImage LogoImage { get; set; }

        public virtual Address Address { get; set; }

        #region Constants
        public const int UnApproved = 0;
        public const int Approved = 1;
        public const int Claimed = 2;
        #endregion
    }
}
