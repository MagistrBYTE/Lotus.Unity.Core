//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseUnityAxes.cs
*		Общие данные для организации работы с осями ввода Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
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
		/// Статический класс содержащий константы используемых осей ввода Unity
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XAxis
		{
			/// <summary>
			/// Ось - Horizontal
			/// </summary>
			public const String HORIZONTAL = "Horizontal";
			
			/// <summary>
			/// Ось - Vertical
			/// </summary>
			public const String VERTICAL = "Vertical";
			
			/// <summary>
			/// Ось - Fire1
			/// </summary>
			public const String FIRE1 = "Fire1";
			
			/// <summary>
			/// Ось - Fire2
			/// </summary>
			public const String FIRE2 = "Fire2";
			
			/// <summary>
			/// Ось - Fire3
			/// </summary>
			public const String FIRE3 = "Fire3";
			
			/// <summary>
			/// Ось - Jump
			/// </summary>
			public const String JUMP = "Jump";
			
			/// <summary>
			/// Ось - Mouse X
			/// </summary>
			public const String MOUSE_X = "Mouse X";
			
			/// <summary>
			/// Ось - Mouse Y
			/// </summary>
			public const String MOUSE_Y = "Mouse Y";
			
			/// <summary>
			/// Ось - Mouse ScrollWheel
			/// </summary>
			public const String MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";
			
			/// <summary>
			/// Ось - Submit
			/// </summary>
			public const String SUBMIT = "Submit";
			
			/// <summary>
			/// Ось - Cancel
			/// </summary>
			public const String CANCEL = "Cancel";
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================