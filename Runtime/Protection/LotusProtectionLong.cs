﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusProtectionLong.cs
*		Защита целого числа (64 бит).
*		Реализация механизма защиты (шифрования/декодирование) целого числа.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreProtection
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура-оболочка для защиты целого числа (64 бит)
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[StructLayout(LayoutKind.Explicit)]
		public struct TProtectionLong
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Маска для шифрования/декодирование
			/// </summary>
			public const UInt64 XOR_MASK = 0XAAAAAAAAAAAAAAAA;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			[FieldOffset(0)]
			private Int64 mEncryptValue;

			[FieldOffset(0)]
			private UInt64 mConvertValue;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Зашифрованное значение
			/// </summary>
			public Int64 EncryptedValue
			{
				get
				{
					// Обходное решение для конструктора структуры по умолчанию
					if (mConvertValue == 0 && mEncryptValue == 0)
					{
						mConvertValue = XOR_MASK;
					}

					return mEncryptValue;
				}
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в обычное целое число
			/// </summary>
			/// <param name="value">Структура-оболочка для защиты целого числа</param>
			/// <returns>Целое число</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Int64(TProtectionLong value)
			{
				value.mConvertValue ^= XOR_MASK;
				var original = value.mEncryptValue;
				value.mConvertValue ^= XOR_MASK;
				return original;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа cтруктуры-оболочки для защиты целого числа
			/// </summary>
			/// <param name="value">Целое число</param>
			/// <returns>Структура-оболочка для защиты целого числа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TProtectionLong(Int64 value)
			{
				var protection = new TProtectionLong();
				protection.mEncryptValue = value;
				protection.mConvertValue ^= XOR_MASK;
				return protection;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================