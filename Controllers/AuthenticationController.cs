using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using DTO;
using DTO.I;
using DTO.O;
using Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public AuthenticationController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> login(LoginDTO loginDTO)
        {

            var isValid = false;

            if (loginDTO.username == "test" && loginDTO.password == "test") isValid = true;

            return new LoginResponseDTO
            {
                IsValid = isValid,
                Token = isValid ? GenerateJWTToken(loginDTO.username, "Jack") : null
            };

        }

        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            var name = User.FindFirst("fullName");
            return Ok("Hello " + name.Value + ", This is a response from Admin method");
        }

        private string GenerateJWTToken(string username, string fullname, string role = "Admin")
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt: SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("fullName", fullname),
                new Claim("role",role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt: Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}