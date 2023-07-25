using AutoMapper;
using Magic_VillaAPI.Models;
using Magic_VillaAPI.Models.Dto;
using Magic_VillaAPI.Repository.IRepos;
using Magic_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net;

namespace Magic_VillaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberRepo _villaNumberRepo;
        private readonly IVillaRepos _villaRepo;
        private readonly IMapper _mapper;
        private readonly ApiResponse _response;
        public VillaNumberAPIController(IVillaNumberRepo villaNumberRepo, IMapper mapper, IVillaRepos villaRepos)
        {
            _villaNumberRepo = villaNumberRepo;
            _villaRepo = villaRepos;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAllVillNumber()
        {
            try
            {
                IEnumerable<VillaNumber> list = await _villaNumberRepo.GetAll(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDto>>(list);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villNumber = await _villaNumberRepo.GetVilla(x => x.VillaNo == id);
                if (villNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNumberDto>(villNumber);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> CreateVillNumber([FromBody] VillNoCreatedDto model)
        {
            try
            {
                if (await _villaNumberRepo.GetVilla(x => x.VillaNo == model.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Already Exist");
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.GetVilla(x => x.Id == model.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Id is Invalid");
                    return BadRequest(ModelState);
                }
                if (model == null)
                {
                    return BadRequest(model);
                }
                VillaNumber villa = _mapper.Map<VillaNumber>(model);
                await _villaNumberRepo.Create(villa);
                _response.Result = _mapper.Map<VillaNumberDto>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _villaNumberRepo.GetVilla(x => x.VillaNo == id);
                if (villa == null)
                {
                    return BadRequest();
                }
                await _villaNumberRepo.Remove(villa);
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

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber([FromBody] VillNoUpdatedDto model)
        {
            try
            {
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                if (await _villaRepo.GetVilla(x => x.Id == model.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "VillaId is Invalid");
                    return BadRequest(ModelState);
                }
                VillaNumber villa = _mapper.Map<VillaNumber>(model);
                await _villaNumberRepo.Update(villa);
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
