using System.Net;
using AutoMapper;
using CW2.DAL.Entities;
using CW2.DAL.Repositories;
using CW2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CW2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerController(
            ICustomerRepository customerRepository, 
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IActionResult GetImage(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer?.ProfilePic == null)
            {
                return NotFound();
            }
            return File(customer.ProfilePic, "image/jpeg");
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var entities = _customerRepository.GetAll();
            var models = entities.Select(e => _mapper.Map<CustomerViewModel>(e));

            return View(models);
        }

        public ActionResult Filter(CustomerFilterViewModel filter)
        {
            var result = _customerRepository.Filter(
                filter.FirstName, 
                filter.LastName, 
                filter.PostalCode, 
                filter.City,
                filter.Street,
                filter.FlatNo,
                filter.BuildingNo,
                filter.PhoneNumber,
                filter.Page,
                filter.PageSize,
                filter.SortColumn,
                filter.SortDesc
            );

            var models = result.Items.Select(e => _mapper.Map<CustomerViewModel>(e));
            filter.Customers = models;

            return View(filter);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var entity = _customerRepository.GetById(id);
            var model = _mapper.Map<CustomerViewModel>(entity);
            return View(model);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(stream);
                        // Save the file bytes to the database or process it as needed
                        var fileBytes = stream.ToArray();
                        // Example: Save fileBytes to customer's profile image property
                        model.ProfilePic = fileBytes;
                    }
                }

                var hasher = new PasswordHasher<CustomerViewModel>();
                model.PasswordHash = hasher.HashPassword(model, model.PasswordHash);

                // remove hyphens from phone number
                model.PhoneNumber = model.PhoneNumber?.Replace("-", "").Trim();

                var customer = _mapper.Map<Customer>(model);
                var insertedCustomer = _customerRepository.Insert(customer);
                

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the customer.");
                return View(model);
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(long id)
        {
            var customer = _customerRepository.GetById(id);
            var model = _mapper.Map<CustomerViewModel>(customer);
            return View(model);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CustomerViewModel model, IFormFile imageFile)
        {
            try
            {
                

                var existingCustomer = _customerRepository.GetById(id);
                if (existingCustomer == null)
                    return NotFound();

                // remove hyphens from phone number
                model.PhoneNumber = model.PhoneNumber?.Replace("-", "").Trim();

                var originalPasswordHash = existingCustomer.PasswordHash;


                if (imageFile != null && imageFile.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await imageFile.CopyToAsync(stream);
                    model.ProfilePic = stream.ToArray();
                }
                else
                {
                    model.ProfilePic = existingCustomer.ProfilePic;
                }

                _mapper.Map(model, existingCustomer);

                

                if (!string.IsNullOrWhiteSpace(model.PasswordHash))
                {
                    var hasher = new PasswordHasher<CustomerViewModel>();
                    existingCustomer.PasswordHash = hasher.HashPassword(model, model.PasswordHash);
                }
                else
                {
                    existingCustomer.PasswordHash = originalPasswordHash;
                }

                _customerRepository.Update(existingCustomer);

                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the customer.");
                return View(model);
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(long id)
        {
            var customer = _customerRepository.GetById(id);
            var model = _mapper.Map<CustomerViewModel>(customer);
            return View(model);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, CustomerViewModel model)
        {
            try
            {
                _customerRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _customerRepository.Dispose();
        }
    }
    
}
