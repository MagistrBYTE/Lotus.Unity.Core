﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusExtensionEnum.cs
*		Методы расширения для работы с типом перечисления.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreExtension
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения для типа <see cref="System.Enum"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionEnum
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на установленный флаг в перечислении
			/// </summary>
			/// <param name="value">Перечисление</param>
			/// <param name="flag">Проверяемый флаг</param>
			/// <returns>Статус установки флага</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsFlagSet(this Enum value, Enum flag)
			{
				long lValue = Convert.ToInt64(value);
				long lFlag = Convert.ToInt64(flag);
				return (lValue & lFlag) != 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка флага в перечислении
			/// </summary>
			/// <param name="value">Перечисление</param>
			/// <param name="flags">Флаг</param>
			/// <param name="on">Статус установки или сброса</param>
			/// <returns>Перечисление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum SetFlags(this Enum value, Enum flags, Boolean on)
			{
				long lValue = Convert.ToInt64(value);
				long lFlag = Convert.ToInt64(flags);
				if (on)
				{
					lValue |= lFlag;
				}
				else
				{
					lValue &= ~lFlag;
				}

				Enum result = (Enum)Enum.ToObject(value.GetType(), lValue);
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка флага в перечислении
			/// </summary>
			/// <param name="value">Перечисление</param>
			/// <param name="flags">Флаг</param>
			/// <returns>Перечисление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum SetFlags(this Enum value, Enum flags)
			{
				return value.SetFlags(flags, true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сброс флага в перечислении
			/// </summary>
			/// <param name="value">Перечисление</param>
			/// <param name="flags">Флаг</param>
			/// <returns>Перечисление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum ClearFlags(this Enum value, Enum flags)
			{
				return value.SetFlags(flags, false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута перечисления
			/// </summary>
			/// <typeparam name="TType">Тип атрибута</typeparam>
			/// <param name="enum_value">Экземпляр перечисления</param>
			/// <returns>Найденный атрибут</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TType GetAttributeOfType<TType>(this Enum enum_value) where TType : Attribute
			{
				var type = enum_value.GetType();
				var member_info = type.GetMember(enum_value.ToString());
				var attributes = member_info[0].GetCustomAttributes(typeof(TType), false);
				return attributes.Length > 0 ? (TType)attributes[0] : null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение описания либо имени указанного перечисления
			/// </summary>
			/// <param name="enum_value">Экземпляр перечисления</param>
			/// <returns>Описание либо имя перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetDescriptionOrName(this Enum enum_value)
			{
				Type type_enum = enum_value.GetType();
				return (XEnum.GetDescriptionOrName(type_enum, enum_value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение аббревиатуры либо имени указанного перечисления
			/// </summary>
			/// <param name="enum_value">Экземпляр перечисления</param>
			/// <returns>Аббревиатура либо имя перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAbbreviationOrName(this Enum enum_value)
			{
				Type type_enum = enum_value.GetType();
				return (XEnum.GetAbbreviationOrName(type_enum, enum_value));
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================