using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Responses;
using Pitstop.RentalManagementAPI.Services.Interfaces;

namespace RentalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalPlanningController(IRentalPlanningService rentalPlanningService, IMapper mapper) : ControllerBase
{
    private IRentalPlanningService _rentalPlanningService = rentalPlanningService;
    
    [HttpGet]
    public List<RentalReservationResponse> GetAll()
    {
        return _rentalPlanningService.GetAll().Select(mapper.Map<RentalReservationResponse>).ToList();
    }
    
    [HttpGet("{id}")]
    public RentalReservationResponse GetById(string id)
    {
        var reservation = _rentalPlanningService.GetById(id);
        return mapper.Map<RentalReservationResponse>(reservation);
    }
    
    [HttpPost]
    public RentalReservationResponse Make(MakeReservation command)
    {
        var reservation = _rentalPlanningService.Make(command);
        return mapper.Map<RentalReservationResponse>(reservation);
    }
    
    [HttpPatch("{id}/car")]
    public RentalReservationResponse UpdateCar(string id, ChangeCar command)
    {
        var reservation = _rentalPlanningService.ChangeCar(id, command);
        return mapper.Map<RentalReservationResponse>(reservation);
    }
    
    [HttpPatch("{id}/extend")]
    public RentalReservationResponse Extend(string id, Extend command)
    {
        var reservation = _rentalPlanningService.Extend(id, command);
        return mapper.Map<RentalReservationResponse>(reservation);
    }
}