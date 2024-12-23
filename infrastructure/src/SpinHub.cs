﻿using Microsoft.AspNetCore.SignalR;

namespace infrastructure.src
{
    public class SpinHub(IGameUserList gameUserList, ILogger<SpinHub> logger) : Hub
    {
        private const string GAME_STATE = nameof(GAME_STATE);
        private const string MESSAGE = nameof(MESSAGE);

        private const string START = nameof(START);
        private const string LOBBY = nameof(LOBBY);
        private const string SPINNER = nameof(SPINNER);
        private const string PERM_CHOOSEN = nameof(PERM_CHOOSEN);

        private readonly IGameUserList gameUserList = gameUserList;
        private readonly ILogger<SpinHub> _logger = logger;

        public async Task Subscribe(string gameId, string userId)
        {
            var success = gameUserList.AddUserToGame(gameId, userId);
            if (!success)
            {
                await Clients.Caller.SendAsync(MESSAGE, "Game does not exist.");
                _logger.LogError("Game does not exist.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync(MESSAGE, $"Joined game with id {gameId}");
            await Clients.Group(gameId).SendAsync(GAME_STATE, LOBBY);
            _logger.LogInformation($"Joined game with id {gameId}");
        }

        public async Task CreateGame(string gameId, string userId)
        {
            var success = gameUserList.AddGame(gameId, userId);
            if (!success)
            {
                await Clients.Caller.SendAsync(MESSAGE, "Failed to create game.");
                _logger.LogError("Failed to create game.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Caller.SendAsync(MESSAGE, $"Game created with id {gameId}");
            await Clients.Caller.SendAsync(GAME_STATE, LOBBY);
            _logger.LogInformation($"Joined game with id {gameId}");
        }

        public async Task StartGame(string gameId)
        {
            await Clients.Group(gameId).SendAsync(GAME_STATE, SPINNER);
        }

        public async Task StartSpinner(string gameId)
        {
            // Ved prod skal disse verdiene tweakes og brukes
            //var random = new Random();
            //double randomCircleCount = 1.1 + (random.NextDouble() * (1.8 - 1.1));
            //int randomIterations = random.Next(1, 8);
            //var totalIterations = randomCircleCount * randomIterations;
            var success = int.TryParse(gameId, out int num);
            if (!success)
            {
                num = 6;
            }

            var userId = gameUserList.GetFirst(gameId);
            var lastPos = 0;
            for (int i = 0; i < num; i++)
            {
                Thread.Sleep(700);
                await Clients.Group(gameId).SendAsync(PERM_CHOOSEN, userId);
                userId = gameUserList.GetNext(gameId, lastPos, out int nextPos);
                lastPos = nextPos;
                _logger.LogInformation($"User is choosen: {userId}");
            }
        }
    }
}