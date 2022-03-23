﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseUnityInterface.cs
*		Общие данные для организации работы с интерфейсами на уровне Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для определения поддерживаемого типа интерфейса
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		public class LotusInterfaceComponentAttribute : PropertyAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal Type mTypeInterface;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип интерфейса
			/// </summary>
			public Type TypeInterface
			{
				get { return mTypeInterface; }
				set { mTypeInterface = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="type_interface">Тип интерфейса</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusInterfaceComponentAttribute(Type type_interface)
			{
				mTypeInterface = type_interface;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс оболочка для хранения скрипта и типа интерфейса который он поддерживает
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CInterfaceComponent
		{
			#region ======================================= ДАННЫЕ ====================================================
			[SerializeField]
			internal MonoBehaviour mScript;
			[SerializeField]
			internal Type mTypeInterface;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип интерфейса
			/// </summary>
			public Type TypeInterface
			{
				get { return mTypeInterface; }
				set { mTypeInterface = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="type_interface">Тип интерфейса</param>
			//---------------------------------------------------------------------------------------------------------
			public CInterfaceComponent(Type type_interface)
			{
				mTypeInterface = type_interface;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс оболочка для хранения скрипта и типа интерфейса который он поддерживает
		/// </summary>
		/// <typeparam name="TInterface">Тип интерфейса</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class InterfaceComponent<TInterface> : CInterfaceComponent
		{
			#region ======================================= ДАННЫЕ ====================================================
			[NonSerialized]
			internal TInterface mInterface;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Интерфейс
			/// </summary>
			public TInterface Interface
			{
				get { return mInterface; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public InterfaceComponent()
				: base(typeof(TInterface))
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="script">Пользовательский скрипт реализующий интерфейс</param>
			//---------------------------------------------------------------------------------------------------------
			public InterfaceComponent(MonoBehaviour script)
				: base(typeof(TInterface))
			{
				mScript = script;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================