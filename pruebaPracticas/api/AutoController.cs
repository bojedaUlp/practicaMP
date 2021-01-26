using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pruebaPracticas.Models;

namespace pruebaPracticas.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly DataContext contex;
        public AutoController(IConfiguration configuration,DataContext context)
        {
            this.configuration = configuration;
            this.contex = context;
        }
        // GET: api/Auto
        [HttpGet]
        public async Task<IEnumerable<Auto>> Get()
        {
            return contex.Auto;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            try
            {
                var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"]));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                { new Claim(ClaimTypes.Name,"pepe"),
                        new Claim("FullName", "pepito"),
                        new Claim(ClaimTypes.Role, "Admin"),};

                var token = new JwtSecurityToken(
                    issuer: configuration["TokenAuthentication:Issuer"],
                    audience: configuration["TokenAuthentication:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credenciales
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // GET: api/Auto/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            try {
                if (id > 0)
                {
                    return Ok(contex.Auto.First(e=>e.Id_Auto==id));
                }
                else
                {
                    return NotFound("Id incorrecto");
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Auto
        [HttpPost]
        public async Task<ActionResult> Post(Auto a)
        {
            try {
                var aux = contex.Auto.First(e=>e.Patente!=a.Patente);
                if (aux == null && a!=null)
                {
                    contex.Add(a);
                    contex.SaveChanges();
                    return Ok(contex.Auto.First(e=>e.Id_Auto==a.Id_Auto));
                }
                else
                {
                    return BadRequest("La patente ya existe");
                }
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Auto/5
        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id,Auto a)
        {
            Auto actualizar = new Auto();
            try
            {
                if (id == a.Id_Auto)
                {
                    actualizar.Id_Auto = id;
                    actualizar.Patente = a.Patente;
                    actualizar.Marca = a.Marca;
                    actualizar.Modelo = a.Modelo;
                    actualizar.Kms = a.Kms;
                    actualizar.Año = a.Año;
                    actualizar.Img1 = a.Img1;
                    actualizar.Img2 = a.Img2;
                    contex.Auto.Update(actualizar);
                    contex.SaveChanges();
                    return Ok(contex.Auto.First(e => e.Id_Auto == a.Id_Auto));
                }
                else
                {
                    return BadRequest("La identificacion no coincide con la del auto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var autito = contex.Auto.First(e=>e.Id_Auto==id);
                    if (autito != null)
                    {
                        contex.Auto.Remove(autito);
                        contex.SaveChanges();
                        return Ok();
                    }
                    else { return BadRequest("No se encontro un auto con esa identificacion");  }
                }
                else
                {
                    return NotFound("id incorrecto");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
