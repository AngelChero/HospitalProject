﻿using System;
using System.Collections.Generic;

namespace HospitalProject.Models;

public partial class TipoUsuarioPagina
{
    public int Iidtipousuariopagina { get; set; }

    public int? Iidtipousuario { get; set; }

    public int? Iidpagina { get; set; }

    public int? Bhabilitado { get; set; }

    public int? Iidvista { get; set; }

    public virtual Pagina? IidpaginaNavigation { get; set; }

    public virtual TipoUsuario? IidtipousuarioNavigation { get; set; }

    public virtual ICollection<TipoUsuarioPaginaBoton> TipoUsuarioPaginaBotons { get; set; } = new List<TipoUsuarioPaginaBoton>();
}
