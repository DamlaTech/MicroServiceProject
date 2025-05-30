﻿// <auto-generated />
using System;
using APP.Identities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APP.Identities.Migrations
{
    [DbContext(typeof(IdentitiesDb))]
    [Migration("20250516030032_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.16");

            modelBuilder.Entity("APP.Identities.Domain.Identity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("IdentityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("APP.Identities.Domain.IdentityQualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdentityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QualificationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.HasIndex("QualificationId");

                    b.ToTable("IdentityQualifications");
                });

            modelBuilder.Entity("APP.Identities.Domain.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("APP.Identities.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("APP.Identities.Domain.Identity", b =>
                {
                    b.HasOne("APP.Identities.Domain.Role", "Role")
                        .WithMany("identities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APP.Identities.Domain.IdentityQualification", b =>
                {
                    b.HasOne("APP.Identities.Domain.Identity", "Identity")
                        .WithMany("IdentityQualifications")
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APP.Identities.Domain.Qualification", "Qualification")
                        .WithMany("IdentityQualifications")
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Identity");

                    b.Navigation("Qualification");
                });

            modelBuilder.Entity("APP.Identities.Domain.Identity", b =>
                {
                    b.Navigation("IdentityQualifications");
                });

            modelBuilder.Entity("APP.Identities.Domain.Qualification", b =>
                {
                    b.Navigation("IdentityQualifications");
                });

            modelBuilder.Entity("APP.Identities.Domain.Role", b =>
                {
                    b.Navigation("identities");
                });
#pragma warning restore 612, 618
        }
    }
}
