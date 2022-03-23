﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreProtectionTesting.cs
*		Тестирование методов защиты модуля базового ядра.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Editor
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования методов защиты модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreProtectionTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов защиты
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestProtection()
			{
				TProtectionInt protect_ind = 6566;
				Int32 encrypted_value = protect_ind.EncryptedValue;
				Int32 decrypted_value = protect_ind;
				Assert.AreEqual(6566, decrypted_value);
			}
		}
	}
}
//=====================================================================================================================