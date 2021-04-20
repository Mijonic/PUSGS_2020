using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergyDomainModels;

namespace SmartEnergy.Service.Services
{
    public class IconService : IIconService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public IconService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void AddIconToSettings(int iconId, int settingsId)
        {

            if (_dbContext.Settings.Find(settingsId) == null )
                throw new InvalidOperationException($"Setting with {settingsId} does not exists!");


            Icon icon = _dbContext.Icons.Find(iconId);

            if (icon == null)
                throw new ArgumentException($"Icon with {iconId} does not exists!");


            icon.SettingsID = settingsId;

            _dbContext.SaveChanges();
        }

        public List<IconDto> GetAllIcons()
        {
            return _mapper.Map<List<IconDto>>(_dbContext.Icons.ToList());
        }

        public IconDto GetIconById(int iconId)
        {
            return _mapper.Map<IconDto>(_dbContext.Icons.Find(iconId));
        }

        public void RemoveIconFromSettings(int iconId, int settingsId)
        {
            if (_dbContext.Settings.Find(settingsId) == null)
                throw new InvalidOperationException($"Setting with {settingsId} does not exists!");



            Icon icon = _dbContext.Icons.Find(iconId);
            
            if(icon == null)
                throw new ArgumentException($"Icon with {iconId} does not exists!");


            icon.SettingsID = null;

            _dbContext.SaveChanges();


        }
    }
}
