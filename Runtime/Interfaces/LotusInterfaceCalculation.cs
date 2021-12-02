﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема интерфейсов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInterfaceCalculation.cs
*		Определение интерфейсов связанных с расчётами и верификацией данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreInterfaces
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов поддерживающих понятие неизменяемости (константности)
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusConstantable
		{
			/// <summary>
			/// Статус константного объекта
			/// </summary>
			Boolean IsConst { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов которые могут не участвовать в расчетах
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusNotCalculation
		{
			/// <summary>
			/// Не учитывать объект в расчетах
			/// </summary>
			Boolean NotCalculation { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов которые поддерживают верификацию данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusVerified
		{
			/// <summary>
			/// Статус верификации данных
			/// </summary>
			Boolean IsVerified { get; set; }
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================