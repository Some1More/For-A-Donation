﻿// <auto-generated />
using System;
using For_A_Donation.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace For_A_Donation.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230306092854_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Family", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryOfTask")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PointsEnd")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RewardId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryOfReward")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsGotten")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryOfTask")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTimeFinish")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExecutorId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FamilyId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.UserProgress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryOfTask")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProgress");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Progress", b =>
                {
                    b.HasOne("For_A_Donation.Models.DataBase.Reward", "Reward")
                        .WithMany("Progress")
                        .HasForeignKey("RewardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reward");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Task", b =>
                {
                    b.HasOne("For_A_Donation.Models.DataBase.User", null)
                        .WithMany("CompleteTasks")
                        .HasForeignKey("UserId");

                    b.HasOne("For_A_Donation.Models.DataBase.User", null)
                        .WithMany("CreatedTasks")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.User", b =>
                {
                    b.HasOne("For_A_Donation.Models.DataBase.Family", "Family")
                        .WithMany("Members")
                        .HasForeignKey("FamilyId");

                    b.Navigation("Family");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.UserProgress", b =>
                {
                    b.HasOne("For_A_Donation.Models.DataBase.User", "User")
                        .WithMany("Progress")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Family", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.Reward", b =>
                {
                    b.Navigation("Progress");
                });

            modelBuilder.Entity("For_A_Donation.Models.DataBase.User", b =>
                {
                    b.Navigation("CompleteTasks");

                    b.Navigation("CreatedTasks");

                    b.Navigation("Progress");
                });
#pragma warning restore 612, 618
        }
    }
}
