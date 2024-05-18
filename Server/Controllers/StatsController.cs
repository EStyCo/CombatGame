﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers;
using Server.Models.Utilities;
using Server.Repository;
using System.Net;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly UserRepository userRep;
        private readonly IMapper mapper;

        public StatsController(UserStorage _userStorage, 
                               UserRepository _userRepository, 
                               IMapper _mapper)
        {
            userStorage = _userStorage;
            userRep = _userRepository;
            mapper = _mapper;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetStats(NameRequestDTO dto)
        {
            StatsHandler? stats = userStorage.ActiveUsers
                .Where(u => u.Name == dto.Name)
                .Select(u => u.Stats)
                .FirstOrDefault();

            if (stats == null) return BadRequest(RespFactory.ReturnBadRequest());

            return Ok(RespFactory.ReturnOk(mapper.Map<StatDTO>(stats)));
        }

        [HttpPut("update")]
        public async Task<IActionResult> ChangeStats(UpdateStatDTO dto)
        {
            var stats = userStorage.ActiveUsers
                .Where(x => x.Name == dto.Name)
                .Select(x => x.Stats)
                .FirstOrDefault();

            if (stats == null || !await userRep.UserExists(dto.Name)) return BadRequest(RespFactory.ReturnBadRequest());

            await userRep.UpdateStats(dto);

            var newCounts = await userRep.GetStats(dto.Name);
            if (newCounts != null)
            {
                stats.CreateStats(newCounts);
            }





           // await userStorage.ChangeStats(dto);
            return Ok(RespFactory.ReturnOk());
        }
    }
}