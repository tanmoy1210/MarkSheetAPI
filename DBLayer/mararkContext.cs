using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MT.DBLayer
{
    public partial class mararkContext : DbContext
    {
        public mararkContext()
        {
        }

        public mararkContext(DbContextOptions<mararkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EAdmApprovals> EAdmApprovals { get; set; }
        public virtual DbSet<EAdmDetails> EAdmDetails { get; set; }
        public virtual DbSet<EAdmPrePaDetails> EAdmPrePaDetails { get; set; }
        public virtual DbSet<EStdAcademicDetails> EStdAcademicDetails { get; set; }
        public virtual DbSet<EStdDetails> EStdDetails { get; set; }
        public virtual DbSet<LoginRequestAttempt> LoginRequestAttempt { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<MBehaviours> MBehaviours { get; set; }
        public virtual DbSet<MBloodgroups> MBloodgroups { get; set; }
        public virtual DbSet<MCasteCat> MCasteCat { get; set; }
        public virtual DbSet<MClient> MClient { get; set; }
        public virtual DbSet<MCountries> MCountries { get; set; }
        public virtual DbSet<MCountry> MCountry { get; set; }
        public virtual DbSet<MGenders> MGenders { get; set; }
        public virtual DbSet<MMenuGrpMaster> MMenuGrpMaster { get; set; }
        public virtual DbSet<MMenuRolePermission> MMenuRolePermission { get; set; }
        public virtual DbSet<MMtounges> MMtounges { get; set; }
        public virtual DbSet<MOptionmaster> MOptionmaster { get; set; }
        public virtual DbSet<MRelations> MRelations { get; set; }
        public virtual DbSet<MReligions> MReligions { get; set; }
        public virtual DbSet<MSchool> MSchool { get; set; }
        public virtual DbSet<MState> MState { get; set; }
        public virtual DbSet<MUserTypes> MUserTypes { get; set; }
        public virtual DbSet<MUsers> MUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=pass##;database=marark");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EAdmApprovals>(entity =>
            {
                entity.HasKey(e => e.ApvId)
                    .HasName("PRIMARY");

                entity.ToTable("e_adm_approvals");

                entity.Property(e => e.ApvId)
                    .HasColumnName("apv_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Aid)
                    .HasColumnName("aid")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ApprovedBy)
                    .HasColumnName("approved_by")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<EAdmDetails>(entity =>
            {
                entity.HasKey(e => e.Aid)
                    .HasName("PRIMARY");

                entity.ToTable("e_adm_details");

                entity.Property(e => e.Aid)
                    .HasColumnName("aid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AadharNo)
                    .HasColumnName("aadhar_no")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ApplicationStatus)
                    .HasColumnName("application_status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisalOfChild)
                    .HasColumnName("appraisal_of_child")
                    .HasMaxLength(2000)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AyId)
                    .HasColumnName("ay_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BloodGroupId)
                    .HasColumnName("blood_group_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CasteCategoryId)
                    .HasColumnName("caste_category_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("district_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FormNo)
                    .HasColumnName("form_no")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GenderId)
                    .HasColumnName("gender_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianContactNo1)
                    .HasColumnName("guardian_contact_no_1")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianContactNo2)
                    .HasColumnName("guardian_contact_no_2")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianName)
                    .HasColumnName("guardian_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianRelationId)
                    .HasColumnName("guardian_relation_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdentificationMark1)
                    .HasColumnName("identification_mark_1")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdentificationMark2)
                    .HasColumnName("identification_mark_2")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastAprvId)
                    .HasColumnName("last_aprv_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MotherTongueId)
                    .HasColumnName("mother_tongue_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Pincode)
                    .HasColumnName("pincode")
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PlaceOfBirth)
                    .HasColumnName("place_of_birth")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PreviousIllnessHistory)
                    .HasColumnName("previous_illness_history")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReligionId)
                    .HasColumnName("religion_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResidentialAddress)
                    .HasColumnName("residential_address")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SecondLanguageIdOpted)
                    .HasColumnName("second_language_id_opted")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SecondLanguageIdPrevious)
                    .HasColumnName("second_language_id_previous")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<EAdmPrePaDetails>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PRIMARY");

                entity.ToTable("e_adm_pre_pa_details");

                entity.Property(e => e.RecordId)
                    .HasColumnName("record_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Aid)
                    .HasColumnName("aid")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AyId)
                    .HasColumnName("ay_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SchoolLocation)
                    .HasColumnName("school_location")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SchoolName)
                    .HasColumnName("school_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<EStdAcademicDetails>(entity =>
            {
                entity.HasKey(e => e.StdAdId)
                    .HasName("PRIMARY");

                entity.ToTable("e_std_academic_details");

                entity.Property(e => e.StdAdId)
                    .HasColumnName("std_ad_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AyId)
                    .HasColumnName("ay_id")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RollNo)
                    .HasColumnName("roll_no")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<EStdDetails>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("e_std_details");

                entity.Property(e => e.Sid)
                    .HasColumnName("SID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AadharNo)
                    .HasColumnName("aadhar_no")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Aid)
                    .HasColumnName("AID")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AyId)
                    .HasColumnName("ay_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BloodGroupId)
                    .HasColumnName("blood_group_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CasteCategoryId)
                    .HasColumnName("caste_category_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("district_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GenderId)
                    .HasColumnName("gender_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianContactNo1)
                    .HasColumnName("guardian_contact_no_1")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianContactNo2)
                    .HasColumnName("guardian_contact_no_2")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianName)
                    .HasColumnName("guardian_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GuardianRelationId)
                    .HasColumnName("guardian_relation_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdentificationMark1)
                    .HasColumnName("identification_mark_1")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdentificationMark2)
                    .HasColumnName("identification_mark_2")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastStdAdId)
                    .HasColumnName("last_std_ad_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MotherTongueId)
                    .HasColumnName("mother_tongue_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Pincode)
                    .HasColumnName("pincode")
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PlaceOfBirth)
                    .HasColumnName("place_of_birth")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReligionId)
                    .HasColumnName("religion_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResidentialAddres)
                    .HasColumnName("residential_addres")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RollNo)
                    .HasColumnName("roll_no")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<LoginRequestAttempt>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PRIMARY");

                entity.ToTable("login_request_attempt");

                entity.Property(e => e.Rid)
                    .HasColumnName("RID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AttemptUser)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClientId)
                    .HasColumnName("ClientId")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.KeyToAuthenticate)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Logins>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("logins");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(10);

                entity.Property(e => e.DeviceId)
                    .HasColumnName("device_id")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProImage).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Utype)
                    .HasColumnName("UType")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0=Member, 1=Admin, 2=mobile reg");
            });

            modelBuilder.Entity<MBehaviours>(entity =>
            {
                entity.HasKey(e => e.BehaviourId)
                    .HasName("PRIMARY");

                entity.ToTable("m_behaviours");

                entity.Property(e => e.BehaviourId)
                    .HasColumnName("behaviour_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BehaviourName)
                    .HasColumnName("behaviour_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MBloodgroups>(entity =>
            {
                entity.HasKey(e => e.BloodGroupId)
                    .HasName("PRIMARY");

                entity.ToTable("m_bloodgroups");

                entity.Property(e => e.BloodGroupId)
                    .HasColumnName("blood_group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BloodGroup)
                    .HasColumnName("blood_group")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                 .HasColumnName("status")
                 .HasColumnType("int(11)")
                 .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<MCasteCat>(entity =>
            {
                entity.HasKey(e => e.CatseCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("m_caste_cat");

                entity.Property(e => e.CatseCategoryId)
                    .HasColumnName("catse_category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CasteCategory)
                    .HasColumnName("caste_category")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                  .HasColumnName("status")
                  .HasColumnType("int(11)")
                  .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<MClient>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("PRIMARY");

                entity.ToTable("m_client");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RegDate)
                    .HasColumnName("reg_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SchoolName)
                    .HasColumnName("school_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MCountries>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PRIMARY");

                entity.ToTable("m_countries");

                entity.Property(e => e.CountryId)
                    .HasColumnName("COUNTRY_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MCountry>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PRIMARY");

                entity.ToTable("m_country");

                entity.Property(e => e.Cid)
                    .HasColumnName("cid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Cname)
                    .HasColumnName("cname")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<MGenders>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PRIMARY");

                entity.ToTable("m_genders");

                entity.Property(e => e.GenderId)
                    .HasColumnName("gender_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GenderName)
                    .HasColumnName("gender_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MMenuGrpMaster>(entity =>
            {
                entity.HasKey(e => e.MenuGrpId)
                    .HasName("PRIMARY");

                entity.ToTable("m_menu_grp_master");

                entity.Property(e => e.MenuGrpId)
                    .HasColumnName("menu_grp_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MenuGrpDesc)
                    .HasColumnName("menu_grp_desc")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuGrpName)
                    .HasColumnName("menu_grp_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UnderMenuGrpId)
                    .HasColumnName("under_menu_grp_id")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MMenuRolePermission>(entity =>
            {
                entity.ToTable("m_menu_role_permission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Permission)
                    .HasColumnName("permission")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MMtounges>(entity =>
            {
                entity.HasKey(e => e.MotherToungeId)
                    .HasName("PRIMARY");

                entity.ToTable("m_mtounges");

                entity.Property(e => e.MotherToungeId)
                    .HasColumnName("mother_tounge_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MotherTounge)
                    .HasColumnName("mother_tounge")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MOptionmaster>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PRIMARY");

                entity.ToTable("m_optionmaster");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MToolTip)
                    .HasColumnName("m_tool_tip")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuDesc)
                    .HasColumnName("menu_desc")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuGrpId)
                    .HasColumnName("menu_grp_id")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuName)
                    .HasColumnName("menu_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuType)
                    .HasColumnName("menu_type")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PageLevel)
                    .HasColumnName("page_level")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PageLink)
                    .HasColumnName("page_link")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Settings)
                    .HasColumnName("settings")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MRelations>(entity =>
            {
                entity.HasKey(e => e.RelationId)
                    .HasName("PRIMARY");

                entity.ToTable("m_relations");

                entity.Property(e => e.RelationId)
                    .HasColumnName("relation_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelationName)
                    .HasColumnName("relation_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                 .HasColumnName("status")
                 .HasColumnType("int(11)")
                 .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<MReligions>(entity =>
            {
                entity.HasKey(e => e.ReligionId)
                    .HasName("PRIMARY");

                entity.ToTable("m_religions");

                entity.Property(e => e.ReligionId)
                    .HasColumnName("religion_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReligionName)
                    .HasColumnName("religion_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MSchool>(entity =>
            {
                entity.HasKey(e => e.SchoolId)
                    .HasName("PRIMARY");

                entity.ToTable("m_school");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_clientid");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccountHolderName)
                    .HasColumnName("account_holder_name")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AccountNumber)
                    .HasColumnName("account_number")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.BankId)
                    .HasColumnName("bank_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IfscCode)
                    .HasColumnName("ifsc_code")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MenuId)
                    .HasColumnName("menu_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MobileNumber)
                    .HasColumnName("mobile_number")
                    .HasMaxLength(13)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SchoolName)
                    .HasColumnName("school_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.MSchool)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_clientid");
            });

            modelBuilder.Entity<MState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PRIMARY");

                entity.ToTable("m_state");

                entity.HasIndex(e => e.CountryId)
                    .HasName("fk_country_id");

                entity.Property(e => e.StateId)
                    .HasColumnName("state_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StateName)
                    .HasColumnName("state_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MState)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_country_id");
            });

            modelBuilder.Entity<MUserTypes>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("m_user_types");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RoleDesc)
                    .HasColumnName("role_desc")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                   .HasColumnName("status")
                   .HasColumnType("int(11)")
                   .HasDefaultValueSql("'1'");

            });

            modelBuilder.Entity<MUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("m_users");

                //entity.HasIndex(e => e.ClientId)
                //    .HasName("fk_client_id");

                //entity.HasIndex(e => e.RoleId)
                //    .HasName("fk_role_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MobileNumber)
                    .HasColumnName("mobile_number")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'"); 

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserPassword)
                    .HasColumnName("user_password")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UserRegId)
                    .HasColumnName("user_reg_id")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastStatusAt)
                   .HasColumnName("last_status_at")
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("'NULL'");

                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.MUsers)
                //    .HasForeignKey(d => d.ClientId)
                //    .HasConstraintName("fk_client_id");

                //entity.HasOne(d => d.Role)
                //    .WithMany(p => p.MUsers)
                //    .HasForeignKey(d => d.RoleId)
                //    .HasConstraintName("fk_role_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
