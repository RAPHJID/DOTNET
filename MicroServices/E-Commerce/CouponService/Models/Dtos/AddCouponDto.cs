﻿using System.ComponentModel.DataAnnotations;

namespace CouponService.Models.Dtos
{
    public class AddCouponDto
    {
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        [Range(1000, 100000)]
        public int CouponAmount { get; set; }
        [Required]
        [Range(1000, int.MaxValue)]
        public int CouponMinAmount { get; set; }
    }
}
