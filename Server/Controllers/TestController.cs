﻿using Microsoft.AspNetCore.Mvc;
using Server.Models.Utilities;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly IServiceProvider services;

        public TestController(UserStorage _userStorage, IServiceProvider _services)
        {
            userStorage = _userStorage;
            services = _services;
        }

        [HttpGet("ActiveUsers")]
        public async Task<IActionResult> GetActiveUsers()
        {
            await Task.Delay(10);
            if (userStorage.ActiveUsers.Count <= 0) return BadRequest(RespFactory.ReturnBadRequest("Активных юзеров нет"));

            List<string> result = new();

            foreach (var user in userStorage.ActiveUsers)
            {
                int index = userStorage.ActiveUsers.IndexOf(user);
                result.Add($"[{index}] Name: {user.Name} ConnectionId: {user.ConnectionId}");
            }
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("ActiveDiscTokens")]
        public async Task<IActionResult> GetActiveDisconnectTokens()
        {
            await Task.Delay(10);
            if (userStorage.disconnectTokens.Count <= 0) return BadRequest(RespFactory.ReturnBadRequest("Активных токенов нет"));

            List<string> result = new();

            foreach (var item in userStorage.disconnectTokens)
            {
                result.Add($"ConnectionId: {item.Key} Status: {item.Value.Token.IsCancellationRequested}");
            }
            return Ok(RespFactory.ReturnOk(result));
        }

        [HttpGet("GetBattleLogs")]
        public async Task<IActionResult> TestBattleLogs(string name)
        {
            var user = userStorage.ActiveUsers.FirstOrDefault(x => x.Name == name);

            if (user == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(user.BattleLogs));
        }
    }
}