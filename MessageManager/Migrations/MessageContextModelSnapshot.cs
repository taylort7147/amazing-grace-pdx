﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MessageManager.Migrations
{
    [DbContext(typeof(MessageContext))]
    partial class MessageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MessageManager.Models.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DownloadUrl")
                        .IsRequired();

                    b.Property<int>("MessageId");

                    b.Property<string>("StreamUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MessageId")
                        .IsUnique();

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("MessageManager.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AudioId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4095);

                    b.Property<int?>("NotesId");

                    b.Property<int?>("SeriesId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<int?>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("MessageManager.Models.Notes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MessageId");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MessageId")
                        .IsUnique();

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("MessageManager.Models.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MessageManager.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MessageId");

                    b.Property<int>("MessageStartTimeSeconds");

                    b.Property<string>("YouTubePlaylistId");

                    b.Property<string>("YouTubeVideoId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MessageId")
                        .IsUnique();

                    b.ToTable("Video");
                });

            modelBuilder.Entity("MessageManager.Models.Audio", b =>
                {
                    b.HasOne("MessageManager.Models.Message", "Message")
                        .WithOne("Audio")
                        .HasForeignKey("MessageManager.Models.Audio", "MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MessageManager.Models.Message", b =>
                {
                    b.HasOne("MessageManager.Models.Series", "Series")
                        .WithMany("Messages")
                        .HasForeignKey("SeriesId");
                });

            modelBuilder.Entity("MessageManager.Models.Notes", b =>
                {
                    b.HasOne("MessageManager.Models.Message", "Message")
                        .WithOne("Notes")
                        .HasForeignKey("MessageManager.Models.Notes", "MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MessageManager.Models.Video", b =>
                {
                    b.HasOne("MessageManager.Models.Message", "Message")
                        .WithOne("Video")
                        .HasForeignKey("MessageManager.Models.Video", "MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
