﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема поддержки инспектора свойств
// Группа: Атрибуты зависимости от логического условия равенства
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorConditionEnabled.cs
*		Атрибут доступности для редактирования(поля/свойства)в зависимости от логического условия равенства.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreUnityInspectorCondition Атрибуты зависимости от условий
		//! Атрибуты зависимости от логического условия равенства
		//! \ingroup CoreUnityInspector
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут доступности для редактирования(поля/свойства)в зависимости от логического условия равенства
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		public sealed class LotusEnabledEqualityAttribute : LotusInspectorItemAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mManagingMemberName;
			internal TInspectorMemberType mMemberType;
			internal Boolean mValue;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя члена объекта от которого зависит доступность свойства для редактирования
			/// </summary>
			public String ManagingMemberName
			{
				get { return mManagingMemberName; }
				set { mManagingMemberName = value; }
			}

			/// <summary>
			/// Тип члена объекта
			/// </summary>
			public TInspectorMemberType MemberType
			{
				get { return mMemberType; }
				set { mMemberType = value; }
			}

			/// <summary>
			/// Целевое значение поля/свойства при котором доступно редактирование
			/// </summary>
			public Boolean Value
			{
				get { return mValue; }
				set { mValue = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="managing_member_name">Имя члена объекта от которого зависит доступность свойства для редактирования</param>
			/// <param name="member_type">Тип члена объекта</param>
			/// <param name="value">Целевое значение поля/свойства при котором доступно редактирование</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusEnabledEqualityAttribute(String managing_member_name, TInspectorMemberType member_type, Boolean value)
			{
				mManagingMemberName = managing_member_name;
				mMemberType = member_type;
				mValue = value;
			}
			#endregion

			#region ======================================= МЕТОДЫ РЕДАКТОРА UNITY ====================================
#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Применение действий перед отображением редактора элемента инспектора свойств
			/// </summary>
			/// <remarks>
			/// При необходимости можно менять входные параметры
			/// </remarks>
			/// <param name="position">Прямоугольник для отображения</param>
			/// <param name="label">Надпись</param>
			/// <returns>Следует ли рисовать редактор элемента инспектора свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean BeforeApply(ref UnityEngine.Rect position, ref UnityEngine.GUIContent label)
			{
				if (mOwned != null && mOwned.Instance != null)
				{
					// Получаем значение от члена объекта от которого зависит доступность
					Boolean enabled = mOwned.GetValueFromMember<Boolean>(ManagingMemberName, mMemberType); 

					// Если значения не совпадают значит элемент недоступен
					if(enabled != Value)
					{
						UnityEditor.EditorGUI.BeginDisabledGroup(true);
					}
				}

				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Применение действий после отображения редактора элемента инспектора свойств
			/// </summary>
			/// <param name="position">Прямоугольник для отображения</param>
			/// <param name="label">Надпись</param>
			//---------------------------------------------------------------------------------------------------------
			public override void AfterApply(UnityEngine.Rect position, UnityEngine.GUIContent label)
			{
				if (mOwned != null && mOwned.Instance != null)
				{
					// Получаем значение от члена объекта от которого зависит доступность
					Boolean enabled = mOwned.GetValueFromMember<Boolean>(ManagingMemberName, mMemberType); 

					// Если значения совпадают значит элемент доступен
					if (enabled == Value)
					{
						UnityEditor.EditorGUI.EndDisabledGroup();
					}
				}
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================