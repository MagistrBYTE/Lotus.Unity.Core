﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBasePacked.cs
*		Упаковка/распаковка данных в битовом формате.
*		Реализация упаковки/распаковки данных в битовом формате упаковки применяется при кэшированных данных, там где
*	на важна скорость и не имеет особого смыла полноценно хранить второй экземпляр данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
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
		/// Статический класс для упаковки/распаковки данных в битовом формате
		/// </summary>
		/// <remarks>
		/// Применяется для упаковки кэшированных данных.
		/// Следует применять там где на важна скорость и не имеет особого смыла полноценно хранить второй экземпляр данных
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPacked
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Int32"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bit_start">Стартовый бит с которого записываются данные</param>
			/// <param name="bit_count">Количество бит для записи</param>
			/// <param name="value">Значение для упаковки (будет записано только указанное количество бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackInteger(ref Int32 pack, Int32 bit_start, Int32 bit_count, Int32 value)
			{
				Int32 mask = (1 << bit_count) - 1;
				pack = (pack & ~(mask << bit_start)) | ((value & mask) << bit_start);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Int64"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bit_start">Стартовый бит с которого записываются данные</param>
			/// <param name="bit_count">Количество бит для записи</param>
			/// <param name="value">Значение для упаковки (будет записано только указанное количество бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackLong(ref Int64 pack, Int32 bit_start, Int32 bit_count, Int64 value)
			{
				Int64 mask = (1L << bit_count) - 1L;
				pack = (pack & ~(mask << bit_start)) | ((value & mask) << bit_start);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Boolean"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bit_start">Стартовый бит с которого записываются данные</param>
			/// <param name="value">Значение для упаковки (будет записан только 1 бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackBoolean(ref Int32 pack, Int32 bit_start, Boolean value)
			{
				Int32 mask = (1 << 1) - 1;
				if (value)
				{
					pack = (pack & ~(mask << bit_start)) | ((1 & mask) << bit_start);
				}
				else
				{
					pack = (pack & ~(mask << bit_start)) | ((0 & mask) << bit_start);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Int32"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bit_start">Стартовый бит с которого начинается распаковка</param>
			/// <param name="bit_count">Количество бит для чтения</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 UnpackInteger(Int32 pack, Int32 bit_start, Int32 bit_count)
			{
				Int32 mask = (1 << bit_count) - 1;
				return (pack >> bit_start) & mask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Int64"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bit_start">Стартовый бит с которого начинается распаковка</param>
			/// <param name="bit_count">Количество бит для чтения</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 UnpackLong(Int64 pack, Int32 bit_start, Int32 bit_count)
			{
				Int64 mask = (1L << bit_count) - 1L;
				return (pack >> bit_start) & mask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Boolean"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bit_start">Стартовый бит с которого начинается распаковка</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean UnpackBoolean(Int32 pack, Int32 bit_start)
			{
				Int32 mask = (1 << 1) - 1;
				Int32 data = (pack >> bit_start) & mask;
				if (data == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================