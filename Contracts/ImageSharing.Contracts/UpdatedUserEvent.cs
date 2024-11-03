using System;

namespace ImageSharing.Contracts
{
    public class UpdatedUserEvent
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? AvatarPath { get; set; }
    }
}