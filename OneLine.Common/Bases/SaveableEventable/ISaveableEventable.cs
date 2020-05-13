﻿using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface ISaveableEventable
    {
        Task Save();
        Action<Action> OnBeforeSave { get; set; }
        Action OnAfterSave { get; set; }
        Action OnSaveFailed { get; set; }
    }
}