//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема интерфейсов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInterfaceData.cs
*		Определение интерфейсов для адаптации данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreInterfaces
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для адаптации объекта к нужному типу
		/// </summary>
		/// <typeparam name="TType">Тип объекта</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusAdapter<out TType>
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение объекта адаптированного к существующему типу
			/// </summary>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			TType GetAdaptedObject();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов поддерживающих дублирование
		/// </summary>
		/// <typeparam name="TType">Тип объекта</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusDuplicate<out TType>
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			TType Duplicate();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов поддерживающих проверку
		/// </summary>
		/// <typeparam name="TType">Тип объекта</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusCheckAll<out TType>
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка объекта на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Объект удовлетворяет условию предиката если каждый его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean CheckAll(Predicate<TType> match);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов поддерживающих проверку
		/// </summary>
		/// <typeparam name="TType">Тип объекта</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusCheckOne<out TType>
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка объекта на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Объект удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean CheckOne(Predicate<TType> match);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для объектов поддерживающих посещение посетителем
		/// </summary>
		/// <typeparam name="TType">Тип объекта</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusVisit<out TType>
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение элементов списка указанным посетителем
			/// </summary>
			/// <param name="on_visitor">Делегат посетителя</param>
			//---------------------------------------------------------------------------------------------------------
			void Visit(Action<TType> on_visitor);
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================