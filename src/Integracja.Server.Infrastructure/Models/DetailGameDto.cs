﻿using System;
using System.Collections.Generic;
using Integracja.Server.Core.Enums;

namespace Integracja.Server.Infrastructure.Models
{
    public class DetailGameDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GameState GameState { get; set; }
        public int QuestionsCount { get; set; }
        public int MaxPlayersCount { get; set; }
        public GamemodeDto Gamemode { get; set; }
        public ICollection<UserDto> Players { get; set; }
    }
}
