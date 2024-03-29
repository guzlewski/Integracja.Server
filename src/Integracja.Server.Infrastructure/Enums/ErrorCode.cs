﻿namespace Integracja.Server.Infrastructure.Enums
{
    public enum ErrorCode
    {
        // GameLogicRepository.GetQuestion
        AlreadyAnsweredAllQuestions = 1,

        // GameLogicRepository.SaveAnswers
        AlreadyAnsweredThisQuestion,

        // GameUserRepository.Join
        AlreadyJoinedGame,

        // GameLogicRepository.GetQuestion, GameLogicRepository.SaveAnswers
        GameHasBeenCancelled,

        // GameUserRepository.Join, GameUserRepository.Leave, GameLogicRepository.GetQuestion, GameLogicRepository.SaveAnswers
        GameHasEnded,

        // GameUserRepository.Join
        GameIsFull,

        // GameLogicRepository.GetQuestion, GameLogicRepository.SaveAnswers
        GameIsOver,

        // GameLogicRepository.GetQuestion, GameLogicRepository.SaveAnswers
        GameTimeHasExpired,

        // GameLogicRepository.SaveAnswers
        QuestionTimeHasExpired,

        // GameLogicRepository.GetQuestion GameLogicRepository.SaveAnswers
        GameHasNotStarted
    }
}
