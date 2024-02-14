﻿using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DTO;
using Server.Models.Utilities;

namespace Server.Repository
{
    public class TravelRepository
    {
        private readonly DbUserContext dbContext;

        public TravelRepository(DbUserContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<TravelDTO> GetArea(TravelDTO travel)
        {
            var user = await dbContext.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CharacterName == travel.CharacterName);

            if (user != null)
            {
                return new TravelDTO
                {
                    CharacterName = user.CharacterName,
                    Area = user.CurrentArea
                };
            }

            return null;
        }

        public async Task<TravelDTO> GoNewArea(TravelDTO travel)
        {
            var user = await dbContext.Characters
                .FirstOrDefaultAsync(x => x.CharacterName == travel.CharacterName);

            if (user != null)
            {
                user.CurrentArea = travel.Area;
                await dbContext.SaveChangesAsync();
                return new TravelDTO
                {
                    CharacterName = user.CharacterName,
                    Area = user.CurrentArea
                };
            }

            return null;
        }
    }
}