using AutoMapper;
using ContactManagementWeb.Models;
using ContactManagementWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactManagementWeb.Controllers;

public class ContactController : Controller
{
    public readonly IMapper _mapper;

    public readonly IContactService _contactService;

    public ContactController(IMapper mapper, IContactService contactService)
    {
        _mapper = mapper;
        _contactService = contactService;
    }
    public async Task<IActionResult> IndexContact()
    {
        List<ContactDTO> contacts = new();

        var response = await _contactService.GetAllAsync<APIResponse>();

        if (response != null && response.IsSuccessStatusCode)
        {
            contacts = JsonConvert.DeserializeObject<List<ContactDTO>>(Convert.ToString(response.Result));
        }
        return View(contacts);
    }

    public async Task<IActionResult> CreateContact()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateContact(ContactCreateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _contactService.CreateAsync<APIResponse>(model);

            if (response != null && response.IsSuccessStatusCode)
            {
                TempData["success"] = "Contact created successfully";
                return RedirectToAction(nameof(IndexContact));
            }
        }

        TempData["error"] = "Error encountered.";

        return View(model);
    }

    public async Task<IActionResult> UpdateContact(int ContactId)
    {
        var response = await _contactService.GetAsync<APIResponse>(ContactId);

        if (response != null && response.IsSuccessStatusCode)
        {
            ContactDTO Contact = JsonConvert.DeserializeObject<ContactDTO>(Convert.ToString(response.Result));

            return View(_mapper.Map<ContactUpdateDTO>(Contact));
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateContact(ContactUpdateDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _contactService.UpdateAsync<APIResponse>(model);

            if (response != null && response.IsSuccessStatusCode)
            {
                TempData["success"] = "Contact updated successfully";
                return RedirectToAction(nameof(IndexContact));
            }
        }

        TempData["error"] = "Error encountered.";

        return View(model);
    }
    public async Task<IActionResult> DeleteContact(int ContactId)
    {
        var response = await _contactService.GetAsync<APIResponse>(ContactId);

        if (response != null && response.IsSuccessStatusCode)
        {
            ContactDTO Contact = JsonConvert.DeserializeObject<ContactDTO>(Convert.ToString(response.Result));

            return View(Contact);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteContact(ContactDTO model)
    {
        var response = await _contactService.DeleteAsync<APIResponse>(model.ID);

        if (response != null && response.IsSuccessStatusCode)
        {
            TempData["success"] = "Contact deleted successfully";
            return RedirectToAction(nameof(IndexContact));
        }

        TempData["error"] = "Error encountered.";

        return View(model);
    }

}
