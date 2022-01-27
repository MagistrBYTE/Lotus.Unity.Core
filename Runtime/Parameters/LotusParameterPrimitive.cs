﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема параметрических объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusParameterPrimitive.cs
*		Определение классов для представления параметров значения которых представляет примитивный тип данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreParameters
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой логический тип
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterBool : ParameterItem<Boolean>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Boolean; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterBool()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterBool(String parameter_name, Boolean value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterBool(Int32 id, Boolean value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой целый тип
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterInteger : ParameterItem<Int32>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Integer; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterInteger()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterInteger(String parameter_name, Int32 value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterInteger(Int32 id, Int32 value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой вещественный тип
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterReal : ParameterItem<Double>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Real; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterReal()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterReal(String parameter_name, Double value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterReal(Int32 id, Double value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой тип даты-времени
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterDatetime : ParameterItem<DateTime>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.DateTime; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterDatetime()
			{
				mValue = DateTime.Now;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterDatetime(String parameter_name, DateTime value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterDatetime(Int32 id, DateTime value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой строковый тип
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterString : ParameterItem<String>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.String; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterString()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterString(String parameter_name, String value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterString(Int32 id, String value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой тип перечисления
		/// </summary>
		/// <typeparam name="TEnum">Тип перечисления</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterEnum<TEnum> : ParameterItem<TEnum> where TEnum : Enum
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Enum; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterEnum()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterEnum(String parameter_name, TEnum value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterEnum(Int32 id, TEnum value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой список указанного типа
		/// </summary>
		/// <typeparam name="TType">Тип элемента списка</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterList<TType> : ParameterItem<ListArray<TType>>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.List; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterList()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="items">Список элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterList(String parameter_name, params TType[] items)
				: base(parameter_name)
			{
				mValue = new ListArray<TType>(items);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="items">Список элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterList(Int32 id, params TType[] items)
				: base(id)
			{
				mValue = new ListArray<TType>(items);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет собой базовый объект
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameterObject : ParameterItem<System.Object>
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Object; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameterObject()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterObject(String parameter_name, System.Object value)
				: base(parameter_name)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="value">Значения параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameterObject(Int32 id, System.Object value)
				: base(id)
			{
				mValue = value;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================