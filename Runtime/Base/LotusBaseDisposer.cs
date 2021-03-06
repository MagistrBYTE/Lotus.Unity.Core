//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseDisposer.cs
*		Общие типы и структуры данных для освобождения неуправляемых ресурсов.
*		Применяется для работы с классами которые содержат неуправляемые ресурсы.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для установки только нового значения и информирования об этом
		/// </summary>
		/// <remarks>
		/// Применяется когда при изменении свойства требуется выполнить дополнительную работу
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XValueSet
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка нового значения свойства типа значения
			/// </summary>
			/// <typeparam name="TType">Тип объекта</typeparam>
			/// <param name="current_value">Текущие значение</param>
			/// <param name="new_value">Новое значение</param>
			/// <returns>Статус установки нового значения свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetStruct<TType>(ref TType current_value, TType new_value) where TType : struct
			{
				if (current_value.Equals(new_value))
				{
					return false;
				}

				current_value = new_value;
				return true;
			}


			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка нового значения свойства ссылочного типа
			/// </summary>
			/// <typeparam name="TType">Тип объекта</typeparam>
			/// <param name="current_value">Текущие значение</param>
			/// <param name="new_value">Новое значение</param>
			/// <returns>Статус установки нового значения свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetClass<TType>(ref TType current_value, TType new_value) where TType : class
			{
				if ((current_value == null && new_value == null) || (current_value != null && current_value.Equals(new_value)))
				{
					return false;
				}

				current_value = new_value;
				return true;
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для реализации утилизации экземпляра объект и установки нулевой ссылки на объект
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDisposer
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Утилизация (освобождение от неуправляемых ресурсов) экземпляра объект и установки нулевой ссылки на объект
			/// </summary>
			/// <remarks>
			/// Этот метод скрывает любые брошенные исключения, которые могут возникнуть во время утилизации объекта
			/// </remarks>
			/// <typeparam name="TResource">Тип объекта</typeparam>
			/// <param name="resource">Ссылка на экземпляр объекта для утилизации</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SafeDispose<TResource>(ref TResource resource) where TResource : class
			{
				if (resource == null)
				{
					return;
				}

				IDisposable disposer = resource as IDisposable;
				if (disposer != null)
				{
					try
					{
						disposer.Dispose();
					}
					catch
					{
					}
				}

				resource = null;
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================