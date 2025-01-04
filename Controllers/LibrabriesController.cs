using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUtad.Data;
using BibliotecaUtad.Models;

namespace BibliotecaUtad.Controllers
{
    public class LibrabriesController : Controller
    {
        private readonly BibliotecaUtadContext _context;

        public LibrabriesController(BibliotecaUtadContext context)
        {
            _context = context;
        }
    }
}
