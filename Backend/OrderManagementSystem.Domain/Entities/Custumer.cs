using OrderManagementSystem.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Entities
{
    public class Customer : IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [MaxLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }
    }
}
