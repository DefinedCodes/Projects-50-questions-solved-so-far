﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusManagementSystem.Context;
using BusManagementSystem.DTOS;
using BusManagementSystem.Entities;
using BusManagementSystem.Enums;
using BusManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Repositories
{
    public class TripRepository: ITripRepository
    {
        private readonly BusManagementSystemDbContext _context;

        public TripRepository()
        {
            _context = new BusManagementSystemDbContext();
        }

        public Trip Create(Trip trip)
        {
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return trip;
        }

        public Trip Get(int id)
        {
            return _context.Trips.Find(id);
        }

        public Trip GetTrip(int id)
        {
            var trip = _context.Trips.Find(id);
            return trip;
        }

        public List<Trip> GetAll()
        {
            return _context.Trips.ToList();
        }
        
        public Trip Update(Trip trip)
        {
            _context.Trips.Update(trip);
            _context.SaveChanges();
            return trip;
        }

        public void Delete(Trip trip)
        {
            _context.Trips.Remove(trip);
            _context.SaveChanges();
        }

        public bool ExistById(int id)
        {
            return _context.Trips.Any(i => i.Id == id);
        }

        public Trip GetByReference(string tripReferenceNumber)
        {
            return _context.Trips.SingleOrDefault(t => t.TripReference == tripReferenceNumber);

        }

        public List<TripDto> GetTripsByBus(string registrationNumber)
        {
            return _context.Trips.Include(t => t.Bus).Include(t => t.Driver)
                .Where(r => r.Bus.RegistrationNumber == registrationNumber).Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,
                    
                }).ToList();
        }

        public List<TripDto> GetInitialisedTrips()
        {
            return _context.Trips.Include(t => t.Bus).Include(t => t.Driver)
                .Where(t => t.Status == TripStatus.Initialize).Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,
                    
                }).ToList();
        }

       

        public List<TripDto> GetTripsByDate(DateTime date)
        {
            return _context.Trips.Include(t => t.Bus).Include(t => t.Driver).Where(d => d.TakeOffTime.Date == date)
                .Select(
                    trip => new TripDto
                    {
                        Id = trip.Id,
                        DriverId = trip.DriverId,
                        DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                        BusId = trip.BusId,
                        BusModel = trip.Bus.Model,
                        BusRegistrationNumber = trip.Bus.RegistrationNumber,
                        AvailableSeat = trip.AvailableSeat,
                        LandingPoint = trip.LandingPoint,
                        TakeOffPoint = trip.TakeOffPoint,
                        TripReference = trip.TripReference,
                        TakeOffTime = trip.TakeOffTime,
                        LandingTime = trip.LandingTime,
                        Status = trip.Status,
                        Price = trip.Price,
                        
                    }).ToList();
        }

        public List<TripDto> GetCompletedTrips()
        {
            return _context.Trips.Include(t => t.Bus).Include(t => t.Driver)
                .Where(x => x.Status == TripStatus.Completed).Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,
                    
                }).ToList();
        }

        public List<TripDto> GetTripsByDateAndLocation(Location from, Location to, DateTime date)
        {
            return _context.Trips.Include(t => t.Bus).Include(t => t.Driver)
                .Where(x => x.TakeOffPoint == from && x.LandingPoint == to && x.TakeOffTime.Date == date).Select(trip =>
                    new TripDto
                    {
                        Id = trip.Id,
                        DriverId = trip.DriverId,
                        DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                        BusId = trip.BusId,
                        BusModel = trip.Bus.Model,
                        BusRegistrationNumber = trip.Bus.RegistrationNumber,
                        AvailableSeat = trip.AvailableSeat,
                        LandingPoint = trip.LandingPoint,
                        TakeOffPoint = trip.TakeOffPoint,
                        TripReference = trip.TripReference,
                        TakeOffTime = trip.TakeOffTime,
                        LandingTime = trip.LandingTime,
                        Status = trip.Status,
                        Price = trip.Price,

                    }).ToList();
        }

        public List<TripDto> GetAvailableTrips(Location from, Location to, DateTime date)
        {
            return _context.Trips.Include(b => b.Bus).Include(d => d.Driver).Where(l =>
                    l.TakeOffPoint == from && l.LandingPoint == to && l.TakeOffTime.Date == date && l.AvailableSeat > 0)
                .Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,

                }).ToList();
        }

        public List<TripDto> GetTripsByDriver(int driverId)
        {
            return _context.Trips.Include(b => b.Bus).Include(d => d.Driver).Where(i => i.Driver.Id == driverId).Select(
                trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,
                    
                }).ToList();
        }

        public List<TripDto> GetCancelledTrips()
        {
            return _context.Trips.Include(b => b.Bus).Include(d => d.Driver).Where(c => c.Status == TripStatus.Canceled)
                .Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,

                }).ToList();
        }

        public List<TripDto> GetCancelledTripsByDate(DateTime date)
        {
            return _context.Trips.Include(b => b.Bus).Include(d => d.Driver)
                .Where(c => c.Status == TripStatus.Canceled && c.TakeOffTime == date).Select(trip => new TripDto
                {
                    Id = trip.Id,
                    DriverId = trip.DriverId,
                    DriverFullName = $"{trip.Driver.FirstName} {trip.Driver.LastName}",
                    BusId = trip.BusId,
                    BusModel = trip.Bus.Model,
                    BusRegistrationNumber = trip.Bus.RegistrationNumber,
                    AvailableSeat = trip.AvailableSeat,
                    LandingPoint = trip.LandingPoint,
                    TakeOffPoint = trip.TakeOffPoint,
                    TripReference = trip.TripReference,
                    TakeOffTime = trip.TakeOffTime,
                    LandingTime = trip.LandingTime,
                    Status = trip.Status,
                    Price = trip.Price,

                }).ToList();
        }
    }
}