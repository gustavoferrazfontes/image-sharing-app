using System;

namespace ImageSharing.Contracts
{
    public sealed class CreatedUserEvent
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
    }
}
