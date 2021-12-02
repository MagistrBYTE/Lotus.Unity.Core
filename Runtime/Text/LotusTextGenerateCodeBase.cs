﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема текстовых данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTextGenerateCodeBase.cs
*		Определение базового класса генератора для генерации/редактирования программного кода.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreText
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс генератора для генерации/редактирования программного кода
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public abstract class CTextGenerateCodeBase : CTextList
		{
			#region ======================================= ДАННЫЕ ====================================================
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			protected CTextGenerateCodeBase(Int32 capacity = 24)
				: base(capacity)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="str">Строка</param>
			//---------------------------------------------------------------------------------------------------------
			protected CTextGenerateCodeBase(String str)
				: base(str)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление разделителя для части
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddDelimetrPart()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление разделителя для секции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddDelimetrSection()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление открытия блока
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddOpenBlock()
			{
				Add("}");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление закрытия блока
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddCloseBlock()
			{
				Add("}");
			}
			#endregion

			#region ======================================= РАБОТА С ПРОСТРАНСТВАМИ ИМЕН  =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление используемых пространств имён
			/// </summary>
			/// <param name="namespaces">Список пространства имён</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddNamespaceUsing(params String[] namespaces)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление открытие пространства имени
			/// </summary>
			/// <param name="space_name">Имя пространства имён</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddNamespaceOpen(String space_name)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление закрытия пространства имени
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddNamespaceClose()
			{

			}
			#endregion

			#region ======================================= ОПИСАНИЯ ФАЙЛА ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного заголовка для файлов кода проекта Lotus
			/// </summary>
			/// <param name="module_name"></param>
			/// <param name="subsystem_name"></param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddFileHeader(String module_name, String subsystem_name)
			{
				AddDelimetrPart();
				Add("// Проект: LotusPlatform");
				Add("// Раздел: " + module_name);
				Add("// Подраздел: " + subsystem_name);
				Add("// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>");
				AddDelimetrSection();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление имени файла и краткое описание
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <param name="brief_desc">Краткое описание</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddFileBriefDesc(String file_name, String brief_desc)
			{
				Add("/** \\file " + file_name);
				Add("*\t\t" + brief_desc);
				Add("*/");
				AddDelimetrSection();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление версии и даты изменения файла
			/// </summary>
			/// <param name="version">Версия файла</param>
			/// <param name="date">Дата файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddFileVersion(String version = "1.0.0.0", String date = "04.04.2020")
			{
				Add("// Версия: " + version);
				Add("// Последнее изменение от " + date);
				AddDelimetrPart();
			}
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ ТИПОВ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации статического публичного класса
			/// </summary>
			/// <param name="class_name">Имя класса</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddClassStaticPublic(String class_name)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление окончание декларации класса
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddClassEndDeclaration()
			{

			}
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ ПОЛЕЙ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации константного публичного поля
			/// </summary>
			/// <param name="type_name">Имя типа</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddFieldConstPublic(String type_name, String field_name, String value)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации константного публичного поля типа String
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddFieldConstPublicString(String field_name, String value)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление декларации статического поля только для чтения
			/// </summary>
			/// <param name="type_name">Имя типа</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="value">Значения поля</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddFieldStaticReadonlyPublic(String type_name, String field_name, String value)
			{

			}
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ СВОЙСТВ ========================================
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ МЕТОДОВ ========================================
			#endregion

			#region ======================================= ДЕКЛАРАЦИЯ КОММЕНТАРИЕВ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление комментария
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddComment(String text)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление комментария для секции или раздела
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddCommentSection(String text)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного краткого комментария
			/// </summary>
			/// <param name="delimetr_section_before">Статус добавления разделителя секции перед комментарием</param>
			/// <param name="text">Текст комментария</param>
			/// <param name="delimetr_section_after">Статус добавления разделителя секции после комментария </param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddCommentSummary(Boolean delimetr_section_before, String text, Boolean delimetr_section_after)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного краткого комментария для полей и свойств
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddCommentSummaryForData(String text)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление стандартного расширенного комментария для полей и свойств
			/// </summary>
			/// <param name="text">Текст комментария</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddCommentRemarksForData(String text)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление команды Doxygen - добавить в группу
			/// </summary>
			/// <param name="group_name">Имя группы</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddDoxygenAddToGroup(String group_name)
			{
				AddDelimetrSection();
				Add("//! \\addtogroup " + group_name);
				Add("/*@{*/");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление команды Doxygen - окончание группы
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddDoxygenEndGroup()
			{
				AddDelimetrSection();
				Add("/*@}*/");
				AddDelimetrSection();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================