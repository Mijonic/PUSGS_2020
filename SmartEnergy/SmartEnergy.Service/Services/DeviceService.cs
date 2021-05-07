using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergyDomainModels;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.Enums;

namespace SmartEnergy.Service.Services
{
    public class DeviceService : IDeviceService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeviceService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        
        public void Delete(int id)
        {
            Device device = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(id));

            if (device == null)
                throw new DeviceNotFoundException($"Device with Id = {id} does not exists!");

            _dbContext.Devices.Remove(device);
            _dbContext.SaveChanges();
        }

        public DeviceDto Get(int id)
        {
            return _mapper.Map<DeviceDto>(_dbContext.Devices.Include("Location").FirstOrDefault(x => x.ID == id));
        }

        public List<DeviceDto> GetAll()
        {
            return _mapper.Map<List<DeviceDto>>(_dbContext.Devices.Include("Location").ToList());         

        }

        public DeviceDto Insert(DeviceDto entity)
        {
            Device newDevice = _mapper.Map<Device>(entity);
            
            newDevice.ID = 0;
            newDevice.Location = null;

            //if (entity.Name.Trim().Equals("") || entity.Name == null)
            //    throw new InvalidDeviceException("You have to enter device name!");

            if(!Enum.IsDefined(typeof(DeviceType), entity.DeviceType))
                throw new InvalidDeviceException("Undefined device type!");

            if (_dbContext.Location.Any(x => x.ID == entity.LocationID) == false)
                throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

            Device deviceWithMaxCounter = _dbContext.Devices.FirstOrDefault(x => x.DeviceCounter == _dbContext.Devices.Max(y => y.DeviceCounter));

            if (deviceWithMaxCounter == null)
            {
                newDevice.Name = $"{newDevice.DeviceType.ToString().Substring(0, 3)}1";
                newDevice.DeviceCounter = 1;
            }      
            else
            {
                newDevice.Name = $"{newDevice.DeviceType.ToString().Substring(0, 3)}{deviceWithMaxCounter.DeviceCounter + 1}";
                newDevice.DeviceCounter = deviceWithMaxCounter.DeviceCounter + 1;
            }

            

            _dbContext.Devices.Add(newDevice);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceDto>(newDevice);



        }

        public DeviceDto Update(DeviceDto entity)
        {

            Device updatedDevice = _mapper.Map<Device>(entity);
            Device oldDevice = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(updatedDevice.ID));


            updatedDevice.Location = null;


            if (oldDevice == null)
                throw new DeviceNotFoundException($"Device with Id = {updatedDevice.ID} does not exists!");

            if (updatedDevice.Name.Trim().Equals("") || updatedDevice.Name == null)
                throw new InvalidDeviceException("You have to enter device name!");

            if (_dbContext.Location.Where(x => x.ID.Equals(updatedDevice.LocationID)) == null)
                throw new LocationNotFoundException($"Location with id = {updatedDevice.LocationID} does not exists!");


            updatedDevice.Name = $"{updatedDevice.DeviceType.ToString().Substring(0, 3)}{updatedDevice.DeviceCounter}";


            oldDevice.UpdateDevice(updatedDevice);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceDto>(oldDevice);
        }
    }
}
