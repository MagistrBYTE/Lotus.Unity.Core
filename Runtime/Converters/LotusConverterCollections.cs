﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема конвертации данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusConverterCollections.cs
*		Конвертация коллекций.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreConverters
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий конвертацию коллекций
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XConverterCollection
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация списка значений вещественных типов
			/// </summary>
			/// <param name="values">Список вещественных значений одинарной точности</param>
			/// <returns>Список вещественных значений двойной точности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IList<Double> ToDouble(IList<Single> values)
			{
				Double[] list = new Double[values.Count];
				for (Int32 i = 0; i < values.Count; i++)
				{
					list[i] = values[i];
				}

				return list;
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================