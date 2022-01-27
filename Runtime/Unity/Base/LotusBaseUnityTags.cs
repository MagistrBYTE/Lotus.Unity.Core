//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseUnityTags.cs
*		Общие данные для организации работы с тэгами объектов Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс содержащий константы используемых тэгов объектов Unity
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XTag
		{
			/// <summary>
			/// Тэг по умолчанию
			/// </summary>
			public const String UNTAGGED = "Untagged";
			
			/// <summary>
			/// Тэг - Respawn
			/// </summary>
			public const String RESPAWN = "Respawn";
			
			/// <summary>
			/// Тэг - Finish
			/// </summary>
			public const String FINISH = "Finish";
			
			/// <summary>
			/// Тэг только для режима редактора
			/// </summary>
			public const String EDITOR_ONLY = "EditorOnly";
			
			/// <summary>
			/// Тэг - MainCamera
			/// </summary>
			public const String MAIN_CAMERA = "MainCamera";
			
			/// <summary>
			/// Тэг - Player
			/// </summary>
			public const String PLAYER = "Player";
			
			/// <summary>
			/// Тэг - GameController
			/// </summary>
			public const String GAME_CONTROLLER = "GameController";
			
			/// <summary>
			/// Тэг - Person
			/// </summary>
			public const String PERSON = "Person";
			
			/// <summary>
			/// Тэг - Space
			/// </summary>
			public const String SPACE = "Space";
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================