//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorAttributeDrawer.cs
*		Редактор атрибута Inspector.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
#if UNITY_EDITOR
//=====================================================================================================================
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Editor;
//=====================================================================================================================
//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор атрибута Inspector
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomPropertyDrawer(typeof(LotusInspectorAttribute), true)]
public class LotusInspectorAttributeDrawer : PropertyDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Получение высоты редактируемого свойства элемента
	/// </summary>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	/// <returns>Высота свойства элемента</returns>
	//-----------------------------------------------------------------------------------------------------------------
	public override Single GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		LotusInspectorAttribute drawer = this.attribute as LotusInspectorAttribute;

		// Проверка на коллекцию Lotus
		// Мы должны проверить уровень свойства (0 уровень рисует сам редактор инспектора свойств)
		// Также мы должны проверить что это не элемент коллекции (пока нет возможности правильно рисовать стандартные коллекции через атрибут)
		if (property.depth > 0 && property.IsElementCollection() == false)
		{
			if (fieldInfo.GetAttribute<LotusReorderableAttribute>() != null)
			{
				CReorderableList list = XReorderableList.Get(property);
				drawer.ReorderableList = list;
				return (list.GetHeight());
			}
		}

		drawer.SetSerializedProperty(property, fieldInfo);
		drawer.GetAttributes();
		drawer.SetSerializedObject(property.serializedObject);

		return (drawer.GetMaxHeight(label));
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование сериализуемого свойства
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		LotusInspectorAttribute drawer = this.attribute as LotusInspectorAttribute;

		// Проверяем на список
		if (property.depth > 0 && drawer.ReorderableList != null)
		{
			CReorderableList list = drawer.ReorderableList as CReorderableList;

			// Небольшая корректировка
			position.xMin += property.depth * XInspectorViewParams.OFFSET_INDENT / 2;
			list.Draw(position);
		}
		else
		{
			// Только если не элемент коллекции - имя там задавать имя бесполезно
			if (property.IsElementCollection() == false)
			{
				// При необходимости задаем имя
				if (drawer.DisplayName.IsExists())
				{
					label.text = drawer.DisplayName;
				}

				// и подсказку
				if (drawer.Tooltip.IsExists())
				{
					label.tooltip = drawer.Tooltip;
				}
			}

			// Рисуем
			drawer.OnDrawElement(position, property, label);
		}
	}
	#endregion
}
//=====================================================================================================================
#endif
//=====================================================================================================================