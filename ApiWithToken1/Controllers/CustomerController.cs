using ApiWithToken.Domain.Extensions;
using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Resources;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiWithToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IService<Customer> customerService;
        private readonly IMapper mapper;

        public CustomerController(IService<Customer> customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            BaseResponse<IEnumerable<Customer>> productListResponse = await customerService.GetWhere(x => x.Id > 0);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Extra);
            }
            else
            {
                return BadRequest(productListResponse.ErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            BaseResponse<Customer> customerResponse = await customerService.GetById(id);
            if (customerResponse.Success)
            {
                return Ok(customerResponse.Extra);
            }
            else
            {
                return BadRequest(customerResponse.Extra);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody]CustomerResource customerResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Customer customer = mapper.Map<CustomerResource, Customer>(customerResource);

                BaseResponse<Customer> customerResponse = await customerService.Add(customer);

                if (customerResponse.Success)
                {
                    return Ok(customerResponse.Extra);
                }
                else
                {
                    return BadRequest(customerResponse.ErrorMessage);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerResource customerResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Customer customer = mapper.Map<CustomerResource, Customer>(customerResource);
                customer.Id = id;

                BaseResponse<Customer> customerResponse = await customerService.Update(customer);

                if (customerResponse.Success)
                {
                    return Ok(customerResponse.Extra);
                }
                else
                {
                    return BadRequest(customerResponse.ErrorMessage);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveCustomer(int id)
        {
            BaseResponse<Customer> customerResponse = await customerService.Delete(id);

            if (customerResponse.Success)
            {
                return Ok(customerResponse.Extra);
            }
            else
            {
                return BadRequest(customerResponse.ErrorMessage);
            }
        }
    }
}