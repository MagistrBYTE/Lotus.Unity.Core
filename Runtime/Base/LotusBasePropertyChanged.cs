﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBasePropertyChanged.cs
*		Базовый класс для реализации оповещения об изменении своих свойств.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение дополнительного интерфейса для нотификации об изменении данных
		/// </summary>
		/// <remarks>
		/// Указанный интерфейс является базой для реализации связывания данных. Он обеспечивает концепцию «издатель-подписчик»,
		/// в рамках того что если объект является «издателем», т.е. другим объектам нужно знать об изменение его свойства,
		/// он обязательно должен правильно и безопасно реализовать указанный интерфейс
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusNotifyPropertyChanged
		{
			/// <summary>
			/// Событие для нотификации об изменении значения свойства. Аргумент - имя свойства и его новое значение
			/// </summary>
			Action<String, System.Object> OnPropertyChanged { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для реализации оповещения об изменении своих свойств
		/// </summary>
		/// <remarks>
		/// В качестве нотификации о изменение свойств используются стандартный интерфейс уведомлений <see cref="INotifyPropertyChanged"/>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class PropertyChangedBase : INotifyPropertyChanged
		{
			#region ======================================= ДАННЫЕ INotifyPropertyChanged =============================
			/// <summary>
			/// Событие срабатывает ПОСЛЕ изменения свойства
			/// </summary>
			public event PropertyChangedEventHandler PropertyChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(String property_name = "")
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(PropertyChangedEventArgs args)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, args);
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================