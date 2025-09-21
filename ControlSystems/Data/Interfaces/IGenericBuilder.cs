using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ControlSystems.Data.Interfaces;

public interface IGenericBuilder
{
    static abstract void Build(ModelBuilder builder);
}
