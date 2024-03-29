﻿using Server.Models.Utilities;

namespace Server.Models.DTO
{
    public class RegistrationRequestDTO
    {
        public string CharacterName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Race Race { get; set; }
    }
}
