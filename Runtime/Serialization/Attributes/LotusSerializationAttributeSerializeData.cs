﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationAttributeSerializeData.cs
*		Атрибут для указания того что тип самостоятельно предоставит данные для сериализации.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Xml;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreSerialization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для указания того что тип самостоятельно предоставит данные для сериализации
		/// </summary>
		/// <remarks>
		/// Тип помеченный данным атрибутом должен обязательно реализовать статический метод
		/// с именем <see cref="LotusSerializeDataAttribute.GET_SERIALIZE_DATA"/> который возвращает данные сериализации
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
		public sealed class LotusSerializeDataAttribute : Attribute
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Имя статического метода типа который представляет данные для сериализации
			/// </summary>
			public const String GET_SERIALIZE_DATA = "GetSerializeData";
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================