using Application.DTOs;
using Infrastructure.Database.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarServiceApi.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    private readonly IGenericRepository<Service> _serviceRepository;

    public ServicesController(IGenericRepository<Service> serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Service>?>> GetAll(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 100)
    {
        var dbResponse = await _serviceRepository.GetAllPaged(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return new NotFoundObjectResult("There were no services found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Service?>> GetById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceRepository.GetById(id);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The service with the specified id: {id} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Service>> Add(
        [Required][FromBody] ServiceAddDto serviceDto)
    {
        var service = new Service
        {
            Id = 0,
            Name = serviceDto.Name,
            Description = serviceDto.Description,
        };

        var dbResponse = await _serviceRepository.Add(service);
        return new OkObjectResult(dbResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Service?>> Update(
        [Required][FromRoute] int id,
        [Required][FromBody] ServiceUpdateDto serviceDto)
    {
        var serviceFromDb = await _serviceRepository.GetById(id);
        if (serviceFromDb is null)
        {
            return new NotFoundObjectResult(
                $"The service with the specified id: {id} was not found!");
        }

        await _serviceRepository.Detach(serviceFromDb);
        var service = new Service
        {
            Id = id,
            Name = serviceDto.Name ?? serviceFromDb.Name,
            Description = serviceDto.Description ?? serviceFromDb.Description,
        };

        var dbResponse = await _serviceRepository.Update(service);
        return new OkObjectResult(dbResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Service?>> Delete(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceRepository.Delete(id);
        if (dbResponse is null)
        {
            return new NotFoundObjectResult(
                $"The service with the specified id: {id} was not found!");
        }

        return new OkObjectResult(dbResponse);
    }
}
