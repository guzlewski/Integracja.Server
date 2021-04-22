using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        public string Name { get; set; }

        public GamemodeModel Gamemode { get; set; } = new GamemodeModel();

        [DataType(DataType.Time)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartDate) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public DateTime StartDate { get; set; }

        public DateTime StartDateTime
        {
            get
            {
                return new DateTime( StartTime.TimeOfDay.Ticks + StartDate.Date.Ticks, StartDate.Kind).ToUniversalTime();
            }
            set
            {
                StartTime = new DateTime(value.TimeOfDay.Ticks, value.Kind).ToUniversalTime();
                StartDate = new DateTime(value.Date.Ticks, value.Kind).ToUniversalTime();
            }
        }

        [DataType(DataType.Time)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(StartDate))]
        public DateTime EndTime { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(StartDate) + "," + nameof(EndTime))]
        public DateTime EndDate { get; set; }

        public DateTime EndDateTime
        {
            get
            {
                return new DateTime(EndTime.TimeOfDay.Ticks + EndDate.Date.Ticks, EndDate.Kind ).ToUniversalTime();
            }
            set
            {
                EndTime = new DateTime(value.TimeOfDay.Ticks, value.Kind).ToUniversalTime();
                EndDate = new DateTime(value.Date.Ticks, value.Kind).ToUniversalTime();
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return EndDateTime - StartDateTime;
            }
        }

        public bool RandomizeQuestionOrder { get; set; }

        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }



    }
}
