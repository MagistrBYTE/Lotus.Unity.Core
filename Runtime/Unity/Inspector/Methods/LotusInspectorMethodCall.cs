﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема поддержки инспектора свойств
// Группа: Атрибуты для определения возможности вызова метода через инспектор свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorMethodCall.cs
*		Атрибут для определения возможности вызова метода объекта через инспектор свойств.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreInspectorMethods Атрибуты вызова метода
		//! Атрибуты для определения возможности вызова метода через инспектор свойств
		//! \ingroup CoreInspector
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Режим вызова метода
		/// </summary>
		/// <remarks>
		/// Применяется в основном в Unity
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TMethodCallMode
		{
			/// <summary>
			/// Метод можно вызвать в любое время
			/// </summary>
			Always,

			/// <summary>
			/// Метод можно вызвать только в режиме редактора
			/// </summary>
			OnlyEditor,

			/// <summary>
			/// Метод можно вызвать только в режиме игры
			/// </summary>
			OnlyPlay
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для определения возможности вызова метода объекта через инспектор свойств
		/// </summary>
		/// <remarks>
		/// Поддерживается до двух аргументов метода.
		/// Аргументы должны быть сопоставимы с типом <see cref="CVariant"/>.
		/// Отображение вызываемых методов в инспекторе свойств происходит после отображения всех данных
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public class LotusMethodCallAttribute : Attribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mDisplayName;
			internal Boolean mIsSignature;
			internal TMethodCallMode mMode;
			internal Int32 mDrawOrder;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Удобочитаемое имя метода
			/// </summary>
			/// <remarks>
			/// Если пустое значение то используется имя метода
			/// </remarks>
			public String DisplayName
			{
				get { return mDisplayName; }
				set { mDisplayName = value; }
			}

			/// <summary>
			/// Статус отображения сигнатуры метода вместе с его имением
			/// </summary>
			public Boolean IsSignature
			{
				get { return mIsSignature; }
				set { mIsSignature = value; }
			}

			/// <summary>
			/// Режим вызова метода
			/// </summary>
			public TMethodCallMode Mode
			{
				get { return mMode; }
				set { mMode = value; }
			}

			/// <summary>
			/// Порядок рисования метода
			/// </summary>
			public Int32 DrawOrder
			{
				get { return mDrawOrder; }
				set { mDrawOrder = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusMethodCallAttribute()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="human_name">Удобочитаемое имя метода</param>
			/// <param name="button_mode">Режим вызова метода</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusMethodCallAttribute(String human_name, TMethodCallMode button_mode = TMethodCallMode.Always)
			{
				mDisplayName = human_name;
				mMode = button_mode;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="human_name">Удобочитаемое имя метода</param>
			/// <param name="draw_order">Порядок отрисовки кнопки</param>
			/// <param name="button_mode">Режим вызова метода</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusMethodCallAttribute(String human_name, Int32 draw_order, TMethodCallMode button_mode = TMethodCallMode.Always)
			{
				mDisplayName = human_name;
				mDrawOrder = draw_order;
				mMode = button_mode;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================