﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    public interface ITrayService
    {
        void Initialize();

        Action ClickHandler { get; set; }
    }
}