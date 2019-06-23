using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEntity
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public long id { get; set; }
        [Column("avatar_url")]
        public string avatar_url { get; set; }
        [Column("gender")]
        public int gender { get; set; }
        [Column("nick")]
        public string nick { get; set; }
        [Column("password")]
        public string password { get; set; }

        public User(long id, string avatar_url, int gender, string nick, string password)
        {
            this.id = id;
            this.avatar_url = avatar_url;
            this.gender = gender;
            this.nick = nick;
            this.password = password;
        }
    }
}
