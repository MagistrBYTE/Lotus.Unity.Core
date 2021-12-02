//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorAttributesHeadersDrawer.cs
*		Редакторы атрибутов отрисовки заголовков секций и групп.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
#if UNITY_EDITOR
//=====================================================================================================================
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Editor;
//=====================================================================================================================
//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор атрибута HeaderSection
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomPropertyDrawer(typeof(LotusHeaderSectionAttribute), true)]
public class LotusHeaderSectionAttributeDrawer : DecoratorDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Получение высоты декоративного элемента
	/// </summary>
	/// <returns>Высота декоративного элемента</returns>
	//-----------------------------------------------------------------------------------------------------------------
	public override Single GetHeight()
	{
		return (base.GetHeight() + EditorGUIUtility.standardVerticalSpacing * 2);
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnGUI(Rect position)
	{
		LotusHeaderSectionAttribute section_attribute = attribute as LotusHeaderSectionAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;
		style.fontSize++;

		if (section_attribute.TextColor.Approximately(XUnityColor.Zero, 0.01f))
		{
			section_attribute.TextColor = XEditorStyles.ColorHeaderSection;
		}

		// Новые параметры
		style.normal.textColor = section_attribute.TextColor;
		style.alignment = section_attribute.TextAlignment;

		// Рисуем
		GUI.Label(position, section_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
		style.fontSize--;
	}
	#endregion
}

//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор атрибута HeaderSectionBox
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomPropertyDrawer(typeof(LotusHeaderSectionBoxAttribute), true)]
public class LotusHeaderSectionBoxAttributeDrawer : DecoratorDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Получение высоты декоративного элемента
	/// </summary>
	/// <returns>Высота декоративного элемента</returns>
	//-----------------------------------------------------------------------------------------------------------------
	public override Single GetHeight()
	{
		return (base.GetHeight() + (EditorStyles.helpBox.border.top + 
			EditorStyles.helpBox.border.bottom + EditorGUIUtility.standardVerticalSpacing));
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnGUI(Rect position)
	{
		LotusHeaderSectionBoxAttribute section_attribute = attribute as LotusHeaderSectionBoxAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;
		style.fontSize++;

		if (section_attribute.TextColor.Approximately(XUnityColor.Zero, 0.01f))
		{
			section_attribute.TextColor = XEditorStyles.ColorHeaderSection;
		}

		// Новые параметры
		style.normal.textColor = section_attribute.TextColor;
		style.alignment = section_attribute.TextAlignment;

		// Рисуем
		position.height -= EditorGUIUtility.standardVerticalSpacing;
		GUI.Box(position, "", EditorStyles.helpBox);
		if (section_attribute.TextAlignment == TextAnchor.UpperLeft ||
			section_attribute.TextAlignment == TextAnchor.MiddleLeft ||
			section_attribute.TextAlignment == TextAnchor.LowerLeft)
		{
			position.x += EditorGUIUtility.standardVerticalSpacing * 2;
		}
		GUI.Label(position, section_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
		style.fontSize--;
	}
	#endregion
}

//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор атрибута HeaderGroup
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomPropertyDrawer(typeof(LotusHeaderGroupAttribute), true)]
public class LotusHeaderGroupAttributeDrawer : DecoratorDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Получение высоты декоративного элемента
	/// </summary>
	/// <returns>Высота декоративного элемента</returns>
	//-----------------------------------------------------------------------------------------------------------------
	public override Single GetHeight()
	{
		return (base.GetHeight() + EditorGUIUtility.standardVerticalSpacing * 2);
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnGUI(Rect position)
	{
		LotusHeaderGroupAttribute group_attribute = attribute as LotusHeaderGroupAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;

		if (group_attribute.TextColor.Approximately(XUnityColor.Zero, 0.01f))
		{
			group_attribute.TextColor = XEditorStyles.ColorHeaderGroup;
		}

		// Новые параметры
		style.normal.textColor = group_attribute.TextColor;
		style.alignment = group_attribute.TextAlignment;

		// Рисуем
		position.x += (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		position.width -= (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		GUI.Label(position, group_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
	}
	#endregion
}

//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор атрибута HeaderGroupBox
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomPropertyDrawer(typeof(LotusHeaderGroupBoxAttribute), true)]
public class LotusHeaderGroupBoxAttributeDrawer : DecoratorDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Получение высоты декоративного элемента
	/// </summary>
	/// <returns>Высота декоративного элемента</returns>
	//-----------------------------------------------------------------------------------------------------------------
	public override Single GetHeight()
	{
		return (base.GetHeight() + (EditorStyles.helpBox.border.top +
			EditorStyles.helpBox.border.bottom));
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnGUI(Rect position)
	{
		LotusHeaderGroupBoxAttribute group_attribute = attribute as LotusHeaderGroupBoxAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;

		if (group_attribute.TextColor.Approximately(XUnityColor.Zero, 0.01f))
		{
			group_attribute.TextColor = XEditorStyles.ColorHeaderGroup;
		}

		// Новые параметры
		style.normal.textColor = group_attribute.TextColor;
		style.alignment = group_attribute.TextAlignment;

		// Рисуем
		position.x += (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		position.width -= (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		position.height -= EditorGUIUtility.standardVerticalSpacing;
		GUI.Box(position, "", EditorStyles.helpBox);
		if (group_attribute.TextAlignment == TextAnchor.UpperLeft ||
			group_attribute.TextAlignment == TextAnchor.MiddleLeft ||
			group_attribute.TextAlignment == TextAnchor.LowerLeft)
		{
			position.x += EditorGUIUtility.standardVerticalSpacing * 2;
		}
		GUI.Label(position, group_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
	}
	#endregion
}
//=====================================================================================================================
#endif
//=====================================================================================================================