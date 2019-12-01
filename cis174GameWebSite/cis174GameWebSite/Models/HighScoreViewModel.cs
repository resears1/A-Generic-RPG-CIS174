using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cis174GameWebSite.Models
{
    public class HighScoreViewModel
    {
        [Key]
        public int ScoreId { get; set; }
        public long Score { get; set; }
        public int UserId { get; set; }
    }
}
 