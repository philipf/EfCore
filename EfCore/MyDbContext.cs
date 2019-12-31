﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=.;Initial Catalog=MyDatabase;persist security info=True;Integrated Security=True";

        optionsBuilder.UseSqlServer(connectionString)
            . EnableSensitiveDataLogging();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }


    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}