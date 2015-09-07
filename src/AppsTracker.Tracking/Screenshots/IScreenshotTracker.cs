﻿using System;
using AppsTracker.Data.Models;

namespace AppsTracker.Tracking
{
    public interface IScreenshotTracker : IDisposable
    {
        event EventHandler<ScreenshotEventArgs> ScreenshotTaken;
        void SettingsChanging(Setting settings);
        void Initialize(Setting settings);
    }
}