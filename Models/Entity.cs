using System;

namespace OnlineShop.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? DeletedDate { get; set; }
    }
}