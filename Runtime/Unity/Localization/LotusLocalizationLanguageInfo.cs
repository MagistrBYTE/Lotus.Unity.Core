//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема локализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLocalizationLanguageInfo.cs
*		Структура для предоставления языка локализации.
*		Определение структуры для хранения данных о поддерживаемом языке локализации.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityLocalization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура для предоставления языка локализации
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CLanguageInfo
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Имена основных языков
			/// </summary>
			public readonly static String[] LanguageNames = new String[]
			{
				"English",
				"German",
				"French",
				"Italian",
				"Dutch",
				"Greek",
				"Polish",
				"Portuguese",
				"Spanish",
				"Russian",
				"Turkish",
				"Chinese",
			};

			/// <summary>
			/// Аббревиатуры основных языков
			/// </summary>
			public readonly static String[] LanguageAbbrs = new String[]
			{
				"en",
				"de",
				"fr",
				"it",
				"nl",
				"el",
				"pl",
				"pt",
				"es",
				"ru",
				"tr",
				"zh",
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			[SerializeField]
			internal String mName;
			[SerializeField]
			internal String mAbbreviation;
			[SerializeField]
			internal TextAsset mFileData;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя языка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Аббревиатура языка
			/// </summary>
			public String Abbreviation
			{
				get { return mAbbreviation; }
				set { mAbbreviation = value; }
			}

			/// <summary>
			/// Файл с данными
			/// </summary>
			public TextAsset FileData
			{
				get { return mFileData; }
				set { mFileData = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CLanguageInfo()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя языка</param>
			/// <param name="abb">Аббревиатура языка</param>
			//---------------------------------------------------------------------------------------------------------
			public CLanguageInfo(String name, String abb)
			{
				mName = name;
				mAbbreviation = abb;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return mName;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================