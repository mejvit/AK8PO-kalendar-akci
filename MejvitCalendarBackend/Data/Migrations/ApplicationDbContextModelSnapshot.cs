// <auto-generated />
using System;
using MejvitCalendarBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MejvitCalendarBackend.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventBaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("RRule")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("StreamUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PlaceId");

                    b.ToTable("EventBase");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventCategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("EventCategory");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventExceptionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EventBaseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventBaseId");

                    b.ToTable("EventException");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventInstanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EventBaseId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EventBaseId");

                    b.ToTable("EventInstance");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.PlaceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DefaultStreamUrl")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double?>("GpsLattitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("GpsLongitude")
                        .HasColumnType("double precision");

                    b.Property<string>("HomePageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventBaseEntity", b =>
                {
                    b.HasOne("MejvitCalendarBackend.Models.Entities.EventCategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MejvitCalendarBackend.Models.Entities.PlaceEntity", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventExceptionEntity", b =>
                {
                    b.HasOne("MejvitCalendarBackend.Models.Entities.EventBaseEntity", "EventBase")
                        .WithMany("EventExceptions")
                        .HasForeignKey("EventBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventBase");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventInstanceEntity", b =>
                {
                    b.HasOne("MejvitCalendarBackend.Models.Entities.EventBaseEntity", "EventBase")
                        .WithMany()
                        .HasForeignKey("EventBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventBase");
                });

            modelBuilder.Entity("MejvitCalendarBackend.Models.Entities.EventBaseEntity", b =>
                {
                    b.Navigation("EventExceptions");
                });
#pragma warning restore 612, 618
        }
    }
}
