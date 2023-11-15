//using Asp.Versioning;
//using AutoMapper;
//using ContactAPI.Logging;
//using ContactAPI.Models;
//using ContactAPI.Repository;
//using ContactManagementAPI.Models.Dtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Text.Json;

//namespace ContactManagementAPI.Controllers.v1;

//[Route("api/v{version:apiVersion}/contactApi")]
//[ApiVersion("1.0")]
//[ApiController]
//public class ContactApiController : ControllerBase
//{
//    public readonly ILogging _logger;

//    public readonly IContactRepository _dbContact;

//    public readonly IMapper _mapper;

//    protected APIResponse _response;
//    public ContactApiController(ILogging logging, IContactRepository dbVilla, IMapper mapper)
//    {
//        _logger = logging;
//        _dbContact = dbVilla;
//        _mapper = mapper;
//        _response = new();
//    }

//    [HttpGet]
//    [ResponseCache(CacheProfileName = "Default30")]
//    [MapToApiVersion("1.0")]
//    [Authorize]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status403Forbidden)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<ActionResult<APIResponse>> GetVillas([FromQuery(Name = "filterOccupancy")] int? occupancy,
//        [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
//    {
//        _logger.Log("Getting all villas", "");

//        try
//        {
//            IEnumerable<Contact> villaList;

//            if (occupancy > 0)
//            {
//                villaList = await _dbContact.GetAllAsync(u => u.Occupancy == occupancy, pageSize: pageSize, pageNumber: pageNumber);
//            }
//            else
//            {
//                villaList = await _dbContact.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
//            }
//            if (!string.IsNullOrEmpty(search))
//            {
//                villaList = villaList.Where(u => u.Name.ToLower().Contains(search.ToLower()));
//            }

//            Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
//            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
//            _response.Result = _mapper.Map<List<ContactDTO>>(villaList);
//            _response.StatusCode = HttpStatusCode.OK;

//            return Ok(_response);
//        }
//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }
//    }

//    [HttpGet("{id:int}", Name = "GetVilla")]
//    //[ResponseCache(Location = ResponseCacheLocation.None, NoStore =true)]
//    [Authorize]
//    [ProducesResponseType(200, Type = typeof(ContactDTO))]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status403Forbidden)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<ActionResult<APIResponse>> GetVilla(int id)
//    {
//        try
//        {
//            if (id == 0)
//            {
//                _logger.Log("Get Vila Error with Id" + id, "error");
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            var vila = await _dbContact.GetAsync(villa => villa.Id == id);
//            if (vila == null)
//            {
//                _response.StatusCode = HttpStatusCode.NotFound;
//                return NotFound(_response);
//            }
//            _response.Result = _mapper.Map<ContactDTO>(vila);
//            _response.StatusCode = HttpStatusCode.OK;

//            return Ok(_response);
//        }

//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }
//    }

//    [HttpPost]
//    [Authorize(Roles = Constants.Admin)]
//    [ProducesResponseType(StatusCodes.Status201Created)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] ContactCreateDTO createvillaDTO)
//    {
//        try
//        {
//            if (createvillaDTO == null)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            if (await _dbContact.GetAsync(x => x.Name.ToLower() == createvillaDTO.Name.ToLower()) != null)
//            {
//                ModelState.AddModelError("ErrorMessages", "Name already Exists");
//                return BadRequest(ModelState);
//            }

//            Contact villa = _mapper.Map<Contact>(createvillaDTO);

//            await _dbContact.CreateAsync(villa);

//            _response.Result = _mapper.Map<ContactDTO>(villa);
//            _response.StatusCode = HttpStatusCode.Created;

//            return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
//        }
//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }
//    }

//    [HttpDelete("{id:int}", Name = "DeleteVilla")]
//    [Authorize(Roles = Constants.Admin)]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status403Forbidden)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
//    {
//        try
//        {
//            if (id == 0)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            var vila = await _dbContact.GetAsync(villa => villa.Id == id);

//            if (vila == null)
//            {
//                _response.StatusCode = HttpStatusCode.NotFound;
//                return NotFound(_response);
//            }

//            await _dbContact.RemoveAsync(vila);

//            _response.IsSuccess = true;
//            _response.StatusCode = HttpStatusCode.NoContent;

//            return Ok(_response);
//        }
//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }

//    }

//    [HttpPut("{id:int}", Name = "UpdateVilla")]
//    [Authorize(Roles = Constants.Admin)]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] ContactUpdateDTO updatevillaDTO)
//    {
//        try
//        {
//            if (updatevillaDTO == null || id != updatevillaDTO.Id)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            Contact model = _mapper.Map<Contact>(updatevillaDTO);

//            await _dbContact.UpdateAsync(model);

//            _response.IsSuccess = true;
//            _response.StatusCode = HttpStatusCode.NoContent;

//            return Ok(_response);
//        }
//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }
//    }

//    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<ContactUpdateDTO> patchDTO)
//    {
//        try
//        {
//            if (patchDTO == null || id == 0)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            var vila = await _dbContact.GetAsync(villa => villa.Id == id, tracked: false);

//            ContactUpdateDTO villaDTO = _mapper.Map<ContactUpdateDTO>(vila);

//            if (vila == null)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                return BadRequest(_response);
//            }

//            patchDTO.ApplyTo(villaDTO, ModelState);

//            Contact model = _mapper.Map<Contact>(villaDTO);

//            await _dbContact.UpdateAsync(model);

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            _response.IsSuccess = true;
//            _response.StatusCode = HttpStatusCode.NoContent;

//            return Ok(_response);
//        }
//        catch (Exception ex)
//        {

//            _response.IsSuccess = false;
//            _response.ErrorMessages = new List<string>
//            {
//                ex.ToString()
//            };

//            return _response;
//        }
//    }
//}
