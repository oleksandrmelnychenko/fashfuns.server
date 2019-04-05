﻿// <auto-generated />
using System;
using FashFuns.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FashFuns.Database.Migrations
{
    [DbContext(typeof(FashFunsDbContext))]
    [Migration("20190225141347_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FashFuns.Domain.Entities.Identity.UserIdentity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanUserResetExpiredPassword");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPasswordExpired");

                    b.Property<DateTime?>("LastModified");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PasswordExpiresAt");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserIdentities");
                });

            modelBuilder.Entity("FashFuns.Domain.Entities.Identity.UserIdentityRoleType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Created");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModified");

                    b.Property<int>("RoleType");

                    b.HasKey("Id");

                    b.ToTable("UserIdentityRoleType");
                });

            modelBuilder.Entity("FashFuns.Domain.Entities.Identity.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Created");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModified");

                    b.Property<long>("UserIdentityId");

                    b.Property<long?>("UserRoleTypeId");

                    b.HasKey("Id");

                    b.HasIndex("UserIdentityId");

                    b.HasIndex("UserRoleTypeId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("FashFuns.Domain.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("FashFuns.Domain.Entities.Identity.UserIdentity", "UserIdentity")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserIdentityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FashFuns.Domain.Entities.Identity.UserIdentityRoleType", "UserRoleType")
                        .WithMany()
                        .HasForeignKey("UserRoleTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
