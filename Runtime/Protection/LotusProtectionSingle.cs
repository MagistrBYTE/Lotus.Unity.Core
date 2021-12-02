﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusProtectionSingle.cs
*		Защита вещественного числа.
*		Реализация механизма защиты (шифрования/декодирование) вещественного числа.
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
		/// Структура-оболочка для защиты вещественного числа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[StructLayout(LayoutKind.Explicit)]
		public struct TProtectionSingle
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Маска для шифрования/декодирование
			/// </summary>
			public const UInt32 XOR_MASK = 0XAAAAAAAA;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			[FieldOffset(0)]
			private Single mEncryptValue;

			[FieldOffset(0)]
			private UInt32 mConvertValue;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Зашифрованное значение
			/// </summary>
			public Single EncryptedValue
			{
				get
				{
					// Обходное решение для конструктора структуры по умолчанию
					if (mConvertValue == 0 && (Single)mEncryptValue == 0)
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
			/// Неявное преобразование в обычное вещественное число
			/// </summary>
			/// <param name="value">Структура-оболочка для защиты вещественного числа</param>
			/// <returns>Целое число</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Single(TProtectionSingle value)
			{
				value.mConvertValue ^= XOR_MASK;
				var original = value.mEncryptValue;
				value.mConvertValue ^= XOR_MASK;
				return original;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа структуры-оболочки для защиты вещественного числа
			/// </summary>
			/// <param name="value">Вещественное число</param>
			/// <returns>Структура-оболочка для защиты вещественного числа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TProtectionSingle(Single value)
			{
				var protection = new TProtectionSingle();
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