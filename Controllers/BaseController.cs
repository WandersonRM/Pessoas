using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pessoas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoas.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public async Task<bool> Usuario_Tem_Acesso(ApplicationDbContext _context)
        {

            var usuario = User.Identity.Name;


            var temAcesso = await (from PF in _context.PerfilUsuario
                                   join US in _context.Usuario on PF.UserId equals US.Id
                                   select new
                                   {
                                       PF.IdTipoUsuario
                                   }).AnyAsync();


            return temAcesso;

        }
    }
}
