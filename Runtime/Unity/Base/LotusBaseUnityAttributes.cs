﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseUnityAttributes.cs
*		Определение общих атрибутов применяемых на уровне Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreUnityBase Базовая подсистема Unity
		//! Базовая подсистема расширяет стандартные возможности и унифицирует работу с общим структурами и данными в Unity.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для определения порядка исполнения скрипта
		/// </summary>
		/// <remarks>
		/// Используется что бы автоматически сформировать нужный порядок исполнения скриптов
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
		public sealed class LotusExecutionOrderAttribute : Attribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Int32 mExecutionOrder = -1;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Порядок исполнения скрипта
			/// </summary>
			public Int32 ExecutionOrder
			{
				get { return mExecutionOrder; }
				set { mExecutionOrder = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="execution_order">Порядок исполнения скрипта</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusExecutionOrderAttribute(Int32 execution_order)
			{
				mExecutionOrder = execution_order;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================