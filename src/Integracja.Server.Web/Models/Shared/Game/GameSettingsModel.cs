using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Integracja.Server.Web.Models.Shared.Time;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        [Required(ErrorMessage = "Podaj nazwę gry")]
        public string GameName { get; set; }
        public GamemodeModel Gamemode { get; set; } = new GamemodeModel();
        public bool RandomizeQuestionOrder { get; set; }
        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = DateTimeRequiredErrorMessage)]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartDate) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = DateTimeRequiredErrorMessage)]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset StartDateTime
        {
            get => GetStartDateTimeOffset(StartDate.Offset);
            set
            {
                StartDate = new DateTimeOffset(value.Date, value.Offset);
                StartTime = value.TimeOfDay;
            }
        }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = DateTimeRequiredErrorMessage)]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(StartDate))]
        public TimeSpan EndTime { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = DateTimeRequiredErrorMessage)]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(StartDate) + "," + nameof(EndTime))]
        public DateTimeOffset EndDate { get; set; }

        public DateTimeOffset EndDateTime
        {
            get => GetEndDateTimeOffset(EndDate.Offset);
            set
            {
                EndDate = new DateTimeOffset(value.Date, value.Offset );
                EndTime = value.TimeOfDay;
            }
        }

        public TimeSpan Duration => (EndDateTime - StartDateTime);

        public void SetTimeZone(TimeZoneInfo timeZone)
        {   
            EndDateTime = EndDateTime.ToOffset(timeZone.GetUtcOffset(EndDateTime));
            StartDateTime = StartDateTime.ToOffset(timeZone.GetUtcOffset(StartDateTime));
        }

        public const string DateTimeRequiredErrorMessage = "Podaj czas rozpoczęcia i zakończenia";

        private DateTimeOffset GetEndDateTimeOffset(TimeSpan timeZoneOffset) => new (EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds, timeZoneOffset);
        private DateTimeOffset GetStartDateTimeOffset(TimeSpan timeZoneOffset) => new (StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds, timeZoneOffset);
    }
}
