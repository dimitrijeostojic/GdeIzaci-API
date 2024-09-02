﻿using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class UpdateReviewDto
    {
        [Required]
        public double NumberOfStars { get; set; }
        [Required]
        public Guid PlaceID { get; set; }
    }
}
