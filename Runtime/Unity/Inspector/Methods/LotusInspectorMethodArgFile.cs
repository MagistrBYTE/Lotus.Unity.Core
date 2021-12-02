﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема поддержки инспектора свойств
// Группа: Атрибуты для определения возможности вызова метода через инспектор свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorMethodArgFile.cs
*		Атрибуты для определения возможности вызова метода через инспектор свойств.
*		Реализация обеспечивающих инфраструктуру вызова метода объекта(компонента) посредством инспектора свойств.
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
		//! \addtogroup CoreInspectorMethods
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для вызова метода с возможностью указания для аргумента имени файла
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public class LotusMethodArgFileAttribute : LotusMethodCallAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mExtension;
			internal String mDefaultName;
			internal String mDefaultPath;
			internal Boolean mIsOpenFile;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Расширение файла
			/// </summary>
			/// <remarks>
			/// Расширение файла задается без точки
			/// </remarks>
			public String Extension
			{
				get { return mExtension; }
				set { mExtension = value; }
			}

			/// <summary>
			/// Имя файла по умолчанию
			/// </summary>
			/// <remarks>
			/// Стандартное значение - File
			/// </remarks>
			public String DefaultName
			{
				get { return mDefaultName; }
				set { mDefaultName = value; }
			}

			/// <summary>
			/// Путь файла по умолчанию
			/// </summary>
			/// <remarks>
			/// Стандартное значение - Assets/
			/// </remarks>
			public String DefaultPath
			{
				get { return mDefaultPath; }
				set { mDefaultPath = value; }
			}

			/// <summary>
			/// Статус открытия файла (Показывается диалог для открытия файла)
			/// </summary>
			/// <remarks>
			/// В случае отрицательного значения будет показываться диалог закрытия файлов
			/// </remarks>
			public Boolean IsOpenFile
			{
				get { return mIsOpenFile; }
				set { mIsOpenFile = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusMethodArgFileAttribute()
			{
				mDefaultName = "File";
#if UNITY_2017_1_OR_NEWER
				mDefaultPath = XEditorSettings.ASSETS_PATH;
#else
				mDefaultPath = "";
#endif

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="extension">Расширение файла</param>
			/// <param name="default_name">Имя файла по умолчанию</param>
			/// <param name="default_path">Путь файла по умолчанию</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusMethodArgFileAttribute(String extension, String default_name = "File", 
				String default_path =
#if UNITY_2017_1_OR_NEWER
				XEditorSettings.ASSETS_PATH)
#else
				"")
#endif
			{
				mExtension = extension;
				mDisplayName = default_name;
				mDefaultPath = default_path;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================