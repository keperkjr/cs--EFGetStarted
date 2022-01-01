using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFGetStarted
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public string DbPath { get; }

        public BloggingContext(DbContextOptions<BloggingContext> options)
             : base(options)
        {
        }
        public BloggingContext()
        {
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(ConnectionsString);
        }

        private string GetConnectionsString()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appconfig.json", optional: false).Build();
            return config.GetConnectionString("blogConnection");
        }

        private string mConnection = null;
        public string ConnectionsString { 
            get { 
                if (mConnection == null)
                {
                    mConnection = GetConnectionsString();
                }
                return mConnection;
            } 
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}