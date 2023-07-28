using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistGestBankABC.Models;

namespace SistGestBankABC.Data
{
    public class SistGestBankABCContext : DbContext
    {
        public SistGestBankABCContext (DbContextOptions<SistGestBankABCContext> options)
            : base(options)
        {
        }

        public DbSet<SistGestBankABC.Models.DatosContacto> DatosContacto { get; set; } = default!;
    }
}
