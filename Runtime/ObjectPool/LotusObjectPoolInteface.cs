﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема объектного пула
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObjectPoolInteface.cs
*		Интерфейсы подсистемы объектного пула.
*		Интерфейсы обеспечивают более гибкое и универсальное управление пулом объектом, однако несколько теряется 
*	производительность так как невозможно статически определить тип объекта и поэтому работы осуществляться через базовый
*	объект System.Object.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreObjectPool
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения объекта поддерживающего пул
		/// </summary>
		/// <remarks>
		/// Максимально общая реализация
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusPoolObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Статус объекта из пула
			/// </summary>
			/// <remarks>
			/// Позволяет определять был ли объект взят из пула и значит его надо вернуть или создан обычным образом
			/// </remarks>
			Boolean IsPoolObject { get; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдо-конструктор
			/// </summary>
			/// <remarks>
			/// Вызывается диспетчером пула в момент взятия объекта из пула
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			void OnPoolTake();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдо-деструктор
			/// </summary>
			/// <remarks>
			/// Вызывается диспетчером пула в момент попадания объекта в пул
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			void OnPoolRelease();
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс диспетчера для управления пулом объектов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusPoolManager
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Максимальное количество объектов для пула
			/// </summary>
			/// <remarks>
			/// В случае, если по запросу объектов в пуле не будет, то это значение увеличится вдвое и создаться указанное количество объектов
			/// </remarks>
			Int32 MaxInstances { get; }

			/// <summary>
			/// Количество объектов в пуле
			/// </summary>
			Int32 InstanceCount { get; }
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Взять готовый объект из пула
			/// </summary>
			/// <remarks>
			/// Это максимально общая и универсальная реализация
			/// </remarks>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			System.Object TakeObjectFromPool();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освободить объект и положить его назад в пул
			/// </summary>
			/// <remarks>
			/// Это максимально общая и универсальная реализация
			/// </remarks>
			/// <param name="pool_object">Объект</param>
			//---------------------------------------------------------------------------------------------------------
			void ReleaseObjectToPool(System.Object pool_object);
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================