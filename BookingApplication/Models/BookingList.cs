﻿using System.ComponentModel.DataAnnotations;

namespace BookingApplication.Models
{
    public class BookingList
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public BookingApplicationUser? User { get; set; }
        public virtual ICollection<BookReservation>? BookReservations { get; set; }
        public int? fullPrice { get; set; }
    }
}