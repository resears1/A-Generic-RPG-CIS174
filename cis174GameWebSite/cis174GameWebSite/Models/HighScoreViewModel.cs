using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cis174GameWebSite.Models
{
    public class HighScoreViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScoreId { get; set; }
        public long Score { get; set; }
        public string UserId { get; set; }
    }
}
