using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEntity
{
    [Table("movie_detail")]
    public class MovieDetail
    {
        [Column("id")]
        public int id { get; set; }
        [Column("source")]
        public string source { get; set; }
        [Column("score_count")]
        public int score_count { get; set; }
        [Column("release_desc")]
        public string release_desc { get; set; }
        [Column("version")]
        public string version { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("eng_name")]
        public string eng_name { get; set; }

        public MovieDetail(string source, int score_count, string release_desc, string version, string description, string eng_name)
        {
            this.source = source;
            this.score_count = score_count;
            this.release_desc = release_desc;
            this.version = version;
            this.description = description;
            this.eng_name = eng_name;
        }
    }
}
