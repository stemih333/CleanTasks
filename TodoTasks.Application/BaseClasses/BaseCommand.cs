﻿namespace TodoTasks.Application.BaseClasses
{
    public abstract class BaseCommand
    {
        public string UserId { get; set; } = "Unknown";
    }
}
