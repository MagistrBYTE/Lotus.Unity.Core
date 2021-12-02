﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingCommon.cs
*		Общие типы и структуры данных подсистемы связывания данных.
*		Связывания данных - концепция, основанная на взаимной связи между источником данных (объектом логики или модели) и
*	приемником данных, который, как правило отображает ее свойства и позволяет ими управлять - объекта представления.
*		Обычно, в качестве объекта представления выступает элемент UI.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreDataBinding Подсистема связывания данных
		//! Подсистема связывания данных определяет тип и характер связанности между источником данных и приемником данных.
		//! Связывания данных - концепция, основанная на взаимной связи между источником данных(объектом логики или модели) и
		//! приемником данных, который, как правило отображает ее свойства и позволяет ими управлять - объекта представления.
		//!
		//! В зависимости от режима связывания обновление свойств источника приводит к немедленному обновлению свойств
		//! объекта представления.
		//!
		//! Здесь и далее используете понятие «объект логики» и «объект представления» если говорится о чистых сущностях. 
		//! Т.е. объект логики представляет собой чистые данные (например ответ от базы данных), а объект представления только
		//! отображает данные. На практике, как правило, часть функциональности одного объекта может быть передана другому. 
		//! Исходя из этого употребляется термин **«объект модели»** или просто модель. Это объект, который содержит данные 
		//! логики, и может содержать, а в некоторых случаях должен содержать, функциональность связанную с его представлением.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Режим связывания данных между объектом модели и объектом представления
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TBindingMode
		{
			/// <summary>
			/// Объект представления лишь отображает данные модели
			/// </summary>
			/// <remarks>
			/// Изменение данных объекта модели влечет автоматическое изменение соответствующих данных объекта представления.
			/// С объектом представления может быть связано только одно свойство/метод модели.
			/// </remarks>
			ViewData,

			/// <summary>
			/// Только объект представления управляет данными модели
			/// </summary>
			/// <remarks>
			/// Поведение объекта представления неопределенно при внешнем изменении данных объекта модели.
			/// С объектом представления может быть связано несколько свойств/методов модели.
			/// </remarks>
			DataManager,

			/// <summary>
			/// Взаимное обновление данными между моделью и объектом представления
			/// </summary>
			/// <remarks>
			/// Поведение объект представления неопределенно при внешнем изменении данных объекта модели.
			/// С объектом представления может быть связано только одно свойство/метод модели.
			/// </remarks>
			TwoWay
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип члена объекта для связывания данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TBindingMemberType
		{
			/// <summary>
			/// Поле
			/// </summary>
			Field,

			/// <summary>
			/// Свойство
			/// </summary>
			Property,

			/// <summary>
			/// Метод
			/// </summary>
			Method
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Режим изменения свойств объекта представления
		/// </summary>
		/// <remarks>
		/// Запланировано для будущих реализаций
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TBindingModeChanged
		{
			/// <summary>
			/// Свойство изменяется непосредственно - режим по умолчанию
			/// </summary>
			Immediate,

			/// <summary>
			/// Свойство изменяется со временем.
			/// Применяется только для числовых свойств. В основном для визуального эффекта
			/// </summary>
			Time
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================