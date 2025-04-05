using System.Net;
using AutoMapper;
using CW2.DAL.Entities;
using CW2.DAL.Repositories;
using CW2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

                var customer = _mapper.Map<Customer>(model);

                _customerRepository.Update(customer);


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
    }
}
