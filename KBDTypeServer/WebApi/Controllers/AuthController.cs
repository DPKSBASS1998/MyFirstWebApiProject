// src/Controllers/AuthController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KBDTypeServer.Application;
using KBDTypeServer.Application.DTOs;
using KBDTypeServer.WebApi;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Domain.Entities.UserEntity;


namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
    }
}
