﻿namespace Server.Models.DTO
{
    public class MonsterDTO
    {
        public int Id { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public List<StateDTO> States { get; set; } = new(); 
    }
}
