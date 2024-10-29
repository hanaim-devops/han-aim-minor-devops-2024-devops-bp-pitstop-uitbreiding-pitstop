using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Services.Interfaces;

namespace RentalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalPlanningController(IRentalPlanningService rentalPlanningService) : ControllerBase
{
    private IRentalPlanningService _rentalPlanningService = rentalPlanningService;
    
    [HttpGet]
    public List<RentalReservation> GetAll()
    {
        return _rentalPlanningService.GetAll();
    }
    
    [HttpPost]
    public RentalReservation Make(MakeReservation command)
    {
        return _rentalPlanningService.Make(command);
    }
}