using AutoMapper;
using ContactManagementAPI.Models;
using ContactManagementAPI.Models.Dtos;
using ContactManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactManagementAPI.Controllers.v1;

[Route("api/v{version:apiVersion}/contactApi")]
[ApiVersion("1.0")]
[ApiController]
public class ContactApiController : ControllerBase
{
    private readonly ILogger<ContactApiController> _logger;

    public readonly IContactRepository _dbContact;

    public readonly IMapper _mapper;

    protected APIResponse _response;
    public ContactApiController(ILogger<ContactApiController> logging, IContactRepository dbcontact, IMapper mapper)
    {
        _logger = logging;
        _dbContact = dbcontact;
        _mapper = mapper;
        _response = new();
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<APIResponse>> GetContacts()
    {
        _logger.LogInformation("Getting all contacts", "");

        try
        {
            IEnumerable<Contact> contactList;

            contactList = await _dbContact.GetAllAsync();

            _response.Result = _mapper.Map<List<ContactDTO>>(contactList);

            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>
            {
                ex.ToString()
            };

            return _response;
        }
    }

    [HttpGet("{id:int}", Name = "GetContact")]
    [ProducesResponseType(200, Type = typeof(ContactDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetContact(int id)
    {
        try
        {
            if (id == 0)
            {
                _logger.LogInformation("Get Contact Error with Id" + id, "error");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var contact = await _dbContact.GetAsync(contact => contact.ID == id);

            if (contact == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<ContactDTO>(contact);
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>
            {
                ex.ToString()
            };

            return _response;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateContact([FromBody] ContactCreateDTO contactDTO)
    {
        try
        {
            if (contactDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _dbContact.GetAsync(x => x.Name.ToLower() == contactDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Name already Exists");
                return BadRequest(ModelState);
            }

            Contact contact = _mapper.Map<Contact>(contactDTO);

            await _dbContact.CreateAsync(contact);

            _response.Result = _mapper.Map<ContactDTO>(contact);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetContact", new { id = contact.ID }, _response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>
            {
                ex.ToString()
            };

            return _response;
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteContact")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> DeleteContact(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var contact = await _dbContact.GetAsync(contact => contact.ID == id);

            if (contact == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            await _dbContact.RemoveAsync(contact);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>
            {
                ex.ToString()
            };

            return _response;
        }

    }

    [HttpPut("{id:int}", Name = "UpdateContact")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateContact(int id, [FromBody] ContactUpdateDTO updatecontactDTO)
    {
        try
        {
            if (updatecontactDTO == null || id != updatecontactDTO.ID)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Contact model = _mapper.Map<Contact>(updatecontactDTO);

            await _dbContact.UpdateAsync(model);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>
            {
                ex.ToString()
            };

            return _response;
        }
    }
}
