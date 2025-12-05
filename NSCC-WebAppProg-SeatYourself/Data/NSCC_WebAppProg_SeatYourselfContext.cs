using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSCC_WebAppProg_SeatYourself.Models;

namespace NSCC_WebAppProg_SeatYourself.Data
{
    public class NSCC_WebAppProg_SeatYourselfContext : DbContext
    {
        public NSCC_WebAppProg_SeatYourselfContext (DbContextOptions<NSCC_WebAppProg_SeatYourselfContext> options)
            : base(options)
        {
        }

        public DbSet<NSCC_WebAppProg_SeatYourself.Models.Occasion> Occasion { get; set; } = default!;
        public DbSet<NSCC_WebAppProg_SeatYourself.Models.Category> Category { get; set; } = default!;
        public DbSet<NSCC_WebAppProg_SeatYourself.Models.Venue> Venue { get; set; } = default!;
        public DbSet<NSCC_WebAppProg_SeatYourself.Models.Purchase> Purchase { get; set; } = default!;
        public DbSet<NSCC_WebAppProg_SeatYourself.Models.Comment> Comment { get; set; } = default!;
    }
}
