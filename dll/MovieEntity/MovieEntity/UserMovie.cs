using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEntity
{
    [Table("user_movie")]
    public class UserMovie
    {
        [Column("id")]
        public long id { get; set; }
        [Column("userId")]
        public long user_id { get; set; }
        [Column("movieId")]
        public long movie_id { get; set; }

        public UserMovie(long user_id, long movie_id)
        {
            this.user_id = user_id;
            this.movie_id = movie_id;
        }
    }
}
