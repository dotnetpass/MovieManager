using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Entities
{
    [Table("user_forum")]
    public class UserForum
    {
        [Column("id")]
        public int id { get; set; }
        [Column("user_id")]
        public long user_id { get; set; }
        [Column("forum_id")]
        public int forum_id { get; set; }

        public UserForum(long user_id, int forum_id)
        {
            this.user_id = user_id;
            this.forum_id = forum_id;
        }

    }
}
