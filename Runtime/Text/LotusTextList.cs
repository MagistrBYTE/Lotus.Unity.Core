﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема текстовых данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTextList.cs
*		Список строк текстовых данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
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
		/// Список строк текстовых данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTextList
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal ListArray<CTextLine> mLines;
			protected internal Int32 mCurrentIndent;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Уровень вложенности строки при добавлении новых строк
			/// </summary>
			public Int32 CurrentIndent
			{
				get { return (mCurrentIndent); }
				set
				{
					mCurrentIndent = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextList(Int32 capacity = 24)
			{
				mLines = new ListArray<CTextLine>(capacity);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="str">Строка</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextList(String str)
			{
				mLines = new ListArray<CTextLine>();
				mLines.Add(str);
				mLines[0].Index = 0;
				mLines[0].Owned = this;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление строки текстовых данных
			/// </summary>
			/// <param name="line">Строка текстовых данных</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(CTextLine line)
			{
				line.Index = mLines.Count;
				line.Owned = this;
				line.Indent = mCurrentIndent;
				mLines.Add(line);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление пустой новой строки
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void AddNewLine()
			{
				CTextLine line = new CTextLine(XString.NewLine);
				line.Index = mLines.Count;
				line.Owned = this;
				line.Indent = mCurrentIndent;
				mLines.Add(line);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление пустой строки
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void AddEmptyLine()
			{
				CTextLine line = new CTextLine(String.Empty);
				line.Index = mLines.Count;
				line.Owned = this;
				line.Indent = mCurrentIndent;
				mLines.Add(line);
			}
			#endregion

			#region ======================================= МЕТОДЫ ОГРАНИЧЕНИЯ ДЛИНЫ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLength(Int32 length)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					mLines[i].SetLength(length);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется указанным символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLength(Int32 length, Char symbol)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					mLines[i].SetLength(length, symbol);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк с указанным последним символом
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом, но последний 
			/// символ всегда указанный
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthAndLastChar(Int32 length, Char symbol)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					mLines[i].SetLengthAndLastChar(length, symbol);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк с учетом начальных символов табуляции
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="tabs_equiv">Размер одного символа табуляции</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthWithTabs(Int32 length, Int32 tabs_equiv = 4)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					mLines[i].SetLengthWithTabs(length, tabs_equiv);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк с учетом начальных символов табуляции
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется указанным символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			/// <param name="tabs_equiv">Размер одного символа табуляции</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthWithTabs(Int32 length, Char symbol, Int32 tabs_equiv = 4)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					mLines[i].SetLengthWithTabs(length, symbol, tabs_equiv);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длины строк с учетом начальных символов табуляции только для разделителей
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="tabs_equiv">Размер одного символа табуляции</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthWithTabsOnlyDelimetrs(Int32 length, Int32 tabs_equiv = 4)
			{
				for (Int32 i = 0; i < mLines.Count; i++)
				{
					if(mLines[i].RawString.Contains("//---------"))
					{
						mLines[i].SetLengthWithTabs(length, tabs_equiv);
					}
					if (mLines[i].RawString.Contains("//========"))
					{
						mLines[i].SetLengthWithTabs(length, tabs_equiv);
					}

				}
			}
			#endregion

			#region ======================================= МЕТОДЫ СОХРАНЕНИЯ/ЗАГРУЗКИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения списка строк текстовых данных в файл
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Save(String file_name)
			{
				// Формируем правильный путь
#if UNITY_2017_1_OR_NEWER
				String path = XFilePath.GetFileName(XEditorSettings.ASSETS_PATH, file_name, ".cs");
#else
				String path = XFilePath.GetFileName(Environment.CurrentDirectory, file_name, ".cs");
#endif
				// Создаем поток для записи
				StreamWriter stream_writer = new StreamWriter(path);

				// Записываем данные
				for (Int32 i = 0; i < mLines.Count - 1; i++)
				{
					stream_writer.WriteLine(mLines[i].RawString);
				}

				stream_writer.Write(mLines.ItemLast.RawString);

				stream_writer.Close();

#if UNITY_EDITOR
				// Обновляем в редакторе
				UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
#endif
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================