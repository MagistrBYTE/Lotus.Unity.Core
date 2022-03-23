﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема ресурсов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusResourcesInterface.cs
*		Определение интерфейса для реализации пользовательского ресурса.
*		С точки зрения программной части пользовательские ресурсы – это типы производные от ScriptableObject, а с точки зрения 
*	физической организации - файлы с расширением .asset.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityResource
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для реализации пользовательского ресурса
		/// </summary>
		/// <remarks>
		/// Интерфейс определяет ряд методов которые обязательно должен реализовать пользовательский ресурс 
		/// для того чтоб нормально функционировать.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusResourceable
		{
			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичное создание ресурса
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Create();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная безопасная инициализация несериализуемых данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Init();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Принудительный сброс записанных данных на диск
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void Flush();
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================