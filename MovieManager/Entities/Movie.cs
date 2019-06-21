using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Entities
{
    [Table("movie")]
    public class Movie
    {
        [Column("id")]
        public int id { get; set; }
        [Column("img")]
        public string img { get; set; }
        [Column("score")]
        public double score { get; set; }
        [Column("name")]
        public string  name { get; set; }
        [Column("duration")]
        public int duration { get; set; }
        [Column("category")]
        public string category { get; set; }
        [Column("ori_lang")]
        public string ori_lang { get; set; }
        [Column("star")]
        public string star { get; set; }
        [Column("director")]
        public string director { get; set; }
        [Column("release_day")]
        public DateTime release_day { get; set; } 
    }
}