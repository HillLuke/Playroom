﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IDrawGizmo
    {
        bool DrawGizmo { get; set; }

        void OnDrawGizmos();
    }
}
