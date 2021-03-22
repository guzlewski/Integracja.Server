﻿// <auto-generated />
using System;
using Integracja.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Integracja.Server.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210312212947_AddGameOverField")]
    partial class AddGameOverField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("GameState")
                        .HasColumnType("int");

                    b.Property<int>("GamemodeId")
                        .HasColumnType("int");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaxPlayersCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionsCount")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("GamemodeId");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Gamemode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfLives")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<int?>("TimeForFullQuiz")
                        .HasColumnType("int");

                    b.Property<int?>("TimeForOneQuestion")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Gamemodes");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<float>("NegativePoints")
                        .HasColumnType("real");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<float>("PositivePoints")
                        .HasColumnType("real");

                    b.Property<int>("QuestionScoring")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileThumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SessionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameQuestion", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<float?>("OverrideNegativePoints")
                        .HasColumnType("real");

                    b.Property<float?>("OverridePositivePoints")
                        .HasColumnType("real");

                    b.HasKey("GameId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("GameQuestions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUser", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AnsweredQuestions")
                        .HasColumnType("int");

                    b.Property<int>("CorrectlyAnsweredQuestions")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("GameEndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("GameOver")
                        .HasColumnType("bit");

                    b.Property<float?>("GameScore")
                        .HasColumnType("real");

                    b.Property<DateTimeOffset?>("GameStartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("GameUserState")
                        .HasColumnType("int");

                    b.Property<int>("IncorrectlyAnsweredQuestions")
                        .HasColumnType("int");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUserQuestion", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnswered")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("QuestionAnswerTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("QuestionDownloadTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<float?>("QuestionScore")
                        .HasColumnType("real");

                    b.HasKey("GameId", "UserId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.HasIndex("GameId", "QuestionId");

                    b.ToTable("GameUserQuestions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUserQuestionAnswer", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "UserId", "QuestionId", "AnswerId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.HasIndex("GameId", "QuestionId");

                    b.ToTable("GameUserQuestionAnswers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Answer", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Category", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "Owner")
                        .WithMany("CreatedCategories")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Game", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Gamemode", "Gamemode")
                        .WithMany("Games")
                        .HasForeignKey("GamemodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "Owner")
                        .WithMany("CreatedGames")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gamemode");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Gamemode", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "Owner")
                        .WithMany("CreatedGamemodes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Question", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "Owner")
                        .WithMany("CreatedQuestions")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameQuestion", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Game", "Game")
                        .WithMany("Questions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Base.Question", "Question")
                        .WithMany("GameQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUser", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "User")
                        .WithMany("GameUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUserQuestion", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Game", "Game")
                        .WithMany("GameUserQuestions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Base.Question", "Question")
                        .WithMany("GameUserQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "User")
                        .WithMany("GameUserQuestions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Joins.GameQuestion", "GameQuestion")
                        .WithMany("GameUserQuestions")
                        .HasForeignKey("GameId", "QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Joins.GameUser", "GameUser")
                        .WithMany("GameUserQuestions")
                        .HasForeignKey("GameId", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("GameQuestion");

                    b.Navigation("GameUser");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUserQuestionAnswer", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Base.Answer", "Answer")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Base.Game", "Game")
                        .WithMany("GameUserQuestionAnswer")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Base.Question", "Question")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", "User")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Joins.GameQuestion", "GameQuestion")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("GameId", "QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Joins.GameUser", "GameUser")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("GameId", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Joins.GameUserQuestion", "GameUserQuestion")
                        .WithMany("GameUserQuestionAnswers")
                        .HasForeignKey("GameId", "UserId", "QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Game");

                    b.Navigation("GameQuestion");

                    b.Navigation("GameUser");

                    b.Navigation("GameUserQuestion");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Integracja.Server.Core.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Integracja.Server.Core.Models.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Answer", b =>
                {
                    b.Navigation("GameUserQuestionAnswers");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Category", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Game", b =>
                {
                    b.Navigation("GameUserQuestionAnswer");

                    b.Navigation("GameUserQuestions");

                    b.Navigation("GameUsers");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Gamemode", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Base.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("GameQuestions");

                    b.Navigation("GameUserQuestionAnswers");

                    b.Navigation("GameUserQuestions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Identity.User", b =>
                {
                    b.Navigation("CreatedCategories");

                    b.Navigation("CreatedGamemodes");

                    b.Navigation("CreatedGames");

                    b.Navigation("CreatedQuestions");

                    b.Navigation("GameUserQuestionAnswers");

                    b.Navigation("GameUserQuestions");

                    b.Navigation("GameUsers");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameQuestion", b =>
                {
                    b.Navigation("GameUserQuestionAnswers");

                    b.Navigation("GameUserQuestions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUser", b =>
                {
                    b.Navigation("GameUserQuestionAnswers");

                    b.Navigation("GameUserQuestions");
                });

            modelBuilder.Entity("Integracja.Server.Core.Models.Joins.GameUserQuestion", b =>
                {
                    b.Navigation("GameUserQuestionAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
