﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleChat.Server
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorImage> AuthorImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var topicBuilder = modelBuilder.Entity<Topic>();
            var messageBuilder = modelBuilder.Entity<Message>();
            var authorBuilder = modelBuilder.Entity<Author>();
            var authorImageBuilder = modelBuilder.Entity<AuthorImage>();

            BuildEntity(topicBuilder);
            BuildEntity(messageBuilder);
            BuildEntity(authorBuilder);
            BuildEntity(authorImageBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void BuildEntity(EntityTypeBuilder<AuthorImage> builder)
        {
            builder.ToTable("author_image").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("author_id")
                ;

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("data")
                ;

            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasColumnName("content_type")
                .HasMaxLength(100)
                ;

            builder.Property(x => x.Filename)
                .IsRequired()
                .HasColumnName("file_name")
                .HasMaxLength(255)
                ;

        }

        private void BuildEntity(EntityTypeBuilder<Author> authorBuilder)
        {
            authorBuilder.ToTable("author").HasKey(x => x.Id);

            authorBuilder.HasOne(x => x.Image)
                .WithOne(x => x.Author)
                .HasForeignKey<AuthorImage>(x => x.Id);

            authorBuilder.Property(x => x.Id)
                         .IsRequired()
                         .ValueGeneratedOnAdd()
                         .HasColumnName("author_id")
                         ;

            authorBuilder.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnName("name")
                        ;
        }

        private void BuildEntity(EntityTypeBuilder<Message> messageBuilder)
        {
            messageBuilder.ToTable("message").HasKey(x => x.Id);

            messageBuilder.Property(x => x.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("message_id")
                        ;

            messageBuilder.Property(x => x.AuthorId)
                        .IsRequired()
                        .HasColumnName("author_id")
                        ;

            messageBuilder.Property(x => x.Date)
                        .IsRequired()
                        .HasColumnName("date")
                        ;

            messageBuilder.Property(x => x.TopicId)
                        .IsRequired()
                        .HasColumnName("topic_id")
                        ;

            messageBuilder.Property(x => x.Text)
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnName("text")
                        ;

            messageBuilder.HasOne(x => x.Topic)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.TopicId)
                ;

            messageBuilder.HasOne(x => x.Author)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.AuthorId)
                ;
        }

        private static void BuildEntity(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Topic> topicBuilder)
        {
            topicBuilder.ToTable("topic").HasKey(x => x.Id);

            topicBuilder.Property(x => x.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("topic_id");

            topicBuilder.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnName("name");

            topicBuilder.Property(x => x.Description)
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnName("description");
        }
    }
}
