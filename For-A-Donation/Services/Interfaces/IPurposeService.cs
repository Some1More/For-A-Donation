﻿using For_A_Donation.Models.DataBase;
using For_A_Donation.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IPurposeService
{
    /// <summary>
    /// Получение цели по Id пользователя
    /// </summary>
    /// <param name="userId"> Id пользователя </param>
    /// <returns> Цель </returns>
    /// <exception cref="ArgumentException"></exception>
    Purpose? GetByUserId(Guid userId);

    /// <summary>
    /// Созданиие новой задачи
    /// </summary>
    /// <param name="purpose"> Новая цель </param>
    /// <returns> Созданная цель </returns>
    Task<Purpose> Create(Purpose purpose);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"> Id цели </param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    Task Delete(Guid Id);
}