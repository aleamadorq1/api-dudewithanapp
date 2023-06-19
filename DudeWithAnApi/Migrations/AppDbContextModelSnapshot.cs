using System;
using DudeWithAnApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DudeWithAnApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("DudeWithAnApi.Models.Quote", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("CreationDate")
                    .HasColumnType("TEXT");

                b.Property<int?>("IsActive")
                    .HasColumnType("INTEGER");

                b.Property<int?>("IsDeleted")
                    .HasColumnType("INTEGER");

                b.Property<string>("QuoteText")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("SecondaryText")
                    .HasColumnType("TEXT");

                b.Property<string>("Url")
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Quotes");
            });

            modelBuilder.Entity("DudeWithAnApi.Models.QuotePrint", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("PrintedAt")
                    .HasColumnType("TEXT");

                b.Property<int>("QuoteId")
                    .HasColumnType("INTEGER");

                b.Property<string>("RequestId")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("QuoteId");

                b.ToTable("QuotePrints");
            });

            modelBuilder.Entity("DudeWithAnApi.Models.QuoteTranslation", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("QuoteId")
                    .HasColumnType("INTEGER");

                b.Property<int?>("IsDeleted")
                    .HasColumnType("INTEGER");

                b.Property<string>("LanguageCode")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("PrimaryText")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("SecondaryText")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("QuoteId");

                b.ToTable("QuoteTranslations");
            });

            modelBuilder.Entity("DudeWithAnApi.Models.QuotePrint", b =>
            {
                b.HasOne("DudeWithAnApi.Models.Quote", "Quote")
                    .WithMany()
                    .HasForeignKey("QuoteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Quote");
            });

            modelBuilder.Entity("DudeWithAnApi.Models.QuoteTranslation", b =>
            {
                b.HasOne("DudeWithAnApi.Models.Quote", "Quote")
                    .WithMany()
                    .HasForeignKey("QuoteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}
