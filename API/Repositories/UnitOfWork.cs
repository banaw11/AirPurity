using API.Data;
using API.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IExternalClientContext _clientContext;

        public UnitOfWork(DataContext context, IMapper mapper, IExternalClientContext clientContext)
        {
            _context = context;
            _mapper = mapper;
            _clientContext = clientContext;
        }

        public IStationRepository StationRepository => new StationRepository(_context, _clientContext, _mapper);
        public ISensorRepository SensorRepository => new SensorRepository(_clientContext, _mapper);
        public ICityRepository CityRepository => new CityRepository(_context, _mapper);
    }
}
