using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public int MouthId { get; set; }

        [ForeignKey("MouthId")]
        public virtual Mouth Mouth { get; set; }

        public int HairId { get; set; }

        [ForeignKey("HairId")]
        public virtual Hair Hair { get; set; }

        public int NoseId { get; set; }

        [ForeignKey("NoseId")]
        public virtual Nose Nose { get; set; }

        public int EyesId { get; set; }

        [ForeignKey("EyesId")]
        public virtual Eye Eye { get; set; }

        public int ClotheId { get; set; }

        [ForeignKey("ClotheId")]
        public virtual Clothe Clothe { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}