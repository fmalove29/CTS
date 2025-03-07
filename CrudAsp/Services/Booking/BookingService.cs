using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CrudAsp.Models.app;
using CrudAsp.Repository;

namespace CrudAsp.Services.Booking;

public class BookingService
{
    public readonly IRepository<CrudAsp.Models.app.Booking> _repository;

    public BookingService(IRepository<CrudAsp.Models.app.Booking> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CrudAsp.Models.app.Booking>> GetAll()
    {
        var bookings = await _repository.GetAllAsync();
        return bookings;
    }

    public async Task<CrudAsp.Models.app.Booking> GetA(Guid Id)
    {
        var booking = await _repository.GetByIdAsync(Id);
        return booking;
    }
    

}
