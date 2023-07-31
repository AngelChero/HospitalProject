﻿using System;
using System.Collections.Generic;

namespace HospitalProject.Models;

public partial class TipoSangre
{
    public int Iidtiposangre { get; set; }

    public string? Nombre { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
