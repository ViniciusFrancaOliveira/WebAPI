using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Data;
using AutoMapper;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<User>> GetAllUser()
        {
            var userItems = _repository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }

        [HttpGet("{id}")]
        public ActionResult<UserReadDto> GetUserByUserName(string userName)
        {
            var userItems = _repository.GetUserByUserName(userName);

            if (userItems != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItems));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<UserCreateDto> CreateUser([FromBody]UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            return Ok(userModel);
        }
    }
}
