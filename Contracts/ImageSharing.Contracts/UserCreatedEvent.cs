using System;

namespace ImageSharing.Contracts
{
    public sealed class UserCreatedEvent
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
