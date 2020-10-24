using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommunity.Models
{

    public interface IDeleteImages
    {
        void Delete(string path, string name);
    }
}