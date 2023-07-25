using AutoMapper;
using Azure;
using Magic_VillaAPI.Data;
using Magic_VillaAPI.Logging;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;
using Magic_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Magic_VillaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepos _villRepo;
        private readonly ApiResponse _response;
        private readonly IMapper _mapper;

        public VillaAPIController(IVillaRepos villRepo, IMapper mapper)
        {
            _villRepo = villRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetVillas([FromQuery(Name = "filterOcuupancy")] int? occupancy, [FromQuery] string ? search,
            int pageSize=0, int pageNumber =1)
        {
            try
            {
                IEnumerable<Villa> villalist;
                if (occupancy > 0)
                {
                    villalist = await _villRepo.GetAll(u => u.Occupancy == occupancy, pageSize:pageSize, pageNumber:pageNumber);
                }
                else
                {
                    villalist = await _villRepo.GetAll(pageSize: pageSize, pageNumber: pageNumber);
                }

                if(!string.IsNullOrWhiteSpace(search))
                {
                    villalist = villalist.Where(u => u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<VillaDTO>>(villalist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _villRepo.GetVilla(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                Villa villa = _mapper.Map<Villa>(createDto);
                await _villRepo.Create(villa);
                VillaDTO villaDTO = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, villaDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _villRepo.GetVilla(x => x.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _villRepo.Remove(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                Villa model = _mapper.Map<Villa>(updateDto);
                await _villRepo.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
