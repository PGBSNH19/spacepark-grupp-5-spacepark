using SpaceParkBackend.Database;
using SpaceParkBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend
{
    public interface IParkingGuardRepository
    {
        public Parkinglot CheckOpenStatus(SpaceparkContext context);
    }
}
