using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MovieEntity
{
    [Table("discussion")]
    public class Discussion
    {
        [Column("id")]
        public int id { get; set; }
        [Column("content")]
        public string content { get; set; }
        [Column("time")]
        public DateTime time { get; set; }
        [Column("user_id")]
        public long user_id { get; set; }
        [Column("forum_id")]
        public int forum_id { get; set; }

        public Discussion(string content, DateTime time, long user_id, int forum_id)
        {
            this.content = content;
            this.time = time;
            this.user_id = user_id;
            this.forum_id = forum_id;
        }

    }
}
