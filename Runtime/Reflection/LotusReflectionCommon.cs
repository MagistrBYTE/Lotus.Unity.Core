﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема рефлексии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusReflectionCommon.cs
*		Общие типы и структуры данных подсистемы рефлексии.
*		Методы отражения и рефлексии предназначенные для извлечения сведений о сборках, модулях, членах, параметрах и
*	других объектах в управляемом коде путем обработки их метаданных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreReflection Подсистема рефлексии
		//! Подсистема рефлексии для кэширования данных отражения. Подсистема реализует методы предназначение для 
		//! упрощения работы с рефлексией данных, проверки типов, а также содержит ряд вспомогательных объектов связанных 
		//! с рефлексией данных.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Объем извлекаемых данных для кэширования
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Flags]
		public enum TExtractMembers
		{
			/// <summary>
			/// Ничего не извлекается
			/// </summary>
			/// <remarks>
			/// Применяется для базовых типов
			/// </remarks>
			None = 0,

			/// <summary>
			/// Извлекаются метаданные полей
			/// </summary>
			Fields = 1,

			/// <summary>
			/// Извлекаются метаданные свойств
			/// </summary>
			Properties = 2,

			/// <summary>
			/// Извлекаются метаданные методов
			/// </summary>
			Methods = 4
		}

#if UNITY_2017_1_OR_NEWER
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение типа объекта в системе Unity
		/// </summary>
		/// <remarks>
		/// Фактически в Unity существует три разных типов данных, которые выполняют различные функции и роль, но при этом 
		/// все они производны от базового класса UnityEngine.Object.
		/// Однако работа с ними принципиально различна, поэтому запроектировано собственная классификация совокупности 
		/// всех типов и созданы методы позволяющие классифицировать объекты на различные осмысленные категории
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TUnityObjectType
		{
			/// <summary>
			/// Игровой объект, представляет собой тип GameObject и выполняет роль контейнера для всех компонент. 
			/// Этот объект сам не выполняет никаких действий, но только с помощью его любые компоненты могут создаваться, 
			/// появиться на сцене и функционировать.
			/// </summary>
			GameObject,

			/// <summary>
			/// Компонент - это тип/объект который реализует определенный функционал.
			/// Компоненты создаются, точнее, присоединяются только к игровому объекту и неотделимы от него.
			/// </summary>
			Component,

			/// <summary>
			/// Пользовательский компонент, который как правило, реализует логику взаимодействия.
			/// Обязательно должен быть производным от класса MonoBehaviour.
			/// </summary>
			UserComponent,

			/// <summary>
			/// Ресурс - это основные данные, фактически это игровой контент. 
			/// Ресурсы как правило неизменяемые в течение всего игрового процесса. 
			/// В Unity ресурс представляет собой программный интерфейс который обеспечивает доступ к физическому 
			/// файлу представляющий этот самый ресурс или к объекту в оперативной памяти.
			/// </summary>
			Resource,

			/// <summary>
			/// Пользовательские ресурсы (хранилища) – специальные типы данных, которые будут которые будут хранить 
			/// пользовательские данные.
			/// Все пользовательские ресурсы будут производны от типа ScriptableObject.
			/// </summary>
			UserResource
		}
#endif
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================