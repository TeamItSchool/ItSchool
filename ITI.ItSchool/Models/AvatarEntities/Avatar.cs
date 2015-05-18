using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.AvatarEntities
{
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [ForeignKey( "MouthId" )]
        public int MouthId { get; set; }

        public Mouth Mouth { get; set; }

        [ForeignKey( "HairId" )]
        public int HairId { get; set; }

        public Hair Hair { get; set; }

        [ForeignKey( "NoseId" )]
        public int NoseId { get; set; }

        public Nose Nose { get; set; }

        [ForeignKey( "EyesId" )]
        public int EyesId { get; set; }

        public Eye Eye { get; set; }

        [ForeignKey( "ClotheId" )]
        public int ClotheId { get; set; }

        public Clothe Clothe { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}