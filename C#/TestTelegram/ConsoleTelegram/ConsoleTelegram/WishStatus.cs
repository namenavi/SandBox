﻿namespace ConsoleTelegram
{
    public enum WishStatus
    {
        // Статус, который означает, что желание новое
        New,

        // Статус, который означает, что желание выбрано для исполнения
        Chosen,

        // Статус, который означает, что желание исполнено
        Done,

        // Статус, который означает, что желание назначено исполнителем
        Assigned
    }