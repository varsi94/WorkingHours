﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Interfaces.Services
{
    public interface ILoadingService
    {
        void ShowIndicator(string message);

        void HideIndicator();
    }
}
