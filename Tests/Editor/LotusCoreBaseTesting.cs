﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreBaseTesting.cs
*		Тестирование базовой подсистемы модуля базового ядра.
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
		/// Статический класс для тестирования базовой подсистемы модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreBaseTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class TestA
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class TestB : TestA, ILotusDuplicate<TestA>
			{
				public TestA Duplicate()
				{
					return (new TestB());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class TestC : TestB
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class TestOther : ILotusDuplicate<TestA>
			{
				TestA test;

				public TestA Duplicate()
				{
					if(test == null)
					{
						test = new TestA();
					}

					return (test);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="ILotusDuplicate{TType}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestInterfaceDuplicate()
			{
				TestA result = null;

				TestB testB = new TestB();
				TestC testC = new TestC();
				TestOther testOther = new TestOther();

				result = testB.Duplicate();
				result = testC.Duplicate();
				result = testOther.Duplicate();

				TComparisonOperator equality = (TComparisonOperator)XEnum.ConvertFromAbbreviationOrName(typeof(TComparisonOperator), "=");
				Assert.AreEqual(equality, TComparisonOperator.Equality);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XPacked"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestPackedInteger()
			{
				//
				// Запись 28 бит с начала
				//
				Int32 uid = 3153600;

				Int32 pack_0 = 0;
				XPacked.PackInteger(ref pack_0, 0, 28, uid);

				Int32 un_pack_0 = XPacked.UnpackInteger(pack_0, 0, 28);

				Assert.AreEqual(un_pack_0, uid);

				//
				// Запись 15 бит с 4 бита
				//
				Int32 pack_4 = 0;
				uid = 25000;
				XPacked.PackInteger(ref pack_4, 4, 15, uid);

				Int32 un_pack_4 = XPacked.UnpackInteger(pack_4, 4, 15);

				Assert.AreEqual(un_pack_4, uid);

				//
				// Запись 27 бит с 4 бита
				//
				Int32 pack_27 = 0;
				uid = 25000022;
				XPacked.PackInteger(ref pack_27, 4, 27, uid);

				Int32 un_pack_27 = XPacked.UnpackInteger(pack_27, 4, 27);

				Assert.AreEqual(un_pack_27, uid);


				//
				// Запись 4 бит с 20 бита
				//
				Int32 pack_4_20 = 0;
				uid = 12;
				XPacked.PackInteger(ref pack_4_20, 4, 27, uid);

				Int32 un_pack_4_20 = XPacked.UnpackInteger(pack_4_20, 4, 27);

				Assert.AreEqual(un_pack_4_20, uid);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XPacked"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestPackedLong()
			{
				//
				// Запись 28 бит с начала
				//
				Int64 uid = 3153600;

				Int64 pack_0 = 0;
				XPacked.PackLong(ref pack_0, 0, 28, uid);

				Int64 un_pack_0 = XPacked.UnpackLong(pack_0, 0, 28);

				Assert.AreEqual(un_pack_0, uid);

				//
				// Запись 15 бит с 4 бита
				//
				Int64 pack_4 = 0;
				uid = 25000;
				XPacked.PackLong(ref pack_4, 4, 15, uid);

				Int64 un_pack_4 = XPacked.UnpackLong(pack_4, 4, 15);

				Assert.AreEqual(un_pack_4, uid);

				//
				// Запись 27 бит с 4 бита
				//
				Int64 pack_27 = 0;
				uid = 25000022;
				XPacked.PackLong(ref pack_27, 4, 27, uid);

				Int64 un_pack_27 = XPacked.UnpackLong(pack_27, 4, 27);

				Assert.AreEqual(un_pack_27, uid);


				//
				// Запись 4 бит с 20 бита
				//
				Int64 pack_4_20 = 0;
				uid = 12;
				XPacked.PackLong(ref pack_4_20, 20, 4, uid);

				Int64 un_pack_4_20 = XPacked.UnpackLong(pack_4_20, 20, 4);

				Assert.AreEqual(un_pack_4_20, uid);

				//
				// Запись 24 бит с 0 бита
				//
				Int64 pack_24_0 = 0;
				uid = 16777213;
				XPacked.PackLong(ref pack_24_0, 0, 24, uid);

				Int64 un_pack_24_0 = XPacked.UnpackLong(pack_24_0, 0, 24);

				Assert.AreEqual(un_pack_24_0, uid);

				//
				// Запись 16 бит с 40 бита
				//
				Int64 pack_16_40 = 0;
				uid = 1677;
				XPacked.PackLong(ref pack_16_40, 40, 16, uid);

				Int64 un_pack_16_40 = XPacked.UnpackLong(pack_16_40, 40, 16);

				Assert.AreEqual(un_pack_16_40, uid);

				//
				// Запись 24 бит с 40 бита
				//
				Int64 pack_24_40 = 0;
				uid = 16770044;
				XPacked.PackLong(ref pack_24_40, 40, 24, uid);

				Int64 un_pack_24_40 = XPacked.UnpackLong(pack_24_40, 40, 24);

				Assert.AreEqual(un_pack_24_40, uid);

				//
				// Запись 40 бит с 24 бита
				//
				Int64 pack_40_24 = 0;
				uid = 31536000000L;
				XPacked.PackLong(ref pack_40_24, 24, 40, uid);

				Int64 un_pack_40_24 = XPacked.UnpackLong(pack_40_24, 24, 40);

				Assert.AreEqual(un_pack_40_24, uid);

				//
				// Упаковка
				//
				Int32 hash_code = 3231545;
				Int64 date_time = 31536000000L;

				Int64 pack_uid = 0;
				XPacked.PackLong(ref pack_uid, 0, 24, hash_code);
				XPacked.PackLong(ref pack_uid, 24, 40, date_time);

				Int32 un_hash_code = (Int32)XPacked.UnpackLong(pack_uid, 0, 24);
				Assert.AreEqual(un_hash_code, hash_code);

				Int64 un_date_time = XPacked.UnpackLong(pack_uid, 24, 40);
				Assert.AreEqual(un_date_time, date_time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XGenerateId"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestIdentifier()
			{
				String test = "TestIdentifier";

				Int64 uid = XGenerateId.Generate(test);

				DateTime date = XGenerateId.UnpackIdToDateTime(uid);

				Int32 oh = ((test.GetHashCode() / 16) * 16);
				Int32 rh = XGenerateId.UnpackIdToHashCode(uid);

				Assert.AreEqual(rh, oh);

			}
		}
	}
}
//=====================================================================================================================