﻿namespace EfCore
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public Blog Blog { get; set; }
    }
}