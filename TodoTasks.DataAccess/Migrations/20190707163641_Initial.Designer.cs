﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoTasks.DataAccess;

namespace TodoTasks.DataAccess.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20190707163641_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoTasks.Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("FileType")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<long>("Size");

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

            modelBuilder.Entity("TodoTasks.Domain.Entities.Todo", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedTo")
                        .HasMaxLength(50);

                    b.Property<string>("CloseReason");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<int?>("LinkedTodoId");

                    b.Property<bool?>("Notify");

                    b.Property<byte[]>("Rowversion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("TodoAreaId")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("TodoId");

                    b.HasIndex("LinkedTodoId");

                    b.HasIndex("TodoAreaId");

                    b.HasIndex("Title", "TodoAreaId")
                        .IsUnique();

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TodoTasks.Domain.Entities.TodoArea", b =>
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

            modelBuilder.Entity("TodoTasks.Domain.Entities.TodoComment", b =>
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

            modelBuilder.Entity("TodoTasks.Domain.Entities.TodoTag", b =>
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

            modelBuilder.Entity("TodoTasks.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("TodoTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Attachments")
                        .HasForeignKey("TodoId");
                });

            modelBuilder.Entity("TodoTasks.Domain.Entities.Todo", b =>
                {
                    b.HasOne("TodoTasks.Domain.Entities.Todo", "LinkedTodo")
                        .WithMany("LinkedTodos")
                        .HasForeignKey("LinkedTodoId");

                    b.HasOne("TodoTasks.Domain.Entities.TodoArea", "TodoArea")
                        .WithMany("Todos")
                        .HasForeignKey("TodoAreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TodoTasks.Domain.Entities.TodoComment", b =>
                {
                    b.HasOne("TodoTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Comments")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TodoTasks.Domain.Entities.TodoTag", b =>
                {
                    b.HasOne("TodoTasks.Domain.Entities.Todo", "Todo")
                        .WithMany("Tags")
                        .HasForeignKey("TodoId");
                });
#pragma warning restore 612, 618
        }
    }
}
