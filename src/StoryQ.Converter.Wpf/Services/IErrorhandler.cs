﻿using System;

namespace StoryQ.Converter.Wpf.Services
{
    public interface IErrorhandler
    {
        /// <summary>
        /// Do something useful (log, messagebox, rethrow) with an exception
        /// </summary>
        /// <param name="e"></param>
        void HandleExpectedError(Exception e);
    }
}