using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEntity
{
    [Table("forum")]
    public class Forum
    {
        [Column("id")]
        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("publisher_id")]
        public long publisher_id { get; set; }

        public Forum(string name, string description, long publisher_id)
        {
            this.name = name;
            this.description = description;
            this.publisher_id = publisher_id;
        }

    }
}
