using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class EStdDetails
    {
        public long Sid { get; set; }
        public long? Aid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? ClassId { get; set; }
        public int? AyId { get; set; }
        public string AadharNo { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public string ResidentialAddres { get; set; }
        public string Pincode { get; set; }
        public int? ReligionId { get; set; }
        public int? GenderId { get; set; }
        public int? CasteCategoryId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? BloodGroupId { get; set; }
        public string IdentificationMark1 { get; set; }
        public string IdentificationMark2 { get; set; }
        public string RollNo { get; set; }
        public int? Status { get; set; }
        public long? LastStdAdId { get; set; }
        public string GuardianName { get; set; }
        public int? GuardianRelationId { get; set; }
        public string GuardianContactNo1 { get; set; }
        public string GuardianContactNo2 { get; set; }
    }
}
