﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема компонентов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusComponentsExtensionMonoBehaviour.cs
*		Методы расширения функциональности компонента MonoBehaviour.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityComponent
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений функциональности компонента MonoBehaviour
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionMonoBehaviour
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение интерфейса указанного типа от компонента
			/// </summary>
			/// <typeparam name="TInterface">Тип интерфейса</typeparam>
			/// <param name="this">Базовый компонент пользователя</param>
			/// <returns>Интерфейс или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TInterface GetInterface<TInterface>(this MonoBehaviour @this)
			{
				if (!typeof(TInterface).IsInterface)
				{
					return default(TInterface);
				}

				try
				{
					TInterface inter = (TInterface)(System.Object)@this;
					return inter;
				}
				catch (Exception)
				{
					
				}

				return default(TInterface);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================