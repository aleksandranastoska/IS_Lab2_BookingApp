
using System.ComponentModel.DataAnnotations;

namespace BookingApplication.Models
{
    public class BookReservation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int NumberOfNights { get; set; }
        public Guid? ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        public Guid? BookingListId { get; set; }
        public BookingList? BookingList { get; set; }
        public string? UserId { get; set; }
        public BookingApplicationUser? User { get; set; }
    }
}