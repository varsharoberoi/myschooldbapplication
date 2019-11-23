using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using myschooldbapplication.Controllers;
using myschooldbapplication.Models;

namespace myschooldbapplication.Models
{
    
    public partial class myschooldbContext : DbContext
    {
        public myschooldbContext()
        {
        }

        public myschooldbContext(DbContextOptions<myschooldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admission1> Admission1 { get; set; }
        public virtual DbSet<Admissiopay> Admissiopay { get; set; }
        public virtual DbSet<AdTypeFee> AdTypeFee { get; set; }
        public virtual DbSet<FeeType> FeeType { get; set; }
        public virtual DbSet<Std> Std { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<myview> Myviews { get;set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Image> Images { get; set; }
                                           //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                                           //        {
                                           //            if (!optionsBuilder.IsConfigured)
                                           //            {
                                           //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                                           //                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2CPMJ6J\\SQLEXPRESS;Initial Catalog=myschooldb;Integrated Security=True");
                                           //            }
                                           //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity => { entity.HasKey(e => e.Id);  });

            modelBuilder.Entity<Admission1>(entity =>
            {
                entity.HasKey(e => e.AdId);

                entity.ToTable("admission1");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.FTId).HasColumnName("f_T_id");

                entity.Property(e => e.FeeYear).HasColumnName("fee_year");

                entity.Property(e => e.Pendingamt).HasColumnName("pendingamt");

                entity.Property(e => e.StId).HasColumnName("st_id");

                entity.Property(e => e.StdId).HasColumnName("std_id");

                entity.Property(e => e.Totalfees).HasColumnName("totalfees");

                entity.HasOne(d => d.FT)
                    .WithMany(p => p.Admission1)
                    .HasForeignKey(d => d.FTId)
                    .HasConstraintName("FK__admission__f_T_i__24927208");

                entity.HasOne(d => d.St)
                    .WithMany(p => p.Admission1)
                    .HasForeignKey(d => d.StId)
                    .HasConstraintName("FK__admission__st_id__267ABA7A");

                entity.HasOne(d => d.Std)
                    .WithMany(p => p.Admission1)
                    .HasForeignKey(d => d.StdId)
                    .HasConstraintName("FK__admission__std_i__286302EC");
            });
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => new { e.UserID });
                entity.ToTable("Userinfo");
                entity.Property(e => e.UserID).HasColumnName("u_id");
                entity.Property(e => e.Password).HasColumnName("U_password");
            }
            );
            modelBuilder.Entity<Admissiopay>(entity =>
            {
                entity.HasKey(e => new { e.AdId, e.AdTId });

                entity.ToTable("admissiopay");

                entity.Property(e => e.AdId).HasColumnName("ad_id");

                entity.Property(e => e.AdTId).HasColumnName("ad_t_id");

                entity.Property(e => e.PayDate)
                    .HasColumnName("pay_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pay_mode).HasColumnName("pay_mode");
                entity.Property(e => e.Receiptno).HasColumnName("receiptno");
                entity.Property(e => e.Transactionno).HasColumnName("transactiono");
                entity.Property(e => e.Chequeno).HasColumnName("chequeno");

                entity.Property(e => e.Chequedt).HasColumnName("Chequedt");
                entity.Property(e => e.BankBranch).HasColumnName("bankbranch");
                entity.Property(e => e.BankName).HasColumnName("bankname");

                entity.Property(e => e.Paystatus).HasColumnName("paystatus");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Admissiopay)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c99");

                entity.HasOne(d => d.AdT)
                    .WithMany(p => p.Admissiopay)
                    .HasForeignKey(d => d.AdTId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c100");
            });

            modelBuilder.Entity<AdTypeFee>(entity =>
            {
                entity.HasKey(e => e.AdTId);

                entity.ToTable("Ad_Type_Fee");

                entity.Property(e => e.AdTId).HasColumnName("ad_T_id");

                entity.Property(e => e.AdYear).HasColumnName("ad_year");

                entity.Property(e => e.FTId).HasColumnName("f_t_id");

                entity.Property(e => e.Term)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TermDate)
                    .HasColumnName("Term_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FeeType>(entity =>
            {
                entity.HasKey(e => e.FTId);

                entity.Property(e => e.FTId).HasColumnName("f_T_id");

                entity.Property(e => e.Feetype1)
                    .HasColumnName("Feetype")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Std>(entity =>
            {
                entity.Property(e => e.StdId).HasColumnName("std_id");

                entity.Property(e => e.Stname)
                    .HasColumnName("stname")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StId);

                entity.ToTable("student");

                entity.Property(e => e.StId).HasColumnName("st_id");
                entity.Property(e => e.Address).HasColumnName("Address");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.ParentMobile)
                    .IsRequired()
                    .HasColumnName("Parent_Mobile")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ParentName)
                    .IsRequired()
                    .HasColumnName("Parent_Name")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StName)
                    .IsRequired()
                    .HasColumnName("st_name")
                    .HasMaxLength(40);
                entity.Property(e =>e.EmailId).HasColumnName("EmailId").HasMaxLength(80).IsUnicode(false);

            });
            
            modelBuilder.Entity<myview>(entity =>
            {
                entity.HasKey(e => new { e.AdmissionID, e.TermID });

                entity.ToTable("myview");

               
            });

        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2CPMJ6J\\SQLEXPRESS;Initial Catalog=myschooldb;Integrated Security=True");
//            }
//        }

        public DbSet<myschooldbapplication.Controllers.TestModel> TestModel { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2CPMJ6J\\SQLEXPRESS;Initial Catalog=myschooldb;Integrated Security=True");
//            }
//        }

        public DbSet<myschooldbapplication.Models.UserInfo> UserInfo { get; set; }
    }
}
