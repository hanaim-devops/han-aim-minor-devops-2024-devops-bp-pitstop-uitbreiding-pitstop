using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pitstop.RentalCarManagementAPI.Commands;
using Pitstop.RentalCarManagementAPI.Exceptions;
using Pitstop.RentalCarManagementAPI.Services.Interfaces;
using RentalCarManagementAPI.Models;

namespace RentalCarManagementAPI.Controller;

[ApiController]
[Route("/api/rentalcars")]
public class RentalCarController(IRentalCarService carService) : ControllerBase
{
    // Todo: implement methods for retrieving rental cars.

    private IRentalCarService _carService = carService;

    [HttpPost]
    public RentalCar RegisterRentalCar([FromBody] RegisterRentalCar command)
    {
        return carService.Add(command);
    }
    
    [HttpGet]
    public List<RentalCar> GetAllRentalCars()
    {
        return _carService.GetAll();
    }
    
    [HttpGet("{licenseNumber}")]
    public RentalCar GetRentalCarById(string licenseNumber)
    {
        return _carService.GetByLicenseNumber(licenseNumber);
    }
}