using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEntity
{
    [Table("comment")]
    public class Comment
    {
        [Column("id")]
        public int id { get; set; }
        [Column("user_id")]
        public long user_id { get; set; }
        [Column("movie_id")]
        public int movie_id { get; set; }
        [Column("content")]
        public string content { get; set; }
        [Column("time")]
        public DateTime time { get; set; }
        [Column("score")]
        public int score { get; set; }

        public Comment(int id, long user_id, int movie_id, string content, DateTime time, int score)
        {
            this.id = id;
            this.user_id = user_id;
            this.movie_id = movie_id;
            this.content = content;
            this.time = time;
            this.score = score;
        }

    }
}
