﻿// <auto-generated />
using System;
using CleanTasks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanTasks.Persistence.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    partial class TodoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CleanTasks.Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int>("Size");

                    b.Property<int?>("TodoId");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("AttachmentId");

                    b.HasIndex("TodoId");

                    b.HasIndex("Name", "TodoId")
                        .IsUnique()
                        .HasFilter("[TodoId] IS NOT NULL");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.Todo", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedTo")
                        .HasMaxLength(25);

                    b.Property<string>("AssignedToName")
                        .HasMaxLength(100);

                    b.Property<string>("CloseReason")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<int?>("LinkedTodoId");

                    b.Property<byte[]>("Rowversion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("TodoAreaId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("TodoId");

                    b.HasIndex("LinkedTodoId");

                    b.HasIndex("TodoAreaId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoArea", b =>
                {
                    b.Property<int>("TodoAreaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("TodoAreaId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TodoAreas");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoAreaPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TodoAreaId");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("TodoAreaId");

                    b.ToTable("TodoAreaPermissions");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TodoId")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("CommentId");

                    b.HasIndex("TodoId");

                    b.HasIndex("Value");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TodoId");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("TagId");

                    b.HasIndex("TodoId");

                    b.HasIndex("Value", "TodoId")
                        .IsUnique()
                        .HasFilter("[TodoId] IS NOT NULL");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("CleanTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Attachments")
                        .HasForeignKey("TodoId");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.Todo", b =>
                {
                    b.HasOne("CleanTasks.Domain.Entities.Todo", "LinkedTodo")
                        .WithMany("LinkedTodos")
                        .HasForeignKey("LinkedTodoId");

                    b.HasOne("CleanTasks.Domain.Entities.TodoArea", "TodoArea")
                        .WithMany("Todos")
                        .HasForeignKey("TodoAreaId");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoAreaPermission", b =>
                {
                    b.HasOne("CleanTasks.Domain.Entities.TodoArea", "TodoArea")
                        .WithMany("Permissions")
                        .HasForeignKey("TodoAreaId");
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoComment", b =>
                {
                    b.HasOne("CleanTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Comments")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CleanTasks.Domain.Entities.TodoTag", b =>
                {
                    b.HasOne("CleanTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Tags")
                        .HasForeignKey("TodoId");
                });
#pragma warning restore 612, 618
        }
    }
}
