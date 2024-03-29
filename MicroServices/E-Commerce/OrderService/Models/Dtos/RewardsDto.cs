﻿namespace OrderService.Models.Dtos
{
    public class RewardsDto
    {
        public Guid OrderId { get; set; }

        public double TotalAmount { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
