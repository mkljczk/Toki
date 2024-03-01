﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Toki.ActivityPub.Persistence.DatabaseContexts;

#nullable disable

namespace Toki.ActivityPub.Migrations
{
    [DbContext(typeof(TokiDatabaseContext))]
    [Migration("20240301113621_Add ReceivedAt and move to DateTimeOffset")]
    partial class AddReceivedAtandmovetoDateTimeOffset
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Toki.ActivityPub.Models.Credentials", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.FollowRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ReceivedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RemoteId")
                        .HasColumnType("text");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("FollowRequests");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.FollowerRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FolloweeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FolloweeId");

                    b.HasIndex("FollowerId");

                    b.ToTable("FollowerRelations");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.Keypair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("PrivateKey")
                        .HasColumnType("text");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ReceivedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RemoteId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Keypairs");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContentWarning")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ReceivedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RemoteId")
                        .HasColumnType("text");

                    b.Property<bool>("Sensitive")
                        .HasColumnType("boolean");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.RemoteInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SharedInbox")
                        .HasColumnType("text");

                    b.Property<string>("Software")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Instances");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("BannerUrl")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Handle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inbox")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemote")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ParentInstanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ReceivedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RemoteId")
                        .HasColumnType("text");

                    b.Property<bool>("RequiresFollowApproval")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ParentInstanceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.Credentials", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Toki.ActivityPub.Models.Credentials", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.FollowRequest", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Toki.ActivityPub.Models.User", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.FollowerRelation", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.User", "Followee")
                        .WithMany()
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Toki.ActivityPub.Models.User", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followee");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.Keypair", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.User", "Owner")
                        .WithOne("Keypair")
                        .HasForeignKey("Toki.ActivityPub.Models.Keypair", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.Post", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Toki.ActivityPub.Models.Post", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Author");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.User", b =>
                {
                    b.HasOne("Toki.ActivityPub.Models.RemoteInstance", "ParentInstance")
                        .WithMany()
                        .HasForeignKey("ParentInstanceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentInstance");
                });

            modelBuilder.Entity("Toki.ActivityPub.Models.User", b =>
                {
                    b.Navigation("Keypair");
                });
#pragma warning restore 612, 618
        }
    }
}
