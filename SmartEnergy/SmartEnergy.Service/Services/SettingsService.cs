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
    public class SettingsService : ISettingsService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public SettingsService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public SettingsDto GetDefaultSettings()
        {
            return _mapper.Map<SettingsDto>(_dbContext.Settings.Where( x => x.IsDefault == true));
        }

        public SettingsDto GetLastSettings()
        {
            return _mapper.Map<SettingsDto>(_dbContext.Settings.Last());
        }

        public void ResetToDefault()
        {
            foreach(Settings settings in _dbContext.Settings.Where(x => !x.IsDefault))
            {
                _dbContext.Settings.Remove(settings);
            }

            _dbContext.SaveChanges();
        }

        public void UpdateSettings(SettingsDto modified)
        {
            if (modified.IsDefault)
            {

                modified.ID = 0;

                _dbContext.Settings.Add(_mapper.Map<Settings>(modified));

            }
            else
            {

                Settings settings = _dbContext.Settings.Find(modified.ID);

                if (modified == null)
                    throw new ArgumentException($"Settings with {settings.ID} does not exists!");

                settings.UpdateSetting(_mapper.Map<Settings>(modified));
            }

            _dbContext.SaveChanges();


        }
    }
}
