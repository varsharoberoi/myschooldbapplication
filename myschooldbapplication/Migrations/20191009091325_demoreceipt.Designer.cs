﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using myschooldbapplication.Models;

namespace myschooldbapplication.Migrations
{
    [DbContext(typeof(myschooldbContext))]
    [Migration("20191009091325_demoreceipt")]
    partial class demoreceipt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("myschooldbapplication.Controllers.TestModel", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("TestModel");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Admission1", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ad_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FTId")
                        .HasColumnName("f_T_id");

                    b.Property<int?>("FeeYear")
                        .HasColumnName("fee_year");

                    b.Property<int?>("PaidAmount");

                    b.Property<int?>("Pendingamt")
                        .HasColumnName("pendingamt");

                    b.Property<int?>("StId")
                        .HasColumnName("st_id");

                    b.Property<int?>("StdId")
                        .HasColumnName("std_id");

                    b.Property<int?>("Totalfees")
                        .HasColumnName("totalfees");

                    b.HasKey("AdId");

                    b.HasIndex("FTId");

                    b.HasIndex("StId");

                    b.HasIndex("StdId");

                    b.ToTable("admission1");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Admissiopay", b =>
                {
                    b.Property<int>("AdId")
                        .HasColumnName("ad_id");

                    b.Property<int>("AdTId")
                        .HasColumnName("ad_t_id");

                    b.Property<string>("BankBranch")
                        .HasColumnName("bankbranch");

                    b.Property<string>("BankName")
                        .HasColumnName("bankname");

                    b.Property<DateTime?>("Chequedt")
                        .HasColumnName("Chequedt");

                    b.Property<string>("Chequeno")
                        .HasColumnName("chequeno");

                    b.Property<DateTime?>("PayDate")
                        .IsRequired()
                        .HasColumnName("pay_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Pay_mode")
                        .HasColumnName("pay_mode");

                    b.Property<bool?>("Paystatus")
                        .IsRequired()
                        .HasColumnName("paystatus");

                    b.Property<int?>("Receiptno")
                        .HasColumnName("receiptno");

                    b.Property<string>("Transactionno")
                        .HasColumnName("transactiono");

                    b.HasKey("AdId", "AdTId");

                    b.HasIndex("AdTId");

                    b.HasIndex("Receiptno");

                    b.ToTable("admissiopay");
                });

            modelBuilder.Entity("myschooldbapplication.Models.AdTypeFee", b =>
                {
                    b.Property<int>("AdTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ad_T_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdYear")
                        .HasColumnName("ad_year");

                    b.Property<int?>("FTId")
                        .HasColumnName("f_t_id");

                    b.Property<string>("Term")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int?>("TermAmount");

                    b.Property<DateTime?>("TermDate")
                        .HasColumnName("Term_date")
                        .HasColumnType("datetime");

                    b.HasKey("AdTId");

                    b.ToTable("Ad_Type_Fee");
                });

            modelBuilder.Entity("myschooldbapplication.Models.FeeType", b =>
                {
                    b.Property<int>("FTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("f_T_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FeeYear");

                    b.Property<string>("Feetype1")
                        .HasColumnName("Feetype")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Feeyeartext");

                    b.Property<int?>("TotalFees");

                    b.HasKey("FTId");

                    b.ToTable("FeeType");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<byte[]>("Data");

                    b.Property<int>("Height");

                    b.Property<int>("Length");

                    b.Property<string>("Name");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("myschooldbapplication.Models.myview", b =>
                {
                    b.Property<int?>("AdmissionID");

                    b.Property<int?>("TermID");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("EmailID");

                    b.Property<int?>("PaidAmount");

                    b.Property<bool?>("PaidStatus");

                    b.Property<string>("ParentName");

                    b.Property<int?>("PendingAmt");

                    b.Property<string>("Standard");

                    b.Property<string>("StudentName");

                    b.Property<string>("Term");

                    b.Property<int>("TermWisePending");

                    b.Property<int?>("TotalFees");

                    b.Property<int?>("TypeID");

                    b.HasKey("AdmissionID", "TermID");

                    b.ToTable("myview");
                });

            modelBuilder.Entity("myschooldbapplication.Models.ReceiptStudent", b =>
                {
                    b.Property<int?>("Receiptno")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankBranch");

                    b.Property<string>("BankName");

                    b.Property<DateTime?>("Chequedt");

                    b.Property<string>("Chequeno");

                    b.Property<DateTime?>("PayDate")
                        .IsRequired();

                    b.Property<string>("Pay_mode");

                    b.Property<string>("Transactionno");

                    b.HasKey("Receiptno");

                    b.ToTable("ReceiptStudent");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Std", b =>
                {
                    b.Property<int>("StdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("std_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Stname")
                        .HasColumnName("stname")
                        .HasMaxLength(40);

                    b.HasKey("StdId");

                    b.ToTable("Std");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Student", b =>
                {
                    b.Property<int>("StId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("st_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnName("Address");

                    b.Property<string>("ContentType");

                    b.Property<byte[]>("Data");

                    b.Property<DateTime>("Dob")
                        .HasColumnName("DOB")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnName("EmailId")
                        .HasMaxLength(80)
                        .IsUnicode(false);

                    b.Property<string>("Gender");

                    b.Property<int?>("Height");

                    b.Property<Guid>("Id");

                    b.Property<int?>("Length");

                    b.Property<string>("MotherName");

                    b.Property<string>("MotherOccu");

                    b.Property<string>("MotherPhone");

                    b.Property<string>("MotherQualification");

                    b.Property<string>("MotherWhatsapp");

                    b.Property<string>("Name");

                    b.Property<string>("Nationality");

                    b.Property<string>("Occupation");

                    b.Property<string>("POB");

                    b.Property<string>("ParentMobile")
                        .IsRequired()
                        .HasColumnName("Parent_Mobile")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("ParentName")
                        .IsRequired()
                        .HasColumnName("Parent_Name")
                        .HasMaxLength(80)
                        .IsUnicode(false);

                    b.Property<string>("ParentWhatsappNo");

                    b.Property<string>("Qualification");

                    b.Property<string>("StName")
                        .IsRequired()
                        .HasColumnName("st_name")
                        .HasMaxLength(40);

                    b.Property<int?>("Width");

                    b.HasKey("StId");

                    b.ToTable("student");
                });

            modelBuilder.Entity("myschooldbapplication.Models.UserInfo", b =>
                {
                    b.Property<string>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("u_id");

                    b.Property<string>("Password")
                        .HasColumnName("U_password");

                    b.HasKey("UserID");

                    b.ToTable("Userinfo");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Admission1", b =>
                {
                    b.HasOne("myschooldbapplication.Models.FeeType", "FT")
                        .WithMany("Admission1")
                        .HasForeignKey("FTId")
                        .HasConstraintName("FK__admission__f_T_i__24927208");

                    b.HasOne("myschooldbapplication.Models.Student", "St")
                        .WithMany("Admission1")
                        .HasForeignKey("StId")
                        .HasConstraintName("FK__admission__st_id__267ABA7A");

                    b.HasOne("myschooldbapplication.Models.Std", "Std")
                        .WithMany("Admission1")
                        .HasForeignKey("StdId")
                        .HasConstraintName("FK__admission__std_i__286302EC");
                });

            modelBuilder.Entity("myschooldbapplication.Models.Admissiopay", b =>
                {
                    b.HasOne("myschooldbapplication.Models.Admission1", "Ad")
                        .WithMany("Admissiopay")
                        .HasForeignKey("AdId")
                        .HasConstraintName("c99");

                    b.HasOne("myschooldbapplication.Models.AdTypeFee", "AdT")
                        .WithMany("Admissiopay")
                        .HasForeignKey("AdTId")
                        .HasConstraintName("c100");

                    b.HasOne("myschooldbapplication.Models.ReceiptStudent", "ReceiptStudent")
                        .WithMany()
                        .HasForeignKey("Receiptno");
                });
#pragma warning restore 612, 618
        }
    }
}
