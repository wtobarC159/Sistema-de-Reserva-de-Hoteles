using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Respositorio
{
    public class RepositorioUsuarioApp : IUsuarioApp
    {
        private readonly DataContext _context;
        private readonly UserManager<UsuarioApp> _userManager;

        public RepositorioUsuarioApp(DataContext context, UserManager<UsuarioApp> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UsuarioApp?> GetUsuarioApp(string usuarioAppId)
        {
            var UserDB = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == usuarioAppId);
            if (UserDB == null) return null;

            return UserDB;
        }

        public async Task<List<UsuarioApp>> GetUsuariosApp(QueryObjects query)
        {
            var UsuariosDB =  _userManager.Users.AsQueryable();            
            if (query.PageSize != 0 || query.PageNumber != 0) 
            {
                if (query.PageSize == 0) query.PageSize = 20;
                var SkipNumber = (query.PageNumber -1) * query.PageSize;
                return await UsuariosDB.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
            }

            return await UsuariosDB.ToListAsync();
        }
    }
}
