using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Services
{

    public interface ICrud
    {
        void Create();
        void Read();
        void Update();
        void Delete();

        void ReadDeleted();

        void Reactivate();
    }

}
